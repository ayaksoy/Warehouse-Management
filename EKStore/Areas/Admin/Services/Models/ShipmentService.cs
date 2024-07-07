using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EKStore.Areas.Admin.Services.Interfaces;
using EKStore.Data;
using EKStore.Models;
using Microsoft.EntityFrameworkCore;

namespace EKStore.Areas.Admin.Services.Models
{
    public class ShipmentService : IShipmentService
    {
        private readonly ApplicationDbContext db;

        public ShipmentService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Shipment>> GetAllAsync()
        {
            return await db.Shipment
                .Include(s => s.Vehicle)
                .Include(s => s.Driver)
                .Include(s => s.ShipmentProducts)
                    .ThenInclude(sp => sp.Product)
                .ToListAsync();
        }

        public async Task<Shipment> GetByIdAsync(int id)
        {
            return await db.Shipment
                .Include(s => s.Vehicle)
                .Include(s => s.Driver)
                .Include(s => s.ShipmentProducts)
                    .ThenInclude(sp => sp.Product)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> AddAsync(Shipment shipment)
        {
            var result = false;
            try{
            var to = await db.Warehouse.FindAsync(shipment.ToWarehouseId);
            foreach (var shipmentProduct in shipment.ShipmentProducts)
            {
                var product = await db.Product.FindAsync(shipmentProduct.ProductId);
                product.WarehouseId = to.Id;
            }
            result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            db.SaveChanges();
            return result;
        }

        public async Task<bool> UpdateAsync(Shipment shipment)
        {
            try
            {
                db.Entry(shipment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var shipment = await db.Shipment.FindAsync(id);
            if (shipment == null)
                return false;

            try
            {
                db.Shipment.Remove(shipment);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                return false;
            }
        }
    }

}