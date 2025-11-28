using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase 
    {
        private readonly IServiceManager serviceManager;
         
        public UsersController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
    }
}
