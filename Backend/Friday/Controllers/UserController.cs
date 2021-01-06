using Friday.Data.IServices;
using Friday.DTOs;
using Friday.Models;
using Friday.Models.Out;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friday.Controllers
{
    /// <summary>
    /// Controller for Users. Shows methods like login, register or gathering of information.
    /// </summary>
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]

    public class UserController : ControllerBase
    {

        private readonly IUserService service;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;

        /// <summary>
        /// Default Ctor. Gets auto injected.
        /// </summary>
        /// <param name="service">Service for Users</param>
        /// <param name="signInManager">Manager to handle login</param>
        /// <param name="userManager">Handles Identity Users</param>
        /// <param name="config">Current configuration file</param>
        public UserController(IUserService service, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration config)
        {
            this.service = service;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.config = config;
        }


        /// <summary>
        /// Returns the currently logged in user based on the provided token
        /// </summary>
        /// <returns>Information of the User</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ShopUserDTO>> Get()
        {
            try
            {
                return Ok(await service.GetUser(User.Identity.Name));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Returns a List of all the registered Users. Only exposes their username and balance.
        /// </summary>
        /// <returns>List of username and balance for each user</returns>
        [HttpGet("all")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<IList<ShopUserDTO>>> GetAll()
        {
            return Ok(await service.GetAll());
        }

        /// <summary>
        /// Gets the roles of the currently logged in user
        /// </summary>
        /// <returns>List of roles</returns>
        [HttpGet("roles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IList<string>> GetRolesAsync()
        {
            return await userManager.GetRolesAsync(await userManager.FindByNameAsync(User.Identity.Name));
        }
        /// <summary>
        /// Checks if the provided username already exists
        /// </summary>
        /// <param name="name">Name to be checked</param>
        /// <returns>True if doesn't exist</returns>
        [AllowAnonymous]
        [HttpGet("checkusername")]
        public async Task<ActionResult<bool>> CheckAvailableUserName(string name)
        {
            return await userManager.FindByNameAsync(name) == null;
        }

        /// <summary>
        /// Login method. Checks the provided credentials and returns a JWT token if valid. Token will contain basic information and roles.
        /// </summary>
        /// <param name="model">Model containing login information</param>
        /// <returns>JWT token</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> CreateToken([FromBody] LoginDTO model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && (await signInManager.CheckPasswordSignInAsync(user, model.Password, false)).Succeeded)
                return Created("", await GetToken(user)); //returns only the token
            return BadRequest();
        }

        /// <summary>
        /// Registers a new User. Creates an account in the system and returns a token as if the User was logging in.
        /// </summary>
        /// <param name="model">Object containing information needed to register</param>
        /// <returns>JWT Token</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDTO model)
        {
            IdentityUser user = new IdentityUser { UserName = model.Username };
            ShopUser customer = new ShopUser { Name = model.Username, Balance = 200D };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded && await service.AddUser(customer))
                return Created("", await GetToken(user));
            return BadRequest();
        }

        /// <summary>
        /// Adds Balance to the specified user's account
        /// </summary>
        /// <param name="dto">Object contain the data for a balance update</param>
        /// <param name="log">Whether or not this event should be logged</param>
        [HttpPut("updatebalance")]
        [Authorize(Roles = Role.Admin)]
        public ActionResult UpdateBalance([FromBody] BalanceUpdateDTO dto, bool log = true)
        {
            try
            {
                return Ok(service.ChangeBalance(dto.Name, dto.Amount, log));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Creates a JWT token based on the provided IdentityUser. Also appends role information
        /// </summary>
        /// <param name="user">User to create a token for</param>
        /// <returns>Token</returns>
        private async Task<string> GetToken(IdentityUser user)
        {
            // Create the token
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName) };
            claims.AddRange((await userManager.GetRolesAsync(user)).Select(s => new Claim(ClaimTypes.Role, s)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                null, null,
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
