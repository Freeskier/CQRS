import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { ConfirmDialogComponent } from '../dialogs/confirm-dialog/confirm-dialog.component';
import { DialogHelperService } from '../_helpers/dialogHelper.service';
import { UserDetailResponse } from '../_models/userDetailResponse';
import { UserService } from '../_services/user.service';

export interface State {
  flag: string;
  name: string;
  population: string;
}

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css'],
})
export class ToolbarComponent implements OnInit {
  
  loggedUser: UserDetailResponse;

  searchInput = new FormControl();
  foundUsers: UserDetailResponse[] = [];

  keyWord = new FormControl('');


  constructor(
    private userService: UserService,
    private router: Router,
    private dialogHelper: DialogHelperService
  ) {
    this.searchInput.valueChanges.subscribe(() => {
      if (this.keyWord.value.length <= 3) this.foundUsers = [];
      this.getUsersWithKeyWord(this.searchInput.value);
    });
  }

  ngOnInit(): void {
    this.loggedUser = JSON.parse(localStorage.getItem('dumplingram-user-info'));
  }

  public logout() {
    this.dialogHelper.openConfrimDialog('Are you sure to sign out?', () => {
      this.userService.logout();
      this.loggedUser = null;
      this.router.navigateByUrl('/login');
    });
  }

  public isLoggedIn(): boolean {
    if (localStorage.getItem('dumplingram-token-info')) return true;
    return false;
  }

  public myProfileClick() {
    this.router.navigateByUrl(`/profile/${this.loggedUser.id}`);
  }

  public searchItemClick(id: string) {
    this.router.navigateByUrl(`/profile/${id}`);
  }

  public navigateHome() {
    this.router.navigateByUrl('/home')
  }

  private getUsersWithKeyWord(keyWord: string) {
    if (keyWord.length >= 3) {
      this.userService.search(keyWord).subscribe(
        (users) => {
          this.foundUsers = users;
        },
        (error) => {
          console.log(error);
        }
      );
    }
    
  }
}
