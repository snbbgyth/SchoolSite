﻿using SchoolSite.Core.DbModel;

namespace SchoolSite.Web.Areas.Admin.Models
{
    public class ProductPictureViewModel
    {
        public ProductPictureViewModel()
        {
            ProductPicture = new ProductPicture();
        }

        public ProductPicture ProductPicture { get; set; }

        public string PictureUrl { get; set; }

        public string PictureId { get; set; }
    }
}