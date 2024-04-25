using backend.Contexts;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly banhangContext _context;

        public HangHoaController(banhangContext context)
        {
            _context = context;
        }

        // GET: api/HangHoa
        [HttpGet]
        public IActionResult GetHangHoas([FromQuery] int skip,
            [FromQuery] int take = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? sortBy = nameof(TblHangHoa.HhId),
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

            //// Filter the data if a search term is provided
            //IQueryable<Location> data = _context.Locations.AsQueryable();

            //// Sort the data
            //if (sortBy == nameof(Location.LocationId).ToLower())
            //{
            //    data = sortType == "asc" ? data.OrderBy(l => l.LocationId) : data.OrderByDescending(l => l.LocationId);
            //}
            //else if (sortBy == nameof(Location.Code).ToLower())
            //{
            //    data = sortType == "asc" ? data.OrderBy(l => l.Code) : data.OrderByDescending(l => l.Code);
            //}
            //else if (sortBy == nameof(Location.Name).ToLower())
            //{
            //    data = sortType == "asc" ? data.OrderBy(l => l.Name) : data.OrderByDescending(l => l.Name);
            //}
            //else if (sortBy == nameof(Location.Description).ToLower())
            //{
            //    data = sortType == "asc" ? data.OrderBy(l => l.Description) : data.OrderByDescending(l => l.Description);
            //}

            //if (!string.IsNullOrEmpty(search))
            //{
            //    data = data.Where(l => l.Code.Contains(search) || l.Name.Contains(search) || l.Description.Contains(search));
            //}

            //int total = data.Count();

            //// Get the locations for the current page
            //var locations = data.Skip(skip).Take(take).ToList();

            //// Return the data in the format expected by DataTables
            //return Ok(new
            //{
            //    draw = Request.Query["draw"],
            //    recordsTotal = total,
            //    recordsFiltered = data.Count(), // Update the number of filtered records
            //    data = locations
            //});

            var query = _context.TblHangHoas.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                // find nhhid nhhten
                query = query.Where(x => x.HhId.ToString().Contains(search) || x.HhTen.Contains(search));
            }

            int total = query.Count();

            var hangHoas = query.Skip(skip).Take(take).ToList();

            return Ok(new
            {
                draw = Request.Query["draw"],
                recordsTotal = total,
                recordsFiltered = query.Count(), // Update the number of filtered records
                data = hangHoas
            });




        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHangHoaById(int id)
        {
            var hangHoa = _context.TblHangHoas.Find(id);

            if (hangHoa == null)
            {
                return NotFound();
            }

            return Ok(hangHoa);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateHangHoa(TblHangHoa hangHoa)
        {
            _context.TblHangHoas.Add(hangHoa);
            _context.SaveChanges();

            return Ok(hangHoa);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateHangHoa(TblHangHoa hangHoa)
        {
            var hh = _context.TblHangHoas.Find(hangHoa.HhId);
            if (hh == null)
            {
                return NotFound();
            }

            // can't change nhhId
            hh.HhTen = hangHoa.HhTen;
            hh.HhMa = hangHoa.HhMa;
            hh.HhGianhap = hangHoa.HhGianhap;
            hh.HhGiaban = hangHoa.HhGiaban;

            _context.TblHangHoas.Update(hh);
            _context.SaveChanges();

            return Ok(hh);
 
        }

        [HttpDelete]
        [Route("deleteOne")]
        public IActionResult DeleteHangHoa(TblHangHoa hangHoa)
        {
            var hh = _context.TblHangHoas.Find(hangHoa.HhId);
            if (hh == null)
            {
                return NotFound();
            }

            _context.TblHangHoas.Remove(hh);
            _context.SaveChanges();

            return Ok();

        }
    }
}
