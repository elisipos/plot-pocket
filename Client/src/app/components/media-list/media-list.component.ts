import { Component, inject, Input, OnInit } from '@angular/core';
import { SearchBarComponent } from "../search-bar/search-bar.component";
import { Observable } from 'rxjs';
import { MediaItem } from '../../models/media-item';
import { TrendingService } from '../../services/trending.service';
import { MediaFilterService } from '../../services/media-filter.service';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { RadioButtonComponent } from "../radio-button/radio-button.component";
import { AuthService } from '../../auth/services/auth.service';
import { User } from '../../auth/models/auth-user';
import { NameSearchPipe } from '../../pipes/name-search.pipe';
import { BookmarkService } from '../../services/bookmark.service';

@Component({
  selector: 'app-media-list',
  imports: [SearchBarComponent, SearchBarComponent, CommonModule, RadioButtonComponent, NameSearchPipe],
  templateUrl: './media-list.component.html',
  styleUrl: './media-list.component.css'
})
export class MediaListComponent implements OnInit{

  private _trendingService = inject(TrendingService);
  private _mediaFilterService = inject(MediaFilterService);
  private _authService = inject(AuthService);
  private _bookmarkService = inject(BookmarkService);

  // TODO: Update this to set the mediaList to whichever page is open.
  public mediaList$: Observable<MediaItem[]> = this._trendingService.trendingList$;
  public user: User | null = null;
  // public show: any = null;
  // public selectedOption: string | null = null;

  public searchQuery$: Observable<string> = 
    this._mediaFilterService.searchQuery$;

  toggleBookmark(mediaItem: MediaItem) {
    if(!mediaItem.isBookmarked) {
      console.log(this._bookmarkService.addBookmark(mediaItem));
    }else if(mediaItem.isBookmarked) {
      this._bookmarkService.removeBookmark();
    }

  }

  handleData(data: number): void {
    switch (data) {
      case 0:
        this.changeList("all");
        break;
      
      case 1:
        this.changeList('movies');
        break;

      case 2:
        this.changeList('tvshows');
        break;

      default: 0;
        break;
    }
  }

  changeList(list: string): void {
    switch (list){
      case 'all':
        this._trendingService.getTrendingAll().subscribe();
        break;

      case 'movies':
        this._trendingService.getTrendingMovies().subscribe();
        break;

      case 'tvshows':
        this._trendingService.getTrendingTvShows().subscribe();
        break;

      default: 'all'
    }
    console.log(this.mediaList$);
  }

  ngOnInit(): void {
    
    this._authService.user$.subscribe(res => {
      this.user = res;
    })

    this.changeList('all');

  }

}
