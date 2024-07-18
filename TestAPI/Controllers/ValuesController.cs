using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [Route("api/Values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Test> GetTests()
        {
            return new List<Test>
            {
                new Test { Id = 1, Name = "Pool" },
                new Test { Id = 2, Name = "Jacuzzi" }
            };
        }
    }
}
