import {async, ComponentFixture, TestBed, tick} from '@angular/core/testing';

import { HomeComponent } from './home.component';
import {DOMHelper} from '../../../testing/dom-helper';
import {RecipesAddComponent} from '../../recipes/recipes-add/recipes-add.component';
import {of} from 'rxjs';
import {CarouselModule} from 'ngx-bootstrap';
import {Recipe} from '../../recipes/shared/recipe';
import {RouterTestingModule} from '@angular/router/testing';
import {RecipesService} from '../../recipes/shared/recipes.service';
import {Location} from '@angular/common';
import {Router} from '@angular/router';
import { Helper } from '../../../testing/recipes-helper';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let domHelper: DOMHelper<HomeComponent>;
  let recipesServiceMock: any;
  let helper: Helper;

  beforeEach(async(() => {

    helper = new Helper();
    recipesServiceMock = jasmine.createSpyObj('RecipeService', ['getAmountOfRecipes']);
    recipesServiceMock.getAmountOfRecipes.and.returnValues([]);
    TestBed.configureTestingModule({
      declarations: [ HomeComponent ],
      providers: [
        {provide: RecipesService, useValue: recipesServiceMock},
      ],
      imports: [
        CarouselModule.forRoot(),
        RouterTestingModule,
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    domHelper = new DOMHelper(fixture);
  });

  describe('Simple HTML Tests', () => {
    it('should contain 1 <p> for websites description', () => {
      expect(domHelper.getAllOfElementsByTag('p').length).toBeGreaterThanOrEqual(1);
    });
    it('should contain 1 carousel', () => {
      expect(domHelper.getAllOfElementsByTag('carousel').length).toBe(1);
    });
    it('should contain 1 slide inside 1 carousel', () => {
      expect(domHelper.getAllOfElementsByTag('slide').length).toBe(1);
    });
    it('should contain 1 h4 tag inside  slide', () => {
      expect(domHelper.getAllOfElementsByTag('h4').length).toBe(1);
    });
    it('should contain h5 tag inside recipe examples', () => {
      recipesServiceMock.getAmountOfRecipes.and.returnValue(helper.createRecipes(1));
      component.ngOnInit();
      fixture.detectChanges();
      expect(domHelper.getAllOfElementsByTag('h5').length).toBe(1);
    });
    it('should contain 5 h5 tag inside recipe examples', () => {
      recipesServiceMock.getAmountOfRecipes.and.returnValue(helper.createRecipes(5));
      component.ngOnInit();
      fixture.detectChanges();
      expect(domHelper.getAllOfElementsByTag('h5').length).toBe(5);
    });
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('load page and OnInit', () => {
    it('should call getAmountOfRecipes once on ngOnInit', () => {
      fixture.detectChanges();
      expect(recipesServiceMock.getAmountOfRecipes).toHaveBeenCalledTimes(1);
    });
    it('should show atleast 2 img tags, for 1 recipe when cereals with image url is loaded from cereals service', () => {
      recipesServiceMock.getAmountOfRecipes.and.returnValue(helper.createRecipes(1));
      helper.recipesList[0].url = 'asd';
      component.ngOnInit();
      fixture.detectChanges();
      expect(domHelper.getAllOfElementsByTag('img').length).toBeGreaterThanOrEqual(2);
    });
    it('should show atleast 4 img tags, for 2 recipe when cereals with image url is loaded from cereals service', () => {
      recipesServiceMock.getAmountOfRecipes.and.returnValue(helper.createRecipes(2));
      helper.recipesList[0].url = 'asd';
      helper.recipesList[1].url = 'asd';
      component.ngOnInit();
      fixture.detectChanges();
      expect(domHelper.getAllOfElementsByTag('img').length).toBeGreaterThanOrEqual(4);
    });
    it('should show no img tags when recipes with no image url is loaded onInit from recipe service', () => {
      recipesServiceMock.getAmountOfRecipes.and.returnValue(helper.createRecipes(1));
      helper.recipesList[0].picture = undefined;
      helper.recipesList[0].url = undefined;
      component.ngOnInit();
      fixture.detectChanges();
      expect(domHelper.getAllOfElementsByTag('img').length).toBe(0);
    });
  });
});

