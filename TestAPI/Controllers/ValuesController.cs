using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Data;
using TestAPI.Models;
using TestAPI.Models.Dto;
//Up until the 1:23:50 min mark for video: https://www.youtube.com/watch?v=_uZYOgzYheU&ab_channel=DotNetMastery
namespace TestAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Values")]
    [ApiController] //Identifies ValuesDTO as the controller would not pass requirements such as [Required] or [MaxLength(30)], would need to do if(!ModelState.IsValid) instead, etc.
    public class ValuesController : ControllerBase
    {
        //Dependency injection below 3 lines of code for logging
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        //Gets all values from database
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ValuesDTO>> GetTests()
        {
            _logger.LogInformation("Getting all values.");
            return Ok(ValueStore.valueList);
        }

        //Gets one value based on integer value
        //Require ID to be int
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
                _logger.LogError("Get Value Error with ID " +  id);
                return BadRequest();
            }
            var values = ValueStore.valueList.FirstOrDefault(u => u.Id == id);
            if (values == null)
            {
                return NotFound();
            }

            return Ok(values);
        }

        //Posts a new value into database
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ValuesDTO> CreateVilla([FromBody] ValuesDTO valuesDTO) //[FromBody] added for [HttpPost] commands
        //ActionResult you can define the return type
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            if (ValueStore.valueList.FirstOrDefault(u => u.Name.ToLower() == valuesDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Value already Exists!"); //first value is fine to be blank, must be a custom sort of name though
                return BadRequest(ModelState);
            }

            if (valuesDTO == null)
            {
                return BadRequest(valuesDTO);
            }
            if (valuesDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //Get maximum ID and add 1
            valuesDTO.Id = ValueStore.valueList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            ValueStore.valueList.Add(valuesDTO);

            //return (Ok(valuesDTO));
            return CreatedAtRoute("GetValue", new { id = valuesDTO.Id }, valuesDTO);

        }

        //Deletes one value from database based on ID
        [HttpDelete("{id:int}", Name = "DeleteValue")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteValue(int id)
        //IActionResult you don't define the return type. Good for when you don't return content 
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var value = ValueStore.valueList.FirstOrDefault(u => u.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            else
            {
                ValueStore.valueList.Remove(value);
                return NoContent(); //This one returns no content
            }
        }

        //Updates one value for all fields
        [HttpPut("{id:int}", Name = "UpdateValue")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateValue(int id, [FromBody]ValuesDTO valueDTO)
        {
            if (valueDTO == null || id != valueDTO.Id)
            {
                return BadRequest();
            }
            var value = ValueStore.valueList.FirstOrDefault(u => u.Id == id);
            value.Name = valueDTO.Name;
            value.Sqft = valueDTO.Sqft;
            value.Occupancy = valueDTO.Occupancy;

            return NoContent();
        }

        //Updates one value for one field, more details on how to use on jsonpatch.com
        [HttpPatch("{id:int}", Name = "UpdatePartialValue")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialValue(int id, JsonPatchDocument<ValuesDTO> patchDTO) //JsonPatchDocument used for Patch
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var value = ValueStore.valueList.FirstOrDefault(u => u.Id == id);

            if(value == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(value, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent() ;

        }

    }
}
