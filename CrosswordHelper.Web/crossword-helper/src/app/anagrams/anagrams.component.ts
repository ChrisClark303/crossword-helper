import { Component, OnInit } from '@angular/core';
import { WordsService } from '../words.service';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-anagrams',
  templateUrl: './anagrams.component.html',
  styleUrls: ['./anagrams.component.css']
})
export class AnagramsComponent implements OnInit {

  anagrams$: Observable<string[]>;

  getAnagrams(): void {
    this.anagrams$ =  this.wordService.getAnagrams();
  }

  onWordAdded(event:Object) : void {
    console.log("responded to wordAdded event")
    this.getAnagrams();
  }
  
  constructor(private wordService: WordsService) {
    this.anagrams$ = new Observable<string[]>;
   }

  ngOnInit(): void {
    this.getAnagrams();
  }
}
