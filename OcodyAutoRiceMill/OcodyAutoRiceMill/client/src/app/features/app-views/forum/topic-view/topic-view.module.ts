import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TopicViewRoutingModule} from './topic-view-routing.module';
import {TopicViewComponent} from './topic-view.component';
import { SmartadminLayoutModule } from '@app/shared/layout';
import { StatsModule } from '@app/shared/stats/stats.module';

@NgModule({
  imports: [
    CommonModule,
    TopicViewRoutingModule,
    SmartadminLayoutModule,
    StatsModule,
  ],
  declarations: [TopicViewComponent]
})
export class TopicViewModule {
}
