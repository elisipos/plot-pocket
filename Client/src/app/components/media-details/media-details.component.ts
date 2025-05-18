import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MoviesService } from '../../services/movies.service';

@Component({
  selector: 'app-media-details',
  imports: [],
  templateUrl: './media-details.component.html',
  styleUrl: './media-details.component.css'
})
export class MediaDetailsComponent implements OnInit{

  private _moviesService = inject(MoviesService);

  public showId!: string;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.showId = params.get('showId')!;
      
      this._moviesService.getMovieById(this.showId).subscribe(res => console.log(res));
    })
  }

}
