import { inject, Injectable } from '@angular/core';
import { MediaItem } from '../models/media-item';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookmarkService {

  private _http = inject(HttpClient);

  private _bookmarkSubject: BehaviorSubject<MediaItem | null> = 
    new BehaviorSubject<MediaItem | null>(null);

  public bookmark$: Observable<MediaItem | null> = 
    this._bookmarkSubject.asObservable();

  constructor() { }

  public addBookmark(mediaItem: MediaItem): Observable<MediaItem> {
    return this._http.post<MediaItem>(`/api/shows/add`, mediaItem).pipe(
      tap(bookmark => {
        this._bookmarkSubject.next(bookmark);
      })
    )
  }

  public removeBookmark(): Observable<null> {
    return this._http.delete<null>(`/api/shows/remove`).pipe(
      tap(bookmark => {
        this._bookmarkSubject.next(null);
      })
    )
  }

}
