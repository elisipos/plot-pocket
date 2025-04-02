using System.Text.Json.Serialization;

namespace PlotPocket.Server.Models.Responses;

public class TrendingResponse {
    /*
        TODO: 
        Define the model based on the response from the Trending endpoints from
        The Movie Database's API. Be sure to use the [JsonPropertyName("<property>")] attribue
        above each of your C# properties to ensure that the JSON maps properly to your objects.
    */

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public List<Trending> Results { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}

public class Trending : ApiMediaItem {
    /*
        TODO: 
        Define the model based on the response from the Trending endpoints from
        The Movie Database's API. Be sure to use the [JsonPropertyName("<property>")] attribue
        above each of your C# properties to ensure that the JSON maps properly to your objects.
    */

    [JsonPropertyName("adult")]
    public bool Adult { get; set; }

    [JsonPropertyName("backdrop_path")]
    public string BackdropPath { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("media_type")]
    public string MediaType { get; set; }

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; }

    [JsonPropertyName("video")]
    public bool Video { get; set; }

    [JsonPropertyName("first_air_date")]
    public string FirstAirDate { get; set; }

    [JsonPropertyName("origin_country")]
    public List<string> OriginCountry { get; set; }

    [JsonPropertyName("original_language")]
    public string OriginalLanguage { get; set; }

    [JsonPropertyName("original_name")]
    public string OriginalName { get; set; }

}