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
