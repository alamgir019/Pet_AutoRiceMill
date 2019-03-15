import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { formValidationRouting } from './form-validation.routing';
import {FormValidationComponent} from "./form-validation.component";
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    formValidationRouting,
    SharedModule
  ],
  declarations: [FormValidationComponent]
})
export class FormValidationModule { }
