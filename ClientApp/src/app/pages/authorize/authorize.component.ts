import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html'
})
export class AuthorizeComponent implements OnInit {
  isChanged: boolean;

  constructor(private router: Router) { }

  ngOnInit() {
  }

  login () {
    window.isAuthenticated = true;
    this.router.navigate(['']);
  }

}
