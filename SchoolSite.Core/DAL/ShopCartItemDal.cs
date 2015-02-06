using System;
using System.Reflection;
using SchoolSite.Core.DbModel;
using SchoolSite.Core.IDAL;
using SchoolSite.Core.QueueDAL;

namespace SchoolSite.Core.DAL
{
    public class ShopCartItemDal : DataOperationActivityBase<ShopCartItem>, IShopCartItemDal
    {
        public int SubmitById(int id)
        {
            int reslut = 0;

            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {

                    var queryString = string.Format(" Update {0} set IsSubmit=1 , LastModifyDate=:lastModifyDate where Id = :id ", typeof(ShopCartItem).Name);
                    reslut = session.CreateQuery(queryString)
                                    .SetParameter("id", id)
                                    .SetParameter("lastModifyDate", DateTime.Now)
                                    .ExecuteUpdate();
                }
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return reslut;
        }

    }
}
