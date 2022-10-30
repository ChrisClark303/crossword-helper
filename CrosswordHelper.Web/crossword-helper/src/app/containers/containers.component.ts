import { Component, OnInit } from '@angular/core';
import { WordsService } from '../words.service';
import { delay, Observable, of } from 'rxjs';
import { WordType } from '../word-type';

@Component({
  selector: 'app-containers',
  templateUrl: './containers.component.html',
  styleUrls: ['./containers.component.css']
})
export class ContainersComponent implements OnInit {

  public wordType: typeof WordType = WordType;

  containers$: Observable<string[]>;

  getContainers(): void {
    this.containers$ =  this.wordService.getContainers();
  }

  onWordAdded(event:Object) : void {
    console.log("responded to wordAdded event")
    delay(500);
    this.getContainers();
  }
  
  constructor(private wordService: WordsService) {
    this.containers$ = new Observable<string[]>;
   }

  ngOnInit(): void {
    this.getContainers();
  }
}
