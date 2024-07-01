using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EKStore.Areas.Admin.Services.Interfaces;
using EKStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EKStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = OtherRoles.Role_Admin)]
    public class ProductController : Controller
    {
        IAdminProductService service;

        public ProductController(IAdminProductService service)
        {
            this.service = service;
        }


        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var list=await service.GetAllAsync();
            return View(list);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        [Authorize(Roles = OtherRoles.Role_Admin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var files=HttpContext.Request.Form.Files;;

                TempData["Message"] = await service.AddAsync(product) ? "product Added Successful" : "";
            }
            return View();
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var product= await service.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                TempData["Message"] = await service.UpdateAsync(product) ? "product Edit Successfull" : "Error";
            }
            
            return View(product);
           
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result=await service.DeleteAsync(id);
            TempData["Message"] = result ? "product Deleted Successful" : "Not Found product";
            
            return RedirectToAction("Index");
        }

    }
}
