import { Component, OnInit } from '@angular/core';
import { WordsService } from '../words.service';
import { delay, Observable, of } from 'rxjs';
import { WordType } from '../word-type';

@Component({
  selector: 'app-removals',
  templateUrl: './removals.component.html',
  styleUrls: ['./removals.component.css']
})
export class RemovalsComponent implements OnInit {

  public wordType: typeof WordType = WordType;

  removals$: Observable<string[]>;

  getRemovals(): void {
    this.removals$ =  this.wordService.getRemovals();
  }

  onWordAdded(event:Object) : void {
    console.log("responded to wordAdded event")
    delay(500);
    this.getRemovals();
  }
  
  constructor(private wordService: WordsService) {
    this.removals$ = new Observable<string[]>;
   }

  ngOnInit(): void {
    this.getRemovals();
  }
}
