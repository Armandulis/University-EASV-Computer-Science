import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NavbarComponent } from './navbar.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ImageCropperModule} from 'ngx-image-cropper';
import {RouterTestingModule} from '@angular/router/testing';
import {DOMHelper} from '../../../testing/dom-helper';
import {Helper} from '../../../testing/recipes-helper';
import {of} from 'rxjs';
import {RecipesService} from '../../recipes/shared/recipes.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {Location} from '@angular/common';
import {Router} from '@angular/router';
import {AuthService} from '../../authentication/auth.service';
import {DataSharingService} from '../services/data-sharing.service';

describe('NavbarComponent', () => {
  let component: NavbarComponent;
  let fixture: ComponentFixture<NavbarComponent>;
  let domHelper: DOMHelper<NavbarComponent>;
  let recipesServiceMock: any;
  let helper: Helper;
  let ngbModal: any;
  let dataShareMock: any;
  let authMock: any;

  beforeEach(async(() => {
    helper = new Helper();
    recipesServiceMock = jasmine.createSpyObj('RecipeService', ['getRecipes']);
    dataShareMock = jasmine.createSpyObj('DataSharingService', ['']);

    // cant figure out how to define shared class
    Object.defineProperty(dataShareMock, 'isUserLoggedIn', { writable: true });
    dataShareMock.isUserLoggedIn = of([]);

    authMock = jasmine.createSpyObj('AuthService', ['login', 'createUser', 'signOut']);
    ngbModal = jasmine.createSpyObj('NgbModal', ['open']);
    recipesServiceMock.getRecipes.and.returnValues(of([helper.createRecipes(2)]));
    TestBed.configureTestingModule({
      declarations: [ NavbarComponent ],
      providers: [
        {provide: RecipesService, useValue: recipesServiceMock},
        {provide: DataSharingService, useValue: dataShareMock},
        {provide: AuthService, useValue: authMock},
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
    fixture = TestBed.createComponent(NavbarComponent);
    component = fixture.componentInstance;
    domHelper = new DOMHelper(fixture);
    fixture.detectChanges();


  });

  it('should create', () => {
   expect(component).toBeTruthy();
  });


  describe('Simple HTML Tests', () => {
    it('should contain 1 nav tag', () => {
      expect(domHelper.getAllOfElementsByTag('nav').length).toBe(1);
    });
    it('should contain 2 a tags', () => {
      expect(domHelper.getAllOfElementsByTag('a').length).toBe(2);
    });
    it('should contain a tag with Home inside', () => {
      expect(domHelper.getWithElementsContainingText('a', 'Home').length).toBe(1);
    });
    it('should contain a tag with Recipes inside', () => {
      expect(domHelper.getWithElementsContainingText('a', 'All Recipes').length).toBe(1);
    });
    it('should contain 1 input: search', () => {
      expect(domHelper.getAllOfElementsByTag('input').length).toBe(1);
    });
    it('should contain 1 form tags: search', () => {
      expect(domHelper.getAllOfElementsByTag('input').length).toBe(1);
    });
    /*
    // cant figure how to test buttons with &times;
    it('should call for close modal login', () => {
      spyOn(component, 'openLogInForm');
      expect(component.openLogInForm).toHaveBeenCalledTimes(1);
      domHelper.clickItemsWithName('button', 'Log in');
      domHelper.clickItemsWithName('button', '×');
      fixture.detectChanges();
      expect(component.openLogInForm).toHaveBeenCalledTimes(1);
    });
    it('should call for close modal sign up', () => {
      spyOn(component, 'openSignUpForm');
      domHelper.clickItemsWithName('button', 'Sign Up');
      domHelper.clickItemsWithName('button', '×');
      fixture.detectChanges();
      expect(component.openSignUpForm).toHaveBeenCalledTimes(1);
    });
     */

  });
  describe('load page and OnInit', () => {
    it('should call getRecipes once on ngOnInit', () => {
      fixture.detectChanges();
      expect(recipesServiceMock.getRecipes).toHaveBeenCalledTimes(1);
    });
  });
  describe('Navigation', () => {
    let location: Location;
    let router: Router;

    beforeEach(() => {
      location = TestBed.get(Location);
      router = TestBed.get(Router);
      fixture.detectChanges();
    });
    it('should activate method navigate after clicking search button', () => {
      spyOn(component, 'navigate');
      domHelper.clickItemsWithName('button', 'Search');
      fixture.detectChanges();
      expect(component.navigate).toHaveBeenCalledTimes(1);
    });
    /*
    it('should navigate to Recipes list after clicking button add recipe', () => {
      spyOn(router, 'navigateByUrl');
      component.recipes = helper.createRecipes(1);
      component.navigate();
      expect(router.navigateByUrl).toHaveBeenCalledWith(router.createUrlTree(['/recipes/' + helper.recipesList[0].id]),
        {skipLocationChange: false, replaceUrl: false});
    });
     */
  });
});
