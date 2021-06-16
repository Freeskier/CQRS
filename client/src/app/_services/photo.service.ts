import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PhotoResponse } from '../_models/photoResponse';

@Injectable({
  providedIn: 'root',
})
export class PhotoService {
  url = environment.api + 'photo/';

  constructor(private http: HttpClient) {}

  public getPhotosForUser(userId: string) : Observable<PhotoResponse[]> {
    return this.http.get<PhotoResponse[]>(`${this.url}user/${userId}`);
  }

  public getPhotosOfFollowees() : Observable<PhotoResponse[]> {
    return this.http.get<PhotoResponse[]>(`${this.url}followees`);
  }
  
}
