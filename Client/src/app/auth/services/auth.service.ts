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
    let user: User | null = userJsonRaw ? JSON.parse(userJsonRaw) : null;
    const now = new Date().getTime();
    // if expired, set user object to null.
    if(user){
      user = user?.expiry < now ? null : user;
    }
    this._userSubject.next(user);
  }

  public get user(): User | null {
    return this._userSubject.value;
  }

  public register(details: EmailLoginDetails): Observable<User> {
    return this._http.post<User>(`${environment.apiUrl}/auth/register`, details, {
      withCredentials: true
    });
  }

  public login(details: EmailLoginDetails): Observable<User> {
    return this._http.post<User>(`${environment.apiUrl}/auth/login`, details, {
      withCredentials: true
      }
    ).pipe(tap(user => {
        const expiryDate = new Date().getTime() + 900; // 15 minutes, Render's inactivity spin down time.
        let userWithExpiration = {
          id: user.id,
          email: user.email,
          expiry: expiryDate
        }

        localStorage.setItem(this._userKey, JSON.stringify(userWithExpiration));
        this._userSubject.next(userWithExpiration);
      }))
  }

  public logout(): Observable<any> {
    return this._http.post<any>(`${environment.apiUrl}/auth/logout`, {}, {
      withCredentials: true
      }
    ).pipe(tap(() => {
        localStorage.removeItem(this._userKey);
        this._userSubject.next(null);
      }))
  }
}
