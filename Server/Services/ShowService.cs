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
        _OriginalImgSize = configuration["TMDB:Images:BackdropSizes:Original"];
        _mapper = mapper;
        _context = context;
    }

    /**
     * Below can be used for converting return objects from the Trending endpoints 
     * to ShowDtos. 
     **/
     
    public async Task<ShowDto> MediaItemToShowDto(ApiMediaItem mediaItem, string? userId) {
        string? dateToParse = mediaItem switch {
            Movie movie => movie.ReleaseDate,
            TvShow tvShow => tvShow.FirstAirDate,
            Trending trendingShow => trendingShow.ReleaseDate ?? trendingShow.FirstAirDate,
            _ => null
        };

        var date = DateTime.TryParse(dateToParse, out DateTime parsedDate) ? parsedDate : (DateTime?)null;

        int existingShowId = await ShowExistsForLoggedInUser(mediaItem.Id, userId);
        
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

    public async Task<ShowDto> ShowToShowDto(Show show, string? userId){
        var showDto =_mapper.Map<ShowDto>(show);
        int existingShowId = await ShowExistsForLoggedInUser(show.Id, userId);
        showDto.ShowApiId = existingShowId;
        return showDto;
    }    

    public async Task<ShowDto> MovieToShowDto(Movie movie, string? userId) {
        var showDto = _mapper.Map<ShowDto>(movie);
        showDto.HighResPosterPath = _SecureImgUrl + _OriginalImgSize + showDto.PosterPath;
        showDto.PosterPath = _SecureImgUrl + _SmallImgSize + showDto.PosterPath;
        showDto.Type = "movie";

        int existingShowId = await ShowExistsForLoggedInUser(movie.Id, userId);
        showDto.ShowApiId = existingShowId;

        return showDto;
    }

    public async Task<ShowDto> TvShowToShowDto(TvShow tvShow, string? userId) {
        var showDto = _mapper.Map<ShowDto>(tvShow);
        showDto.HighResPosterPath = _SecureImgUrl + _OriginalImgSize + showDto.PosterPath;
        
        foreach(SeasonDto s in showDto.Seasons) {
            if(s.PosterPath != null) {
                s.PosterPath = _SecureImgUrl + _SmallImgSize + s.PosterPath;
                s.HighResPosterPath = _SecureImgUrl + _OriginalImgSize + s.PosterPath;
            }
        }

        showDto.PosterPath = _SecureImgUrl + _SmallImgSize + showDto.PosterPath;

        showDto.Type = "tv";

        int existingShowId = await ShowExistsForLoggedInUser(tvShow.Id, userId);
        showDto.ShowApiId = existingShowId;

        return showDto;
    }

    public async Task<int> ShowExistsForLoggedInUser(int showApiId, string? userId) {
        int existingShowId = 0;
        if (null != userId) {
            bool hasShow = await _context.ApplicationUsers
                .Where(u => u.Id == userId)
                .AnyAsync(u => u.Shows.Any(s => s.Id == showApiId));
            if(hasShow) existingShowId = showApiId;
        }

        return existingShowId;
    }
    
}
