using SchoolSite.Core.DbModel;

namespace SchoolSite.Core.IDAL
{
    public interface IShopCartItemDal : IDataOperationActivity<ShopCartItem>
    {
        int SubmitById(int id);
    }
}
