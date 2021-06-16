import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { FollowType } from '../dialogs/user-list-dialog/user-list-dialog.component';
import { DialogHelperService } from '../_helpers/dialogHelper.service';
import { PhotoResponse } from '../_models/photoResponse';
import { UserDetailResponse } from '../_models/userDetailResponse';
import { FollowService } from '../_services/follow.service';
import { PhotoService } from '../_services/photo.service';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css'],
})
export class AccountDetailsComponent implements OnInit {
  //temporary
  isMine: boolean = false;
  isFollowed: boolean = false;
  user: UserDetailResponse;
  photos: PhotoResponse[];
  loggedUser: UserDetailResponse;

  //followers
  followers: UserDetailResponse[];
  followees: UserDetailResponse[];

  constructor(
    private photoService: PhotoService,
    private route: ActivatedRoute,
    private followService: FollowService,
    private snackBar: MatSnackBar,
    private dialogHelper: DialogHelperService
  ) {}
  ngOnInit(): void {
    //temporary
    this.initializeArrays();

    this.route.data.subscribe((data) => {
      this.checkIfProfileIsMine(data);
      this.getFollowers();
      this.getFollowees();
      this.checkIfUserHasBeenAlreadyFollowed();
      this.loadPhotos();
    });
  }

  

  public follow() {
    
    this.followService.addFollow(this.user.id).subscribe(
      () => {
        this.snackBar.open(`You followed ${this.user.userName}!`, 'Ok', {
          duration: 2000,
          horizontalPosition: 'center',
          verticalPosition: 'bottom'
        });
        this.getFollowees();
        this.getFollowers();
      },
      (error) => {
        this.snackBar.open(error);
      }
    );
  }

  public unfollow() {
    this.followService.removeFollow(this.user.id).subscribe(
      () => {
        this.snackBar.open(`You unfollowed ${this.user.userName}!`, 'Ok', {
          duration: 2000,
          horizontalPosition: 'center',
          verticalPosition: 'bottom'
        });
        this.getFollowees();
        this.getFollowers();
      },
      (error) => {
        this.snackBar.open(error);
      }
    );
  }

  public openFolloweesDialog() {
    this.dialogHelper.openUsersListDialog(this.followees, FollowType.followees);
  }

  public openFollowersDialog() {
    this.dialogHelper.openUsersListDialog(this.followers, FollowType.followers);
  }

  public openPostPreviewDialog(photo: PhotoResponse) {
    this.dialogHelper.openPostPreviewDialog(photo);
  }

  private checkIfUserHasBeenAlreadyFollowed() {
    this.followService.getSingle(this.loggedUser.id, this.user.id).subscribe(
      (data) => {
        this.isFollowed = data.isTrue;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  private getFollowers() {
    this.followService.getFollowers(this.user.id).subscribe(
      (data) => {
        this.followers = data;
        this.checkIfUserHasBeenAlreadyFollowed();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  private getFollowees() {
    this.followService.getFollowees(this.user.id).subscribe(
      (data) => {
        this.followees = data;
        this.checkIfUserHasBeenAlreadyFollowed();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  private initializeArrays() {
    this.photos = [];
    this.followees = [];
    this.followers = [];
  }

  private checkIfProfileIsMine(data: any) {
    this.loggedUser = JSON.parse(localStorage.getItem('dumplingram-user-info'));
    this.user = data['user'];
    if (this.loggedUser.id == this.user.id) {
      this.isMine = true;
    } else {
      this.isMine = false;
    }
  }

  private loadPhotos() {
    this.photoService.getPhotosForUser(this.user.id).subscribe(
      (photos) => {
        this.photos = photos;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
