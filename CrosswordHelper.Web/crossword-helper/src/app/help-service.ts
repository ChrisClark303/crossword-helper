import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ServiceBase } from './service-base';
import { CrosswordHelp } from './crossword-help';


@Injectable({
  providedIn: 'root'
})
export class HelpService extends ServiceBase {

  constructor(httpClient: HttpClient) { super(httpClient); }

  getHelp(clue:string): Observable<CrosswordHelp[]>
  {
    return this.get<CrosswordHelp>(`/help/${clue}`);
  }
}
