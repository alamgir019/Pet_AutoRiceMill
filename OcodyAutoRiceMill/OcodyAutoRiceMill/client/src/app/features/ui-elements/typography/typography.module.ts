import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { typographyRouting } from './typography.routing';
import {TypographyComponent} from "./typography.component";
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    typographyRouting,
    SharedModule,
  ],
  declarations: [TypographyComponent]
})
export class TypographyModule { }
