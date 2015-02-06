using SchoolSite.Core.DbModel;

namespace SchoolSite.Core.IDAL
{
    public interface IOrderItemDal : IDataOperationActivity<OrderItem>
    {
        int DeleteByOrderId(dynamic id);
    }
}
