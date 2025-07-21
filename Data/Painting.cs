using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Aplikacja_RazorPages.Repositories;
namespace Aplikacja_RazorPages.Data
{
    public class Painting
    {
        [Key]
        public int Id { get; set; } // Primary Key


        public string Name { get; set; } // Name of the painting


        public string Author { get; set; } // Author of the painting


        public string Description { get; set; } // Description of the painting


        public double Price { get; set; } // Price of the painting


        public byte[] Image { get; set; } // Image of the painting

    }

}

