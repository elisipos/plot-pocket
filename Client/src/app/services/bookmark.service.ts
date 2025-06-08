import { inject, Injectable } from '@angular/core';
import { MediaItem } from '../models/media-item';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookmarkService {

  private _http = inject(HttpClient);

  private _bookmarkSubject: BehaviorSubject<MediaItem[]> = 
    new BehaviorSubject<MediaItem[]>([] as MediaItem[]);

  public bookmark$: Observable<MediaItem[]> = 
    this._bookmarkSubject.asObservable();

  constructor() { }

  public addBookmark(mediaItem: MediaItem): Observable<any> {
    return this._http.post<MediaItem>(`${environment.apiUrl}/shows/add`, mediaItem, {
      withCredentials: true
    })
  }

  public removeBookmark(showApiId: number): Observable<any> {
    return this._http.delete<null>(`${environment.apiUrl}/shows/remove/${showApiId}`, {
      withCredentials: true
    })
  }

  public getAllBookmarkedMedia(): Observable<MediaItem[]> {
    return this._http.get<MediaItem[]>(`${environment.apiUrl}/shows/all`, {
      withCredentials: true
    }).pipe(
      tap(mediaItems => {
        console.log("mediaItems: ", mediaItems);
        this._bookmarkSubject.next(mediaItems);
      })
    );
  }

}
