using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla.Models.DTO
{
    public class VillaDTO
    {
        public int VillaId { get; set; }

        [Required]
        [MaxLength(30)]
        public string VillaName { get; set; }

        public int Sqft { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }

        public int Occupancy { get; set; }
    }
}
