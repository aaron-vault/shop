using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(string title, int price)
        {
            AddProduct(new Product { Title = title, Price = price }).Wait();
            return RedirectToAction("Index", "Home");
        }

        private async Task<Product> AddProduct(Product product)
        {
            using (var db = new ProductContext())
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
            }
            return product;
        }

        public ActionResult Delete(int id)
        {
            DeleteProduct(id).Wait();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("Delete")]
        private async Task<int> DeleteProduct(int id)
        {
            using (var db = new ProductContext())
            {
                Product product = await db.Products.FindAsync(id);
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return id;
            }
        }

        public ActionResult Details(int id)
        {
            Product product = GetProduct(id).Result;
            return View(product);
        }

        private async Task<Product> GetProduct(int id)
        {
            using (var db = new ProductContext())
            {
                Product product = await db.Products.FindAsync(id);
                return product;
            }
        }

        public ActionResult Edit(int id)
        {
            Product product = GetProduct(id).Result;
            return View(product);
        }

        public ActionResult EditProduct([Bind(Include = "Id,Title,Price")] Product product)
        {
            EditProductAction(product).Wait();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Product> EditProductAction(Product product)
        {
            using (var db = new ProductContext())
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return product;
            }
        }
    }
}