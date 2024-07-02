using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EKStore.Models;

namespace EKStore.Areas.Admin.Services.Interfaces
{
    public interface IWarehouseService
    {
        public Task<bool> AddAsync(Warehouse warehouse);
        public Task<bool> UpdateAsync(Warehouse warehouse);
        public Task<bool> DeleteAsync(int id);
        public Task<Warehouse> GetByIdAsync(int id);
        public Task<List<Warehouse>> GetAllAsync();
    }
}