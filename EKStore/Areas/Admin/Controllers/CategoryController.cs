using EKStore.Areas.Admin.Services.Interfaces;
using EKStore.GenericModels;
using EKStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EKStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = OtherRoles.Role_Admin)]
    public class CategoryController : Controller
    {
        IAdminCategoryService _categoryService;

        public CategoryController(IAdminCategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        // GET: CategoryController
        public async Task<ActionResult> Index()
        {
            var list=await _categoryService.GetAllAsync();
            return View(list);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        [Authorize(Roles = OtherRoles.Role_Admin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var files=HttpContext.Request.Form.Files;;

                TempData["Message"] = await _categoryService.AddAsync(category) ? "Category Added Successful" : "";
            }
            return View();
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var category= await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                TempData["Message"] = await _categoryService.UpdateAsync(category) ? "Category Edit Successfull" : "Error";
            }
            
            return View(category);
           
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result=await _categoryService.DeleteAsync(id);
            TempData["Message"] = result ? "Category Deleted Successful" : "Not Found Category";
            
            return RedirectToAction("Index");
        }

    }
}
