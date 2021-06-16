import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserDetailResponse } from '../_models/userDetailResponse';

@Injectable({
  providedIn: 'root',
})
export class FollowService {
  url = `${environment.api}follow/`;

  constructor(private http: HttpClient) {}

  public getFollowers(userId: string): Observable<UserDetailResponse[]> {
    return this.http.get<UserDetailResponse[]>(
      `${this.url}followers?Id=${userId}`
    );
  }

  public getFollowees(userId: string): Observable<UserDetailResponse[]> {
    return this.http.get<UserDetailResponse[]>(
      `${this.url}followees?Id=${userId}`
    );
  }

  public getSingle(
    followerId: string,
    followeeId: string
  ): Observable<{ isTrue: boolean }> {
    return this.http.get<{ isTrue: boolean }>(
      `${this.url}single?followerId=${followerId}&followeeId=${followeeId}`
    );
  }

  public addFollow(userId : string) : Observable<any> {
    return this.http.post<any>(`${this.url}follow/${userId}`, {});
  }

  public removeFollow(userId: string) : Observable<any> {
    return this.http.delete<any>(`${this.url}unfollow?Id=${userId}`);
  }
}
