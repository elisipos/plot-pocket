import { Component, inject, Input, OnInit } from '@angular/core';
import { SearchBarComponent } from "../search-bar/search-bar.component";
import { Observable } from 'rxjs';
import { MediaItem } from '../../models/media-item';
import { TrendingService } from '../../services/trending.service';
import { MediaFilterService } from '../../services/media-filter.service';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { RadioButtonComponent } from "../radio-button/radio-button.component";
import { AuthService } from '../../auth/services/auth.service';
import { User } from '../../auth/models/auth-user';
import { NameSearchPipe } from '../../pipes/name-search.pipe';
import { BookmarkService } from '../../services/bookmark.service';
import { MoviesService } from '../../services/movies.service';
import { TvShowService } from '../../services/tv-show.service';

@Component({
  selector: 'app-media-list',
  imports: [SearchBarComponent, SearchBarComponent, CommonModule, RadioButtonComponent, NameSearchPipe],
  templateUrl: './media-list.component.html',
  styleUrl: './media-list.component.css'
})
export class MediaListComponent implements OnInit{

  private _trendingService = inject(TrendingService);
  private _moviesService = inject(MoviesService);
  private _tvShowService = inject(TvShowService);
  private _mediaFilterService = inject(MediaFilterService);
  private _authService = inject(AuthService);
  private _bookmarkService = inject(BookmarkService);

  public currentRoute: string = '';
  
  public mediaList$: Observable<MediaItem[]> | null = null;
  public user: User | null = null;
  public selectedOption: string = '';
  public selectedType: string = '';

  public searchQuery$: Observable<string> = 
    this._mediaFilterService.searchQuery$;

  constructor(private router: Router) {
    const url = this.router.url;
    this.currentRoute = url.substring(url.lastIndexOf('/') + 1);
  }

  toggleBookmark(mediaItem: MediaItem) {
    if(!mediaItem.showApiId) {
      this._bookmarkService.addBookmark(mediaItem);
    }else if(mediaItem.showApiId) {
      this._bookmarkService.removeBookmark(mediaItem.showApiId);
    }
    this.changeList(this.selectedOption, this.selectedType);
  }

  handleBtnData(data: number, type: string): void {
    switch (type){
      case 'trending':
        this.selectedType = 'trending';

        switch (data) {
          case 0:
            this.selectedOption = 'all';
            break;
            
          case 1:
            this.selectedOption = 'movies';
            break;
          
          case 2:
            this.selectedOption = 'tv-shows';
            break;
                
          default: 0;
            break;
        }

        break;

      case 'movies':
        this.selectedType = 'movies';
        // movies switch case
        switch (data) {
          case 0:
            this.selectedOption = 'now-playing';
            break;
            
          case 1:
            this.selectedOption = 'top-rated';
            break;
          
          case 2:
            this.selectedOption = 'popular';
            break;
                
          default: 0;
            break;
        }
        break;

      case 'tv-shows':
        this.selectedType = 'tv-shows'
        // tvshows switch case
        switch (data) {
          case 0:
            this.selectedOption = 'airing-today';
            break;
            
          case 1:
            this.selectedOption = 'top-rated';
            break;
          
          case 2:
            this.selectedOption = 'popular';
            break;
                
          default: 0;
            break;
        }
        break;
      // OPTIONAL: add btns for bookmarks, prob not gunna lets be real
    }
    this.changeList(this.selectedOption, this.selectedType);
  }

  changeList(list: string, url: string): void {
    switch (url){
      case 'trending':
        // trending switch case

        this.mediaList$ = this._trendingService.trendingList$;

        switch (list){
          case 'all':
            this._trendingService.getTrendingAll().subscribe();
            break;
    
          case 'movies':
            this._trendingService.getTrendingMovies().subscribe();
            break;
    
          case 'tv-shows':
            this._trendingService.getTrendingTvShows().subscribe();
            break;
    
          default: 'all'
            break;
        }
        break;
      
      case 'movies':
        // movies switch case

        this.mediaList$ = this._moviesService.movieList$;

        switch (list){
          case 'now-playing':
            this._moviesService.getMoviesNowPlaying().subscribe();
            break;
    
          case 'top-rated':
            this._moviesService.getMoviesTopRated().subscribe();
            break;
    
          case 'popular':
            this._moviesService.getMoviesPopular().subscribe();
            break;
    
          default: 'now-playing'
            break;
        }
        break;
      
      case 'tv-shows':
        // tvshows switch case

        this.mediaList$ = this._tvShowService.tvShowsList$;

        switch (list){
          case 'airing-today':
            this._tvShowService.getTvShowsAiringToday().subscribe();
            break;
    
          case 'top-rated':
            this._tvShowService.getTvShowsTopRated().subscribe();
            break;
    
          case 'popular':
            this._tvShowService.getTvShowsPopular().subscribe();
            break;
    
          default: 'airing-today'
            break;
        }
        break;
      
      case 'bookmarks':
        this.mediaList$ = this._bookmarkService.bookmark$;

        this._bookmarkService.getAllBookmarkedMedia().subscribe();
        break;

      default: 'trending'
        break;
    }
  }

  ngOnInit(): void {
    
    this._authService.user$.subscribe(res => {
      this.user = res;
    })
    
    // I hate this but I backed myself into a corner with the way Im handling the radio buttons and refreshes.
    if(this.currentRoute == 'bookmarks'){
      this.selectedType = 'bookmarks'
      this.selectedOption = 'bookmarks'
    }

    this.handleBtnData(0, this.currentRoute)
    this.changeList(this.selectedOption, this.selectedType);
    console.log(this.mediaList$);
  }

}
