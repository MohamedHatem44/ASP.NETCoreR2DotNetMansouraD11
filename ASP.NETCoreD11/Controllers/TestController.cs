using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreD11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /*------------------------------------------------------------------*/
        [HttpGet]
        public string Get()
        {
            return "Hello From Test";
        }
        /*------------------------------------------------------------------*/
        //[HttpGet]
        //[Route("{id}")]
        [HttpGet("{id:int}")]
        public string GetById(int id)
        {
            return $"Hello From Test {id}";
        }
        /*------------------------------------------------------------------*/
        [HttpGet("{name:alpha}")]
        public string GetByName(string name)
        {
            return $"Hello From Test {name}";
        }
        /*------------------------------------------------------------------*/
    }
}
