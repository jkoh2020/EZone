using EZone.Data;
using EZone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Services
{
    public class CategoryService
    {
        // Create Category
        public bool CreateCategory(CategoryCreate model)
        {
            var entity = new Category()
            {
                CategoryName = model.CategoryName
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Get categories
        public IEnumerable<Category> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Categories.ToList();
            }
        }

        // Get category list
        public List<CategoryListItem> GetCategoryList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Categories
                        .Select(e => new CategoryListItem
                        {
                // CategoryListItem model   Category Data
                            CategoryId = e.CategoryId,
                            CategoryName = e.CategoryName
                        });
                //return query.ToArray();
                return query.OrderBy(c => c.CategoryName).ToList();
            }
        }

        // Get catetory detail by category id
        public CategoryDetail GetCategoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == id);
                return new CategoryDetail
                {
        // CategoryDetail model         Category data
                    CategoryId = (int)entity.CategoryId,
                    CategoryName = entity.CategoryName
                };
                        
            }
        }

        // Update category
        public bool UpdateCategory(CategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == model.CategoryId);
                entity.CategoryName = model.CategoryName;
                return ctx.SaveChanges() == 1;
            }
        }

        // Delete category by category id

        public bool DeleteCategory(int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == categoryId);
                ctx.Categories.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
