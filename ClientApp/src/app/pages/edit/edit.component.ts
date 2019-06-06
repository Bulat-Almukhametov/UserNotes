import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {NoteService} from "../../common/services/NoteService";

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html'
})
export class EditComponent implements OnInit {
  /**
   * Text for user describing edit mode.
   */
  private editMode: string;
  private id: number;

  private note: Note;
  private oldNoteString: string;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private noteService: NoteService) {
  }

  ngOnInit() {
    this.id = Number(this.route.snapshot.params.id);
    if (this.id) {
      this.editMode = "Edit";
      this.loadData(this.id);
    }
    else {
      this.editMode = "New";
      this.note = <Note>{
        title: "",
        body: "",
        isPublic: true
      };
      this.oldNoteString = JSON.stringify(this.note);
    }
  }

  loadData(id: number) {

  }

  isChanged() {
    return JSON.stringify(this.note) != this.oldNoteString;
  }

  submitNote() {
    this.noteService.Edit(this.note)
      .subscribe(() => this.router.navigate(['/']),
      response => alert('Sending request error.'));
  }

  restoreOldNote() {
    this.note = JSON.parse(this.oldNoteString);
  }
}
