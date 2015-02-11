using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SchoolSite.Core.DbModel;
using SchoolSite.Core.IDAL;
using SchoolSite.Web.Areas.Admin.Models;
using SchoolSite.Web.DAL.Manage;

namespace SchoolSite.Web.Controllers
{
    public class WebContentsController : Controller
    {
 
        private IWebContentDal _webContentDal;
        private IWebContentTypeDal _webContentTypeDal;

        public WebContentsController()
        {
            _webContentDal = DependencyResolver.Current.GetService<IWebContentDal>();
            _webContentTypeDal = DependencyResolver.Current.GetService<IWebContentTypeDal>();
        }
        // GET: WebContents
        public async Task<ActionResult> Index(int webContentTypeId, string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            IEnumerable<WebContent> entityList = new List<WebContent>();
            ViewBag.CurrentFilter = searchString;
            ViewBag.WebContentTypeId = webContentTypeId;
            //var webContentType = await _webContentTypeDal.FirstOrDefaultAsync(t => t.Name == "学校领导");
            //if (webContentType != null)
            entityList = await _webContentDal.QueryByFunAsync(t => t.WebContentTypeId == webContentTypeId);
            if (entityList.Any())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    entityList = entityList.Where(s => (s.Content != null && s.Content.Contains(searchString))
                                                       || (s.Title != null && s.Title.Contains(searchString))
                                                       || (s.Creater != null && s.Creater.Contains(searchString))
                                                       || (s.LastModifier != null && s.LastModifier.Contains(searchString)));
                }
                entityList = entityList.OrderBy(s => s.DisplayOrder);
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            ViewBag.Title = WebContentManage.QueryWebContentTypeById(webContentTypeId).Name;
            return View(entityList.ToPagedList(pageNumber, pageSize));
         
        }

        // GET: WebContents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            var webContent = await _webContentDal.QueryByIdAsync(id);
            webContent.WebContentType = await _webContentTypeDal.QueryByIdAsync(webContent.WebContentTypeId);
            return View(webContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
