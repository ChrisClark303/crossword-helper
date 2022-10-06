import { Component, OnInit } from '@angular/core';
import { ANAGRAMS } from '../mock-anagrams';

@Component({
  selector: 'app-anagrams',
  templateUrl: './anagrams.component.html',
  styleUrls: ['./anagrams.component.css']
})
export class AnagramsComponent implements OnInit {

  anagrams = ANAGRAMS;

  constructor() { }

  ngOnInit(): void {
  }

}
