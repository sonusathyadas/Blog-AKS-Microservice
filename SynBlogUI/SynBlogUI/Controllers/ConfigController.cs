using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SynBlogUI.Models;

namespace SynBlogUI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private IConfiguration configuration;

        public ConfigController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<Configuration> GetConfig()
        {
           var config=new Configuration
            {
                UserServiceUrl=configuration.GetValue<string>("UserServiceUrl"),
                BlogServiceUrl=configuration.GetValue<string>("BlogServiceUrl")
            };
            return config;
        }
    }
}