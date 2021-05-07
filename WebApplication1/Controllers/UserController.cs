using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Utils;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDemoService _context;

        public UserController(IDemoService context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var users = await _context.GetUsers();
            return users.ToList();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.GetUser(id);
            if (user == null)            
                return NotFound("User does not exist");            

            return user;
        }

        // GET: api/User/Search/Ali
        [HttpGet("Search/{name}")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUser(string name)
        {
            List<User> lsUser = null;
            var users = await _context.SearchUser(name);
            lsUser = users.ToList();
            //try
            //{
            //    var users = await _context.SearchUser(name);
            //    lsUser = users.ToList();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("An error occured whilst processing the request");
            //}

            if (lsUser == null || lsUser.Count < 1)            
                return NotFound("User does not exist");            

            return lsUser;
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
                return BadRequest("User ID and User object does not match");

            bool retval;
            try
            {
                retval = await _context.PutUser(id, user);
            }
            catch (Exception ex)
            {
                return new ObjectResult("An error occured whilst processing the request") { StatusCode = 500 };
            }

            return NoContent();
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (user == null)
                return BadRequest("User cannot be null");

            bool retval;
            try
            {
                retval = await _context.PostUser(user);
            }
            catch (Exception ex)
            {
                return new ObjectResult("An error occured whilst processing the request") { StatusCode = 500 };
            }

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool retval;
            try
            {
                retval = await _context.DeleteUser(id);
            }
            catch (Exception ex)
            {
                return new ObjectResult("An error occured whilst processing the request") { StatusCode = 500 };
            }

            return NoContent();
        }
    }
}
