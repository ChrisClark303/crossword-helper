import { Component, OnInit, NgModule, EventEmitter, Output, Input } from '@angular/core';
import { AddWordsService } from '../add-words.service';
import { WordType } from '../word-type';

@Component({
  selector: 'app-word-add',
  templateUrl: './word-add.component.html',
  styleUrls: ['./word-add.component.css']
})
export class WordAddComponent implements OnInit {

  constructor(private addWordsService: AddWordsService) { }
 
  @Input() addWordType: WordType;
  @Output() wordAdded = new EventEmitter<boolean>();
  wordToAdd: string;

  ngOnInit(): void {
  }

  addWord() {
    this.callWordsService(this.wordToAdd, this.addWordType);
    this.wordToAdd = "";
  }

  async callWordsService(word:string, wordType:WordType) {
    switch (wordType)
    {
        case WordType.Anagram:
          await this.addWordsService.addAnagramIndicator(word);
          break;
        case WordType.Container:
          await this.addWordsService.addContainerIndicator(word);
          break;
        case WordType.Removal:
          await this.addWordsService.addRemovalIndicator(word);
          break;
        case WordType.Reversal:
          await this.addWordsService.addReversalIndicator(word);
          break;
        case WordType.LetterSelections:
          await this.addWordsService.addLetterSelectionIndicator(word);
          break;
    }
    this.wordAdded.emit(true);
  }
}
