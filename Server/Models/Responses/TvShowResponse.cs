using System.Text.Json.Serialization;

namespace PlotPocket.Server.Models.Responses;

public class TvShowResponse {

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public List<TvShow> Results { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}

public class Person {
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("credit_id")]
    public string? CreditId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("original_name")]
    public string? OriginalName { get; set; }
    [JsonPropertyName("gender")]
    public int Gender { get; set; }
    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; set; }
}

public class Episode {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("overview")]
    public string Overview { get; set; }

    [JsonPropertyName("vote_average")]
    public float VoteAverage { get; set; }

    [JsonPropertyName("vote_count")]
    public int VoteCount { get; set; }

    [JsonPropertyName("air_date")]
    public string AirDate { get; set; }

    [JsonPropertyName("episode_number")]
    public int EpisodeNumber { get; set; }

    [JsonPropertyName("episode_type")]
    public string? EpisodeType { get; set; }

    [JsonPropertyName("runtime")]
    public int Runtime { get; set; }

    [JsonPropertyName("season_number")]
    public int SeasonNumber { get; set; }

    [JsonPropertyName("show_id")]
    public int ShowId { get; set; }

    [JsonPropertyName("still_path")]
    public string StillPath { get; set; }
}

public class Season {
    [JsonPropertyName("air_date")]
    public string? AirDate { get; set; }

    [JsonPropertyName("episode_count")]
    public int EpisodeCount { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("overview")]
    public string? Overview { get; set; }

    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; }

    [JsonPropertyName("season_number")]
    public int? SeasonNumber { get; set; }

    [JsonPropertyName("vote_average")]
    public float VoteAverage { get; set;}
}


public class TvShow : ApiMediaItem {

    [JsonPropertyName("adult")]
    public bool Adult { get; set; }

    [JsonPropertyName("backdrop_path")]
    public string BackdropPath { get; set; }

    [JsonPropertyName("first_air_date")]
    public string FirstAirDate { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("origin_country")]
    public string[] OriginCountry { get; set; }

    [JsonPropertyName("original_name")]
    public string OriginalName { get; set; }

    [JsonPropertyName("created_by")]
    public Person[] CreatedBy { get; set; }

    [JsonPropertyName("in_production")]
    public bool InProduction { get; set; }

    [JsonPropertyName("languages")]
    public string[]? Languages { get; set; }

    [JsonPropertyName("last_air_date")]
    public string? LastAirDate { get; set; }

    [JsonPropertyName("last_episode_to_air")]
    public Episode LastEpisodeToAir { get; set; }

    [JsonPropertyName("next_episode_to_air")]
    public Episode NextEpisodeToAir { get; set; }

    [JsonPropertyName("number_of_episodes")]
    public int? NumberOfEpisodes { get; set; }

    [JsonPropertyName("number_of_seasons")]
    public int? NumberOfSeasons { get; set; }

    [JsonPropertyName("seasons")]
    public Season[] Seasons { get; set; }
    
}