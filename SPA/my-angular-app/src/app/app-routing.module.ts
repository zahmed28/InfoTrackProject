import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchComponent } from './search/search.component';
import { SearchHistoryComponent } from './search-history/search-history.component';

const routes: Routes = [
  { path: 'search', component: SearchComponent },
  { path: 'search-history', component: SearchHistoryComponent },
  { path: '', redirectTo: '/search-history', pathMatch: 'full' }, // default route
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
