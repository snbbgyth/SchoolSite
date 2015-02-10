using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SchoolSite.Core.DbModel;
using SchoolSite.Core.IDAL;
using SchoolSite.Web.DAL.Manage;

namespace SchoolSite.Web.Controllers
{
    public class SchoolInfoController : Controller
    {
        private IWebContentDal _webContentDal;
        private IWebContentTypeDal _webContentTypeDal;

        public SchoolInfoController()
        {
            _webContentDal = DependencyResolver.Current.GetService<IWebContentDal>();
            _webContentTypeDal = DependencyResolver.Current.GetService<IWebContentTypeDal>();
        }

        // GET: SchoolInfo
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> SchoolIntroduction()
        {
            var webContentType = await _webContentTypeDal.FirstOrDefaultAsync(t => t.Name == "学校简介");
            if (webContentType == null) return View(new WebContent());
            var webContentList = await _webContentDal.QueryByFunAsync(t => t.WebContentTypeId == webContentType.Id);
            if (!webContentList.Any()) return View(new WebContent());
            ViewBag.Title = "学校简介";
            return View(webContentList.First(t => t.DisplayOrder == webContentList.Max(c => c.DisplayOrder)));

        }

        public async Task<ActionResult> SchoolLeader(string currentFilter, string searchString, int? page)
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
            var webContentType = await _webContentTypeDal.FirstOrDefaultAsync(t => t.Name == "学校领导");
            if (webContentType != null)
                entityList = await _webContentDal.QueryByFunAsync(t => t.WebContentTypeId == webContentType.Id);

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
            ViewBag.Title = "学校领导";
            return View(entityList.ToPagedList(pageNumber, pageSize));

        }


        public async Task<ActionResult> SchoolLeaderDetail(int id)
        {
            var webContent = await _webContentDal.QueryByIdAsync(id);
            webContent.WebContentType = await _webContentTypeDal.QueryByIdAsync(webContent.WebContentTypeId);
            return View(webContent);
        }

        public async Task<ActionResult> SchoolScenery(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            IEnumerable<WebContent> entityList = new List<WebContent>();
            var webContentType = await _webContentTypeDal.FirstOrDefaultAsync(t => t.Name == "校园风光");
            if (webContentType != null)
                entityList = await _webContentDal.QueryByFunAsync(t => t.WebContentTypeId == webContentType.Id);

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
            if (entityList == null || !entityList.Any()) entityList = new List<WebContent>();
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            ViewBag.Title = "校园风光";
            return View(entityList.ToPagedList(pageNumber, pageSize));

        }

        public async Task<ActionResult> SchoolSceneryDetail(int id)
        {
            var webContent = await _webContentDal.QueryByIdAsync(id);
            webContent.WebContentType = await _webContentTypeDal.QueryByIdAsync(webContent.WebContentTypeId);
            return View(webContent);
        }


        public ActionResult ElectronicMap()
        {
            ViewBag.Title = "赣州中学交通图";
            return View();
        }
    }
}