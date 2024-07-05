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
using Microsoft.Extensions.Logging;

namespace EKStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = OtherRoles.Role_Admin)]
    public class ShipmentController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IAdminShipmentService service;

        public ShipmentController(ApplicationDbContext db, IAdminShipmentService service)
        {
            this.db = db;
            this.service = service;
        }

        public async Task<ActionResult> Index()
        {
            var list = await service.GetAllAsync();
            return View(list);
        }

        // GET: ShipmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShipmentController/Create
        [Authorize(Roles = OtherRoles.Role_Admin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShipmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files; ;

                TempData["Message"] = await service.AddAsync(shipment) ? "warehouse Added Successful" : "";
            }
            return View();
        }

        // GET: ShipmentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var shipment = await service.GetByIdAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            return View(shipment);
        }

        // POST: ShipmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                TempData["Message"] = await service.UpdateAsync(shipment) ? "shipment Edit Successfull" : "Error";
            }

            return View(shipment);

        }

        // GET: ShipmentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await service.DeleteAsync(id);
            TempData["Message"] = result ? "shipment Deleted Successful" : "Not Found shipment";

            return RedirectToAction("Index");
        }

    }
}
