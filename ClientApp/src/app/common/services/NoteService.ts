import {HttpClient} from "@angular/common/http";
import {Inject, Injectable} from "@angular/core";
import {Observable} from "rxjs/index";

@Injectable()
export class NoteService {
  constructor (private http: HttpClient, @Inject("BASE_URL")private baseUrl){

  }

  public GetPublicNotes(): Observable<Note[]>{
    return this.http.get<Note[]>(this.baseUrl + 'api/Note/AllPublic');
  }
}
