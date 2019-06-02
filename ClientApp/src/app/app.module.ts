import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
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
  providers: [NoteService, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule {
}
