import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ConsoleComponent} from './components/console/console.component';

// Base: '/console'
const routes: Routes = [
  {
    // Example ".com/console/"
    path: '',
    component: ConsoleComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConsoleRoutingModule { }
