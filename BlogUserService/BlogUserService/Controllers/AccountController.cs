using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogUserService.Infrastructure;
using BlogUserService.Models;
using BlogUserService.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogUserService.Controllers
{
    [Route("api/v1/users")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private BlogUserContext dbContext;
        private IConfiguration configuration;

        public AccountController(BlogUserContext context, IConfiguration configuration)
        {
            this.dbContext = context;
            this.configuration = configuration;
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("register",Name ="AddUser")]
        public async Task<ActionResult<BlogUser>> RegisterAsync([FromBody]BlogUser user)
        {
            if (ModelState.IsValid)
            {
                var res= await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                return Created("",new { FirstName = res.Entity.FirstName, LastName = res.Entity.LastName, Email = res.Entity.Email });
            }
            return BadRequest(ModelState);
        }

        
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]
        [HttpPost("token")]
        public IActionResult CreateToken([FromBody]LoginUser user)
        {
            IActionResult response = Unauthorized();
            var blogUser = dbContext.Users.SingleOrDefault(s => s.Email == user.Email && s.Password == user.Password);
            if (blogUser != null) //if valid , issue the token
            {
                var tokenManager = new TokenManager(this.configuration);
                var tokenString = tokenManager.BuildToken(blogUser); //generate the token
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        
    }
}