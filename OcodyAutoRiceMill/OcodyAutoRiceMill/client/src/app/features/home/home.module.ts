import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { homeRouting } from './home.routing';

import {HomeComponent} from "./home.component";
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    homeRouting,
      SharedModule
  ],
  declarations: [HomeComponent]
})
export class HomeModule { }
