import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChatRoutingModule } from './chat-routing.module';
import {ChatComponent} from './chat.component';

// Angular modules
import {ReactiveFormsModule} from '@angular/forms';

@NgModule({
  declarations: [ChatComponent],
  imports: [
    CommonModule,
    ChatRoutingModule,

    // Angular
    ReactiveFormsModule
  ]
})
export class ChatModule { }
