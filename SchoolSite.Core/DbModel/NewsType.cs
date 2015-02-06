using System.ComponentModel;
using SchoolSite.Core.Model;

namespace SchoolSite.Core.DbModel
{
    public class NewsType : BaseTable
    {
        [DisplayName("新闻类型")]
        public virtual string Name { get; set; }
    }
}
