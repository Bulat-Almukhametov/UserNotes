import {Component, OnInit, Input} from "@angular/core";

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html'
})
export class CommentListComponent implements OnInit {
@Input("comments") commentsArray: Comment[];
  constructor() { }

  ngOnInit() {
  }

}
