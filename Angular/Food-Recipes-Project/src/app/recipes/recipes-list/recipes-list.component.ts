import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs';
import {Recipe} from '../shared/recipe';
import {Select, Store} from '@ngxs/store';
import {LoadRecipes, RecipesState, RemoveRecipe} from '../../store';

@Component({
  selector: 'app-recipes-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.scss']
})
export class RecipesListComponent implements OnInit {

  @Select(RecipesState.recipes)
  recipes: Observable<Recipe[]>;

  constructor(
    private store: Store) {

  }

  ngOnInit() {
   // this.recipes = this.store.select(state => state.recipes.recipes);
    this.store.dispatch(new LoadRecipes());
  }

  deleteRecipe(recipe: Recipe) {
    this.store.dispatch(new RemoveRecipe(recipe));
  }

}
