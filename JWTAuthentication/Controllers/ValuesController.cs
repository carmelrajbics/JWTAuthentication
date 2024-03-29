﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JWTAuthentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        // JWT Authentication token is used to call this API
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "This is method is called after verifying JWT Token" };
        }
    }
}
