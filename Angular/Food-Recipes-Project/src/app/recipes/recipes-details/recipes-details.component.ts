
import {Component, OnDestroy, OnInit} from '@angular/core';
import {Recipe} from '../shared/recipe';
import {RecipesService} from '../shared/recipes.service';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {Observable, Subscription} from 'rxjs';
import {Select} from '@ngxs/store';
import {RecipesState} from '../../store';
@Component({
  selector: 'app-recipes-details',
  templateUrl: './recipes-details.component.html',
  styleUrls: ['./recipes-details.component.scss']
})
export class RecipesDetailsComponent implements OnInit, OnDestroy {

  @Select(RecipesState.recipes)
  recipes: Observable<Recipe[]>;
  recipe: Recipe;
  recipeID: string;
  searchSubscription: Subscription;
  getRecipeWithIDSubscription: Subscription;
  constructor(private recipeService: RecipesService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.searchSubscription = this.route.params.subscribe(
      (params: Params) => {
        this.getRecipe();
      });
  }

  getRecipe() {
    this.recipeID = this.route.snapshot.paramMap.get('id');
    this.getRecipeWithIDSubscription = this.recipeService.getRecipeWithID(this.recipeID).subscribe(
        recipeDB => {
          this.recipe = recipeDB;
          this.recipe.id = this.recipeID;
        }
      );
  }

  ngOnDestroy(): void {
    this.searchSubscription.unsubscribe();
    this.getRecipeWithIDSubscription.unsubscribe();
  }
}
