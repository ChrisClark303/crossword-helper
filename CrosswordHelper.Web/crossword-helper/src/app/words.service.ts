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
    return this.getMany<IndicatorWord>(`/help/anagram-indicators`);
  }

  getContainers(): Observable<IndicatorWord[]> {
    return this.getMany<IndicatorWord>(`/help/container-indicators`);
  }

  getReversals(): Observable<IndicatorWord[]> {
    return this.getMany<IndicatorWord>(`/help/reversal-indicators`);
  }

  getRemovals(): Observable<IndicatorWord[]> {
    return this.getMany<IndicatorWord>(`/help/removal-indicators`);
  }

  getLetterSelections(): Observable<IndicatorWord[]> {
    return this.getMany<IndicatorWord>(`/help/letter-selection-indicators`);
  }
}
