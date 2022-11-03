import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServiceBase } from './service-base';

@Injectable({
  providedIn: 'root'
})
export class AddWordsService extends ServiceBase {

  constructor(httpClient: HttpClient) { super(httpClient);}

  private async addIndicator(word: string, wordType: string)
  {
      await this.add(`/help/${wordType}-indicators/${word}`);
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
