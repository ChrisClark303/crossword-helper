import { Component, OnInit } from '@angular/core';
import { WordsService } from '../words.service';
import { delay, Observable, of } from 'rxjs';
import { WordType } from '../word-type';
import { IndicatorWord } from '../indicator-word';

@Component({
  selector: 'app-reversals',
  templateUrl: './reversals.component.html',
  styleUrls: ['./reversals.component.css']
})
export class ReversalsComponent implements OnInit {

  public wordType: typeof WordType = WordType;

  reversals$: Observable<IndicatorWord[]>;

  getReversals(): void {
    this.reversals$ =  this.wordService.getReversals();
  }

  onWordAdded(event:Object) : void {
    console.log("responded to wordAdded event")
    delay(500);
    this.getReversals();
  }
  
  constructor(private wordService: WordsService) {
    this.reversals$ = new Observable<IndicatorWord[]>;
   }

  ngOnInit(): void {
    this.getReversals();
  }
}
