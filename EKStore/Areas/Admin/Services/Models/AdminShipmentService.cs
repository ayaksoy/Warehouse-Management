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
    public class AdminShipmentService : IAdminShipmentService
    {
        ApplicationDbContext db;

        public AdminShipmentService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Task<bool> AddAsync(Shipment shipment)
        {
            var result = false;
            if (shipment == null)
            {
                return Task.FromResult(result);
            }

            db.Shipment.Add(shipment);
            db.SaveChanges();
            result = true;
            return Task.FromResult(result);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var result = false;
            var silinecek = db.Shipment.FirstOrDefaultAsync(x => x.Id == id);
            if (silinecek != null)
            {
                db.Shipment.Remove(silinecek.Result);
                db.SaveChanges();
                result = true;
            }
            return Task.FromResult(result);
        }

        public Task<List<Shipment>> GetAllAsync()
        {
            var list = db.Shipment.Include(x => x.Driver).Include(x => x.Vehicle).ToListAsync();
            return list;
        }

        public Task<Shipment> GetByIdAsync(int id)
        {
            var shipment = db.Shipment.FirstOrDefaultAsync(c => c.Id == id);

            return shipment;
        }

        public async Task<bool> UpdateAsync(Shipment shipment)
        {
            Shipment updateShipment =  db.Shipment.FirstOrDefault(c => c.Id == shipment.Id);
            var result = false;
            if (updateShipment != null)
            {
                updateShipment.FromWarehouseId = shipment.FromWarehouseId;
                updateShipment.ToWarehouseId = shipment.ToWarehouseId;
                updateShipment.ShipmentDate = shipment.ShipmentDate;
                updateShipment.VehicleId = shipment.VehicleId;
                updateShipment.DriverId = shipment.DriverId;
                updateShipment.Cost = shipment.Cost;
                updateShipment.ShipmentProducts = shipment.ShipmentProducts;
                db.SaveChanges();
                result = true;
            }

            return result;
        }
    }
}
