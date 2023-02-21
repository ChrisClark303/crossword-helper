import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CrosswordHelp } from '../crossword-help';
import { HelpService } from '../help-service';

@Component({
  selector: 'app-get-help',
  templateUrl: './get-help.component.html',
  styleUrls: ['./get-help.component.css']
})
export class GetHelpComponent implements OnInit {

  crosswordClue: string;
  crosswordClueHelp$: Observable<CrosswordHelp>;

  constructor(private service:HelpService) { }

  ngOnInit(): void {
  }

  getHelp() {
    this.crosswordClueHelp$ = this.service.getHelp(this.crosswordClue);
  }

}
