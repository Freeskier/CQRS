import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../dialogs/confirm-dialog/confirm-dialog.component';
import { InfoDialogComponent } from '../dialogs/info-dialog/info-dialog.component';
import { PostPreviewDialogComponent } from '../dialogs/post-preview-dialog/post-preview-dialog.component';
import {
  FollowType,
  UserListDialogComponent,
} from '../dialogs/user-list-dialog/user-list-dialog.component';
import { PhotoResponse } from '../_models/photoResponse';
import { UserDetailResponse } from '../_models/userDetailResponse';

@Injectable({
  providedIn: 'root',
})
export class DialogHelperService {
  constructor(
    private infoDialog: MatDialog,
    private confirmDialog: MatDialog,
    private usersListDialog: MatDialog,
    private photoPreviewDialog: MatDialog
  ) {}

  openInfoDialog(text: string, timeout: number) {
    const dialogRef = this.infoDialog.open(InfoDialogComponent, {
      data: text,
      position: {
        bottom: '20px',
        right: '20px',
      },
    });

    dialogRef.afterOpened().subscribe((_) => {
      setTimeout(() => {
        dialogRef.close();
      }, timeout);
    });
  }

  openConfrimDialog(text: string, callback: () => any) {
    const dialogRef = this.confirmDialog.open(ConfirmDialogComponent, {
      width: '350px',
      data: {
        text: text,
        okCallback: () => {
          callback();
          dialogRef.close();
        },
      },
    });
  }

  openUsersListDialog(users: UserDetailResponse[], followType: FollowType) {
    this.usersListDialog.open(UserListDialogComponent, {
      width: '500px',
      data: {
        users: users,
        followType: followType,
      },
    });
  }

  openPostPreviewDialog(photo: PhotoResponse) {
    this.photoPreviewDialog.open(PostPreviewDialogComponent, {
      width: '700px',
      data: {
        photo: photo,
      },
    });
  }
}
