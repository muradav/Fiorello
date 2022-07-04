using FrontToBack.DAL;
using FrontToBack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool existNameCategory = _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (existNameCategory)
            {
                ModelState.AddModelError("Name", "This category is exist");
                return View();
            }

            Category newCategory = new Category
            {
                Name = category.Name,
                Desc = category.Desc
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if(dbCategory==null) return NotFound();

            return View(dbCategory);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();

            return View(dbCategory);
        }
    }
}
