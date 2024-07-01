using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EKStore.Models;

namespace EKStore.Areas.Admin.Services.Interfaces
{
    public interface IAdminProductService
    {
        public Task<bool> AddAsync(Product product);
        public Task<bool> UpdateAsync(Product product);
        public Task<bool> DeleteAsync(int id);
        public Task<Product> GetByIdAsync(int id);
        public Task<List<Product>> GetAllAsync();
    }
}