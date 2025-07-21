using Aplikacja_RazorPages.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // For session handling
using System.Collections.Generic;
using Aplikacja_RazorPages.Repositories;
using Aplikacja_RazorPages.Data;

namespace Aplikacja_RazorPages.Pages
{
    public class cartModel : PageModel
    {
        private readonly AppDbContext _context;

        public cartModel(AppDbContext context)
        {
            _context = context;
        }
        public List<Painting> CartPaintings { get; set; } = new List<Painting>();
        
        public void OnGet()
        {
            CartPaintings = _context.Paintings.ToList();
            var cart = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cart))
            {
                var ids = cart.Split(',').Select(int.Parse).ToList();
                CartPaintings = _context.Paintings.Where(p => ids.Contains(p.Id)).ToList();
            }
        }

        public IActionResult OnPostRemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetString("Cart");
            var cartList = string.IsNullOrEmpty(cart) ? new List<int>() : cart.Split(',').Select(int.Parse).ToList();
            cartList.Remove(id);
            HttpContext.Session.SetString("Cart", string.Join(",", cartList));
            return RedirectToPage();
        }
    }
}
