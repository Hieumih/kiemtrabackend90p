using backend.Contexts;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhomHangHoaController : ControllerBase
    {
        // context
        private readonly banhangContext _context;

        public NhomHangHoaController(banhangContext context)
        {
            _context = context;
        }

        // GET: api/NhomHangHoa
        [HttpGet]
        public IActionResult GetNhomHangHoas([FromQuery] int skip,
                       [FromQuery] int take = 10,
                                  [FromQuery] string? search = null,
                                             [FromQuery] string? sortBy = nameof(TblNhomhanghoa.NhhId),
                                                        [FromQuery] string? sortType = "asc")
        {
            var query = _context.TblNhomhanghoas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.NhhTen.Contains(search));
            }

            int total = query.Count();

            var NhomHangHoas = query.Skip(skip).Take(take).ToList();

            return Ok(new
            {
                draw = Request.Query["draw"],
                recordsTotal = total,
                recordsFiltered = query.Count(), // Update the number of filtered records
                data = NhomHangHoas
            });

        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetNhomHangHoaById(int id)
        {
            var nhomHangHoa = _context.TblNhomhanghoas.Find(id);

            if (nhomHangHoa == null)
            {
                return NotFound();
            }

            return Ok(nhomHangHoa);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateNhomHangHoa(TblNhomhanghoa nhomHangHoa)
        {
            _context.TblNhomhanghoas.Add(nhomHangHoa);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateNhomHangHoa(TblNhomhanghoa nhomHangHoa)
        {
            var nhh = _context.TblNhomhanghoas.Find(nhomHangHoa.NhhId);
            if (nhh == null)
            {
                return NotFound();
            }

            nhh.NhhMa = nhomHangHoa.NhhMa;
            nhh.NhhTen = nhomHangHoa.NhhTen;
            _context.TblNhomhanghoas.Update(nhh);
            _context.SaveChanges();

            return Ok(nhomHangHoa);
        }


        [HttpDelete]
        [Route("deleteOne")]
        public IActionResult DeleteNhomHangHoa(TblNhomhanghoa item)
        {
            var result = _context.TblNhomhanghoas.Find(item.NhhId);
            if (result == null)
            {
                return NotFound();
            }

            _context.TblNhomhanghoas.Remove(result);
            _context.SaveChanges();

            return Ok();
        }


    }
}
