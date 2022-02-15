import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'recipes',
    loadChildren: './recipes/recipes.module#RecipesModule',
  },
  {
    path: '',
    loadChildren: './home/home.module#HomeModule',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation: 'reload'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
