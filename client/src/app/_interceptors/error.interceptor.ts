import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { UserService } from '../_services/user.service';
import { Observable, throwError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private userService: UserService, private router: Router) {}

  intercept(
    request: HttpRequest<any>,
    newRequest: HttpHandler
  ): Observable<HttpEvent<any>> {
    return newRequest.handle(request).pipe(
      catchError((err) => {
        if (err.status === 401) {
          //if 401 response returned from api, logout from application & redirect to login page.
          this.userService.logout();
        }
        const error = err.error.message || err.error;
        return throwError(error);
      })
    );
  }
}
