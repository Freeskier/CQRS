import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PhotoLikeService {
  url = environment.api + 'photoLike';

  constructor(private http: HttpClient) {}

  public addLike(photoId: string) {
    return this.http.post(this.url, { id: photoId });
  }

  public removeLike(photoId: string) {
    return this.http.delete(`${this.url}?Id=${photoId}`);
  }

  public getLikesForPhoto(photoId: string) {
    return this.http.get<any[]>(`${this.url}/${photoId}`);
  }

  public isPhotoLiked(photoId: string, userId: string) {
    return this.http.get<any>(
      `${this.url}/isLiked?PhotoId=${photoId}&UserId=${userId}`
    );
  }
}
