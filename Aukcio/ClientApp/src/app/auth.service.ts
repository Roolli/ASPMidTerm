import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {User} from './user';
import {TokenResponse} from './token-response';
import {tap, shareReplay} from 'rxjs/operators';
import {Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isLoggedin = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  sendLogin(user: User) {
    return this.http.put<TokenResponse>(this.baseUrl + 'Authentication', user).pipe(tap({
      next: (val: TokenResponse) => {
        this.setSession(val);
      }
    }), shareReplay(1));
  }

  setSession(authResult: TokenResponse) {
    const token = authResult.token;
    sessionStorage.setItem('token', token);
    sessionStorage.setItem('expiresAt', authResult.expiration);
    this.isLoggedin = true;
  }

  public isLoggedIn() {
    if (sessionStorage.getItem('expiresAt') == null) {
      return false;
    }
    return Date.now() <= new Date(sessionStorage.getItem('expiresAt')).getTime();
  }

  private isLoggedOut() {
    return !this.isLoggedin;
  }

  logout() {
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('expiresAt');

  }
}
