import { Component, inject, OnInit } from '@angular/core';
import { SearchBarComponent } from "../search-bar/search-bar.component";
import { Observable } from 'rxjs';
import { MediaItem } from '../../models/media-item';
import { TrendingService } from '../../services/trending.service';
import { MediaFilterService } from '../../services/media-filter.service';

@Component({
  selector: 'app-media-list',
  imports: [SearchBarComponent, SearchBarComponent],
  templateUrl: './media-list.component.html',
  styleUrl: './media-list.component.css'
})
export class MediaListComponent implements OnInit{

  private _trendingService = inject(TrendingService);
  private _mediaFilterService = inject(MediaFilterService);


  // TODO: Update this to set the mediaList to whichever page is open.
  public mediaList$: Observable<MediaItem[]> = this._trendingService.trendingList$;

  public searchQuery$: Observable<string> = 
    this._mediaFilterService.searchQuery$;
  
  ngOnInit(): void {
    this._trendingService.getTrendingAll().subscribe();
  }

}
