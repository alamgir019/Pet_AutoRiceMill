import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { jqueryUiRouting } from './jquery-ui.routing';
import {JqueryUiComponent} from "./jquery-ui.component";
import { JqueryUiModule } from '@app/shared/ui/jquery-ui/jquery-ui.module';
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    jqueryUiRouting,
    SharedModule,

    JqueryUiModule,

  ],
  declarations: [JqueryUiComponent]
})
export class JqueryUiShowcaseModule { }
