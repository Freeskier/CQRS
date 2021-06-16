import { Injectable } from '@angular/core';
import {
  Router,
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    //If token data exist, user may login to application
    if (
      localStorage.getItem('dumplingram-token-info') &&
      localStorage.getItem('dumplingram-user-info')
    ) {
      return true;
    }

    // otherwise redirect to login page with the return url
    this.router.navigateByUrl('/login');
    return false;
  }
}
