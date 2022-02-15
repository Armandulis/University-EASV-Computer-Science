import { Component, OnInit } from '@angular/core';
import {Recipe} from '../../recipes/shared/recipe';
import {RecipesService} from '../../recipes/shared/recipes.service';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  recipes: Recipe[];
  constructor(private recipeService: RecipesService) { }

  ngOnInit() {
    this.recipes = this.recipeService.getAmountOfRecipes(3);
  }

}
