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
    public class AdminProductService : IAdminProductService
    {
        ApplicationDbContext db;

        public AdminProductService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Task<bool> AddAsync(Product product)
        {
            var result = false;

            if (product != null || String.IsNullOrEmpty(product.Name))
            {
                db.Product.AddAsync(product);
                db.SaveChanges();
                result = true;
            }

            return Task.FromResult(result);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var result = false;
            var product = db.Product.FirstOrDefault(c => c.Id == id && !c.IsDelete);

            if (product != null)
            {
                product.IsDelete = true;
                db.SaveChanges();
                result = true;
            }

            return Task.FromResult(result);
        }

        public Task<List<Product>> GetAllAsync()
        {
            var list = db.Product.Where(c => !c.IsDelete).Include(x => x.Category).Include(x => x.Warehouse).ToListAsync();

            foreach(var item in list.Result)
            {
                item.Category = db.Category.FirstOrDefault(c => c.Id == item.CategoryId);
                item.Warehouse = db.Warehouse.FirstOrDefault(c => c.Id == item.WarehouseId);
            }
            return list;
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = db.Product.FirstOrDefaultAsync(c => c.Id == id && !c.IsDelete);

            return product;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            Product updateProduct = db.Product.FirstOrDefault(c => c.Id == product.Id && !c.IsDelete);
            var result = false;
            if (updateProduct != null)
            {
                updateProduct.Name = product.Name;
                updateProduct.Description = product.Description;
                updateProduct.IsStatus = product.IsStatus;

                db.SaveChanges();
                result = true;
            }

            return result;
        }
    }
}
