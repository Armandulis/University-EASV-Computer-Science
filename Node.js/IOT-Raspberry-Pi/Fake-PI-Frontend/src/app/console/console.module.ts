import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConsoleComponent } from './components/console/console.component';
import {ConsoleRoutingModule} from './console-routing.module';
import {ReactiveFormsModule} from '@angular/forms';

@NgModule({
  declarations: [ConsoleComponent],
  imports: [
    CommonModule,
    ConsoleRoutingModule,
    ReactiveFormsModule,
  ]
})
export class ConsoleModule { }
