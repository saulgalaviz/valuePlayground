﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Logging;
using TestAPI.Models;
using TestAPI.Models.Dto;
//Up until the 2:09 min mark for video: https://www.youtube.com/watch?v=_uZYOgzYheU&ab_channel=DotNetMastery
namespace TestAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Values")]
    [ApiController] //Identifies ValuesDTO as the controller would not pass requirements such as [Required] or [MaxLength(30)], would need to do if(!ModelState.IsValid) instead, etc.
    public class ValuesController : ControllerBase
    {
        //Dependency injection below 3 lines of code for logging
        //private readonly ILogger<ValuesController> _logger;

        /*public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }*/

        //custom logging built using own interface and class
        private readonly ILogging _logger;
        /*public ValuesController(ILogging logger)
        {
            _logger = logger;
        }*/

        //Extract database service we registered within Program.cs and here we use dependency injection to utilize database
        private readonly ApplicationDbContext _db;

        public ValuesController(ApplicationDbContext db, ILogging logger)
        {
            _db = db;
            _logger = logger;
        }

        //Gets all values from database
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ValuesDTO>> GetValues()
        {
            //used with serilog
            //_logger.LogInformation("Getting all values.");
            _logger.Log("Getting all values.", "");
            //return Ok(ValueStore.valueList);  this used with runtime storage class
            return Ok(_db.Values.ToList());
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
        public ActionResult<ValuesDTO> GetValue(int id)
        {
            if (id == 0)
            {
                _logger.Log("Get Value Error with ID " +  id, "error");
                return BadRequest();
            }
            //var values = ValueStore.valueList.FirstOrDefault(u => u.Id == id);
            var values = _db.Values.FirstOrDefault(u => u.Id == id);
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
        public ActionResult<ValuesDTO> CreateValue([FromBody] ValuesDTO valuesDTO) //[FromBody] added for [HttpPost] commands
        //ActionResult you can define the return type
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            if (_db.Values.FirstOrDefault(u => u.Name.ToLower() == valuesDTO.Name.ToLower()) != null) //ValueStore.valueList.FirstOrDefault(u => u.Name.ToLower() == valuesDTO.Name.ToLower()) != null
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
            //valuesDTO.Id = ValueStore.valueList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            //ID is an identity column in Entity framework core, so above line not needed

            //ValueStore.valueList.Add(valuesDTO); if we try to switch this to database, we need to manually convert since this cannot convert database value to valueDTO
            //_db.Values.Add(valuesDTO);
            //instead we use this
            Value model = new()
            {
                Amenity = valuesDTO.Amenity,
                Details = valuesDTO.Details,
                Id = valuesDTO.Id,
                ImageUrl = valuesDTO.ImageUrl,
                Name = valuesDTO.Name,
                Occupancy = valuesDTO.Occupancy,
                Rate = valuesDTO.Rate,
                Sqft = valuesDTO.Sqft,
            };
            _db.Values.Add(model);
            _db.SaveChanges(); //saves changes into database

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
            //var value = ValueStore.valueList.FirstOrDefault(u => u.Id == id);
            var value = _db.Values.FirstOrDefault(u => u.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            else
            {
                //ValueStore.valueList.Remove(value);
                _db.Values.Remove(value);
                _db.SaveChanges();
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
            //Entity framework core just needs ID and can update all values at once, just need to convert ValueDTO to Value object 
            //var value = ValueStore.valueList.FirstOrDefault(u => u.Id == id);
            //value.Name = valueDTO.Name;
            //value.Sqft = valueDTO.Sqft;
            //value.Occupancy = valueDTO.Occupancy;
            Value model = new()
            {
                Amenity = valueDTO.Amenity,
                Details = valueDTO.Details,
                Id = valueDTO.Id,
                ImageUrl = valueDTO.ImageUrl,
                Name = valueDTO.Name,
                Occupancy = valueDTO.Occupancy,
                Rate = valueDTO.Rate,
                Sqft = valueDTO.Sqft,
            };
            _db.Values.Update(model);
            _db.SaveChanges();

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
            //var value = ValueStore.valueList.FirstOrDefault(u => u.Id == id);
            //when below line is executed, entity framework core tracks the value which isn't optimal when working with multiple values - since we created 2 values they would both be referenced when updating database with one of the values - we prevent that by including .asNoTracking() 
            var value = _db.Values.AsNoTracking().FirstOrDefault(u => u.Id == id); //will need to convert this value object into ValueDTO

            ValuesDTO valueDTO = new()
            {
                Amenity = value.Amenity,
                Details = value.Details,
                Id = value.Id,
                ImageUrl = value.ImageUrl,
                Name = value.Name,
                Occupancy = value.Occupancy,
                Rate = value.Rate,
                Sqft = value.Sqft,
            };

            if (value == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(valueDTO, ModelState); //for database, record here is updated for DTO but now it needs to be converted to type Value to be updated in database as shown below
            Value model = new Value()
            {
                Amenity = valueDTO.Amenity,
                Details = valueDTO.Details,
                Id = valueDTO.Id,
                ImageUrl = valueDTO.ImageUrl,
                Name = valueDTO.Name,
                Occupancy = valueDTO.Occupancy,
                Rate = valueDTO.Rate,
                Sqft = valueDTO.Sqft,
            };

            _db.Values.Update(model); //this would update entire record for the only single patch field, but if we really needed to, we would go into stored prog - create a stored prog to update one record and if it had many columns figure it out
            _db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent() ;

        }

    }
}
