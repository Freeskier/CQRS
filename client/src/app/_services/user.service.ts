import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserToLogin } from '../_models/userToLoginRequest';
import { UserToRegisterRequest } from '../_models/userToRegisterRequest';
import { map } from 'rxjs/operators';
import { UseExistingWebDriver } from 'protractor/built/driverProviders';
import { UserDetailResponse } from '../_models/userDetailResponse';
@Injectable({
  providedIn: 'root',
})
export class UserService {
  url = environment.api + 'user/';

  constructor(private http: HttpClient) {}

  public register(user: UserToRegisterRequest): Observable<any> {
    return this.http.post<any>(this.url + 'register', user);
  }

  public login(userToLogin: UserToLogin): Observable<UserDetailResponse> {
    return this.http
      .post<UserDetailResponse>(this.url + 'login', userToLogin)
      .pipe(
        map((user) => {
          if (user && user.token) {
            const userJson: string = JSON.stringify(user);
            localStorage.setItem('dumplingram-token-info', user.token);
            localStorage.setItem('dumplingram-user-info', userJson);
          }
          return user;
        })
      );
  }

  public logout() {
    localStorage.removeItem('dumplingram-token-info');
    localStorage.removeItem('dumplingram-user-info');
  }

  public search(keyWord: string): Observable<UserDetailResponse[]> {
    return this.http.get<UserDetailResponse[]>(
      `${this.url}search?KeyWord=${keyWord}`
    );
  }

  public getSingle(id: string) {
    return this.http.get<UserDetailResponse>(`${this.url}single?Id=${id}`);
  }
}
