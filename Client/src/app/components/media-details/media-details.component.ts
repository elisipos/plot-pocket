import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MoviesService } from '../../services/movies.service';
import { MediaItem } from '../../models/media-item';

@Component({
  selector: 'app-media-details',
  imports: [],
  templateUrl: './media-details.component.html',
  styleUrl: './media-details.component.css'
})
export class MediaDetailsComponent implements OnInit{

  private _moviesService = inject(MoviesService);

  public showId!: string;
  public show!: MediaItem;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.showId = params.get('showId')!;
      
      this._moviesService.getMovieById(this.showId).subscribe(res => {
        this.show = res;
      });
    })
  }

}
