using System.ComponentModel;
using System.Web;

namespace SchoolSite.Web.Areas.Admin.Models
{
    public class UploadFileViewModel
    {
        [DisplayName("选择上传文件")]
        public HttpPostedFileBase File { get; set; }
    }
}