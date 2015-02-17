using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using SchoolSite.Core.Model;

namespace SchoolSite.Core.DbModel
{
    public class WebContent:BaseTable
    {
        public WebContent()
        {
            WebContentTypeList = new List<WebContentType>();
        }

        public virtual int WebContentTypeId { get; set; }

        /// <summary>
        /// Each content type just display max display order
        /// </summary>
        [DisplayName("显示顺序")]
        public virtual int DisplayOrder { get; set; }

        [DisplayName("标题")]
        public virtual string Title { get; set; }

        [DisplayName("标题图片")]
        public virtual string ImageForTitle { get; set; }

        [AllowHtml]
        [DisplayName("正文")]
        public virtual string Content { get; set; }

        public virtual WebContentType WebContentType { get; set; }

        public virtual IEnumerable<WebContentType> WebContentTypeList { get; set; }

    }
}
