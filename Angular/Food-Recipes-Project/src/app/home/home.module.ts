import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import {HomeComponent} from './frontPage/home.component';
import {CarouselModule} from 'ngx-bootstrap';

@NgModule({
  declarations: [ HomeComponent],
  imports: [
    CommonModule,
    HomeRoutingModule,
    CarouselModule.forRoot(),
  ]
})
export class HomeModule { }
