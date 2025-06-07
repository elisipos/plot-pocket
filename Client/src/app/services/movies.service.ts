import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { MediaItem } from '../models/media-item';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  private _http = inject(HttpClient);

  private _moviesListSubject: BehaviorSubject<MediaItem[]> =
    new BehaviorSubject<MediaItem[]>([] as MediaItem[]);

  public movieList$: Observable<MediaItem[]> =
    this._moviesListSubject.asObservable();


  private _movieSubject: BehaviorSubject<MediaItem | null> =
    new BehaviorSubject<MediaItem | null>(null);

  public movie$: Observable<MediaItem | null> =
    this._movieSubject.asObservable();

  constructor() { }

  public getMovieById(id: string): Observable<MediaItem> {
    return this._http.get<MediaItem>(`${environment.apiUrl}/movies/${id}`).pipe(
      tap(mediaItem => {
        this._movieSubject.next(mediaItem);
      })
    )
  }

  public getMoviesNowPlaying(): Observable<MediaItem[]> {
    return this._http.get<MediaItem[]>(`${environment.apiUrl}/movies/now-playing`, {
      withCredentials: true
    }).pipe(
      tap(mediaItems => {
        this._moviesListSubject.next(mediaItems);
      })
    )
  }

  public getMoviesTopRated(): Observable<MediaItem[]> {
    return this._http.get<MediaItem[]>(`${environment.apiUrl}/movies/top-rated`, {
      withCredentials: true
    }).pipe(
      tap(mediaItems => {
        this._moviesListSubject.next(mediaItems);
      })
    )
  }

  public getMoviesPopular(): Observable<MediaItem[]> {
    return this._http.get<MediaItem[]>(`${environment.apiUrl}/movies/popular`, {
      withCredentials: true
    }).pipe(
      tap(mediaItems => {
        this._moviesListSubject.next(mediaItems);
      })
    )
  }
}
