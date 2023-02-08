import { Component, OnInit } from '@angular/core';
import { WordsService } from '../words.service';
import { delay, Observable, of } from 'rxjs';
import { WordType } from '../word-type';
import { IndicatorWord } from '../indicator-word';

@Component({
  selector: 'app-anagrams',
  templateUrl: './anagrams.component.html',
  styleUrls: ['./anagrams.component.css']
})
export class AnagramsComponent implements OnInit {

  public wordType: typeof WordType = WordType;

  anagrams$: Observable<IndicatorWord[]>;

  getAnagrams(): void {
    this.anagrams$ =  this.wordService.getAnagrams();
  }

  onWordAdded(event:Object) : void {
    console.log("responded to wordAdded event")
    delay(500);
    this.getAnagrams();
  }
  
  constructor(private wordService: WordsService) {
    this.anagrams$ = new Observable<IndicatorWord[]>;
   }

  ngOnInit(): void {
    this.getAnagrams();
  }
}
