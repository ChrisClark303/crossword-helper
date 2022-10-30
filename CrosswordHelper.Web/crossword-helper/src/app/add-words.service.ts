import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AddWordsService {

  private serviceUrl: string = 'http://localhost:5144';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private httpClient: HttpClient) { }

  private async add(url:string)  {
    console.log("Sending request to " + url)
    this.httpClient.post(url, this.httpOptions).subscribe(
      res => {
         console.log(res);
      },
      err => {
         console.log('Error occured');
      });
  }

  private async addIndicator(word: string, wordType: string)
  {
    console.log(`ServiceUrl: ${this.serviceUrl}`);
      await this.add(`${this.serviceUrl}/help/${wordType}-indicators/${word}`);
  }

  async addAnagramIndicator(word: string)
  {
      await this.addIndicator(word, "anagram");
  }

  async addContainerIndicator(word: string)
  {
    await this.addIndicator(word, "container");
  }

  async addReversalIndicator(word: string)
  {
     await this.addIndicator(word, "reversal");
  }

  async addRemovalIndicator(word: string)
  {
      await this.addIndicator(word, "removal");
  }
}
