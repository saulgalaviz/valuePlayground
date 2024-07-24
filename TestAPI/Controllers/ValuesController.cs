using Microsoft.AspNetCore.Mvc;
using TestAPI.Data;
using TestAPI.Models;
using TestAPI.Models.Dto;
//Up until the 1:03 min mark for video: https://www.youtube.com/watch?v=_uZYOgzYheU&ab_channel=DotNetMastery
namespace TestAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ValuesDTO>> GetTests()
        {
            return Ok(ValueStore.valueList);
        }

        [HttpGet("{id:int}", Name = "GetValue")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //Integer way of writing it, but above is more clear when reading. Also, Type is the return type of the response but we don't need if we define it with <ValuesDTO>
        //[ProducesResponseType(200, Type = typeof(ValuesDTO))]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ValuesDTO> CreateVilla([FromBody]ValuesDTO valuesDTO) //[FromBody] added for [HttpPost] commands
        {
            if(valuesDTO == null)
            {
                return BadRequest(valuesDTO);
            }
            if(valuesDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //Get maximum ID and add 1
            valuesDTO.Id = ValueStore.valueList.OrderByDescending(u => u.Id).FirstOrDefault().Id+1;
            ValueStore.valueList.Add(valuesDTO);

            //return (Ok(valuesDTO));
            return CreatedAtRoute("GetValue", new { id = valuesDTO.Id }, valuesDTO);

        }
    }
}
