import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { wizardsRouting } from './wizards.routing';
import {WizardsComponent} from "./wizards.component";

import {BasicWizardWidgetComponent} from "./basic-wizard-widget/basic-wizard-widget.component";
import {FuelUxWizardWidgetComponent} from "./fuel-ux-wizard-widget/fuel-ux-wizard-widget.component";
import { SharedModule } from '@app/shared/shared.module';
import { SmartadminWizardsModule } from '@app/shared/forms/wizards/smartadmin-wizards.module';

@NgModule({
  imports: [
    CommonModule,
    wizardsRouting,
    SharedModule,
    SmartadminWizardsModule
  ],
  declarations: [WizardsComponent, BasicWizardWidgetComponent, FuelUxWizardWidgetComponent]
})
export class WizardsModule { }
