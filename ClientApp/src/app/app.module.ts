import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './pages/home/home.component';
import {CommentListComponent} from './pages/home/comment-list/comment-list.component';
import {NoteService} from "./common/services/NoteService";
import {PaginationComponent} from './common/components/pagination/pagination.component';
import {EditComponent} from './pages/edit/edit.component';
import { AuthorizeComponent } from './pages/authorize/authorize.component';
import { AuthGuardService as AuthGuard} from './common/services/auth-guard.service';
import { AuthInterceptorService as AuthInterceptor} from './common/services/auth-interceptor.service';
import {AuthService} from "./common/services/auth.service";
import {JWT_OPTIONS, JwtHelperService} from "@auth0/angular-jwt";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CommentListComponent,
    PaginationComponent,
    EditComponent,
    AuthorizeComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard]},
      {path: 'List/:page', component: HomeComponent, canActivate: [AuthGuard]},
      {path: 'Edit/:id', component: EditComponent, canActivate: [AuthGuard]},
      {path: 'New', component: EditComponent, canActivate: [AuthGuard]},
      {path: 'Login', component: AuthorizeComponent}
    ])
  ],
  providers: [
    NoteService,
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    AuthService,
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
