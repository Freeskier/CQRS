import { Inject } from '@angular/core';
import { Component, Input, OnInit } from '@angular/core';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { Router } from '@angular/router';
import { UserDetailResponse } from 'src/app/_models/userDetailResponse';

@Component({
  selector: 'app-user-list-dialog',
  templateUrl: './user-list-dialog.component.html',
  styleUrls: ['./user-list-dialog.component.css'],
})
export class UserListDialogComponent implements OnInit {
  
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: UsersArrayDialogData,
    public dialogRef: MatDialogRef<UserListDialogComponent>,
    private router: Router
  ) {}

  ngOnInit() {}

  public navigateToProfile(id: string) {
    this.router.navigateByUrl(`/profile/${id}`);
    this.dialogRef.close();
  }
}

export interface UsersArrayDialogData {
  users: UserDetailResponse[];
  followType: FollowType
}

export enum FollowType {
  followers,
  followees
}
