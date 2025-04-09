using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using PlotPocket.Server.Models.Dtos;
using PlotPocket.Server.Models.Entities;
using PlotPocket.Server.Models.Responses;
using PlotPocket.Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TMDBService _TMDBService;
        private readonly ShowService _ShowService;


        public MoviesController(TMDBService TMDBService, ShowService showService, UserManager<ApplicationUser> userManager) {
            _TMDBService = TMDBService;
            _userManager = userManager;
            _ShowService = showService;
        }


        [HttpGet("now-playing")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetNowPlaying() {
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            MovieResponse movieItems = await _TMDBService.GetNowPlayingMoviesAsync();

            if(null == movieItems) {
                return BadRequest("Expected list of media items but got null instead.");
            }

            ICollection<ShowDto> shows = new List<ShowDto>();
            foreach(Movie m in movieItems.Results) {
                ShowDto movie = await _ShowService.MovieToShowDto(m, user?.Id);
                if(user == null){
                    if(!m.Adult){
                        shows.Add(movie);
                    }
                }else{
                    shows.Add(movie);
                }
            }

            return Ok(shows);
        }

        [HttpGet("top-rated")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetTopRatedMovies() {
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            MovieResponse movieItems = await _TMDBService.GetTopRatedMoviesAsync();

            if(null == movieItems) {
                return BadRequest("Expected list of media items but got null instead.");
            }

            ICollection<ShowDto> shows = new List<ShowDto>();
            foreach(Movie m in movieItems.Results) {
                ShowDto movie = await _ShowService.MovieToShowDto(m, user?.Id);
                if(user == null){
                    if(!m.Adult){
                        shows.Add(movie);
                    }
                }else{
                    shows.Add(movie);
                }
            }

            return Ok(shows);
        }

        [HttpGet("popular")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetPopular() {
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            MovieResponse movieItems = await _TMDBService.GetPopularMoviesAsync();

            if(null == movieItems) {
                return BadRequest("Expected list of media items but got null instead.");
            }

            ICollection<ShowDto> shows = new List<ShowDto>();
            foreach(Movie m in movieItems.Results) {
                ShowDto movie = await _ShowService.MovieToShowDto(m, user?.Id);
                if(user == null){
                    if(!m.Adult){
                        shows.Add(movie);
                    }
                }else{
                    shows.Add(movie);
                }
            }

            return Ok(shows);
        }

    }
}
