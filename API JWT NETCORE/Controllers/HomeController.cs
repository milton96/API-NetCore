using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_JWT_NETCORE.Controllers
{
    [Route("api")]
    [ApiController]
    [AllowAnonymous]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("ping")]
        public async Task<IActionResult> Ping()
        {
            return Ok("API Up");
        }

        [HttpGet]
        [Route("version")]
        public async Task<IActionResult> Version()
        {
            return Ok("Version 1");
        }
    }
}
