using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PassangerController(TravelUpContext context) : ControllerBase
    {
        
        [HttpGet("{id}")]
        public async Task<IEnumerable<Passenger>> GetPassangers(int id)
        {
                List<Passenger> passangerList = await context.Passengers.Where(x => x.SegmentId == id).ToListAsync();
                return passangerList;
           
        }
       
    }
}
