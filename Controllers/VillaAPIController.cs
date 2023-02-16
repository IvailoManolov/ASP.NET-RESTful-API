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
        private readonly ApplicationDBContext _dbContext;


        public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(_dbContext.Villas);
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Getting Villa Error with Id:[{id}]");
                return BadRequest();
            }

            var villa = _dbContext.Villas.FirstOrDefault(x => x.VillaId== id);

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
            if (_dbContext.Villas.FirstOrDefault(x => x.VillaName.ToLower() == villaDTO.VillaName.ToLower()) != null)
            {
                _logger.LogWarning("Villa already exists!");
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

            Villa model = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                VillaId = villaDTO.VillaId,
                ImageUrl = villaDTO.ImageUrl,
                VillaName = villaDTO.VillaName,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };

            _dbContext.Villas.Add(model);

            _dbContext.SaveChanges();

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
                _logger.LogError("Id can't be 0");
                return BadRequest();
            }

            var villa = _dbContext.Villas.FirstOrDefault(x => x.VillaId == id);

            if (villa == null)
            {
                return NotFound();
            }
            _dbContext.Villas.Remove(villa);

            _dbContext.SaveChanges();

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

            Villa model = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                VillaId = villaDTO.VillaId,
                ImageUrl = villaDTO.ImageUrl,
                VillaName = villaDTO.VillaName,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };

            _dbContext.Villas.Update(model);

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
