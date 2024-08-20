using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CitiesController (TravelUpContext context) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCities()
        {
            try
            {
                List<City> segments = context.Cities.ToList();
                if (segments == null || segments.Count == 0)
                {
                    return NotFound("Cities Not Found");
                }
                else
                {
                    return Ok(segments);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {

            try
            {
                var city = context.Cities.Find(id);
                if (city == null)
                    return NotFound("Segment Not Found");
                else
                {
                    return Ok(city);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }





    }
}
