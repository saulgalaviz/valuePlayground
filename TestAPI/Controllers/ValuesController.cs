using Microsoft.AspNetCore.Mvc;
using TestAPI.Data;
using TestAPI.Models;
using TestAPI.Models.Dto;
//Up until the 45 min mark for video: https://www.youtube.com/watch?v=_uZYOgzYheU&ab_channel=DotNetMastery
namespace TestAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ValuesDTO>> GetTests()
        {
            return Ok(ValueStore.valueList);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ValuesDTO> GetTest(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var values = ValueStore.valueList.FirstOrDefault(u => u.Id == id);
            if(values == null)
            {
                return NotFound();
            }

            return Ok(values);
        }
    }
}
