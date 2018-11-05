using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T1708M1_Web.Models;

namespace T1708M1_Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Products.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Save(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Redirect("Index");
        }
        public IActionResult Edit(long id)
        {
            var update = _context.Products.Find(id);
            if (update == null)
            {
                return NotFound();
            }
            return View(update);
        }

        public IActionResult Update(Product product)
        {
            var updatePro = _context.Products.Find(product.id);
            if (updatePro == null)
            {
                return NotFound();
            }

            updatePro.Name = product.Name;
            updatePro.Price = product.Price;
            _context.Products.Update(updatePro);
            _context.SaveChanges();
            return Redirect("Index");
        }

        public IActionResult Delete(long id)
        {
            var del = _context.Products.Find(id);
            if (del == null)
            {
                return NotFound();
            }

            _context.Products.Remove(del);
            _context.SaveChanges();
            return Redirect("Index");
        }
        public IActionResult DeleteMany(string ids)
        {
            var stringProductIds = ids.Split(",");
            foreach (var productId in stringProductIds)
            {
                var product = _context.Products.Find(Convert.ToInt64(productId));
                if (product == null)
                {
                    return NotFound();
                }
                _context.Products.Remove(product);

            }
            _context.SaveChanges();
            return Redirect("Index");
        }


    }
}