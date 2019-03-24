import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { bootstrapElementsRouting } from './bootstrap-elements.routing';
import {BootstrapElementsComponent} from "./bootstrap-elements.component";
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    bootstrapElementsRouting,
    SharedModule
  ],
  declarations: [BootstrapElementsComponent]
})
export class BootstrapElementsModule { }
