using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NHibernate.Criterion;
using SchoolSite.Core.DbModel;
using SchoolSite.Core.IDAL;
using SchoolSite.Core.QueueDAL;

namespace SchoolSite.Core.DAL
{
    public class WebContentDal : DataOperationActivityBase<WebContent>, IWebContentDal
    {

        public virtual IEnumerable<WebContent> QueryByWebContentTypeIds(List<int> ids)
        {
            IEnumerable<WebContent> entityList = new List<WebContent>();
            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {
                    //var queryString = string.Format("select * from {0} where WebContentTypeId in (:ids)", typeof(WebContent).Name);
                    //entityList = session.CreateQuery(queryString)
                    //                .SetParameter("ids", string.Join(",", ids))
                    //                .Future<WebContent>().ToList();
                    entityList = session.QueryOver<WebContent>().Where(Restrictions.In("WebContentTypeId", ids)).List<WebContent>();
                }
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return entityList;
        }

        public async virtual Task<IEnumerable<WebContent>> QueryByWebContentTypeIdsAsync(List<int> ids)
        {
            var task = Task.Factory.StartNew(() => QueryByWebContentTypeIds(ids));
            return await task;
        }
    }
}
