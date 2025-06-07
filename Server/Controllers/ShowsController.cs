using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PlotPocket.Server.Models.Dtos;
using PlotPocket.Server.Models.Entities;
using PlotPocket.Server.Models.Responses;
using PlotPocket.Server.Services;
using Server.Data;

namespace MyApp.Namespace
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ShowService _ShowService;

        public ShowsController(ApplicationDbContext context, ShowService showService, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
            _ShowService = showService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ShowDto>> AddBookmark([FromBody] Show show) {
            if(null == show) return BadRequest();

            Console.WriteLine("\n\nAuthenticated: " + User.Identity?.IsAuthenticated);
            Console.WriteLine("User Name: " + User.Identity?.Name + "\n\n");

            ApplicationUser? user = await _userManager.GetUserAsync(User);
            if(null == user) {
                Console.WriteLine("\n\nUser object is null\n\n");
                return Unauthorized();
            }

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

        [HttpGet("all")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetAllBookmarkedMedia() {
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            
            Console.WriteLine("\n\nAuthenticated: " + User.Identity?.IsAuthenticated);
            Console.WriteLine("User Name: " + User.Identity?.Name + "\n\n");

            if(null == user){
                Console.WriteLine("\n\nUser object is null\n\n");
                return Unauthorized();
            }

            ICollection<ShowDto> shows = new List<ShowDto>();
            var bookmarkedShows = await _context.Shows
                .Where(s => s.Users.Any(u => u.Id == user.Id))
                .ToListAsync();

            var bookmarkedDtos = await Task.WhenAll(
                bookmarkedShows.Select(s => _ShowService.ShowToShowDto(s, user.Id))
            );

            // foreach(ShowDto s in bookmarkedShows){
            //     ShowDto show = await _ShowService.ShowToShowDto(s, user?.Id);
            //     shows.Add(show);
            // }

            return Ok(bookmarkedDtos);
        }

    }
}
