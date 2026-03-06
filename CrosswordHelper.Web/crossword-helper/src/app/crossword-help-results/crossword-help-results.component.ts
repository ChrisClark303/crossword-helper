import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CrosswordHelp } from '../crossword-help';

@Component({
    selector: 'app-crossword-help-results',
    templateUrl: './crossword-help-results.component.html',
    styleUrls: ['./crossword-help-results.component.css'],
    standalone: false
})
export class CrosswordHelpResultsComponent implements OnInit {

  @Input() crosswordHelp$: Observable<CrosswordHelp>;

  constructor() { }

  ngOnInit(): void {
  }

}
