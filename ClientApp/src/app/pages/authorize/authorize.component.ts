import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {JwtHelperService} from '@auth0/angular-jwt';
import {AuthService} from "../../common/services/auth.service";

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html'
})
export class AuthorizeComponent implements OnInit {
  isChanged: boolean;
  credentials: any = {
    nickname: "",
    password: ""
  };

  constructor(private router: Router,
              private jwtHelper: JwtHelperService,
              private auth: AuthService) {
  }

  ngOnInit() {
  }

  login() {
    let credentials = this.credentials;
    let context = this;
    this.auth.authenticate(credentials.nickname, credentials.password)
      .subscribe(token => context.router.navigate(['']), response => this.showError(<string>response.error));
  }

  showError(error: string) {
    alert(error);
  }
}
