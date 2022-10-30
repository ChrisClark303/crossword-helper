import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class WordsService {

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  private serviceUrl: string = 'http://localhost:5144';

  constructor(private httpClient: HttpClient) { }

  get<T>(url:string): Observable<T[]> {
    return this.httpClient.get<T[]>(url);
  }

  getAnagrams(): Observable<string[]>
  {
    return this.get<string>(`${this.serviceUrl}/help/anagram-indicators`);
  }

  getContainers(): Observable<string[]> {
    return this.get<string>(`${this.serviceUrl}/help/container-indicators`);
  }

  getReversals(): Observable<string[]> {
    return this.get<string>(`${this.serviceUrl}/help/reversals-indicators`);
  }

  getRemovals(): Observable<string[]> {
    return this.get<string>(`${this.serviceUrl}/help/removals-indicators`);
  }
}