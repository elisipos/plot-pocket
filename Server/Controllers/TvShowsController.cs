using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlotPocket.Server.Models.Dtos;
using PlotPocket.Server.Models.Entities;
using PlotPocket.Server.Models.Responses;
using PlotPocket.Server.Services;
using Server.Controllers;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TvShowsController : ControllerBase {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TMDBService _TMDBService;
        private readonly ShowService _ShowService;


        public TvShowsController(TMDBService TMDBService, ShowService showService, UserManager<ApplicationUser> userManager) {
            _TMDBService = TMDBService;
            _userManager = userManager;
            _ShowService = showService;
        }

        [HttpGet("{showId}")]
        public async Task<ActionResult<ShowDto>> GetTvShowById(int showId) {
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            TvShow tvShow = await _TMDBService.GetTvShowByIdAsync(showId);

            if(null == tvShow) {
                return BadRequest("Expected show details but got null instead.");
            }

            ShowDto tvShowDto = await _ShowService.TvShowToShowDto(tvShow, user?.Id);

            return Ok(tvShowDto);
        }


        [HttpGet("airing-today")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetAiringToday() {
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            TvShowResponse tvShowItems = await _TMDBService.GetAiringTodayShowsAsync();

            if(null == tvShowItems) {
                return BadRequest("Expected list of media items but got null instead.");
            }

            ICollection<ShowDto> shows = new List<ShowDto>();
            foreach(TvShow t in tvShowItems.Results) {
                ShowDto tvShow = await _ShowService.TvShowToShowDto(t, user?.Id);
                if(user == null){
                    if(!t.Adult){
                        shows.Add(tvShow);
                    }
                }else{
                    shows.Add(tvShow);
                }
            }

            return Ok(shows);
        }

        [HttpGet("top-rated")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetTopRatedShows() {
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            TvShowResponse tvShowItems = await _TMDBService.GetTopRatedShowsAsync();

            if(null == tvShowItems) {
                return BadRequest("Expected list of media items but got null instead.");
            }

            ICollection<ShowDto> shows = new List<ShowDto>();
            foreach(TvShow t in tvShowItems.Results) {
                ShowDto tvShow = await _ShowService.TvShowToShowDto(t, user?.Id);
                if(user == null){
                    if(!t.Adult){
                        shows.Add(tvShow);
                    }
                }else{
                    shows.Add(tvShow);
                }
            }

            return Ok(shows);
        }

        [HttpGet("popular")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetPopularShows() {
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            TvShowResponse tvShowItems = await _TMDBService.GetPopularShowsAsync();

            if(null == tvShowItems) {
                return BadRequest("Expected list of media items but got null instead.");
            }

            ICollection<ShowDto> shows = new List<ShowDto>();
            foreach(TvShow t in tvShowItems.Results) {
                ShowDto tvShow = await _ShowService.TvShowToShowDto(t, user?.Id);
                if(user == null){
                    if(!t.Adult){
                        shows.Add(tvShow);
                    }
                }else{
                    shows.Add(tvShow);
                }
            }

            return Ok(shows);
        }

    }
}
