using DataLibary;
using DataLibary.Data;
using DataLibary.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserData userdata;

        public UserController(IUserData userdata)
        {
            this.userdata = userdata;
        }

        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserModel>> Get()
        {
            try {

                return Ok(await userdata.GetUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //GET api/<UserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<UserModel>> Get(int id)
        {
            try
            {
                
                return Ok(await userdata.GetUser(id));
            }
            catch 
            {
                return NotFound("Was Unable to find a user with chosen id");
            }
        }

        // POST api/<UserController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserModel>> Post([FromBody] UserModel value)
        {
            if(string.IsNullOrEmpty(value.FirstName) || string.IsNullOrEmpty(value.LastName))
            {
                return BadRequest("firstName and lastName can´t be empty");
            }
            try 
            {
                await userdata.InsertUser(value);

                var users = await userdata.GetUsers();

                return Created("api/",users.Last());
            }
            catch
            {
                return BadRequest("No user was found");
            }

        }

        // PUT api/<UserController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserModel>> Put([FromBody] UserModel value)
        {
            if(value == null)
            {
                return BadRequest("Need to send a user");
            };
            if(value.id <= 0)
            {
                return BadRequest("Invalid user id");
            }
            if (string.IsNullOrEmpty(value.FirstName) || string.IsNullOrEmpty(value.LastName))
            {
                return BadRequest("firstName or lastName was a empty string");
            };

            try
            {
                await userdata.UpdateUser(value);

                return Ok("User was successfully Updated");
            }
            catch
            {
                return Problem("Unable to update User");
            }
        }

        // DELETE api/<UserController>/5
        /// <summary>
        /// Delete a user by sending the id of the user that you want to delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user id");
            }

            try
            {
                await userdata.DeleteUser(id);

                return Ok("User was Deleted");
            }
            catch
            {
                return Problem("Unable to Delete User");
            }
        }
    }
}
