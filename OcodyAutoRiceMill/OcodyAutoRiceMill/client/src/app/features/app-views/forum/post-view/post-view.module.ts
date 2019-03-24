import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PostViewRoutingModule} from './post-view-routing.module';
import {PostViewComponent} from './post-view.component';
import { SmartadminLayoutModule } from '@app/shared/layout';
import { StatsModule } from '@app/shared/stats/stats.module';

@NgModule({
  imports: [
    CommonModule,
    PostViewRoutingModule,

    SmartadminLayoutModule,
    StatsModule,
  ],
  declarations: [PostViewComponent]
})
export class PostViewModule {
}
