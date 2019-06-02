import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(public router: Router) {}
  canActivate(): boolean {
    if (!window.isAuthenticated) {
      this.router.navigate(['Login']);
      return false;
    }
    return true;
  }
}
