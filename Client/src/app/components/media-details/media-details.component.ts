import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MoviesService } from '../../services/movies.service';
import { MediaItem } from '../../models/media-item';
import { CommonModule } from '@angular/common';
import { TvShowService } from '../../services/tv-show.service';

@Component({
  selector: 'app-media-details',
  imports: [CommonModule],
  templateUrl: './media-details.component.html',
  styleUrl: './media-details.component.css'
})
export class MediaDetailsComponent implements OnInit{

  private _moviesService = inject(MoviesService);
  private _tvShowService = inject(TvShowService);

  public showId!: string;
  public showType!: string;
  public show!: MediaItem;

  constructor(private route: ActivatedRoute) {}

  public convertToHrsMin(time: number): string {
    let minutes = time % 60;
    let hours = (time - minutes) / 60;

    return `${hours}h ${minutes}m`;
  }

  public formatUSD(input: number): string {
    const USDFormatter = new Intl.NumberFormat("en-US", { style: "currency", currency: "USD", minimumFractionDigits: 0, maximumFractionDigits: 0})

    return USDFormatter.format(input);
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.showId = params.get('showId')!;
      this.showType = params.get('type')!;
      
      if(this.showType === "movie"){
        this._moviesService.getMovieById(this.showId).subscribe(res => {
          this.show = res;
        });
      }else{
        this._tvShowService.getTvShowById(this.showId).subscribe(res => {
          this.show = res;
        });
      }
    })
  }

}
