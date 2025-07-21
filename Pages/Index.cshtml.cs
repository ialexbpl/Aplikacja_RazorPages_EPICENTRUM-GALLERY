using Microsoft.AspNetCore.Mvc.RazorPages; // Add this
using Aplikacja_RazorPages.Data;
using System.Collections.Generic;
using System.Linq;
using Aplikacja_RazorPages.Repositories;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;// For session handling

namespace Aplikacja_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Painting> Paintings { get; set; } = new List<Painting>();

        public void OnGet()
        {
            // Fetch paintings from the database  
            Paintings = _context.Paintings.ToList();
        }


        public IActionResult OnPostAddToCart(int id)
        {
            var cart = HttpContext.Session.GetString("Cart");
            var cartList = string.IsNullOrEmpty(cart) ? new List<int>() : cart.Split(',').Select(int.Parse).ToList();
            if (!cartList.Contains(id))
                cartList.Add(id);
            HttpContext.Session.SetString("Cart", string.Join(",", cartList));
            return RedirectToPage();
        }

    }
}
