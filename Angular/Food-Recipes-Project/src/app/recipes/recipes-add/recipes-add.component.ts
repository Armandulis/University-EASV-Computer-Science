import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Observable} from 'rxjs';
import {Recipe} from '../shared/recipe';
import {FormControl, FormGroup, FormArray} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {RecipesService} from '../shared/recipes.service';
import {ImageCroppedEvent} from 'ngx-image-cropper';
import {FileMetaData} from '../../files/shared/file-metadata';
import {ImageMetadata} from '../../files/shared/image-metadata';
import {FileService} from '../../files/shared/file.service';
import {Store} from '@ngxs/store';
import {CreateRecipe} from '../../store';

@Component({
  selector: 'app-recipes-add',
  templateUrl: './recipes-add.component.html',
  styleUrls: ['./recipes-add.component.scss']
})
export class RecipesAddComponent implements OnInit {

    recipeFormGroup: FormGroup;
    ingredientsFormArray: FormArray;

  imageMetadata: ImageMetadata;

  imageChangedEvent: any = '';

  isLoading = false;

  constructor(private router: Router,
              private activatedRoute: ActivatedRoute,
              private fileService: FileService,
              private store: Store) {

    this.ingredientsFormArray = new FormArray([this.createIngredient()]);

    this.recipeFormGroup = new FormGroup({
      name: new FormControl(''),
      type: new FormControl(''),
      portion: new FormControl(''),
      howTo: new FormControl(''),
      ingredients: this.ingredientsFormArray,
      cookTime: new FormControl('')
    });

  }

  ngOnInit() {
  }

  addRecipe() {

    const recipeData: Recipe = this.recipeFormGroup.value;
    if (this.imageMetadata == null){
      window.alert('You must choose an image to upload Recipe!');
    }  else {
      this.store.dispatch(new CreateRecipe(recipeData, this.imageMetadata)).subscribe(() => {

      });
      this.router.navigate(['../'],
        {relativeTo: this.activatedRoute});
      this.isLoading = true;
    }
  }


  addIngrediantForm() {
    this.ingredientsFormArray = this.recipeFormGroup.get('ingredients') as FormArray;
    this.ingredientsFormArray.push(this.createIngredient());
  }


  private createIngredient(): FormGroup {
    return new FormGroup({
      name: new FormControl(''),
      amount: new FormControl(''),
    });
  }

  uploadImage(event) {
    this.imageChangedEvent = event;
    this.imageMetadata = this.fileService.chosenImage(event);
  }

  imageCropped(event: ImageCroppedEvent) {
    this.imageMetadata = this.fileService.imageCropped(event);
  }


  removeIngredient(ingridientToRemove: number) {
    this.ingredientsFormArray.removeAt(ingridientToRemove);
  }
}
