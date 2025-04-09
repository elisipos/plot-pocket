import { inject, Injectable } from '@angular/core';
import { MediaItem } from '../models/media-item';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';

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

  public addBookmark(mediaItem: MediaItem): any {
    return this._http.post<MediaItem>(`/api/shows/add`, mediaItem).subscribe();
  }

  public removeBookmark(showApiId: number): any {
    return this._http.delete<null>(`/api/shows/remove/${showApiId}`).subscribe();
  }

  public getAllBookmarkedMedia(): Observable<MediaItem[]> {
    return this._http.get<MediaItem[]>(`/api/shows/all`).pipe(
      tap(mediaItems => {
        this._bookmarkSubject.next(mediaItems);
      })
    )
  }

}
