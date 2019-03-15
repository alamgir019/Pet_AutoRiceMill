import {NgModule} from '@angular/core';


import {ReviewFormComponent} from "./review-form/review-form.component";
import {OrderFormComponent} from "./order-form/order-form.component";
import {CommentFormComponent} from "./comment-form/comment-form.component";
import {ContactFormComponent} from "./contact-form/contact-form.component";
import {FormLayoutsComponent} from "./form-layouts.component";
import {formLayoutsRouting} from "./form-layouts.routing";
import { SmartadminValidationModule } from '@app/shared/forms/validation/smartadmin-validation.module';
import { SmartadminInputModule } from '@app/shared/forms/input/smartadmin-input.module';
import { SharedModule } from '@app/shared/shared.module';
import { CheckoutFormComponent } from '@app/features/forms/form-layouts/checkout-form/checkout-form.component';
import { RegistrationFormComponent } from '@app/features/forms/form-layouts/registration-form/registration-form.component';


@NgModule({
  imports: [
    SharedModule,

    formLayoutsRouting,
    SmartadminValidationModule,
    SmartadminInputModule
  ],
  declarations: [CheckoutFormComponent, RegistrationFormComponent,
    ReviewFormComponent, OrderFormComponent, CommentFormComponent, ContactFormComponent,
    FormLayoutsComponent
  ],
})
export class FormLayoutsModule {}
