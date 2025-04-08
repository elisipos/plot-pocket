using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlotPocket.Server.Models.Dtos;
using PlotPocket.Server.Models.Entities;
using Server.Data;

namespace MyApp.Namespace
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShowsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ShowDto>> AddBookmark([FromBody] Show show) {
            Console.WriteLine("\n\n\nREACHED\n\n\n");
            if(null == show) return BadRequest();

            ApplicationUser? user = await _userManager.GetUserAsync(User);
            if(null == user) return Unauthorized();

            var existingShow = await _context.Shows.FirstOrDefaultAsync(s => s.Id == show.Id);

            if(existingShow == null) {
                _context.Shows.Add( new Show{
                    Id = show.Id,
                    Type = show.Type,
                    Title = show.Title,
                    Date = show.Date,
                    PosterPath = show.PosterPath,
                    Users = new List<ApplicationUser> { user }
                }
                );
            }else{
                bool userHasShowBookmarked = user.Shows.Any(s => s.Id == show.Id);
                if(!userHasShowBookmarked) {
                    user.Shows.Add(existingShow);
                    //exisingShow.Users.Add(user);
                }
            }

            await _context.SaveChangesAsync();
            return Ok(existingShow);
        }

        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> RemoveBookmark(int id) {
            if(null == id) {
                return BadRequest();
            }

            ApplicationUser? user = await _userManager.GetUserAsync(User);

            if(null == user) {
                return Unauthorized();
            }

            
            var show = _context.Shows.FirstOrDefault(s => s.Id == id);

            if(show != null) {
                _context.Shows.Remove(show);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
