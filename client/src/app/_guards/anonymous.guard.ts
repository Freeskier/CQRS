import { Injectable } from '@angular/core';
import {
  Router,
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';

@Injectable()
export class AnonymousGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    //If token data exist, user may login to application
    if (
      localStorage.getItem('dumplingram-token-info') &&
      localStorage.getItem('dumplingram-user-info')
    ) {
        this.router.navigateByUrl('/home');
      return false;
    }

    // otherwise redirect to login page with the return url
    
    return true;
  }
}