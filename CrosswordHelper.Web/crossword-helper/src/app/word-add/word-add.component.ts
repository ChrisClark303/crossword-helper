import { Component, OnInit, NgModule, EventEmitter, Output } from '@angular/core';
import { AddWordsService } from '../add-words.service';

@Component({
  selector: 'app-word-add',
  templateUrl: './word-add.component.html',
  styleUrls: ['./word-add.component.css']
})
export class WordAddComponent implements OnInit {

  constructor(private addWordsService: AddWordsService) { }
 
  @Output() wordAdded = new EventEmitter<boolean>();
  wordToAdd: string;

  ngOnInit(): void {
  }

  addAnagram() {
    this.addWordsService.addAnagramIndicator(this.wordToAdd);
    this.wordAdded.emit(true);
    this.wordToAdd = "";
  }

}
