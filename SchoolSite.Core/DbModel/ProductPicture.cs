using SchoolSite.Core.Model;

namespace SchoolSite.Core.DbModel
{
    public class ProductPicture : BaseTable
    {
        public ProductPicture()
        {

        }

        public virtual int DisplayOrder { get; set; }

        public virtual int PictureId { get; set; }

        public virtual int ProductId { get; set; }

    }
}
