using Microsoft.AspNetCore.Mvc;
using TestAPI.Data;
using TestAPI.Models;
using TestAPI.Models.Dto;
//Up until the 35 min mark for video: https://www.youtube.com/watch?v=_uZYOgzYheU&ab_channel=DotNetMastery
namespace TestAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ValuesDTO> GetTests()
        {
            return ValueStore.valueList;
        }

        [HttpGet("{id:int}")]
        public ValuesDTO GetTest(int id)
        {
            return ValueStore.valueList.FirstOrDefault(u=>u.Id==id);
        }
    }
}
