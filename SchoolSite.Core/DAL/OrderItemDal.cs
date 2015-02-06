using System;
using System.Reflection;
using SchoolSite.Core.DbModel;
using SchoolSite.Core.IDAL;
using SchoolSite.Core.QueueDAL;

namespace SchoolSite.Core.DAL
{
    public class OrderItemDal : DataOperationActivityBase<OrderItem>, IOrderItemDal
    {
        public virtual int DeleteByOrderId(dynamic id)
        {
            int reslut = 0;

            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {

                    var queryString = string.Format(" delete {0} where OrderId = :id ", typeof(OrderItem).Name);
                    reslut = session.CreateQuery(queryString)
                                    .SetParameter("id", id)
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
