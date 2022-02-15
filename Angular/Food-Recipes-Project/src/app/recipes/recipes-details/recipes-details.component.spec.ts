import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipesDetailsComponent } from './recipes-details.component';
import {of} from 'rxjs';
import {DOMHelper} from '../../../testing/dom-helper';
import {RecipeUpdateComponent} from '../recipe-update/recipe-update.component';
import {RecipesService} from '../shared/recipes.service';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterTestingModule} from '@angular/router/testing';
import {Recipe} from '../shared/recipe';
import {Helper} from '../../../testing/recipes-helper';
import {Location} from '@angular/common';
import {Router} from '@angular/router';

describe('RecipesDetailsComponent', () => {
  let component: RecipesDetailsComponent;
  let fixture: ComponentFixture<RecipesDetailsComponent>;
  let domHelper: DOMHelper<RecipesDetailsComponent>;
  let recipesServiceMock: any;
  let helper: Helper;

  beforeEach(async(() => {
    helper = new Helper();
    recipesServiceMock = jasmine.createSpyObj('RecipeService', ['getRecipeWithID']);
    recipesServiceMock.getRecipeWithID.and.returnValues(of([helper.createRecipes(2)]));
    TestBed.configureTestingModule({
      declarations: [ RecipesDetailsComponent ],
      providers: [
        {provide: RecipesService, useValue: recipesServiceMock},
      ],
      imports: [
        ReactiveFormsModule,
        FormsModule,
        RouterTestingModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecipesDetailsComponent);
    component = fixture.componentInstance;
    domHelper = new DOMHelper(fixture);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  describe('Simple HTML Tests', () => {
    it('should contain 1 <p> for websites description', () => {
      helper.createRecipes(1);
      component.recipe = helper.recipesList[0];
      fixture.detectChanges();
      expect(domHelper.getAllOfElementsByTag('p').length).toBeGreaterThanOrEqual(1);
    });
    it('should contain 1 img tag', () => {
      helper.createRecipes(1);
      component.recipe = helper.recipesList[0];
      fixture.detectChanges();
      expect(domHelper.getAllOfElementsByTag('img').length).toBe(1);
    });
    it('should contain at least 4<p>, 3 for info and 1 for instructions', () => {
      expect(domHelper.getAllOfElementsByTag('p').length).toBe(4);
    });
    it('should contain at least 3 h5, 1 for each card as a title', () => {
      expect(domHelper.getAllOfElementsByTag('h5').length).toBe(3);
    });
    it('should contain an update button', () => {
      expect(domHelper.getWithElementsContainingText('button', 'Update Recipe').length).toBe(1);
    });
    it('should a strong tag with Approximately cook time:', () => {
      expect(domHelper.getWithElementsContainingText('strong', 'Approximately cook time:').length).toBe(1);
    });
    it('should a strong tag with For:', () => {
      expect(domHelper.getWithElementsContainingText('strong', 'For:').length).toBe(1);
    });
    it('should a strong tag with Type:', () => {
      expect(domHelper.getWithElementsContainingText('strong', 'Type:').length).toBe(1);
    });
    it('should show no img tags when recipes with no image url is loaded onInit from recipe service', () => {
      component.recipe = helper.createRecipes(1)[0];
      helper.recipesList[0].url = undefined;
      fixture.detectChanges();
      expect(domHelper.getAllOfElementsByTag('img').length).toBe(0);
    });
  });
  describe('Load Page', () => {
    it('should call service to get recipe with an id', () => {
      expect(recipesServiceMock.getRecipeWithID).toHaveBeenCalledTimes(1);
    });
    it('should get Recipe should be called once', () => {
      spyOn(component, 'getRecipe');
      component.ngOnInit();
      fixture.detectChanges();
      expect(component.getRecipe).toHaveBeenCalledTimes(1);
    });
    it('should have 1 img tag after oninit, for recipe with image', () => {
      helper.createRecipes(1);
      component.recipe = helper.recipesList[0];
      fixture.detectChanges();
      expect(domHelper.getAllOfElementsByTag('img').length).toBe(1);
    });
    it('should have no img tag after oninit, for recipe with no image', () => {
      helper.createRecipes(1);
      helper.recipesList[0].url = undefined;
      component.recipe = helper.recipesList[0];
      fixture.detectChanges();
      expect(domHelper.getAllOfElementsByTag('img').length).toBe(0);
    });


  });
});

