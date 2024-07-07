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
    public class ShipmentProductService : IShipmentProductService
    {
        ApplicationDbContext db;

        public ShipmentProductService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Task<bool> AddAsync(ShipmentProduct shipmentProduct)
        {
            var result = false;

            if (shipmentProduct != null)
            {
                db.ShipmentProduct.AddAsync(shipmentProduct);
                db.SaveChanges();
                result = true;
            }

            return Task.FromResult(result);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var result = false;
            var shipmentProduct = db.ShipmentProduct.FirstOrDefault(c => c.Id == id);

            if (shipmentProduct != null)
            {
                db.ShipmentProduct.Remove(shipmentProduct);
                result = true;
            }

            return Task.FromResult(result);
        }

        public Task<List<ShipmentProduct>> GetAllAsync()
        {
            var list = db.ShipmentProduct.Include(x => x.Product).ToListAsync();
            return list;
        }

        public Task<ShipmentProduct> GetByIdAsync(int id)
        {
            var shipmentProduct = db.ShipmentProduct.FirstOrDefaultAsync(c => c.Id == id);

            return shipmentProduct;
        }
    }
}
