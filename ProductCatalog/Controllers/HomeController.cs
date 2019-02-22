using ProductCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCatalog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = from p in _products select p;

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                foreach (var item in _products)
                {
                    if (item.Name == collection["Name"])
                    {
                        ModelState.AddModelError("Name", "A product with this name already exists.");
                        return View();
                    }
                }

                var product = new Product
                {
                    Id = Convert.ToInt32(collection["Id"]),
                    Name = collection["Name"],
                    Description = collection["Description"],
                    Quantity = Convert.ToInt32(collection["Quantity"])
                };

                _products.Add(product);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        static List<Product> _products = new List<Product>();
    }
}