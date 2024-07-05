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
    public class WarehouseService : IWarehouseService
    {
        private readonly ApplicationDbContext db;

        public WarehouseService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> AddAsync(Warehouse warehouse)
        {
            var result = false;
            if(warehouse == null || string.IsNullOrEmpty(warehouse.Name))
            {
                return result;
            }
            Warehouse add = new Warehouse
            {
                Name = warehouse.Name,
                LocationId = warehouse.LocationId,
                Products = warehouse.Products
            };
            await db.Warehouse.AddAsync(add);
            db.SaveChanges();
            result = true;
            return result;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = false;
            var isDelete = await db.Warehouse.FirstOrDefaultAsync(x => x.IsDelete == false && x.Id == id);
            if(isDelete == null)
            {
                return result;
            }
            isDelete.IsDelete = true;
            db.SaveChanges();
            result = true;
            return result;
        }

        public async Task<List<Warehouse>> GetAllAsync()
        {
            var list =await db.Warehouse.Where(c => !c.IsDelete).Include(x => x.Location).ToListAsync();
            return list;
        }
        

        public async Task<Warehouse> GetByIdAsync(int id)
        {
            var result =await db.Warehouse.FirstOrDefaultAsync(x => x.Id == id && x.IsDelete == false);
            return result; 
        }

        public async Task<bool> UpdateAsync(Warehouse warehouse)
        {
            Warehouse updateWarehouse=db.Warehouse.FirstOrDefault(c=>c.Id == warehouse.Id && !c.IsDelete);
            var result = false;
            if(updateWarehouse!=null)
            {
                updateWarehouse.Name=warehouse.Name;
                updateWarehouse.LocationId = warehouse.LocationId;
                updateWarehouse.IsStatus=warehouse.IsStatus;
                db.SaveChanges();
                result=true;
            }

            return result;
        }
    }
}