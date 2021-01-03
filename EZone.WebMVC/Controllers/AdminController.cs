using EZone.Data;
using EZone.Models;
using EZone.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace EZone.WebMVC.Controllers
{
    [Authorize(Users ="jkoh@yahoo.com")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Admin/CategoryIndex
        public ActionResult CategoryIndex()
        {
            return View((new CategoryService()).GetCategoryList());
        }

        // Get: Admin/CreateCategory
        public ActionResult CreateCategory()
        {
            return View();
        }

        // Post: Admin/CreateCategory

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            if ((new CategoryService()).CreateCategory(model))
            {
                TempData["SaveResult"] = "Your category was created.";
                return RedirectToAction("CategoryIndex");
            };
            ModelState.AddModelError("", "Category could not created");
            return View(model);
        }

        // Get category details
        public ActionResult CategoryDetails(int id)
        {
            var service = new CategoryService();
            var model = service.GetCategoryById(id);
            return View(model);
        }

        // Get category edit
        public ActionResult EditCategory(int id)
        {
            var service = new CategoryService();
            var detail = service.GetCategoryById(id);
            var model = new CategoryEdit
            {
                CategoryId = detail.CategoryId,
                CategoryName = detail.CategoryName
            };

            return View(model);
        }

        // Post category edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(int id, CategoryEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CategoryId != id)
            {
                ModelState.AddModelError("", "ID mismatch");
                return View(model);
            }

            var service = new CategoryService();

            if (service.UpdateCategory(model))
            {
                TempData["SaveResult"] = "Your category was updated.";
                return RedirectToAction("CategoryIndex");
            }

            ModelState.AddModelError("", "Your category could not be updated.");
            return View(model);
        }

        // Get delete category
        //public ActionResult CategoryDelete(int id)
        //{
        //    var service = new CategoryService();
        //    var model = service.GetCategoryById(id);
        //    return View(model);
        //}

        public ActionResult CategoryDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = _db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // Post delete category

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CategoryDeletePost(int id)
        //{
        //    var service = new CategoryService();
        //    service.DeleteCategory(id);
        //    TempData["SaveResult"] = "Your category was deleted";
        //    return RedirectToAction("CategoryIndex");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryDelete(int id)
        {
            Category category = _db.Categories.Find(id);
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("CategoryIndex");
        }

        // Get: Product list
        public ActionResult ProductIndex()
        {
            return View(CreateProductService().GetProductsList());
            
        }

        // Get: Admin/AddProduct
        public ActionResult AddProduct()
        {
            ViewBag.Title = "New Product";
            List<Category> Categories = new CategoryService().GetCategories().ToList();
            var query = from c in Categories
                        select new SelectListItem()
                        {
                            Value = c.CategoryId.ToString(),
                            Text = c.CategoryName
                        };
            ViewBag.CategoryId = query.ToList();
            return View();
        }

        // Post: Admin/AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product model, HttpPostedFileBase file)
        {


            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            //if (CreateProductService().CreateProduct(model) && file !=null)
            //{

            //    TempData["SaveResult"] = "Product was created";
            //    return RedirectToAction("ProductIndex");
            //}

            //ModelState.AddModelError("", "Product could not be created.");
            //return View(model);

            string pic = null;
            if (file != null)
            {
                pic = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            model.ProductImage = pic;

            model.CreatedDate = DateTimeOffset.Now;

            _db.Products.Add(model);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Saved Successfully";
            return RedirectToAction("ProductIndex");

        }

        //// Get: Edit Product

        //public ActionResult EditProduct(int id) // Right click on the EditProduct(int id) method to create the physical view for the ProductEdit model. Add a view.
        //{
        //    var service = CreateProductService();
        //    var detail = service.GetProductDetailById(id);
        //    var model =
        //        new ProductEdit
        //        {
        //            // ProductEdit model         ProductDetail model
        //            ProductId = detail.ProductId,
        //            CategoryId = detail.CategoryId,
        //            ProductName = detail.ProductName,
        //            ProductImage = detail.ProductImage,
        //            Description = detail.Description,
        //            Quantity = detail.Quantity,
        //            Price = detail.Price,
        //            IsNew = detail.IsNew,
        //            ModifiedDate = DateTimeOffset.Now

        //        };
        //    return View(model);
        //}



        // Post: Edit Product
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditProduct(int id, ProductEdit model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    if (model.ProductId != id)
        //    {
        //        ModelState.AddModelError("", "ID Mismatch");
        //        return View(model);
        //    }

        //    var service = CreateProductService();
        //    // UpdateProduct service
        //    if (service.UpdateProduct(model))
        //    {
        //        TempData["SaveResult"] = "Your product was updated.";
        //        return RedirectToAction("ProductIndex"); // Return to Product list
        //    }

        //    ModelState.AddModelError("", "Your product could not be updated.");
        //    return View();
        //}

        public ActionResult EditProduct(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = _db.Products.Find(id);
            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);  // return HttpNotFound();
            }
            ViewBag.Title = "Edit Product";
            List<Category> Categories = new CategoryService().GetCategories().ToList();
            var query = from c in Categories
                        select new SelectListItem()
                        {
                            Value = c.CategoryId.ToString(),
                            Text = c.CategoryName
                        };
            ViewBag.CategoryId = query.ToList();
            return View(product);

        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Product product, HttpPostedFileBase file)
        {
           
            string pic = null;
            if (file != null)
            {
                pic = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            product.ProductImage = file !=null? pic : product.ProductImage;

            product.ModifiedDate = DateTimeOffset.Now;

            _db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("ProductIndex");

        }

        // Get Product detail by id
        public ActionResult ProductDetails(int id)
        {
            var detail = CreateProductService().GetProductDetailById(id);
            return View(detail);
        }


        // Get delete product
        [ActionName("ProductDelete")]
        public ActionResult ProductDelete(int id)
        {
            var service = CreateProductService();
            var model = service.GetProductDetailById(id);
            return View(model);
        }

        // Post: delete product
        [HttpPost]
        [ActionName("ProductDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDeletePost(int id)
        {
            var service = CreateProductService();
            service.DeleteProduct(id);
            TempData["SaveResult"] = "Your product was deleted";
            return RedirectToAction("ProductIndex");
        }

        public ActionResult Delete(int id)
        {
            Product product = _db.Products.Find(id);
            _db.Products.Remove(product);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Deleted Successuflly";
            return RedirectToAction("ProductIndex");
        }

        // Get: order list

        public ActionResult OrderIndex()
        {
            return View(CreateOrderService().GetOrdersList());
        }

        // Get: order detail by id
        public ActionResult OrderDetailById(int id)
        {
            var detail = CreateOrderService().GetOrderDetailById(id);
            return View(detail);
        }



        private ProductService CreateProductService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProductService(userId);
            return service;
        }

        private OrderService CreateOrderService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new OrderService(userId);
            return service;
        }
    }
}