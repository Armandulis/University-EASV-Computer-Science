import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipesAddComponent } from './recipes-add.component';
import {DOMHelper} from '../../../testing/dom-helper';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ImageCroppedEvent, ImageCropperModule} from 'ngx-image-cropper';
import {RouterTestingModule} from '@angular/router/testing';
import {RecipesService} from '../shared/recipes.service';
import {of} from 'rxjs';
import {Router} from '@angular/router';
import {Recipe} from '../shared/recipe';
import {Location} from '@angular/common';
import {FileService} from '../../files/shared/file.service';
import {Helper} from '../../../testing/recipes-helper';
import {Store} from '@ngxs/store';

describe('RecipesAddComponent', () => {
  let component: RecipesAddComponent;
  let fixture: ComponentFixture<RecipesAddComponent>;
  let domHelper: DOMHelper<RecipesAddComponent>;
  let fileServiceMock: any;
  let helper: Helper;
  let storeMock: any;
  beforeEach(async(() => {
    fileServiceMock = jasmine.createSpyObj('FileService', ['chosenImage']);
    storeMock = jasmine.createSpyObj('Store', ['dispatch']);

    TestBed.configureTestingModule({
      declarations: [ RecipesAddComponent ],
      providers: [
        {provide: Store, useValue: storeMock},
        {provide: FileService, useValue: fileServiceMock},
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
    fixture = TestBed.createComponent(RecipesAddComponent);
    component = fixture.componentInstance;
    domHelper = new DOMHelper(fixture);
    helper = new Helper();
    fixture.detectChanges();

  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('Simple HTML Tests', () => {
    it('should contain 9 labels at least: name, portion, type, time,' +
      ' instructions, picture button, ingredients: + its name, amount', () => {
        expect(domHelper.getAllOfElementsByTag('label').length).toBeGreaterThanOrEqual(9);
    });
    it('should contain 4 inputs: name, fileChooser, ingredients name and amount', () => {
      expect(domHelper.getAllOfElementsByTag('input').length).toBe(4);
    });
    it('should contain 1 textArea for Instructions', () => {
      expect(domHelper.getAllOfElementsByTag('textarea').length).toBe(1);
    });
    it('should contain 3 selects: protion, cookTime, type', () => {
      expect(domHelper.getAllOfElementsByTag('select').length).toBe(3);
    });
  });

  describe('add Recipes', () => {
    it('should call dispach with create recipe from store 1time when addRecipe is triggered', () => {
      component.imageMetadata = {
        fileMeta: {
          size: 2,
          type: 'w',
          id: 'w',
          lastModified: 2,
          name: 'w'
        }
      };
      storeMock.dispatch.and.returnValue(of());
      component.addRecipe();
      expect(storeMock.dispatch).toHaveBeenCalledTimes(1);
    });
  });

  describe('Image for Recipe', () => {
    it('should call fileService once after choosing image with filechooser', () => {
      component.uploadImage(event);
      fixture.detectChanges();
      expect(fileServiceMock.chosenImage).toHaveBeenCalledTimes(1);
    });
  });
  describe('ignredients from', () => {
    it('should have 2 form group inside array after onInit', () => {
      domHelper.clickItemsWithName('h5', 'Click to Add Ingredient');
      fixture.detectChanges();
      expect(component.ingredientsFormArray.length).toBe(2);
    });
    it('should have 0 form group inside array after onInit', () => {
      domHelper.clickItemsWithName('button', 'Remove');
      fixture.detectChanges();
      expect(component.ingredientsFormArray.length).toBe(0);
    });
    it('should have called remove ingredients once after clicking remove ingredients', () => {
      spyOn(component, 'removeIngredient');
      domHelper.clickItemsWithName('button', 'Remove');
      fixture.detectChanges();
      expect(component.removeIngredient).toHaveBeenCalledTimes(1);
    });
  });
  describe('Navigation', () => {
    let location: Location;
    let router: Router;
    let helper: Helper;

    beforeEach(() => {
      location = TestBed.get(Location);
      router =  TestBed.get(Router);
      fixture.detectChanges();
      helper = new Helper();
    });
    /*
    fix this
    it('should navigate to Recipes list after clicking button add recipe', () => {
      reciperServiceMock.addRecipeWithImage.and.returnValues(of(helper.createRecipes(1)[0]));
      spyOn(router, 'navigateByUrl');
      domHelper.clickItemsWithName('button', 'Add Recipe');
      expect(router.navigateByUrl).toHaveBeenCalledWith(router.createUrlTree(['/add']),
        {skipLocationChange: false, replaceUrl: false},
        { relativeTo: Route(url:'', path:'') });
    });
    it('should navigate to Recipes list after clicking button add recipe', () => {
      reciperServiceMock.addRecipeWithImage.and.returnValues(of(helper.createRecipes(1)));
      domHelper.clickItemsWithName('button', 'Add Recipe');
      expect(location.path()).toBe('/add');
    });

     */
  });
});

