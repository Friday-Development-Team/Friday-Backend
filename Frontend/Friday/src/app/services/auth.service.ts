import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly tokenKey = 'currentUser';
  private user: BehaviorSubject<string>;

  public redirectUrl: string;

  constructor(private http: HttpClient) {
    let parsedToken = parseJwt(localStorage.getItem(this.tokenKey));
    if (parsedToken) {
      const expires = new Date(parseInt(parsedToken.exp, 10) * 1000) < new Date();
      if (expires) {
        localStorage.removeItem(this.tokenKey);
        parsedToken = null;
      }
    }
    this.user = new BehaviorSubject<string>(parsedToken && parsedToken.unique_name);
  }

  get user$(): BehaviorSubject<string> {
    return this.user;
  }

  get token(): string {
    const localToken = localStorage.getItem(this.tokenKey);
    return !!localToken ? localToken : '';
  }

  login(username: string, password: string): Observable<boolean> {
    return this.http.post(
      `${environment.apiUrl}/user/login`,
      { username, password },
      { responseType: 'text' }
    ).pipe(
      map((token: string) => {
        if (token) {
          localStorage.setItem(this.tokenKey, token.replace(/^"(.*)"$/, '$1'));
          this.user.next(username);
          return true;
        } else {
          return false;
        }
      })
    );
  }

  register(
    firstname: string, lastname: string, email: string, password: string
  ): Observable<boolean> {
    return this.http
      .post(
        `${environment.apiUrl}/user/register`,
        {
          firstname, lastname,
          email, password,
          passwordConfirmation: password
        },
        { responseType: 'text' }
      )
      .pipe(
        map((token: any) => {
          if (token) {
            localStorage.setItem(this.tokenKey, token);
            this.user.next(email);
            return true;
          } else {
            return false;
          }
        })
      );
  }

  logout() {
    if (this.user.getValue()) {
      localStorage.removeItem('currentUser');
      this.user.next(null);
    }
  }

  checkUserNameAvailability = (name: string): Observable<boolean> => {
    return this.http.get<boolean>(
      `${environment.apiUrl}/user/checkusername`,
      {
        params: { name }
      }
    );
  }

}

function parseJwt(token) {
  if (!token) {
    return null;
  }
  const base64Token = token.split('.')[1];
  const base64 = base64Token.replace(/-/g, '+').replace(/_/g, '/');
  return JSON.parse(window.atob(base64));
}

