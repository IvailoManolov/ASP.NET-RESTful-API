using MagicVilla.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    VillaId= 1,
                    VillaName="Roylal villa",
                    Details="loreimpsum",
                    ImageUrl= "https://static-cse.canva.com/blob/666314/bestfreestockphotos.jpg",
                    Occupancy=5,
                    Rate=200,
                    Sqft=550,
                    Amenity=""
                }, 
                new Villa()
                {
                    VillaId = 2,
                    VillaName = "Roylal villa",
                    Details = "loreimpsum",
                    ImageUrl = "https://static-cse.canva.com/blob/666314/bestfreestockphotos.jpg",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 550,
                    Amenity = ""
                },
                new Villa()
                {
                    VillaId = 3,
                    VillaName = "Roylal villa",
                    Details = "loreimpsum",
                    ImageUrl = "https://static-cse.canva.com/blob/666314/bestfreestockphotos.jpg",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 550,
                    Amenity = ""
                },
                new Villa()
                {
                    VillaId = 4,
                    VillaName = "Roylal villa",
                    Details = "loreimpsum",
                    ImageUrl = "https://static-cse.canva.com/blob/666314/bestfreestockphotos.jpg",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 550,
                    Amenity = ""
                }
                );
        }
    }
}
