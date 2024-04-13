using CRM_CMC.Contexts;
using CRM_CMC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_CMC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly CRM_CMCContext _context;

        public LocationsController(CRM_CMCContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetLocations([FromQuery] int skip, [FromQuery] int take, [FromQuery] string? search)
        {
            // Get the total number of locations
            

            // Filter the data if a search term is provided
            IQueryable<TblLocation> data = _context.TblLocations.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(l => l.Code.Contains(search) || l.Name.Contains(search) || l.Description.Contains(search));
            }

            int total = data.Count();

            // Get the locations for the current page
            var locations = data.Skip(skip).Take(take).ToList();

            // Return the data in the format expected by DataTables
            return Ok(new
            {
                draw = Request.Query["draw"],
                recordsTotal = total,
                recordsFiltered = data.Count(), // Update the number of filtered records
                data = locations
            });
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddLocaton([FromBody] TblLocation location)
        {
            try
            {
                this._context.TblLocations.Add(location);
                this._context.SaveChanges();
                return Ok("Location added successfully");
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateLocation([FromBody] TblLocation location)
        {
            try
            {
                this._context.TblLocations.Update(location);
                this._context.SaveChanges();
                return Ok("Location updated successfully");
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteLocation(int id)
        {
            try
            {
                var location = this._context.TblLocations.Find(id);
                this._context.TblLocations.Remove(location);
                this._context.SaveChanges();
                return Ok("Location deleted successfully");
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
