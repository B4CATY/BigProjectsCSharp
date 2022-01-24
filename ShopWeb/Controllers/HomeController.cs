using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWeb.Data;
using ShopWeb.Models;
using ShopWeb.ViewModels;
using System.Diagnostics;

namespace ShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopContext _context;

        public HomeController(ILogger<HomeController> logger, ShopContext context)
        {
            _context = context;
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            if(User.Identity.Name == null)
            {
                RedirectToAction("Login", "Account");
            }
            else
            {
                ViewData["UserName"] = _context.Accounts.Single(s => s.Login == User.Identity.Name).Name;
            }
          
           return View();
        }
        public IActionResult EditName()
        {
            Account user = _context.Accounts.Single(s => s.Login == User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditName(Account account)
        {
            if (ModelState.IsValid)
            {
                if(account.Name != _context.Accounts.Single(s => s.Login == User.Identity.Name).Name)
                {
                    var oldUser = _context.Accounts.Single(s => s.Login == User.Identity.Name);
                    oldUser.Name = account.Name;
                    _context.Update(oldUser);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Старое и новое имя идентичны");
            }
            return View(account);
        }
        
        public IActionResult EditPassword()
        {
            Account user = _context.Accounts.Single(s => s.Login == User.Identity.Name);
            EditPassword editPassword = new EditPassword();
            editPassword.OldPassword = user.Password;
            if (user == null)
            {
                return NotFound();
            }
            return View(editPassword);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPassword(EditPassword password)
        {

            if (password.ConfirmPassword == _context.Accounts.Single(s => s.Login == User.Identity.Name).Password)
            {
                var oldUser = _context.Accounts.Single(s => s.Login == User.Identity.Name);
                oldUser.Password = password.NewPassword;
                _context.Update(oldUser);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "The old password is incorrect");
            }


            return View(password);
        }


        public IActionResult ShowOrders()
        {
            var orders = (from ac in _context.Accounts
                          join c in _context.Carts on ac.Id equals c.Accountid
                          join cp in _context.CardProducts on c.Id equals cp.Cardid
                          join p in _context.Products on cp.Productid equals p.Id
                          where ac.Id == _context.Accounts.Single(s => s.Login == User.Identity.Name).Id
                          select new OrderView
                          {
                              CartId = c.Id,
                              Product = p.NameProduct
                          }).OrderBy(s => s.CartId).ToList();
            
            return View(orders);
        }
        public IActionResult CreateNewOrder()
        {
            var products = _context.Products;
            
            return View(products);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewOrder(int[] responsables)
        {
            int idAccount = _context.Accounts.Single(s => s.Login == User.Identity.Name).Id;
            int maxIdCard = _context.Carts.Max(x => x.Id) + 1;
            _context.Carts.Add(new Cart { Accountid = idAccount });
            await _context.SaveChangesAsync();
            foreach (var item in responsables)
            {
                _context.CardProducts.Add(new CardProduct { Cardid = maxIdCard, Productid = item });
                await _context.SaveChangesAsync();

            }

            return RedirectToAction("Index");
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}