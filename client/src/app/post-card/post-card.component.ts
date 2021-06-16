import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { PhotoResponse } from '../_models/photoResponse';
import { UserDetailResponse } from '../_models/userDetailResponse';
import { PhotoLikeService } from '../_services/photoLike.service';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css'],
})
export class PostCardComponent implements OnInit {
  @Input() photo: PhotoResponse;
  loggedUser: UserDetailResponse;
  isLiked: boolean;
  likers: any[] = [];
  constructor(
    private router: Router,
    private photoLikeService: PhotoLikeService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loggedUser = JSON.parse(localStorage.getItem('dumplingram-user-info'));
    this.checkIfPhotoIsLiked(this.photo.id, this.loggedUser.id);
    this.getLikes();
  }

  public navigateToProfile(id: string) {
    this.router.navigateByUrl(`/profile/${id}`);
  }

  public checkIfPhotoIsLiked(photoId: string, userId: string) {
    this.photoLikeService.isPhotoLiked(photoId, userId).subscribe((data) => {
      this.isLiked = data.isTrue;
    });
  }

  public likePhoto() {
    this.photoLikeService.addLike(this.photo.id).subscribe(
      () => {
        this.isLiked = true;
      },
      (error) => {
        this.snackBar.open(error, '', {
          duration: 2000,
          horizontalPosition: 'end',
          verticalPosition: 'top',
        });
      }
    );
  }

  public dislikePhoto() {
    this.photoLikeService.removeLike(this.photo.id).subscribe(
      () => {
        this.isLiked = false;
      },
      (error) => {
        this.snackBar.open(error, '', {
          duration: 2000,
          horizontalPosition: 'end',
          verticalPosition: 'top',
        });
      }
    );
  }

  public getLikes() {
    this.photoLikeService.getLikesForPhoto(this.photo.id).subscribe(
      (data) => {
        this.likers = data;
      }
    )
  }
}
