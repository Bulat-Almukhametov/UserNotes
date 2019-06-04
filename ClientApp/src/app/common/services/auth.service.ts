import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs/index";
import {JwtHelperService} from "@auth0/angular-jwt";
import { map } from 'rxjs/operators';

@Injectable()
export class AuthService {
  private tokenKey: string = 'token';

  constructor(private  http: HttpClient,
              @Inject("BASE_URL") private baseUrl,
              private jwtHelper: JwtHelperService) {
  }

  public isAuthenticated(): boolean {
    const token = this.getToken();
    // Check whether the token is expired and return
    // true or false
    return !!token && !this.jwtHelper.isTokenExpired(token);
  }

  public authenticate(nickname: string, password: string): Observable<any> {
    return this.http.post(this.baseUrl + 'api/Auth/Login', {nickname: nickname, password: password})
      .pipe(map(token => {
        localStorage.setItem(this.tokenKey, <string>token.access_token);
        return token;
      }));
  }

  public getToken(): string {
    return localStorage.getItem(this.tokenKey);
  }
}
