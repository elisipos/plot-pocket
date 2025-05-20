using RestSharp;
using PlotPocket.Server.Models.Responses;
using System.Text.Json;

namespace PlotPocket.Server.Services;

public class TMDBService {
	private readonly RestClient _restClient;
	private readonly string? _apiKey;
	private readonly string? _apiReadAccessToken;
	private readonly string? _baseUrl;
	
	public TMDBService(IConfiguration configuration) {
		/*
			Get your API key from the appSettings.json.
		*/
		_apiKey = configuration["TMDB:ApiKey"];
		_apiReadAccessToken = configuration["TMDB:ApiReadAccessToken"];
		 /* 
			 This is how we are reading the TMBD data in the appSettings.json file.
		 */
		_baseUrl = configuration["TMDB:BaseUrl"] ?? "";
		/*
			The base url that we are requesting to the api stays the same for all endpoints.
			the only thing that is changing is the uri. We will build the appropriate uri in the
			each below method.
		*/
		_restClient = new RestClient(_baseUrl);
	}


	/* =============== */
	/* GET MOVIE BY ID */
	/* =============== */

	public async Task<Movie> GetMovieByIdAsync(int id) {
		var request = new RestRequest($"/movie/{id}?api_key={_apiKey}")
									.AddHeader("accept", "application/json");

		var response = await _restClient.GetAsync(request);
		Movie? movieResp = JsonSerializer.Deserialize<Movie>(response.Content);

		return movieResp;
	}

	/* ================ */
	/* GET TVSHOW BY ID */
	/* ================ */

	public async Task<TvShow> GetTvShowByIdAsync(int id) {
		var request = new RestRequest($"/tv/{id}?api_key={_apiKey}")
									.AddHeader("accept", "application/json");

		var response = await _restClient.GetAsync(request);
		TvShow? tvShowResp = JsonSerializer.Deserialize<TvShow>(response.Content);

		return tvShowResp;
	}
	
	/* ============== */
	/* BEGIN TRENDING */
	/* ============== */

	public async Task<TrendingResponse> GetTrendingShowsAsync(string timeWindow = "day") {
    var request = new RestRequest($"/trending/all/{timeWindow}?api_key={_apiKey}")
                  .AddHeader("accept", "application/json"); // This header says that we are expecting JSON as a response.

    var response = await _restClient.GetAsync(request);
    TrendingResponse? trendingRespResp = JsonSerializer.Deserialize<TrendingResponse>(response.Content);

    return trendingRespResp ?? new TrendingResponse { Results = new List<Trending>() };
	}

	public async Task<TrendingResponse> GetTrendingMoviesAsync(string timeWindow = "day") {
    var request = new RestRequest($"/trending/movie/{timeWindow}?api_key={_apiKey}")
                  .AddHeader("accept", "application/json");

    var response = await _restClient.GetAsync(request);
    TrendingResponse? trendingRespResp = JsonSerializer.Deserialize<TrendingResponse>(response.Content);

    return trendingRespResp ?? new TrendingResponse { Results = new List<Trending>() };
	}

	public async Task<TrendingResponse> GetTrendingTvShowsAsync(string timeWindow = "day") {
    var request = new RestRequest($"/trending/tv/{timeWindow}?api_key={_apiKey}")
                  .AddHeader("accept", "application/json");

    var response = await _restClient.GetAsync(request);
    TrendingResponse? trendingRespResp = JsonSerializer.Deserialize<TrendingResponse>(response.Content);

    return trendingRespResp ?? new TrendingResponse { Results = new List<Trending>() };
	}

	/* =========== */
	/* BEGIN MOVIE */
	/* =========== */

	public async Task<MovieResponse> GetNowPlayingMoviesAsync() {
    var request = new RestRequest("/movie/now_playing")
									.AddHeader("Authorization", $"Bearer {_apiReadAccessToken}")
                  .AddHeader("accept", "application/json");

    var response = await _restClient.GetAsync(request);
    MovieResponse? movieRespResp = JsonSerializer.Deserialize<MovieResponse>(response.Content);

    return movieRespResp ?? new MovieResponse { Results = new List<Movie>() };
	}

	public async Task<MovieResponse> GetTopRatedMoviesAsync() {
    var request = new RestRequest("/movie/top_rated")
									.AddHeader("Authorization", $"Bearer {_apiReadAccessToken}")
                  .AddHeader("accept", "application/json");

    var response = await _restClient.GetAsync(request);
    MovieResponse? movieRespResp = JsonSerializer.Deserialize<MovieResponse>(response.Content);

    return movieRespResp ?? new MovieResponse { Results = new List<Movie>() };
	}

	public async Task<MovieResponse> GetPopularMoviesAsync() {
    var request = new RestRequest("/movie/popular")
									.AddHeader("Authorization", $"Bearer {_apiReadAccessToken}")
                  .AddHeader("accept", "application/json");

    var response = await _restClient.GetAsync(request);
    MovieResponse? movieRespResp = JsonSerializer.Deserialize<MovieResponse>(response.Content);

    return movieRespResp ?? new MovieResponse { Results = new List<Movie>() };
	}

	/* ============= */
	/* BEGIN TV SHOW */
	/* ============= */

	public async Task<TvShowResponse> GetAiringTodayShowsAsync() {
    var request = new RestRequest($"/tv/airing_today?api_key={_apiKey}")
                  .AddHeader("accept", "application/json");

    var response = await _restClient.GetAsync(request);
    TvShowResponse? tvShowRespResp = JsonSerializer.Deserialize<TvShowResponse>(response.Content);

    return tvShowRespResp ?? new TvShowResponse { Results = new List<TvShow>() };
	}

	public async Task<TvShowResponse> GetTopRatedShowsAsync() {
    var request = new RestRequest($"/tv/top_rated?api_key={_apiKey}")
                  .AddHeader("accept", "application/json");

    var response = await _restClient.GetAsync(request);
    TvShowResponse? tvShowRespResp = JsonSerializer.Deserialize<TvShowResponse>(response.Content);

    return tvShowRespResp ?? new TvShowResponse { Results = new List<TvShow>() };
	}

	public async Task<TvShowResponse> GetPopularShowsAsync() {
    var request = new RestRequest($"/tv/popular?api_key={_apiKey}")
                  .AddHeader("accept", "application/json");

    var response = await _restClient.GetAsync(request);
    TvShowResponse? tvShowRespResp = JsonSerializer.Deserialize<TvShowResponse>(response.Content);

    return tvShowRespResp ?? new TvShowResponse { Results = new List<TvShow>() };
	}
}