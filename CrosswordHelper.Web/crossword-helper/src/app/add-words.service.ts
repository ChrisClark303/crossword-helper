import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AddWordsService {

  private serviceUrl: string = 'http://localhost:49160';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private httpClient: HttpClient) { }

  private add(url:string)  {
    console.log("Sending request to " + url)
    this.httpClient.post(url, this.httpOptions).subscribe(
      res => {
         console.log(res);
      },
      err => {
         console.log('Error occured');
      });
  }

  addAnagramIndicator(word: string)
  {
      this.add(`${this.serviceUrl}/help/anagram-indicators/${word}`);
  }

}
