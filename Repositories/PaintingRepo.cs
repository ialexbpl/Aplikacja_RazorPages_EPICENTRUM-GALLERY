using Aplikacja_RazorPages.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aplikacja_RazorPages.Repositories
{
    public class PaintingRepository : IPaintingRepository
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.db");
        public static string connectionString = $"Data Source={dbPath}";

        private AppDbContext _dbContext = new AppDbContext();

        public PaintingRepository()
        {
            _dbContext.Database.EnsureCreated(); // Ensure the database is created
        }


        //--------------------------------------------------------------------------------------------------------------------------------
        // Create a new painting
        public bool CreatePainting(Painting painting)
        {
            try
            {
                if (painting.Image != null && painting.Image.Length > 0)
                {
                    Console.WriteLine("Image uploaded successfully!");
                }

                _dbContext.Paintings.Add(painting);
                var result = _dbContext.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while saving: {ex.Message}");
                return false;
            }
        }


        //--------------------------------------------------------------------------------------------------------------------------------

        // Get all paintings
        public List<Painting> GetAllPaintings()
        {
            return _dbContext.Paintings.ToList();
        }

        //--------------------------------------------------------------------------------------------------------------------------------

        // Get a single painting by ID
        public Painting? GetPaintingById(int id)
        {
            return _dbContext.Paintings.FirstOrDefault(p => p.Id == id);
        }

       

        //--------------------------------------------------------------------------------------------------------------------------------

        // Update an existing painting
        public bool UpdatePainting(Painting painting)
        {

            if (painting == null) return false;

            var existingPainting = _dbContext.Paintings.Find(painting.Id);
            if (existingPainting == null) return false;

            // Update fields
            existingPainting.Name = painting.Name;
            existingPainting.Author = painting.Author;
            existingPainting.Description = painting.Description;
            existingPainting.Price = painting.Price;

            // Update image only if provided
            if (painting.Image != null && painting.Image.Length > 0)
            {
                existingPainting.Image = painting.Image;
            }

            return _dbContext.SaveChanges() > 0;

        }

   

        //--------------------------------------------------------------------------------------------------------------------------------

        // Delete a painting by ID
        public bool DeletePainting(Painting painting)
        {
            try
            {
                
                if (painting != null)
                {
                    _dbContext.Paintings.Remove(painting);
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting: {ex.Message}");
                return false;
            }
        }


    }
}



