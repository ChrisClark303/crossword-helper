import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-word-list',
  templateUrl: './word-list.component.html',
  styleUrls: ['./word-list.component.css']
})
export class WordListComponent implements OnInit {

  @Input() wordlist?: string[];
  @Input() title?:string;

  constructor() { }

  ngOnInit(): void {
  }

}
