import {Component, OnInit} from '@angular/core';
import {NoteService} from "../../common/services/NoteService";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  currentPage: number = 1;
  pageSize: number = 10;
  totalItems: number;
  commentsArray: Note[] = [];

  constructor(private noteService: NoteService,
              private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.currentPage = Number(this.route.snapshot.params.page) || 1;
    this.loadData(this.currentPage);

    this.route.params.subscribe(params => {
      this.currentPage = Number(params['page']) || 1;
      this.loadData(this.currentPage)
    });
  }

  loadData(currentPage) {
    this.noteService.GetPublicNotes(currentPage, this.pageSize)
      .subscribe(paginatedData => {
        this.commentsArray = paginatedData.data;
        this.totalItems = paginatedData.totalItems;
      });
  }
}
