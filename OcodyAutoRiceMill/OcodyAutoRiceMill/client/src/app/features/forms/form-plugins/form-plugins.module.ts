import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { formPluginsRouting } from './form-plugins.routing';
import {FormPluginsComponent} from "./form-plugins.component";
import {XEditableWidgetComponent} from "./x-editable-widget/x-editable-widget.component";
import {DuallistboxWidgetComponent} from "./duallistbox-widget/duallistbox-widget.component";
import { SharedModule } from '@app/shared/shared.module';
import { SmartadminInputModule } from '@app/shared/forms/input/smartadmin-input.module';

@NgModule({
  imports: [
    CommonModule,
    formPluginsRouting,
    SharedModule,
    SmartadminInputModule,
  ],
  declarations: [FormPluginsComponent, XEditableWidgetComponent, DuallistboxWidgetComponent]
})
export class FormPluginsModule { }
