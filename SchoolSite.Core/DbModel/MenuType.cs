using System.ComponentModel;
using SchoolSite.Core.Model;

namespace SchoolSite.Core.DbModel
{
   public class MenuType:BaseTable
    {
       [DisplayName("菜单名称")]
       public virtual string Name { get; set; }
    }
}
