using AirLine_TestCases;
using AirLineAssignment.Entities;
using AirLineLibrary;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirLineAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirLineController : ControllerBase
    {
        private readonly AirDbContext _airDbContext;
        private readonly IMapper _mapper;
      
        public AirLineController(AirDbContext airDbContext , IMapper mapper)
        {
            _airDbContext = airDbContext;
            _mapper = mapper;
        }

      


        /// <summary>
        /// Creating AirLine List
        /// </summary>
        /// <param name="airLine"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(AirLine airLine)
        {
            if (airLine == null)
            {
                return BadRequest();
            }
            else
            {
                _airDbContext.AirLines.Add(airLine);
                _airDbContext.SaveChanges();
                return Ok("Created Successfully");
            }
        }


        /// <summary>
        /// Updating the AirLine list
        /// </summary>
        /// <param name="airLineId"></param>
        /// <param name="airLine"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateAirLine(int airLineId, AirLine airLine)
        {
            if (airLine == null)
            {
                return BadRequest("AirLine object can't be null");
            }
            if (_airDbContext == null)
            {
                return NotFound("Table doesn't exists");
            }
            var Update = _airDbContext.AirLines.Where(e => e.AirLineId == airLineId).FirstOrDefault();
            if (Update == null)
            {
                return NotFound("AirLine with this AirLineId doesn't exists");
            }
            _airDbContext.AirLines.Remove(Update);
            Update.AirLineName = airLine.AirLineName;
            Update.FromCity = airLine.FromCity;
            Update.ToCity = airLine.ToCity;
            Update.Fare = airLine.Fare;
            Update.AirLineImage = airLine.AirLineImage;

            _airDbContext.AirLines.Update(Update);
            _airDbContext.SaveChanges();

            return Ok(_airDbContext.AirLines);
        }


        /// <summary>
        /// Displaying AirLines List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<AirLine>> GetAirLines()
        {
            return Ok(_airDbContext.AirLines);
        }


        /// <summary>
        /// Deleting Airline List
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("id")]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            else
            {
                var delAirLine = _airDbContext.AirLines.Find(id);
                _airDbContext.AirLines.Remove(delAirLine);
                _airDbContext.SaveChanges();
                return Ok("Deleted Successfully");
            }

        }
        /// <summary>
        /// Searching the List using Strings
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpGet("{searchString}")]

        public async Task<IActionResult> Show(string searchString)
        {
            if (searchString == null)
            {
                return BadRequest("input can't be null");
            }
            if (_airDbContext.AirLines == null)
            {
                return NotFound("Table doesn't exists");
            }
            var airLines = _airDbContext.AirLines.Where(e => e.AirLineName.Contains(searchString) || e.FromCity.Contains(searchString) || e.ToCity.Contains(searchString)).ToList();
            if (airLines == null)
            {
                return NotFound("Record doesn't exists");
            }
            return Ok(airLines);
        }

        /// <summary>
        /// Displaying data Through Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("id")]
        public async Task<ActionResult> GetbyId(int id)
        {
            if (_airDbContext.AirLines == null)
            {
                return NoContent();
            }
            if (id == null)
            {
                return BadRequest();
            }
            var book = await _airDbContext.AirLines.FindAsync(id);
            return Ok(book);

        }
    }
}

