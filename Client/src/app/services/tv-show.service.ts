import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { MediaItem } from '../models/media-item';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TvShowService {

  private _http = inject(HttpClient);

  private _tvShowsListSubject: BehaviorSubject<MediaItem[]> =
    new BehaviorSubject<MediaItem[]>([] as MediaItem[]);

  public tvShowsList$: Observable<MediaItem[]> =
    this._tvShowsListSubject.asObservable();


  private _tvShowSubject: BehaviorSubject<MediaItem | null> =
    new BehaviorSubject<MediaItem | null>(null);

  public tvShow$: Observable<MediaItem | null> =
    this._tvShowSubject.asObservable();

  constructor() { }

  public getTvShowById(id: string): Observable<MediaItem> {
    return this._http.get<MediaItem>(`${environment.apiUrl}/tvshows/${id}`).pipe(
      tap(mediaItem => {
        this._tvShowSubject.next(mediaItem);
      })
    )
  }

  public getTvShowsAiringToday(): Observable<MediaItem[]> {
    return this._http.get<MediaItem[]>(`${environment.apiUrl}/tvshows/airing-today`, {
      withCredentials: true
    }).pipe(
      tap(mediaItems => {
        this._tvShowsListSubject.next(mediaItems);
      })
    )
  }

  public getTvShowsTopRated(): Observable<MediaItem[]> {
    return this._http.get<MediaItem[]>(`${environment.apiUrl}/tvshows/top-rated`, {
      withCredentials: true
    }).pipe(
      tap(mediaItems => {
        this._tvShowsListSubject.next(mediaItems);
      })
    )
  }

  public getTvShowsPopular(): Observable<MediaItem[]> {
    return this._http.get<MediaItem[]>(`${environment.apiUrl}/tvshows/popular`, {
      withCredentials: true
    }).pipe(
      tap(mediaItems => {
        this._tvShowsListSubject.next(mediaItems);
      })
    )
  }
}
