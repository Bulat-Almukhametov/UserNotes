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

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CommentListComponent,
    PaginationComponent,
    EditComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'List/:page', component: HomeComponent},
      {path: 'Edit/:id', component: EditComponent},
      {path: 'New', component: EditComponent}
    ])
  ],
  providers: [NoteService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
