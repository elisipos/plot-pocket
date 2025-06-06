import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { MediaItem } from '../models/media-item';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TrendingService {

  private _http = inject(HttpClient);

  private _trendingListSubject: BehaviorSubject<MediaItem[]> = 
    new BehaviorSubject<MediaItem[]>([] as MediaItem[]);

  public trendingList$: Observable<MediaItem[]> =
    this._trendingListSubject.asObservable();

  constructor() { }

  public getTrendingAll(): Observable<MediaItem[]> {
    return this._http.get<MediaItem[]>(`${environment.apiUrl}/trending/all`).pipe(
      tap(mediaItems => {
        this._trendingListSubject.next(mediaItems);
      })
    )
  }

  public getTrendingMovies(): Observable<MediaItem[]> {
    return this._http.get<MediaItem[]>(`${environment.apiUrl}/trending/movies`).pipe(
      tap(mediaItems => {
        this._trendingListSubject.next(mediaItems);
      })
    )
  }

  public getTrendingTvShows(): Observable<MediaItem[]> {
    return this._http.get<MediaItem[]>(`${environment.apiUrl}/trending/tvshows`).pipe(
      tap(mediaItems => {
        this._trendingListSubject.next(mediaItems);
      })
    )
  }
}
