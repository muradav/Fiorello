using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;

        public BasketController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddItem(int? id)
        {
            if (id == null) return NotFound();

            Product dbProduct = await _context.Products.FindAsync(id);

            if (dbProduct == null) return NotFound();
            List<BasketVM> products;

            if (Request.Cookies["basket"] == null)
            {
                products = new List<BasketVM>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }

            BasketVM existProduct = products.Find(p => p.Id == id);
            if (existProduct == null)
            {
                BasketVM basketVM = new BasketVM
                {
                    Id = dbProduct.Id,
                    Count = 1
                };

                products.Add(basketVM);
            }
            else
            {
                existProduct.Count++;
            }


            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromDays(5) });

            return RedirectToAction("index", "home");
        }
        public IActionResult ShowItem()
        {

            string basket = Request.Cookies["basket"];
            List<BasketVM> products;
            if (basket != null)
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

                foreach (var item in products)
                {
                    Product dbProduct = _context.Products.FirstOrDefault(p => p.Id == item.Id);
                    item.Name = dbProduct.Name;
                    item.Price = dbProduct.Price;
                    item.ImageUrl = dbProduct.ImageUrl;
                    item.CategoryId = dbProduct.CategoryId;
                }
            }
            else
            {
                products = new List<BasketVM>();
            }
            return View(products);
        }
        public IActionResult Remove(int id)
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            BasketVM deleteProduct = products.Find(p => p.Id == id);
            products.Remove(deleteProduct);
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            return RedirectToAction("showitem", "basket");
        }
        public IActionResult Plus(int id)
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            BasketVM plusProduct = products.Find(p => p.Id == id);
            if (plusProduct.Count < 10)
            {
                plusProduct.Count++;
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            return RedirectToAction("showitem", "basket");
        }

        public IActionResult Minus(int id)
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            BasketVM minusProduct = products.Find(p => p.Id == id);
            if (minusProduct.Count > 1)
            {
                minusProduct.Count--;
            }
            else
            {
                products.Remove(minusProduct);
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            return RedirectToAction("showitem", "basket");
        }
    }
}
