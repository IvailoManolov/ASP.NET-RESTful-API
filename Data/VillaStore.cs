using MagicVilla.Models;
using MagicVilla.Models.DTO;

namespace MagicVilla.Data
{
    public class VillaStore
    {
        public static List<VillaDTO> VillaDTOs = new List<VillaDTO>()
        {
                new VillaDTO() { VillaId = 1,VillaName = "Pool View",Sqft=100,Occupancy=4},
                new VillaDTO() { VillaId = 2,VillaName = "Beach View",Sqft=233,Occupancy=5}
        };
    }
}
