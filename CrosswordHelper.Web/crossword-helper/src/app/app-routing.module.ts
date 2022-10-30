import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnagramsComponent } from './anagrams/anagrams.component';
import { ContainersComponent } from './containers/containers.component';
import { GetHelpComponent } from './get-help/get-help.component';
import { RemovalsComponent } from './removals/removals.component';
import { ReversalsComponent } from './reversals/reversals.component';

const routes: Routes = [
  { path: 'get-help', component: GetHelpComponent },
  { path: 'anagrams', component: AnagramsComponent },
  { path: 'containers', component: ContainersComponent },
  { path: 'removals', component: RemovalsComponent },
  { path: 'reversals', component: ReversalsComponent },
  { path: '', redirectTo: '/get-help', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }