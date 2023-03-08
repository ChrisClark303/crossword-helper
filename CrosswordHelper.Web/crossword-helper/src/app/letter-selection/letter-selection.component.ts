import { Component, OnInit } from '@angular/core';
import { WordsService } from '../words.service';
import { delay, Observable, of } from 'rxjs';
import { WordType } from '../word-type';
import { IndicatorWord } from '../indicator-word';

@Component({
  selector: 'app-removals',
  templateUrl: './letter-selection.component.html',
  styleUrls: ['./letter-selection.component.css']
})
export class LetterSelectionComponent implements OnInit {

  public wordType: typeof WordType = WordType;

  letterSelections$: Observable<IndicatorWord[]>;

  getLetterSelections(): void {
    this.letterSelections$ =  this.wordService.getLetterSelections();
  }

  onWordAdded(event:Object) : void {
    console.log("responded to wordAdded event")
    delay(500);
    this.getLetterSelections();
  }
  
  constructor(private wordService: WordsService) {
    this.letterSelections$ = new Observable<IndicatorWord[]>;
   }

  ngOnInit(): void {
    this.getLetterSelections();
  }
}
