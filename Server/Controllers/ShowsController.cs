using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
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
            if(null == show) {
                return BadRequest();
            }

            ApplicationUser? user = await _userManager.GetUserAsync(User);

            if(null == user) {
                return Unauthorized();
            }

            if(_context.Shows.Any(s => s.ShowApiId == show.ShowApiId)) {
                await _context.Shows.AddAsync(show);
            }

            if(!user.Shows.Any(s => s.ShowApiId == show.ShowApiId)) {
                user.Shows.Add(show);
            }

            await _context.SaveChangesAsync();

            return Ok(show);
        }

    }
}
