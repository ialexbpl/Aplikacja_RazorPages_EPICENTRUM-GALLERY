using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Aplikacja_RazorPages.Data;
using Aplikacja_RazorPages.Repositories;
using System.Collections.Generic;
using System.IO;

namespace Aplikacja_RazorPages.Pages
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public Painting painting { get; set; }

        private readonly IPaintingRepository _paintingRepository;

      
        public UpdateModel(IPaintingRepository paintingRepository)
        {
            _paintingRepository = paintingRepository;
        }

      

        [BindProperty]
        public IFormFile? PhotoFile { get; set; }

        public List<Painting> Paintings { get; set; } = new List<Painting>();
        //--------------------------------------------------------------------------------------------------------------------------------

        public IActionResult OnGet(int? id)
        {
            Paintings = _paintingRepository.GetAllPaintings();

            if (id.HasValue)
            {
                painting = _paintingRepository.GetPaintingById(id.Value);
                if (painting == null) return NotFound();
            }

            return Page();
        }

      

        //--------------------------------------------------------------------------------------------------------------------------------

        public IActionResult OnPost()
        {
            if (PhotoFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    PhotoFile.CopyTo(ms);
                    painting.Image = ms.ToArray();
                }
            }

            _paintingRepository.UpdatePainting(painting);
            return RedirectToPage("/Index", new { id = painting.Id });
        }

        //--------------------------------------------------------------------------------------------------------------------------------


        public IActionResult OnPostDelete(int id)
        {
            _paintingRepository.DeletePainting(_paintingRepository.GetPaintingById(id));
            return RedirectToPage("/Index"); 
        }
    }
}
