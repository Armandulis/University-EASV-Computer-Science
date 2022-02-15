import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {RecipesListComponent} from './recipes-list/recipes-list.component';
import {RecipesAddComponent} from './recipes-add/recipes-add.component';
import {RecipesDetailsComponent} from './recipes-details/recipes-details.component';
import {RecipeUpdateComponent} from './recipe-update/recipe-update.component';

const routes: Routes = [
  {
    path: '', component: RecipesListComponent
  },
  { path: 'add', component: RecipesAddComponent},
  { path: ':id', component: RecipesDetailsComponent},
  { path: 'update/:id', component: RecipeUpdateComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RecipesRoutingModule { }
