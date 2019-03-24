import {Component, OnInit, NgModule} from '@angular/core';
import {MovieFormComponent} from "./movie-form/movie-form.component";
import {TogglingFormComponent} from "./toggling-form/toggling-form.component";
import {AttributeFormComponent} from "./attribute-form/attribute-form.component";
import {ButtonGroupFormComponent} from "./button-group-form/button-group-form.component";
import {ProductFormComponent} from "./product-form/product-form.component";
import {ProfileFormComponent} from "./profile-form/profile-form.component";
import {ContactFormComponent} from "./contact-form/contact-form.component";
import {BootstrapValidationComponent} from "./bootstrap-validation.component";
import {bootstrapValidationRouting} from "./bootstrap-validation.routing";
import { SmartadminValidationModule } from '@app/shared/forms/validation/smartadmin-validation.module';
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  imports: [
    SharedModule,
    SmartadminValidationModule,
    bootstrapValidationRouting
  ],
  declarations: [ MovieFormComponent, TogglingFormComponent, ContactFormComponent,
    AttributeFormComponent, ButtonGroupFormComponent, ProductFormComponent, ProfileFormComponent,
    BootstrapValidationComponent,
  ],
  exports: []

})
export class BootstrapValidationModule{}
