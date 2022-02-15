import {Component, OnDestroy, OnInit} from '@angular/core';
import {RecipesService} from '../shared/recipes.service';
import {ActivatedRoute, Router} from '@angular/router';
import {Recipe} from '../shared/recipe';
import {FormArray, FormControl, FormGroup} from '@angular/forms';
import {ImageMetadata} from '../../files/shared/image-metadata';
import {ImageCroppedEvent} from 'ngx-image-cropper';
import {FileService} from '../../files/shared/file.service';
import {forEach} from '@angular/router/src/utils/collection';
import {CreateRecipe, UpdateRecipe} from '../../store';
import {Store} from '@ngxs/store';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-recipe-update',
  templateUrl: './recipe-update.component.html',
  styleUrls: ['./recipe-update.component.scss']
})
export class RecipeUpdateComponent implements OnInit, OnDestroy {
  recipe: Recipe;

  recipeFormGroup: FormGroup;
  ingredientsFormArray: FormArray;

  isLoading = false;

  recipeID: string;
  getRecipeWithIDSubscription: Subscription;
  constructor(private recipeService: RecipesService,
              private router: Router,
              private route: ActivatedRoute,
              private store: Store) {
  }

  ngOnInit() {
    this.getRecipe();
  }

  getRecipe() {
    this.recipeID = this.route.snapshot.paramMap.get('id');
    this.getRecipeWithIDSubscription = this.recipeService.getRecipeWithID(this.recipeID).subscribe(
      recipeDB => {
        this.recipe = recipeDB;
        this.ingredientsFormArray = new FormArray([]);

        this.recipeFormGroup = new FormGroup({
          name: new FormControl(this.recipe.name),
          type: new FormControl(this.recipe.type),
          portion: new FormControl(this.recipe.portion),
          howTo: new FormControl(this.recipe.howTo),
          ingredients: this.ingredientsFormArray,
          cookTime: new FormControl(this.recipe.cookTime)
        });
        this.setUpIngredients(recipeDB);
      }
    );
  }
  setUpIngredients(recipe: Recipe) {
    this.ingredientsFormArray = this.recipeFormGroup.get('ingredients') as FormArray;
    recipe.ingredients.forEach( ingred => {
      this.ingredientsFormArray.push(
        new FormGroup({
          name: new FormControl(ingred.name),
          amount: new FormControl(ingred.amount),
        })
      );
    });
  }

  private addIngrediantForm() {
    this.ingredientsFormArray = this.recipeFormGroup.get('ingredients') as FormArray;
    this.ingredientsFormArray.push(this.createIngredient());
  }

  private createIngredient(): FormGroup {
    return new FormGroup({
      name: new FormControl(''),
      amount: new FormControl(''),
    });
  }
  removeIngredient(ingridientToRemove: number) {
    this.ingredientsFormArray.removeAt(ingridientToRemove);
  }
  updateRecipe() {
    this.recipe = this.recipeFormGroup.value;
    this.recipe.id = this.recipeID;

    this.store.dispatch(new UpdateRecipe(this.recipe)).subscribe();

    this.router.navigate(['../../' + this.recipe.id],
      {relativeTo: this.route});

  }
  ngOnDestroy(): void {
    this.getRecipeWithIDSubscription.unsubscribe();
  }
}
