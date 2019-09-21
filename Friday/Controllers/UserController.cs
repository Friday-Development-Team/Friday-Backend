using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friday.Controllers {
    [Route("api/[controller]")]
    public class UserController : Controller {

        private readonly IUserService service;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;

        public UserController(IUserService service, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration config) {
            this.service = service;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.config = config;
        }


        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<String>> CreateToken(LoginDTO model) {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null) {
                var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded) {
                    string token = GetToken(user); return Created("", token); //returns only the token

                }
            }
            return BadRequest();
        }


        ////// PUT api/<controller>/5
        ////[HttpPut("{id}")]
        ////public void Put(int id, [FromBody]string value) {
        ////}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id) {
        //}
        /// <summary>
        /// Adds Balance to the specified user's account
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <param name="amount">Amount to be added. Negative to subtract</param>
        [HttpPut("{id}")]
        public void UpdateBalance(int id, [FromBody]double amount) {
            service.ChangeBalance(id, amount);
        }

        private string GetToken(IdentityUser user) {
            // Create the token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                null, null,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
