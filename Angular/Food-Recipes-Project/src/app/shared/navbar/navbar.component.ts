import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {NgbModal, NgbModalConfig} from '@ng-bootstrap/ng-bootstrap';
import {FormControl, FormGroup, ValidatorFn, Validators} from '@angular/forms';
import {Recipe} from '../../recipes/shared/recipe';
import {RecipesService} from '../../recipes/shared/recipes.service';
import {Router} from '@angular/router';
import {validate} from 'codelyzer/walkerFactory/walkerFn';
import {AuthService} from '../../authentication/auth.service';
import {DataSharingService} from '../services/data-sharing.service';
import * as firebase from 'firebase';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  logInForm: FormGroup;
  searchForm: FormGroup;
  signUpForm: FormGroup;
  recipesToSearch: Recipe[];
  recipes: Recipe[];
  isUserLoggedIn: boolean;
  test = false;
  user: any = null;
  constructor(private modalService: NgbModal,
              private recipesService: RecipesService,
              private router: Router,
              private auth: AuthService,
              private dataSharingService: DataSharingService) {

    this.logInForm = new FormGroup({
      emailLogIn: new FormControl(''),
      passwordLogIn: new FormControl('')
    });
    this.searchForm = new FormGroup({
      search: new FormControl('')
    });
    this.signUpForm = new FormGroup({
      emailSignUp: new FormControl(''),
      passwordSignUp: new FormControl(''),
      passwordSignUpRepeat: new FormControl(''),
    });

    this.dataSharingService.isUserLoggedIn.subscribe(value => {
      this.isUserLoggedIn = value;
    });

    this.dataSharingService.test.subscribe(value => {
      this.test = value;
    });
  }

  ngOnInit() {
   this.getRecipesForSearch();

    if (firebase.auth().currentUser) {
      this.dataSharingService.isUserLoggedIn.next(true);
    }
    firebase.auth().onAuthStateChanged(function(user) {


      if (user) {
        this.user = user;

      } else {
        this.user = null;
      }
    }.bind(this));
  }

  testF() {

    if (this.test === true) {
      this.dataSharingService.test.next(false);
    } else {
      this.dataSharingService.test.next(true);
    }

  }

  getRecipesForSearch() {
  this.recipesService.getRecipes().subscribe( list => {
    this.recipesToSearch = list;
  });
  }

  openLogInForm(logIn) {
    this.modalService.open(logIn);
  }
  openSignUpForm(signUp) {
    this.modalService.open(signUp);
  }

  tryLogIn() {
    this.auth.login(this.logInForm.get('emailLogIn').value, this.logInForm.get('passwordLogIn').value);
    this.modalService.dismissAll();
  }

  trySignUp() {
    this.auth.createUser(this.signUpForm.get('emailSignUp').value, this.signUpForm.get('passwordSignUp').value);
    this.modalService.dismissAll();
  }

  tryLogOut() {
    this.auth.signOut();
    alert('You have been logged out');
  }


  search($event: any) {
    if ($event.target.value === '') {
      this.recipes = [];
    } else {
      this.recipes = this.recipesToSearch.filter(rec =>
        rec.name.toLowerCase().indexOf($event.target.value.toLowerCase()) === 0);
      console.log($event.target.value.toLowerCase());
    }
  }

  navigate() {

    this.recipes.filter(rec =>
      rec.name.toLowerCase().indexOf(this.searchForm.get('search').value.toLowerCase()) === 0);
    this.router.navigateByUrl('/recipes/' + this.recipes[0].id);
    console.log(this.recipesToSearch[0].id);
    this.searchForm.get('search').setValue('');
  }
}
