import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {DataSharingService} from '../shared/services/data-sharing.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [DataSharingService]
})
export class FilesModule { }
