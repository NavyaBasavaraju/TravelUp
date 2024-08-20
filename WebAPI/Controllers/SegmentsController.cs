using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SegmentsController(TravelUpContext context) : ControllerBase
    {
        
        // Get All segments
        [HttpGet]
        public IActionResult GetSegments()
        {
            try
            {
                List<Segment> segments = context.Segments.Include(x => x.ArrivalCityNavigation).Include(x => x.DepartureCityNavigation).ToList();
                if (segments == null || segments.Count == 0)
                {
                    return NotFound("Segment not found");
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

        //Get segment for the id
        [HttpGet("{id:int}")]
        public IActionResult GetSegment(int id)
        {

            try
            {
                var segment = context.Segments.Find(id);
                if (segment == null)
                    return NotFound("Segment Not Found");
                else
                {
                    return Ok(segment);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        // Delete Segment
        [HttpDelete("{id:int}")]
        public IActionResult DeleteSegment(int id)
        {


            try
            {
                //Include is used to delete passangers related to segment being deleted
                var segment = context.Segments.Include(x => x.Passengers).SingleOrDefault(x => x.SegmentId == id);
                if (segment == null)
                {
                    return NotFound("Segment Not Found");
                }
                else
                {
                    context.Segments.Remove(segment);
                    context.SaveChanges();
                    return Ok("Deleted");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        
        //Creating new segment
        [HttpPost]
        public IActionResult CreateSegment(Segment segment)
        {
            try
            {
                context = new TravelUpContext();
                context.Set<Segment>().Add(segment);
                context.Entry(segment.ArrivalCityNavigation).State = EntityState.Unchanged; // Do not update City table when segment is updated
                context.Entry(segment.DepartureCityNavigation).State = EntityState.Unchanged; // Do not update City table when segment is updated
                context.SaveChanges();
                return Ok("Segment created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //updating segment
        [HttpPut]
        public IActionResult UpdateSegment(Segment segment)
        {
            try
            {
                context.Set<Segment>().Update(segment);
                context.Entry(segment.ArrivalCityNavigation).State = EntityState.Unchanged;
                context.Entry(segment.DepartureCityNavigation).State = EntityState.Unchanged;
                context.SaveChanges();
                return Ok("Segment updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
