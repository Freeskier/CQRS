import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { PhotoResponse } from 'src/app/_models/photoResponse';

@Component({
  selector: 'app-post-preview-dialog',
  templateUrl: './post-preview-dialog.component.html',
  styleUrls: ['./post-preview-dialog.component.css']
})
export class PostPreviewDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: PostPreviewDialogData,
  public dialogRef: MatDialogRef<PostPreviewDialogComponent>,
  private router: Router) { }

  ngOnInit() {
  }
}

export interface PostPreviewDialogData {
  photo: PhotoResponse
}
