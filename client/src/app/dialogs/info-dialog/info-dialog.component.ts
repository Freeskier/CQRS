import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-info-dialog',
  templateUrl: './info-dialog.component.html',
  styleUrls: ['./info-dialog.component.css'],
})
export class InfoDialogComponent implements OnInit {
  constructor(
    @Inject(MAT_DIALOG_DATA) public text: string,
    public dialogRef: MatDialogRef<InfoDialogComponent>
  ) {}
  ngOnInit() {}

  closeDialog(): void {
    this.dialogRef.close();
  }
}
