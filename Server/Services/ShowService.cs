using System.Configuration;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PlotPocket.Server.Models.Dtos;
using PlotPocket.Server.Models.Entities;
using PlotPocket.Server.Models.Responses;
using Server.Data;

namespace PlotPocket.Server.Services;

public class ShowService {
    private readonly ApplicationDbContext _context;
    private readonly string? _TMDB;

    public ShowService(IConfiguration configuration, ApplicationDbContext context) {
        _TMDB = configuration["TMDB"];
        _context = context;
    }

    /**
     * Below can be used for converting return objects from the Trending endpoints 
     * to ShowDtos. 
     * 
     * TODO: Make sure to fill in the ShowDto properties on the return of this method.
     *          You should **NOT** need to modify anything else.
     * 
     **/
     
    public ShowDto MediaItemToShowDto(ApiMediaItem mediaItem, string? userId) {
        string? dateToParse = mediaItem switch {
            Movie movie => movie.ReleaseDate,
            TvShow tvShow => tvShow.FirstAirDate,
            Trending trendingShow => trendingShow.ReleaseDate ?? trendingShow.FirstAirDate,
            _ => null
        };

        var date = DateTime.TryParse(dateToParse, out DateTime parsedDate) ? parsedDate : (DateTime?)null;

        int existingShowId = null != userId ? this.ShowExistsForLoggedInUser(mediaItem.Id, userId) : 0;
        
        string? title;
        if(mediaItem is Trending trendingMedia) {
            title = trendingMedia.MediaType == "movie" ? trendingMedia.Title : trendingMedia?.Name;

        } else {
            title = (mediaItem as Movie)?.Title ?? (mediaItem as TvShow)?.Name;
        }

        return new ShowDto {
            Id = mediaItem.Id,
            Type = mediaItem is Trending trendingItem ? trendingItem.MediaType : (mediaItem is Movie ? "Movie" : "TV Show"),
            Title = title,
            Date = date,
            PosterPath = mediaItem.PosterPath
        };
    }

/*
    public ShowDto ShowToShowDto(Show show){
        // TODO: Implement
    }    

    public ShowDto MovieToShowDto(Movie movie) {
       // TODO: Implement
    }

    public ShowDto TvShowToShowDto(TvShow tvShow) {
        // TODO: Implement
    }
*/

    public int ShowExistsForLoggedInUser(int showApiId, string? userId) {
        int existingShowId = 0;
        if (null != userId) {
            // TODO: Implement
        }

        return existingShowId;
    }
    
}
