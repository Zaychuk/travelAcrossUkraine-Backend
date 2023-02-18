using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;

namespace TravelAcrossUkraine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeoPointsController : ControllerBase
    {
        private readonly IGeoPointService _geoPointService;

        public GeoPointsController(IGeoPointService geoPointService)
        {
            _geoPointService = geoPointService;
        }

        /// <summary>
        /// Returns all geopoint on the map
        /// </summary>
        /// <returns>
        /// Returns all geopoint on the map
        /// </returns>
        [HttpGet()]
        public async Task<ActionResult<GeoPointDto>> GetAllAsync()
        {
            return Ok(await _geoPointService.GetAllAsync());
        }

        /// <summary>
        /// Return geopoint by provided id
        /// </summary>
        /// <param name="id">Id of the geopoint to retrieve</param>
        /// <returns>Geopoint by provided id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GeoPointDto>> GetAllAsync(Guid id)
        {
            try
            {
                return Ok(await _geoPointService.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<ActionResult<Guid>> CreateAsync(GeoPointDto geoPoint)
        {
            var result = await _geoPointService.CreateAsync(geoPoint);
            return Ok(result);
        }
    }
}