using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlotPocket.Server.Models.Dtos;
using PlotPocket.Server.Models.Entities;
using PlotPocket.Server.Models.Responses;
using PlotPocket.Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrendingController : ControllerBase
    {

        private readonly TMDBService _TMDBService;
        private readonly ShowService _ShowService;

        public TrendingController(TMDBService TMDBService, ShowService ShowService) {
            _TMDBService = TMDBService;
            _ShowService = ShowService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetTrendingAll() {
            TrendingResponse mediaItems = await _TMDBService.GetTrendingShowsAsync();

            if(null == mediaItems) {
                return BadRequest("Expected list of media items but got null instead.");
            }

            ICollection<ShowDto> shows = new List<ShowDto>();
            foreach(Trending t in mediaItems.Results) {
                ShowDto show = _ShowService.MediaItemToShowDto(t, null);
                shows.Add(show);
            }

            return Ok(shows);
        }

        [HttpGet("movies")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetTrendingMovies() {
            TrendingResponse mediaItems = await _TMDBService.GetTrendingMoviesAsync();

            if(null == mediaItems) {
                return BadRequest("Expected list of media items but got null instead.");
            }

            ICollection<ShowDto> shows = new List<ShowDto>();
            foreach(Trending t in mediaItems.Results) {
                ShowDto show = _ShowService.MediaItemToShowDto(t, null);
                shows.Add(show);
            }

            return Ok(shows);
        }

    }
}
