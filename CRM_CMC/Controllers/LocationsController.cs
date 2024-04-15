using CRM_CMC.Contexts;
using CRM_CMC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using System.IO;
using CsvHelper;
using System.Globalization;

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
        public IActionResult GetLocations([FromQuery] int skip, 
            [FromQuery] int take = 10, 
            [FromQuery] string? search = null, 
            [FromQuery] string? sortBy = nameof(TblLocation.LocationId),
            [FromQuery] string? sortType = "asc")
        {
            // Get the total number of locations
            if (skip < 0)
            {
                return BadRequest("Skip must be greater than or equal to 0");
            }
            if (take <= 0)
            {
                return BadRequest("Take must be greater than or equal to 0");
            }
            if (take > 200)
            {
                return BadRequest("Take must be less than or equal to 200");
            }

            // Filter the data if a search term is provided
            IQueryable<TblLocation> data = _context.TblLocations.AsQueryable();

            // Sort the data
            if (sortBy == nameof(TblLocation.LocationId).ToLower()) {
                data = sortType == "asc" ? data.OrderBy(l => l.LocationId) : data.OrderByDescending(l => l.LocationId);
            } else if (sortBy == nameof(TblLocation.Code).ToLower()) {
                data = sortType == "asc" ? data.OrderBy(l => l.Code) : data.OrderByDescending(l => l.Code);
            } else if (sortBy == nameof(TblLocation.Name).ToLower()) {
                data = sortType == "asc" ? data.OrderBy(l => l.Name) : data.OrderByDescending(l => l.Name);
            } else if (sortBy == nameof(TblLocation.Description).ToLower()) {
                data = sortType == "asc" ? data.OrderBy(l => l.Description) : data.OrderByDescending(l => l.Description);
            }

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

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult GetLocation(int id)
        {
            try
            {
                var location = this._context.TblLocations.Find(id);
                return Ok(location);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult UploadLocations([FromBody]List<TblLocation> locations)
        {
            try
            {
                locations.ForEach(l => l.LocationId = 0);
                this._context.TblLocations.AddRange(locations);
                this._context.SaveChanges();
                return Ok("Locations uploaded successfully");
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        } 

        [HttpDelete]
        [Route("deleteMultiple")]
        public IActionResult DeleteMultipleLocations([FromBody] List<TblLocation> locations)
        {
            foreach (var location in locations)
            {
                var entity = this._context.TblLocations.Find(location.LocationId);
                if (entity != null)
                {
                    this._context.TblLocations.Remove(entity);
                }
            }
            this._context.SaveChanges();
            return Ok("Locations deleted successfully");
        }

        
    }
}
