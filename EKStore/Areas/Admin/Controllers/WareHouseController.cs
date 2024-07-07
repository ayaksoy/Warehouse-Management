using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EKStore.Areas.Admin.Services.Interfaces;
using EKStore.Data;
using EKStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EKStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = OtherRoles.Role_Admin)]
    public class WarehouseController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWarehouseService service;

        public WarehouseController(ApplicationDbContext db, IWarehouseService service)
        {
            this.db = db;
            this.service = service;
        }

        public async Task<ActionResult> Index()
        {
            var list = await service.GetAllAsync();
            return View(list);
        }

        // GET: WarehouseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WarehouseController/Create
        [Authorize(Roles = OtherRoles.Role_Admin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: WarehouseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files; ;

                TempData["Message"] = await service.AddAsync(warehouse) ? "warehouse Added Successful" : "";
            }
            return View();
        }

        // GET: WarehouseController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var warehouse = await service.GetByIdAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return View(warehouse);
        }

        // POST: WarehouseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                TempData["Message"] = await service.UpdateAsync(warehouse) ? "warehouse Edit Successfull" : "Error";
            }

            return View(warehouse);

        }

        // GET: WarehouseController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await service.DeleteAsync(id);
            TempData["Message"] = result ? "warehouse Deleted Successful" : "Not Found warehouse";

            return RedirectToAction("Index");
        }
        public IActionResult Detail(int id)
        {
            var warehouse = db.Warehouse
                .Include(w => w.Products)
                    .ThenInclude(p => p.Category)
                .FirstOrDefault(w => w.Id == id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }


    }
}
