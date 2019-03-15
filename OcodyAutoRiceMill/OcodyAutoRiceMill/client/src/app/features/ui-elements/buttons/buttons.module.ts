import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { buttonsRouting } from './buttons.routing';
import {ButtonsComponent} from "./buttons.component";
import { SharedModule } from '@app/shared/shared.module';
@NgModule({
  imports: [
    CommonModule,
    buttonsRouting,
    SharedModule
  ],
  declarations: [ButtonsComponent]
})
export class ButtonsModule { }
