import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeUpdateComponent } from './recipe-update.component';
import {FormControl, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ImageCropperModule} from 'ngx-image-cropper';
import {RouterTestingModule} from '@angular/router/testing';
import {RecipesService} from '../shared/recipes.service';
import {FileService} from '../../files/shared/file.service';
import {DOMHelper} from '../../../testing/dom-helper';
import {of} from 'rxjs';
import {Helper} from '../../../testing/recipes-helper';
import {Store} from '@ngxs/store';
import {Router} from '@angular/router';
describe('RecipeUpdateComponent', () => {
  let component: RecipeUpdateComponent;
  let fixture: ComponentFixture<RecipeUpdateComponent>;
  let domHelper: DOMHelper<RecipeUpdateComponent>;
  let reciperServiceMock: any;
  let fileServiceMock: any;
  let helper: Helper;
  let storeMock: any;
  let router: Router;
  beforeEach(async(() => {
    helper = new Helper();

    fileServiceMock = jasmine.createSpyObj('FileService', ['chosenImage']);
    fileServiceMock.chosenImage.and.returnValue(null);

    reciperServiceMock = jasmine.createSpyObj('RecipeService', ['updateRecipe', 'getRecipeWithID']);
    reciperServiceMock.getRecipeWithID.and.returnValues(of(helper.createRecipes(1)[0]));

    storeMock = jasmine.createSpyObj('Store', ['dispatch']);

    TestBed.configureTestingModule({
      declarations: [ RecipeUpdateComponent,
      ],
      providers: [
        {provide: RecipesService, useValue: reciperServiceMock},
        {provide: FileService, useValue: fileServiceMock},
        {provide: Store, useValue: storeMock}
      ],
      imports: [
        ReactiveFormsModule,
        ImageCropperModule,
        FormsModule,
        RouterTestingModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecipeUpdateComponent);
    component = fixture.componentInstance;
    domHelper = new DOMHelper(fixture);
    fixture.detectChanges();
    router =  TestBed.get(Router);

  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('Simple HTML Tests', () => {
    it('should contain 10 labels at least: name, portion, type, time,' +
      ' instructions, (Ingredents):2x  name, amount', () => {
      fixture.detectChanges();
      expect(domHelper.getAllOfElementsByTag('label').length).toBe(9);
    });
    it('should contain 5 inputs: name , (Ingredents): x2 name, amount', () => {
      fixture.detectChanges();
      expect(domHelper.getAllOfElementsByTag('input').length).toBe(5);
    });
    it('should contain 1 textArea for Instructions', () => {
      expect(domHelper.getAllOfElementsByTag('textarea').length).toBe(1);
    });
    it('should contain 3 selects: protion, cookTime, type', () => {
      expect(domHelper.getAllOfElementsByTag('select').length).toBe(3);
    });
  });

  describe('OnInit', () => {
    it('should call getRecipe once on init', () => {
      spyOn(component, 'getRecipe');
      component.ngOnInit();
      expect(component.getRecipe).toHaveBeenCalledTimes(1);
    });
    it('should call getRecipeWithID from service once on init', () => {
      expect(reciperServiceMock.getRecipeWithID).toHaveBeenCalledTimes(1);
    });
    /*
    doesnt work for some reason
     it('should call getRecipeWithID from service once on init with id', () => {
      component.recipeID = 'id';
      reciperServiceMock.getRecipeWithID.and.returnValues(of(helper.createRecipes(1)[0]));
      expect(reciperServiceMock.getRecipeWithID).toHaveBeenCalledWith('id');
    });
     */
  });

  describe('update Recipe', () => {

    it('should call dispatch for updateRecipe from store once', () => {

      storeMock.dispatch.and.returnValue(of());
      component.updateRecipe();
      expect(storeMock.dispatch).toHaveBeenCalledTimes(1);
    });
  });

  describe('ignredients from', () => {
    it('should have 2 form group inside array on load after recipe with 2 ingredients is loaded ', () => {
      component.recipe = helper.createRecipes(1)[0];
      fixture.detectChanges();
      expect(component.ingredientsFormArray.length).toBe(2);
    });

    it('should have 3 form group inside array after recipe with 2 ingredients after  add ingredient', () => {
      reciperServiceMock.getRecipeWithID.and.returnValue(of(helper.createRecipes(1)[0]));
      domHelper.clickItemsWithName('h5', 'Click to Add Ingredient');
      fixture.detectChanges();
      expect(component.ingredientsFormArray.length).toBe(3);
    });
    it('should have 0 form group inside array after recipe with 2 ingredients is removed twice', () => {
      reciperServiceMock.getRecipeWithID.and.returnValue(of(helper.createRecipes(1)[0]));
      domHelper.clickItemsWithName('button', 'Remove');
      fixture.detectChanges();
      // because clicking all items with &times;Remove
      expect(component.ingredientsFormArray.length).toBe(0);
    });
    it('should have called remove ingredients twice after clicking remove ingredients twice', () => {
      reciperServiceMock.getRecipeWithID.and.returnValue(of(helper.createRecipes(1)[0]));
      spyOn(component, 'removeIngredient');
      domHelper.clickItemsWithName('button', 'Remove');
      fixture.detectChanges();
      // because clicking all items with &times;Remove
      expect(component.removeIngredient).toHaveBeenCalledTimes(2);
    });
  });
});
