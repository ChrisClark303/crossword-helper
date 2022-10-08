import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AnagramsComponent } from './anagrams/anagrams.component';
import { WordListComponent } from './word-list/word-list.component';
import { WordAddComponent } from './word-add/word-add.component';
import { GetHelpComponent } from './get-help/get-help.component';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    AnagramsComponent,
    WordListComponent,
    WordAddComponent,
    GetHelpComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
