using System.ComponentModel;
using SchoolSite.Core.Model;

namespace SchoolSite.Core.DbModel
{
   public class WebContentType:BaseTable
   {
       [DisplayName("类型名称")]
       public virtual string Name { get; set; }
    }
}
