using System;
using Friday.Data.IServices;
using Friday.DTOs.Items;
using Friday.Models;
using Friday.Models.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friday.Controllers
{
    /// <summary>
    /// Controller for all methods involving Item instances.
    /// </summary>
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly IItemService service;
        private readonly IUserService users;
        /// <summary>
        /// Default ctor. Gets auto inject.
        /// </summary>
        /// <param name="service">Service for Items</param>
        /// <param name="userSer">Service for Users</param>
        public ItemController(IItemService service, IUserService userSer)
        {
            this.service = service;
            users = userSer;
        }
        // GET: api/<controller>
        /// <summary>
        /// Returns a list containing all the Items. Check schemas for their format. A default set of Items has been provided. These can be modified or deleted if needed.
        /// </summary>
        /// <returns>List of Items</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<IList<Item>>> Get()
        {
            return Ok(await service.GetAll());
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Changes the Amount of an Item, or how many items of a certain Item are still in stock. Use a negative number to subtract. 
        /// </summary>
        /// <param name="request">Request object for the change</param>

        /// <returns>True if it was successful. You can't get a negative amount, at best the count can be reduced to 0</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<bool>> Put(ItemAmountChangeRequest request)
        {
            try
            {
                return Ok(await service.ChangeCount(await users.GetByUsername(User.Identity.Name), request));
            }
            catch (Exception)
            {
                return NotFound($"User \'{User.Identity.Name}\' or item with ID {request.Id} could not be found!");
            }
        }

        /// <summary>
        /// Adds a new Item to the database.
        /// </summary>
        /// <param name="dto">ItemDTO object containing the data to make the Item</param>
        /// <returns>HTTP Code depending on result.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AuthorizeAdminOrCatering]
        public async Task<ActionResult> Post(ItemDTO dto)
        {
            return Ok(await service.AddItem(dto.ToItem(), dto.Details.ToItemDetails()));
        }

        /// <summary>
        /// Deletes an Item. Removes the Item itself, its ItemDetails and all Orders where this Item was ordered.
        /// Use this with caution. This action cannot be undone and should only be used during setup.
        /// </summary>
        /// <param name="id">ID of the Item</param>
        /// <returns>ActionResult depending on the outcome</returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [AuthorizeAdminOrCatering]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                return Ok(await service.DeleteItem(id));
            }
            catch (Exception)
            {
                return NotFound($"Item with id {id} was not found!");
            }
        }
    }
}
