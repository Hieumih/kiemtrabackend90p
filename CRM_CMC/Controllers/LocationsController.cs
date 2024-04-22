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
        private readonly BACKEND_CRMContext _context;

        public LocationsController(BACKEND_CRMContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetLocations([FromQuery] int skip,
            [FromQuery] int take = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? sortBy = nameof(Location.LocationId),
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
            IQueryable<Location> data = _context.Locations.AsQueryable();

            // Sort the data
            if (sortBy == nameof(Location.LocationId).ToLower())
            {
                data = sortType == "asc" ? data.OrderBy(l => l.LocationId) : data.OrderByDescending(l => l.LocationId);
            }
            else if (sortBy == nameof(Location.Code).ToLower())
            {
                data = sortType == "asc" ? data.OrderBy(l => l.Code) : data.OrderByDescending(l => l.Code);
            }
            else if (sortBy == nameof(Location.Name).ToLower())
            {
                data = sortType == "asc" ? data.OrderBy(l => l.Name) : data.OrderByDescending(l => l.Name);
            }
            else if (sortBy == nameof(Location.Description).ToLower())
            {
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
        public IActionResult AddLocaton([FromBody] Location location)
        {
            try
            {
                this._context.Locations.Add(location);
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
        public IActionResult UpdateLocation([FromBody] Location location)
        {
            try
            {
                this._context.Locations.Update(location);
                this._context.SaveChanges();
                return Ok("Location updated successfully");
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteOne")]
        public IActionResult DeleteLocation([FromBody] Location location)
        {
            try
            {
                var entity = this._context.Locations.Find(location.LocationId);
                if (entity != null)
                {
                    _context.Locations.Remove(entity);
                    this._context.SaveChanges();
                }
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
                var location = this._context.Locations.Find(id);
                return Ok(location);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("import")]
        public IActionResult UploadLocations([FromBody] List<Location> locations)
        {
            try
            {
                locations.ForEach(l => l.LocationId = 0);
                this._context.Locations.AddRange(locations);
                this._context.SaveChanges();
                return Ok("Locations uploaded successfully");
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteRange")]
        public IActionResult DeleteMultipleLocations([FromBody] List<Location> locations)
        {
            foreach (var location in locations)
            {
                var entity = this._context.Locations.Find(location.LocationId);
                if (entity != null)
                {
                    _context.Locations.Remove(entity);
                }
            }
            this._context.SaveChanges();
            return Ok("Locations deleted successfully");
        }


    }
}
