using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolSite.Core.DbModel;
using SchoolSite.Core.IDAL;
using SchoolSite.Web.Areas.Admin.Models;
using SchoolSite.Web.DAL.Manage;

namespace SchoolSite.Web.Areas.Admin.Controllers
{
 [Authorize(Roles = "Admin")]
    public class MenuTypesController : Controller
    {

        private IMenuTypeDal _menuTypeDal;

        public MenuTypesController()
        {
            _menuTypeDal = DependencyResolver.Current.GetService<IMenuTypeDal>();
        }

        // GET: Admin/MenuTypes
        public async Task<ActionResult> Index()
        {
            return View(await _menuTypeDal.QueryAllAsync());
        }

        // GET: Admin/MenuTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuType menuType = await _menuTypeDal.QueryByIdAsync(id);
            if (menuType == null)
            {
                return HttpNotFound();
            }
            return View(menuType);
        }

        // GET: Admin/MenuTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MenuTypes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] MenuType menuType)
        {
            if (!string.IsNullOrEmpty(menuType.Name))
            {
                await _menuTypeDal.InsertAsync(menuType);
                WebContentManage.RefreshMenuTypes();
                return RedirectToAction("Index");
            }
            return View(menuType);
        }

        // GET: Admin/MenuTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuType menuType = await _menuTypeDal.QueryByIdAsync(id);
            if (menuType == null)
            {
                return HttpNotFound();
            }
            return View(menuType);
        }

        // POST: Admin/MenuTypes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] MenuType menuType)
        {
            if (!string.IsNullOrEmpty(menuType.Name))
            {

                await _menuTypeDal.ModifyAsync(menuType);
                WebContentManage.RefreshMenuTypes();
                return RedirectToAction("Index");
            }
            return View(menuType);
        }

        // GET: Admin/MenuTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuType menuType = await _menuTypeDal.QueryByIdAsync(id);
            if (menuType == null)
            {
                return HttpNotFound();
            }
            return View(menuType);
        }

        // POST: Admin/MenuTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _menuTypeDal.DeleteByIdAsync(id);
            WebContentManage.RefreshMenuTypes();
            return RedirectToAction("Index");
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
