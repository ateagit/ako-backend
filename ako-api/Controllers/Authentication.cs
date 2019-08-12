using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ako_api.Models;
using Google.Apis.Auth;
using static Google.Apis.Auth.GoogleJsonWebSignature;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ako_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly AkoContext _context;
        private readonly IConfiguration _configuration;

        public Authentication(AkoContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpPost("Google")]
        public async Task<IActionResult> GoogleAuthenticate([FromBody] string idToken)
        {
            try
            {
                // Validate the id token from client, it would return a payload consisting of claims such as user information. 
                Payload claims = await ValidateAsync(idToken, new ValidationSettings());

                // The claim to store in the db is the subject claim which is unique!.
                string subjectClaim = claims.Subject;

                if(!UserExists(subjectClaim))
                {
                    // If the user doesent exist, create the new user and add it in the database
                    User user = new User
                    {
                        AuthProviderId = subjectClaim,
                        FirstName = claims.GivenName,
                        LastName = claims.FamilyName
                    };

                    await PostUser(user);
                }


                // Create new claims to give back the client in a jwt
                List<Claim> newClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, subjectClaim),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                // Signing credentials are required when constructing a jwt
                SigningCredentials signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AppSettings:JwtSecret"])),
                    SecurityAlgorithms.HmacSha256);

                // Construct a jwt
                JwtSecurityToken jwtToken = new JwtSecurityToken(null, null, newClaims, null, DateTime.Now.AddSeconds(3000), signingCredentials);

                // Seralize the jwt to send to client.
                string serializedToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                return Ok(serializedToken);
            }
            catch(InvalidJwtException e)
            {
                // Invalid Id Token given, return a bad request.
                return BadRequest(e);
            }
        }
        // GET: api/Authentication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Authentication/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Authentication/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserIdExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authentication
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Authentication/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.AuthProviderId == id);
        }

        private bool UserIdExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }

    }
}
