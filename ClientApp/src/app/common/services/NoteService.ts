import {HttpClient} from "@angular/common/http";
import {Inject, Injectable} from "@angular/core";
import {Observable} from "rxjs/index";

@Injectable()
export class NoteService {
  constructor(private http: HttpClient, @Inject("BASE_URL") private baseUrl) {

  }

  public GetAllNotes(currentPage: number, size: number, isPublic:boolean = false): Observable<PaginatedData<Note>> {
    return this.http.get<PaginatedData<Note>>(this.baseUrl + 'api/Note/All?page=' + currentPage + '&size=' + size + '&public=' + isPublic);
  }

  public Edit(note: Note): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'api/Note/Edit', note);
  }
}
