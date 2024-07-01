using EKStore.Models;

namespace EKStore.Areas.Admin.Services.Interfaces
{
    public interface IAdminCategoryService
    {
        public Task<bool> AddAsync(Category category);
        public Task<bool> UpdateAsync(Category category);
        public Task<bool> DeleteAsync(int id);
        public Task<Category> GetByIdAsync(int id);
        public Task<List<Category>> GetAllAsync();
    }
}
