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
    public class ProductController : Controller
    {
        private readonly ShopContext _context;

        public ProductController(ShopContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var shopContext = _context.Products.Include(p => p.Category);
            return View(await shopContext.ToListAsync());
        }
        public async Task<IActionResult> IndexOrdered()
        {
            var shopContext = _context.Products.Include(p => p.Category).OrderBy(s=>s.Price);
            return View(await shopContext.ToListAsync());
        }
    }
}
