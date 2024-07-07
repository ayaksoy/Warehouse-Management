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
        private readonly ApplicationDbContext db;

        public ShipmentProductService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddAsync(ShipmentProduct shipmentProduct)
        {
            if (shipmentProduct == null)
            {
                throw new ArgumentNullException(nameof(shipmentProduct), "ShipmentProduct is null.");
            }

            var product = await db.Product.FindAsync(shipmentProduct.ProductId);
            if (product == null)
            {
                throw new ArgumentNullException(nameof(shipmentProduct.Product), "Product in ShipmentProduct is null.");
            }
            shipmentProduct.Product = product;

            var shipment = await db.Shipment.FindAsync(shipmentProduct.ShipmentId);
            if (shipment == null)
            {
                throw new ArgumentNullException(nameof(shipmentProduct.Shipment), "Shipment in ShipmentProduct is null.");
            }
            shipmentProduct.Shipment = shipment;

            if (shipmentProduct.Product.Quantity == null)
            {
                throw new ArgumentNullException(nameof(shipmentProduct.Product.Quantity), "Quantity in Product is null.");
            }

            bool result = false;

            if (shipmentProduct.Quantity <= shipmentProduct.Product.Quantity)
            {
                var toWarehouse = await db.Warehouse.FindAsync(shipmentProduct.Shipment.ToWarehouseId);
                if (toWarehouse == null)
                {
                    throw new InvalidOperationException("Warehouse not found.");
                }
                var varmi = db.Product.Where(x => x.Name == shipmentProduct.Product.Name && x.WarehouseId == toWarehouse.Id).FirstOrDefault();
                if (varmi != null)
                {
                    varmi.Quantity += shipmentProduct.Quantity;
                    db.Product.Update(varmi);
                }
                else
                {
                    var newProduct = new Product
                    {
                        Name = shipmentProduct.Product.Name,
                        Quantity = shipmentProduct.Quantity,
                        Description = shipmentProduct.Product.Description,
                        CategoryId = shipmentProduct.Product.CategoryId,
                        WarehouseId = toWarehouse.Id
                    };

                    db.Product.Add(newProduct);
                }
                if (shipmentProduct.Quantity == shipmentProduct.Product.Quantity)
                {
                    db.Product.Remove(shipmentProduct.Product);
                }
                else
                {
                    shipmentProduct.Product.Quantity -= shipmentProduct.Quantity;
                }

                await db.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            var shipmentProduct = await db.ShipmentProduct.FindAsync(id);

            if (shipmentProduct != null)
            {
                db.ShipmentProduct.Remove(shipmentProduct);
                await db.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public Task<List<ShipmentProduct>> GetAllAsync()
        {
            return db.ShipmentProduct.Include(x => x.Product).Include(x => x.Shipment).ToListAsync();
        }

        public Task<ShipmentProduct> GetByIdAsync(int id)
        {
            return db.ShipmentProduct.Include(x => x.Product).Include(x => x.Shipment).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}