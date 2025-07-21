using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Aplikacja_RazorPages.Repositories;
using System.IO;
using Aplikacja_RazorPages.Data;

namespace Aplikacja_RazorPages.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IPaintingRepository _paintingRepository;

        [BindProperty]
        public Painting Painting { get; set; }

        [BindProperty]
        public IFormFile? PhotoFile { get; set; }

        public CreateModel(IPaintingRepository paintingRepository)
        {
            _paintingRepository = paintingRepository;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
          
            if (PhotoFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    PhotoFile.CopyTo(memoryStream);
                    Painting.Image = memoryStream.ToArray();
                }
            }

            bool isCreated = _paintingRepository.CreatePainting(Painting);

            if (!isCreated)
            {
                ModelState.AddModelError("", "Failed to create the painting.");
                return Page();
            }

            return RedirectToPage("/Index", new { id = Painting.Id });
        }
    }
}

