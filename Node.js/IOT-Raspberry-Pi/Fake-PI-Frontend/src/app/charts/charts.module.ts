import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChartsRoutingModule } from './charts-routing.module';
import {ChartsComponent} from './components/charts/charts.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';

@NgModule({
  declarations: [ ChartsComponent],
  imports: [
    CommonModule,
    ChartsRoutingModule,
    NgxChartsModule,
  ]
})
export class ChartsModule { }
