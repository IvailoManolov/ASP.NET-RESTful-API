using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers
{
    [ApiController]
    [Route("api/VillaAPI")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;

        public VillaAPIController(ILogger<VillaAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.VillaDTOs);
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.VillaDTOs.FirstOrDefault(x => x.VillaId == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {

            if (VillaStore.VillaDTOs.FirstOrDefault(x => x.VillaName.ToLower() == villaDTO.VillaName.ToLower()) != null)
            {
                ModelState.AddModelError("", "Villa already exists!");
                return BadRequest(ModelState);
            }

            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }

            if (villaDTO.VillaId > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDTO.VillaId = VillaStore.VillaDTOs.OrderByDescending(x => x.VillaId).FirstOrDefault().VillaId + 1;

            VillaStore.VillaDTOs.Add(villaDTO);

            return CreatedAtRoute("GetVilla", new { id = villaDTO.VillaId }, villaDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.VillaDTOs.FirstOrDefault(x => x.VillaId == id);

            if (villa == null)
            {
                return NotFound();
            }

            VillaStore.VillaDTOs.Remove(villa);

            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if(villaDTO == null || id != villaDTO.VillaId)
            {
                return BadRequest();
            }

            var villa = VillaStore.VillaDTOs.FirstOrDefault(x => x.VillaId == id);

            villa.VillaName = villaDTO.VillaName;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy= villaDTO.Occupancy;

            return NoContent();
        }
    }
}
