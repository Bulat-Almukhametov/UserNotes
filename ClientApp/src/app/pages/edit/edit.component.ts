import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";

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
  private isChanged: boolean;

  constructor(private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.id = Number(this.route.snapshot.params.id);
    if (this.id) {
      this.editMode = "Edit";
      this.loadData(this.id);
    }
    else {
      this.editMode = "New";
    }
  }

  loadData (id: number) {

  }
}
