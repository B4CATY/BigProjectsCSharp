#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopWeb.Data;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ShopContext _context;

        public CategoriesController(ShopContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.Include(p => p.Products).ToListAsync());
        }
        public async Task<IActionResult> IndexOrdered(int? id)
        {
            ViewData["Category"] = _context.Categories.Find(id).Name;
            var shopContext = _context.Products
                .Include(p => p.Category).Select(s=>s)
                .Where(s=>s.Categoryid == id)
                .OrderBy(s => s.Price);
            return View(await shopContext.ToListAsync());
        }
        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _context.Products.Select(s => s).Where(s => s.Categoryid == id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["Category"] = _context.Categories.Find(id).Name;
            return View(await category.ToListAsync());
        }

        // GET: Categories/Create
        
       
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
