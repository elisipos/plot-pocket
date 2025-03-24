using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlotPocket.Server.Models.Dtos;
using PlotPocket.Server.Models.Entities;
using PlotPocket.Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrendingController : ControllerBase
    {

        private readonly TMDBService _TMDBService;

        public TrendingController(TMDBService TMDBService) {
            _TMDBService = TMDBService;
        }

        [HttpGet("trending/all")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetTrendingAll() {
            ICollection<ShowDto>? mediaItems = await _TMDBService.GetTrendingShowsAsync();

            if(null == mediaItems) {
                return BadRequest("Expected list of media items but got null instead.");
            }

            return Ok(mediaItems);
        }

    }
}
