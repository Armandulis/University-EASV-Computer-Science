import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipesListComponent } from './recipes-list.component';
import {DOMHelper} from '../../../testing/dom-helper';
import {RecipeUpdateComponent} from '../recipe-update/recipe-update.component';
import {of} from 'rxjs';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterTestingModule} from '@angular/router/testing';
import {Helper} from '../../../testing/recipes-helper';
import {Select, Store} from '@ngxs/store';
import {RecipesState} from '../../store';

describe('RecipesListComponent', () => {
  let component: RecipesListComponent;
  let fixture: ComponentFixture<RecipesListComponent>;
  let storeMock: any;
  let helper: Helper;
  let state: any;
  let stateRecipes: any;


  beforeEach(async(() => {
    helper = new Helper();
    storeMock = jasmine.createSpyObj('Store', ['dispatch']);
    stateRecipes = jasmine.createSpyObj('RecipesState', ['recipes']);
    stateRecipes.recipes.and.returnValue(helper.createRecipes(1));



    TestBed.configureTestingModule({
      declarations: [ RecipesListComponent ],
      providers: [
        {provide: Store, useValue: storeMock},
        {provide: RecipesState, useValue: stateRecipes }
      ],
      imports: [
        ReactiveFormsModule,
        RouterTestingModule,
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecipesListComponent);
    component = fixture.componentInstance;

    Object.defineProperty(state, 'recipes', { writable: true });
    component.recipes = of(helper.createRecipes(1));

    fixture.detectChanges();
  });
  it('should create', () => {
    expect(component).toBeTruthy();
  });

});

