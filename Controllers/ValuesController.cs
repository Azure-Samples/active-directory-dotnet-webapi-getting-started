using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TokenValidator.Controllers
{
    [Route("api/[controller]")]
    public class ClaimsController : Controller
    {
        // GET api/values
        [Authorize]
        [HttpGet]
        public object Get()
        {
            return User.Claims.Select(x =>
            new 
            {
                Type = x.Type,
                Value = x.Value
            }
            );
        }

    }
}
