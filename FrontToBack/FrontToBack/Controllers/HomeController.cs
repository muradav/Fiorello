using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FrontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Sliders = _context.Sliders.ToList();
            homeVM.SliderContent = _context.SliderContents.FirstOrDefault();
            homeVM.Categories = _context.Categories.ToList();
            homeVM.Products = _context.Products.Include(p=>p.Category).ToList();
            homeVM.AboutImage = _context.AboutImages.FirstOrDefault();
            homeVM.AboutContent = _context.AboutContents.FirstOrDefault();
            homeVM.Experts=_context.Experts.ToList();
            homeVM.Blogs = _context.Blogs.ToList();
            homeVM.Florists = _context.Florists.ToList();
            homeVM.SocialAddressFlorists = _context.SocialAddressFlorists.ToList();

            return View(homeVM);
        }

        public IActionResult SearchProduct(string search)
        {
            List<Product> products = _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Id)
                .Where(p => p.Name.ToLower()
                .Contains(search.ToLower()))
                .Take(5)
                .ToList();
            return PartialView("_SearchPartial",products);
        }
    }
}
