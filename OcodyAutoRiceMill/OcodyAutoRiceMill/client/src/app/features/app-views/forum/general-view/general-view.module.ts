import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {GeneralViewRoutingModule} from './general-view-routing.module';
import {GeneralViewComponent} from './general-view.component';
import { SmartadminLayoutModule } from '@app/shared/layout';
import { StatsModule } from '@app/shared/stats/stats.module';

@NgModule({
  imports: [
    CommonModule,
    GeneralViewRoutingModule,
    SmartadminLayoutModule,
    StatsModule,
  ],
  declarations: [GeneralViewComponent]
})
export class GeneralViewModule {
}
