using System.Configuration;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PlotPocket.Server.Models.Dtos;
using PlotPocket.Server.Models.Entities;
using PlotPocket.Server.Models.Responses;
using Server.Data;

namespace PlotPocket.Server.Services;

public class ShowService {
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly string? _SecureImgUrl;
    private readonly string? _OriginalImgSize;
    private readonly string? _SmallImgSize;

    public ShowService(IConfiguration configuration, IMapper mapper, ApplicationDbContext context) {
        _SecureImgUrl = configuration["TMDB:Images:SecureBaseUrl"];
        _SmallImgSize = configuration["TMDB:Images:BackdropSizes:Small"];
        _mapper = mapper;
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
     
    public async Task<ShowDto> MediaItemToShowDto(ApiMediaItem mediaItem, string? userId) {
        string? dateToParse = mediaItem switch {
            Movie movie => movie.ReleaseDate,
            TvShow tvShow => tvShow.FirstAirDate,
            Trending trendingShow => trendingShow.ReleaseDate ?? trendingShow.FirstAirDate,
            _ => null
        };

        var date = DateTime.TryParse(dateToParse, out DateTime parsedDate) ? parsedDate : (DateTime?)null;

        int existingShowId = null != userId ? await ShowExistsForLoggedInUser(mediaItem.Id, userId) : 0;
        
        string? title;
        if(mediaItem is Trending trendingMedia) {
            title = trendingMedia.MediaType == "movie" ? trendingMedia.Title : trendingMedia?.Name;

        } else {
            title = (mediaItem as Movie)?.Title ?? (mediaItem as TvShow)?.Name;
        }

        return new ShowDto {
            Id = mediaItem.Id,
            ShowApiId = existingShowId,
            Type = mediaItem is Trending trendingItem ? trendingItem.MediaType : (mediaItem is Movie ? "Movie" : "TV Show"),
            Title = title,
            Date = date,
            PosterPath = _SecureImgUrl + _SmallImgSize + mediaItem.PosterPath
        };
    }

    public ShowDto ShowToShowDto(Show show){
        return _mapper.Map<ShowDto>(show);
    }    

    public ShowDto MovieToShowDto(Movie movie) {
        return _mapper.Map<ShowDto>(movie);
    }

    public ShowDto TvShowToShowDto(TvShow tvShow) {
        return _mapper.Map<ShowDto>(tvShow);
    }

    public async Task<int> ShowExistsForLoggedInUser(int showApiId, string? userId) {
        int existingShowId = 0;
        if (null != userId) {
            bool hasShow = await _context.ApplicationUsers
                .Where(u => u.Id == userId)
                .AnyAsync(u => u.Shows.Any(s => s.Id == showApiId));
            if(hasShow) {
                existingShowId = showApiId;
            }
        }

        return existingShowId;
    }
    
}
