import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ServiceBase } from './service-base';

@Injectable({
  providedIn: 'root'
})
export class WordsService extends ServiceBase {

   constructor(httpClient: HttpClient) { super(httpClient); }

  getAnagrams(): Observable<string[]>
  {
    return this.get<string>(`/help/anagram-indicators`);
  }

  getContainers(): Observable<string[]> {
    return this.get<string>(`/help/container-indicators`);
  }

  getReversals(): Observable<string[]> {
    return this.get<string>(`/help/reversal-indicators`);
  }

  getRemovals(): Observable<string[]> {
    return this.get<string>(`/help/removal-indicators`);
  }
}
