import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { formElementsRouting } from './form-elements.routing';
import {FormElementsComponent} from "./form-elements.component";
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    formElementsRouting,
    SharedModule
  ],
  declarations: [FormElementsComponent]
})
export class FormElementsModule { }
