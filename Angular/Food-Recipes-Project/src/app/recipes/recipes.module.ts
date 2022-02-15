import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RecipesRoutingModule } from './recipes-routing.module';
import { RecipesAddComponent } from './recipes-add/recipes-add.component';
import { RecipesListComponent } from './recipes-list/recipes-list.component';
import { RecipesDetailsComponent } from './recipes-details/recipes-details.component';
import {ReactiveFormsModule} from '@angular/forms';
import {ImageCropperModule} from 'ngx-image-cropper';
import {FilesModule} from '../files/files.module';
import { RecipeUpdateComponent } from './recipe-update/recipe-update.component';

@NgModule({
  declarations: [RecipesAddComponent, RecipesListComponent, RecipesDetailsComponent, RecipeUpdateComponent],
  imports: [
    CommonModule,
    RecipesRoutingModule,
    ReactiveFormsModule,
    ImageCropperModule,
    FilesModule,
  ]
})
export class RecipesModule { }
