using EZone.Data;
using EZone.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EZone.Services
{
    public class ProductService
    {
        private readonly Guid _userId;
        public ProductService(Guid userId)
        {
            _userId = userId;
        }

        // Create product
        public bool CreateProduct(ProductCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //bool isValid = int.TryParse(model.CategoryId, out int id);
                //if(!isValid)
                //{
                //    id = 0;
                //}

                

                var entity =
                    new Product()
                    {
                        CategoryId = model.CategoryId,
                        ProductName = model.ProductName,
                        Description = model.Description,
                        ProductImage = model.ProductImage,
                        Quantity = model.Quantity,
                        Price = model.Price,
                        IsNew = model.IsNew,
                        CreatedDate = DateTimeOffset.Now
                    };

                ctx.Products.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Get all products list

        public List<ProductListItem> GetProductsList()
        {
            using (var ctx = new ApplicationDbContext())
            {
               
                var query = ctx.Products.Select(p => new ProductListItem
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CategoryId = p.CategoryId,
                    CategoryList = p.Category.Select(x => new CategoryListItem {CategoryName = x.CategoryName }).ToList(),
                   
                    Quantity = p.Quantity,
                    Price = p.Price
                });
                
                return query.OrderBy(p => p.ProductName).ToList();                       
            }
        }

        // Get Product detail by id

        public ProductDetail GetProductDetailById(int id)
        {
            using (var ctx = new ApplicationDbContext()) // Ues using statement to connect to the database
            {
                var product =
                    ctx
                        .Products
                        .Single(p => p.ProductId == id);
                var category =
                    ctx
                        .Categories
                        .Single(c => c.CategoryId == product.CategoryId);
                return
                    new ProductDetail
                    {
                        ProductId = product.ProductId,
                        CategoryId = product.CategoryId,
                        CategoryName = category.CategoryName,
                        ProductName = product.ProductName,
                        Description = product.Description,
                        ProductImage = product.ProductImage,
                        Quantity = product.Quantity,
                        Price = product.Price,
                        IsNew = product.IsNew,
                        CreatedDate = product.CreatedDate,
                        ModifiedDate = product.ModifiedDate
                    };
                        
            }
        }

        // Update method
        public bool UpdateProduct(ProductEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var product = ctx.Products.Single(p => p.ProductId == model.ProductId);
                //          Product Data            ProductEdit model
                product.CategoryId = model.CategoryId;
                product.ProductName = model.ProductName;
                product.Description = model.Description;
                product.ProductImage = model.ProductImage;
                product.Quantity = model.Quantity;
                product.Price = model.Price;
                product.IsNew = model.IsNew;
                product.ModifiedDate = model.ModifiedDate;

                return ctx.SaveChanges() == 1;


            }
        }

        // Delete method

        public bool DeleteProduct(int productId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var product =
                    ctx
                        .Products
                        .Single(p => p.ProductId == productId);

                ctx.Products.Remove(product);
                return ctx.SaveChanges() == 1;
            }
        }

        
    }
}


