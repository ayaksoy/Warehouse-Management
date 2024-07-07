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
            if (shipmentProduct == null)
            {
                throw new ArgumentNullException(nameof(shipmentProduct), "ShipmentProduct is null.");
            }

            if (shipmentProduct.Product == null)
            {
                throw new ArgumentNullException(nameof(shipmentProduct.Product), "Product in ShipmentProduct is null.");
            }

            if (shipmentProduct.Product.Quantity == null)
            {
                throw new ArgumentNullException(nameof(shipmentProduct.Product.Quantity), "Quantity in Product is null.");
            }
            var result = false;
            db.ShipmentProduct.Include(x => x.Product).Include(x => x.Shipment);
            if (shipmentProduct.Quantity < shipmentProduct.Product.Quantity)
            {
                var to = db.Warehouse.Find(shipmentProduct.Shipment.ToWarehouseId);
                var product = db.Product.Find(shipmentProduct.ProductId);
                Product product1 = new Product();
                product1.Name = product.Name;
                product1.Quantity = shipmentProduct.Quantity;
                product1.Description = product.Description;
                product1.CategoryId = product.CategoryId;
                product1.WarehouseId = to.Id;
                db.Product.Add(product1);
                product.Quantity -= shipmentProduct.Quantity;
                db.SaveChanges();
                result = true;
            }
            else if (shipmentProduct.Quantity == shipmentProduct.Product.Quantity)
            {
                var to = db.Warehouse.Find(shipmentProduct.Shipment.ToWarehouseId);
                var product = db.Product.Find(shipmentProduct.ProductId);
                Product product1 = new Product();
                product1.Name = product.Name;
                product1.Quantity = shipmentProduct.Quantity;
                product1.Description = product.Description;
                product1.CategoryId = product.CategoryId;
                product1.WarehouseId = to.Id;
                db.Product.Add(product1);
                db.Product.Remove(product);
                db.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            db.SaveChanges();
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
            var list = db.ShipmentProduct.Include(x => x.Product).Include(x => x.Shipment).ToListAsync();
            return list;
        }

        public Task<ShipmentProduct> GetByIdAsync(int id)
        {
            var shipmentProduct = db.ShipmentProduct.FirstOrDefaultAsync(c => c.Id == id);

            return shipmentProduct;
        }
    }
}
