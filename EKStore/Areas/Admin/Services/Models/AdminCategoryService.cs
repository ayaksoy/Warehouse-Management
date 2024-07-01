using EKStore.Areas.Admin.Services.Interfaces;
using EKStore.Data;
using EKStore.Models;
using Microsoft.EntityFrameworkCore;

namespace EKStore.Areas.Admin.Services.Models
{
    public class AdminCategoryService : IAdminCategoryService
    {
        ApplicationDbContext db;

        public AdminCategoryService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Task<bool> AddAsync(Category category)
        {
            var result = false;

            if(category != null || String.IsNullOrEmpty(category.Name)) 
            {
                db.Category.AddAsync(category);
                db.SaveChanges();
                result = true;
            }

            return Task.FromResult(result);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var result= false;
            var category=db.Category.FirstOrDefault(c => c.Id == id && !c.IsDelete);

            if(category != null)
            {
                category.IsDelete = true;
                db.SaveChanges();
                result = true;
            }

            return Task.FromResult(result);
        }

        public Task<List<Category>> GetAllAsync()
        {
            var list=db.Category.Where(c => !c.IsDelete).ToListAsync();

            return list;
        }

        public Task<Category> GetByIdAsync(int id)
        {
            var category=db.Category.FirstOrDefaultAsync(c=>c.Id == id && !c.IsDelete);

            return category;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            Category updateCategory=db.Category.FirstOrDefault(c=>c.Id == category.Id && !c.IsDelete);
            var result = false;
            if(updateCategory!=null)
            {
                updateCategory.Name=category.Name;
                updateCategory.Description=category.Description;
                updateCategory.IsStatus=category.IsStatus;

                db.SaveChanges();
                result=true;
            }

            return result;
        }
    }
}
