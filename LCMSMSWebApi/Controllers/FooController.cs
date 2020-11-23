using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LCMSMSWebApi.Controllers
{

    public class FooModel
    {
        public string Message { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class FooController : ControllerBase
    {
        public IActionResult Get()
        {
            var dto = new FooModel { Message = "This foo controller is working!" };
            return Ok(dto);
        }
    }
}
