import {Component, OnInit} from '@angular/core';
import {NoteService} from "../../common/services/NoteService";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  commentsArray: Note[] = [];

  constructor(private noteService: NoteService) {

  }

  ngOnInit() {
    this.noteService.GetPublicNotes()
      .subscribe(notes => this.commentsArray = notes);
  }
}
