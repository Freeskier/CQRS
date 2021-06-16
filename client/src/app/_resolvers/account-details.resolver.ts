import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { UserDetailResponse } from '../_models/userDetailResponse';
import { UserService } from '../_services/user.service';

@Injectable()
export class AccountDetailsResolver implements Resolve<UserDetailResponse> {
  constructor(
    private userService: UserService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<UserDetailResponse> {
    return this.userService.getSingle(route.params['id']).pipe(
      catchError((error) => {
        this.snackBar.open(error, '', {
          duration: 1000,
          horizontalPosition: 'end',
          verticalPosition: 'top',
        });
        this.router.navigateByUrl('/home');
        return of(null);
      })
    );
  }
}
