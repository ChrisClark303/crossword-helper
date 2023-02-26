import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { IndicatorWord } from '../indicator-word';

@Component({
  selector: 'app-word-list',
  templateUrl: './word-list.component.html',
  styleUrls: ['./word-list.component.css']
})
export class WordListComponent implements OnInit {

  @Input() wordlist$: Observable<IndicatorWord[]>;
  @Input() title?:string;
  
  constructor() { }

  ngOnInit(): void {
  }

}
