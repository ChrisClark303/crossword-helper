import { Component, OnInit } from '@angular/core';
import { WordsService } from '../words.service';
import { delay, Observable, of } from 'rxjs';
import { WordType } from '../word-type';
import { IndicatorWord } from '../indicator-word';

@Component({
  selector: 'app-containers',
  templateUrl: './containers.component.html',
  styleUrls: ['./containers.component.css']
})
export class ContainersComponent implements OnInit {

  public wordType: typeof WordType = WordType;

  containers$: Observable<IndicatorWord[]>;

  getContainers(): void {
    this.containers$ =  this.wordService.getContainers();
  }

  onWordAdded(event:Object) : void {
    console.log("responded to wordAdded event")
    delay(500);
    this.getContainers();
  }
  
  constructor(private wordService: WordsService) {
    this.containers$ = new Observable<IndicatorWord[]>;
   }

  ngOnInit(): void {
    this.getContainers();
  }
}
