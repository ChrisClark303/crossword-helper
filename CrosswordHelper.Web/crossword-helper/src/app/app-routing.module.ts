import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnagramsComponent } from './anagrams/anagrams.component';
import { GetHelpComponent } from './get-help/get-help.component';

const routes: Routes = [
  { path: 'get-help', component: GetHelpComponent },
  { path: 'anagrams', component: AnagramsComponent },
  { path: '', redirectTo: '/get-help', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }