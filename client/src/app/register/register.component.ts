import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormGroupDirective,
  NgForm,
  Validators,
} from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { InfoDialogComponent } from '../dialogs/info-dialog/info-dialog.component';
import { DialogHelperService } from '../_helpers/dialogHelper.service';
import { MyErrorStateMatcher } from '../_helpers/myErrorStateMatcher';
import { UserToRegisterRequest } from '../_models/userToRegisterRequest';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  hide = true;
  hideConfirm = true;
  registerForm: FormGroup;
  passwordMatcher = new MyErrorStateMatcher();
  emailMatcher = new MyErrorStateMatcher();

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private dialogHelper: DialogHelperService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group(
      {
        email: ['', [Validators.required, Validators.email]],
        confirmEmail: ['', [Validators.required, Validators.email]],
        userName: [
          '',
          [
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(20),
          ],
        ],
        firstName: ['', [Validators.required, Validators.maxLength(20)]],
        lastName: ['', [Validators.required, Validators.maxLength(20)]],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(30),
          ],
        ],
        confirmPassword: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(30),
          ],
        ],
        birthDate: [Validators.required],
        city: ['', [Validators.required, Validators.maxLength(20)]],
      },
      { validators: [this.checkPasswords, this.checkEmails] }
    );
  }

  public getErrorMessage(path: string) {
    if (this.registerForm.hasError('required', path)) {
      return 'Field is required.';
    }

    if (this.registerForm.hasError('minlength', path)) {
      return 'Field must be minimum 4 length.';
    }

    if (this.registerForm.hasError('maxlength', path)) {
      return 'Field must be maximum 20 length.';
    }

    if (this.registerForm.hasError('email', path)) {
      return "E-mail address isn't correct.";
    }
  }

  public getErrorMessagePassword(path: string) {
    if (this.registerForm.hasError('required', path)) {
      return 'Field is required.';
    }

    if (this.registerForm.hasError('minlength', path)) {
      return 'Field must be minimum 6 length.';
    }

    if (this.registerForm.hasError('maxlength', path)) {
      return 'Field must be maximum 30 length.';
    }
  }

  public register() {
    let user: any = {};

    Object.assign(user, this.registerForm.value);

    this.userService
      .register(user)
      .toPromise()
      .then(() => {
        const firstName = this.registerForm.get('firstName').value;

        this.snackBar.open(
          `Congratulations ${firstName}.\n
        You're now member of dumplings community.`,
          '',
          {
            duration: 2000,
            horizontalPosition: 'end',
            verticalPosition: 'top',
          }
        );
      })
      .catch((error) => {
        this.snackBar.open(error, '', {
          duration: 2000,
          horizontalPosition: 'end',
          verticalPosition: 'top',
        });
      });
  }

  private checkPasswords(group: FormGroup) {
    // here we have the 'passwords' group
    const password = group.get('password').value;
    const confirmPassword = group.get('confirmPassword').value;

    return password === confirmPassword ? null : { notSamePasswords: true };
  }

  private checkEmails(group: FormGroup) {
    // here we have the 'passwords' group
    const email = group.get('email').value;
    const confirmEmail = group.get('confirmEmail').value;

    return email === confirmEmail ? null : { notSameEmails: true };
  }
}
