using System.Collections.Generic;
using Aplikacja_RazorPages.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; 

namespace Aplikacja_RazorPages.Repositories
{
    public interface IPaintingRepository
    {
        // Create a new painting
        bool CreatePainting(Painting painting);

        // Get all paintings
        List<Painting> GetAllPaintings();

        // Get a single painting by ID
        Painting GetPaintingById(int id);

        // Update an existing painting
        bool UpdatePainting(Painting painting);

        // Delete a painting by ID
        bool DeletePainting(Painting painting);



    }
}

