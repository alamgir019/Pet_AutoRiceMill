import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { AlertService } from '../../../core/services/alert.service';
import { AuthService } from "../../../core/services/auth.service";
import { FormsModule } from '@angular/forms';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    LoginRoutingModule
  ],
  declarations: [LoginComponent],
  providers: [AlertService,
    AuthService]
})
export class LoginModule { }