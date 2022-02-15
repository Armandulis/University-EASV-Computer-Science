import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ChartsComponent} from './components/charts/charts.component';

// Base: '/charts'
const routes: Routes = [
  {
    // Example ".com/console/"
    path: '',
    component: ChartsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ChartsRoutingModule { }
