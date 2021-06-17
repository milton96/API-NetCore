﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_JWT_NETCORE.Controllers
{
    [ApiController]
    [Route("api")]    
    [AllowAnonymous]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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

        [HttpGet]
        [Route("conexion")]
        public async Task<IActionResult> Conexion()
        {
            string ok = null;
            using(SqlConnection con = Models.Conexion.Conectar())
            {
                ok = "Conexión exitosa";
            }

            if (ok == null)
                return BadRequest();
            return Ok("Conexión exitosa");
        }
    }
}