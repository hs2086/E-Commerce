using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager service;

        public AuthController(IServiceManager service)
        {
            this.service = service;
        }
    }
}
