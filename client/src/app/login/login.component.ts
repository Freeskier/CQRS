import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { InfoDialogComponent } from '../dialogs/info-dialog/info-dialog.component';
import { DialogHelperService } from '../_helpers/dialogHelper.service';
import { UserToLogin } from '../_models/userToLoginRequest';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  hide: boolean = true;
  loginFrom: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private userService: UserService,
    private snackBar: MatSnackBar
  ) {}
  
  ngOnInit(): void {
    this.loginFrom = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  login() {
    const user: UserToLogin = {
      userName: this.loginFrom.get('userName').value,
      password: this.loginFrom.get('password').value,
    };

    this.userService
      .login(user)
      .toPromise()
      .then((result) => {
        this.router.navigateByUrl('/home');
      })
      .catch((error) => {
        this.snackBar.open(error, '', {
          duration: 2000,
          horizontalPosition: 'end',
          verticalPosition: 'top',
        });
      });
  }

  redirectToRegister() {
    this.router.navigateByUrl('/register');
  }
}
