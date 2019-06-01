import {Component} from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  commentsArray: Note[] = [];

  constructor() {

    let users: User[] = [
      {nickname: "MickyMouse", admin: false},
      {nickname: "DonaldDuck", admin: false},
      {nickname: "WaltDisney", admin: true},
      {nickname: "Pluto", admin: false},
    ];

    for (let i = 0; i < 10; i++) {
      let i = Math.round(Math.random() * (users.length - 1))
      this.commentsArray.push({
        user: users[i],
        title: "Comment " + (i ),
        body: users[i].nickname + ' says some interestin thing: "So, I think bla bla bla!"',
        createTime: new Date()
      });
    }
  }

}
