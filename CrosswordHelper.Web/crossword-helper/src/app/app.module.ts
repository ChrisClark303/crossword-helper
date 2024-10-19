import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient, withInterceptorsFromDi, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';


import { AppComponent } from './app.component';
import { AnagramsComponent } from './anagrams/anagrams.component';
import { WordListComponent } from './word-list/word-list.component';
import { WordAddComponent } from './word-add/word-add.component';
import { GetHelpComponent } from './get-help/get-help.component';
import { AppRoutingModule } from './app-routing.module';
import { ContainersComponent } from './containers/containers.component';
import { RemovalsComponent } from './removals/removals.component';
import { ReversalsComponent } from './reversals/reversals.component';
import { CrosswordHelpResultsComponent } from './crossword-help-results/crossword-help-results.component';
import { LetterSelectionComponent } from './letter-selection/letter-selection.component';
import { HttpApiKeyInterceptor } from './HttpApiKeyInterceptor';


@NgModule({ declarations: [
        AppComponent,
        AnagramsComponent,
        WordListComponent,
        WordAddComponent,
        GetHelpComponent,
        ContainersComponent,
        RemovalsComponent,
        ReversalsComponent,
        CrosswordHelpResultsComponent,
        LetterSelectionComponent
    ],
    bootstrap: [AppComponent], imports: 
    [
        BrowserModule,
        FormsModule,
        AppRoutingModule], 
    providers: [
        provideHttpClient(withInterceptorsFromDi()),
        {provide: HTTP_INTERCEPTORS, useClass: HttpApiKeyInterceptor, multi: true}
    ] })
export class AppModule { }
