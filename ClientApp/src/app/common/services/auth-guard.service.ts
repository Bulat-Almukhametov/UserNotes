import {Injectable} from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import {AuthService} from "./auth.service";
@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(public router: Router,
              private  authService: AuthService) {}

  canActivate(): boolean {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['Login']);
      return false;
    }
    return true;
  }
}
