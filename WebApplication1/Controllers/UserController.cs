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
            IEnumerable<User> users;
            try
            {
                users = await _context.GetUsers();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException("An error occured whilst processing the request", ex);
            }
            
            return users.ToList();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (id < 0)
                return BadRequest("User ID must be higher than 0");

            User user;
            try
            {
                user = await _context.GetUser(id);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException("An error occured whilst processing the request", ex);
            }
            
            if (user == null)            
                return NotFound("User does not exist");            

            return user;
        }

        // GET: api/User/Search/Ali
        [HttpGet("Search/{name}")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUser(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return BadRequest("User name cannot be null");

            List<User> lsUser = null;
            try
            {
                IEnumerable<User> users = await _context.SearchUser(name);
                lsUser = users.ToList();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException("An error occured whilst processing the request", ex);
            }

            if (lsUser == null || lsUser.Count < 1)            
                return NotFound("User does not exist");            

            return lsUser;
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
                return BadRequest("User ID and User object does not match");

            bool retval;
            try
            {
                retval = await _context.PutUser(id, user);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException("An error occured whilst processing the request", ex);
            }

            if(!retval)
               throw new HttpResponseException("An error occured whilst adding the user", new Exception("PutUser error"));

            return Ok("User updated");
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null)
                return BadRequest("User cannot be null");

            bool retval;
            try
            {
                retval = await _context.PostUser(user);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException("An error occured whilst processing the request", ex);
            }

            if (!retval)
                throw new HttpResponseException("An error occured whilst updating the user", new Exception("PostUser error"));

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
                throw new HttpResponseException("An error occured whilst processing the request", ex);
            }

            if (!retval)
                throw new HttpResponseException("An error occured whilst deleting the user", new Exception("DeleteUser error"));

            return Ok("User deleted");
        }
    }
}
