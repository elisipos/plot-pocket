import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { User } from '../models/auth-user';
import { EmailLoginDetails } from '../models/email-login-details';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _http = inject(HttpClient);
  private _userKey: string = 'curUser'
  private _userSubject: BehaviorSubject<User | null> = new BehaviorSubject<User | null>(null);

  public user$: Observable<User | null> = this._userSubject.asObservable();

  constructor() {
    // attempt to get user from localStorage
    const userJsonRaw = localStorage.getItem(this._userKey);
    const user: User | null = userJsonRaw ? JSON.parse(userJsonRaw) : null;
    this._userSubject.next(user);
  }

  public get user(): User | null {
    return this._userSubject.value;
  }

  public register(details: EmailLoginDetails): Observable<User> {
    return this._http.post<User>(`${environment.apiUrl}/auth/register`, details);
  }

  public login(details: EmailLoginDetails): Observable<User> {
    return this._http.post<User>(`${environment.apiUrl}/auth/login`, details)
      .pipe(tap(user => {
        localStorage.setItem(this._userKey, JSON.stringify(user));
        this._userSubject.next(user);
      }))
  }

  public logout(): Observable<any> {
    return this._http.post<any>(`${environment.apiUrl}/auth/logout`, {})
      .pipe(tap(() => {
        localStorage.removeItem(this._userKey);
        this._userSubject.next(null);
      }))
  }
}
