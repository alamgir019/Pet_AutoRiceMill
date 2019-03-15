import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BootstrapEditorsComponent } from './bootstrap-editors.component';
import {bootstrapEditorsRouting} from "./bootstrap-editors.routing";
import { SharedModule } from '@app/shared/shared.module';
import { SmartadminEditorsModule } from '@app/shared/forms/editors/smartadmin-editors.module';

@NgModule({
  imports: [
    CommonModule,
    bootstrapEditorsRouting,
    SmartadminEditorsModule,
    SharedModule,

  ],
  declarations: [BootstrapEditorsComponent]
})
export class BootstrapEditorsModule { }
