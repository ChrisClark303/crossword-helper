import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ServiceBase } from './service-base';
import { IndicatorWord } from './indicator-word';

@Injectable({
  providedIn: 'root'
})
export class WordsService extends ServiceBase {

   constructor(httpClient: HttpClient) { super(httpClient); }

  getAnagrams(): Observable<IndicatorWord[]>
  {
    return this.get<IndicatorWord>(`/help/anagram-indicators`);
  }

  getContainers(): Observable<IndicatorWord[]> {
    return this.get<IndicatorWord>(`/help/container-indicators`);
  }

  getReversals(): Observable<IndicatorWord[]> {
    return this.get<IndicatorWord>(`/help/reversal-indicators`);
  }

  getRemovals(): Observable<IndicatorWord[]> {
    return this.get<IndicatorWord>(`/help/removal-indicators`);
  }
}
