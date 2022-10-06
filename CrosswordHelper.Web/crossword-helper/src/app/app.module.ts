import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AnagramsComponent } from './anagrams/anagrams.component';
import { WordListComponent } from './word-list/word-list.component';
import { WordAddComponent } from './word-add/word-add.component';
import { GetHelpComponent } from './get-help/get-help.component';

@NgModule({
  declarations: [
    AppComponent,
    AnagramsComponent,
    WordListComponent,
    WordAddComponent,
    GetHelpComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
