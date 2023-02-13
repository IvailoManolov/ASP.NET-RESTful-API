using System.ComponentModel.DataAnnotations;

namespace MagicVilla.Models.DTO
{
    public class VillaDTO
    {
        public int VillaId { get; set; }

        [Required]
        [MaxLength(30)]
        public string VillaName { get; set; }

        public int Sqft { get; set; }

        public int Occupancy { get; set; }
    }
}
