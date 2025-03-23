using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Dtos;


using Server.Models.Entities;
using PlotPocket.Server.Models.Entities;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager
        ){
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] EmailLoginDetails details) {
            // create IdentityUser from input
            ApplicationUser user = new ApplicationUser { UserName = details.Email, Email = details.Email };

            // create the User
            IdentityResult result = await _userManager
                .CreateAsync(user, details.Password)
                .ConfigureAwait(false);

            if(!result.Succeeded){
                List<string> errors = result.Errors
                    .Select(e => e.Description)
                    .ToList();

                    return BadRequest(new { errors });
            }

            return Ok(new UserDto { Id = user.Id, Email = user.Email });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] EmailLoginDetails details) {
            // retrieve user
            IdentityUser? user = await _userManager
                .FindByEmailAsync(details.Email);

            if(null == user) {
                return BadRequest($"No user with username {details.Email}");
            }

            // check if the password is correct and attempt to sign in
            var result = await _signInManager
                .PasswordSignInAsync(details.Email, details.Password, false, false)
                .ConfigureAwait(false);

            if(!result.Succeeded) {
                return Unauthorized();
            }

            var userDto = new UserDto { Id = user.Id, Email = user.Email };
            return Ok(userDto);
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout() {
            await _signInManager.SignOutAsync();

            return Ok(new { message = "Logged out successfully."} );
        }
    }
}
