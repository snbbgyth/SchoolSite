using System.ComponentModel;
using SchoolSite.Core.Model;

namespace SchoolSite.Core.DbModel
{
    public class ProductType:BaseTable
    {
        [DisplayName("产品类型")]
        public virtual string Name { get; set; }
    }
}
