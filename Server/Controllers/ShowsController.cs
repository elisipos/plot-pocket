using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<ShowDto>> AddBookmark(Show show) {
            Console.WriteLine("\n\n\nREACHED\n\n\n");
            if(null == show) {
                return BadRequest();
            }

            ApplicationUser? user = await _userManager.GetUserAsync(User);

            if(null == user) {
                return Unauthorized();
            }

            if(!_context.Shows.Any(s => s.ShowApiId == show.ShowApiId)) {
                Show newShow = new Show{
                    Id = show.Id,
                    ShowApiId = show.ShowApiId,
                    Type = show.Type,
                    Title = show.Title,
                    Date = show.Date,
                    PosterPath = show.PosterPath
                };
                newShow.Users.Add(user);
                await _context.Shows.AddAsync(show);
            }

            if(!user.Shows.Any(s => s.ShowApiId == show.ShowApiId)) {
                user.Shows.Add(show);
            }

            await _context.SaveChangesAsync();

            return Ok(show);
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

            if(_context.Shows.Any(s => s.ShowApiId == id)) {
                var show = _context.Shows.FirstOrDefault(s => s.ShowApiId == id);
                _context.Shows.Remove(show);
            }

            if(!user.Shows.Any(s => s.ShowApiId == id)) {
                var show = user.Shows.FirstOrDefault(s => s.ShowApiId == id);
                user.Shows.Remove(show);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
