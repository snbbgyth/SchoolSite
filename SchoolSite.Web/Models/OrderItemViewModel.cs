using SchoolSite.Core.DbModel;

namespace SchoolSite.Web.Models
{
    public class OrderItemViewModel
    {
        public OrderItem OrderItem { get; set; }

        public ShopCartItem ShopCartItem { get; set; }
    }
}