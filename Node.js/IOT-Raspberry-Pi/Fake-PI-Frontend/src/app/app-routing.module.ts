import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    // Example: ".com/"
    path: '',
    loadChildren: () => ( import('./home/home.module') ).then( m => m.HomeModule )
  },
  {
    // Example: ".com/charts"
    path: 'charts',

    loadChildren: () => ( import('./charts/charts.module') ).then( m => m.ChartsModule )
  },
  {
    // Example: ".com/console"
    path: 'console',

    loadChildren: () => ( import('./console/console.module') ).then( m => m.ConsoleModule )
  }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
