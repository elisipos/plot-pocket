using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlotPocket.Server.Models.Dtos;
using PlotPocket.Server.Models.Entities;
using PlotPocket.Server.Models.Responses;
using PlotPocket.Server.Services;
using Server.Data;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrendingController : ControllerBase
    {

        private readonly TMDBService _TMDBService;
        private readonly ShowService _ShowService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TrendingController(TMDBService TMDBService, ShowService ShowService, UserManager<ApplicationUser> userManager) {
            _TMDBService = TMDBService;
            _ShowService = ShowService;
            _userManager = userManager;
        }

        [HttpGet("all")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetTrendingAll() {
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            TrendingResponse mediaItems = await _TMDBService.GetTrendingShowsAsync();

            if(null == mediaItems) {
                return BadRequest("Expected list of media items but got null instead.");
            }

            ICollection<ShowDto> shows = new List<ShowDto>();
            foreach(Trending t in mediaItems.Results) {
                ShowDto show = await _ShowService.MediaItemToShowDto(t, user?.Id);
                if(user == null){
                    if(!t.Adult){
                        shows.Add(show);
                    }
                }else{
                    shows.Add(show);
                }
            }

            return Ok(shows);
        }

        [HttpGet("movies")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetTrendingMovies() {
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            TrendingResponse mediaItems = await _TMDBService.GetTrendingMoviesAsync();

            if(null == mediaItems) {
                return BadRequest("Expected list of media items but got null instead.");
            }

            ICollection<ShowDto> shows = new List<ShowDto>();
            foreach(Trending t in mediaItems.Results) {
                ShowDto show = await _ShowService.MediaItemToShowDto(t, user?.Id);
                shows.Add(show);
            }

            return Ok(shows);
        }

        [HttpGet("tvshows")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetTrendingTvShows() {
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            TrendingResponse mediaItems = await _TMDBService.GetTrendingTvShowsAsync();

            if(null == mediaItems) {
                return BadRequest("Expected list of media items but got null instead.");
            }

            ICollection<ShowDto> shows = new List<ShowDto>();
            foreach(Trending t in mediaItems.Results) {
                ShowDto show = await _ShowService.MediaItemToShowDto(t, user?.Id);
                shows.Add(show);
            }

            return Ok(shows);
        }

    }
}
