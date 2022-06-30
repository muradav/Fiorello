using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public HeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.BasketCount = 0;
            ViewBag.TotalPrice = 0;
            double totalPrice = 0;
            string basket = Request.Cookies["basket"];

            if (basket != null)
            {
                List<BasketVM> products = ViewBag.BasketCount = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                ViewBag.BasketCount = products.Count;
                foreach (var item in products)
                {
                    totalPrice += item.Price * item.Count;
                }
                ViewBag.TotalPrice = totalPrice;
            }
            Bio bio = _context.Bios.FirstOrDefault();
            return View(await Task.FromResult(bio));
        }
    }
}
