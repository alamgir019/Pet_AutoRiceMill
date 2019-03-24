import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {GalleryDemoRoutingModule} from './gallery-demo-routing.module';
import {GalleryDemoComponent} from './gallery-demo.component';
import { SmartadminGalleryModule } from '@app/shared/ui/gallery/gallery.module';
import { StatsModule } from '@app/shared/stats/stats.module';
import { SmartadminLayoutModule } from '@app/shared/layout';

@NgModule({
  imports: [
    CommonModule,
    GalleryDemoRoutingModule,
    SmartadminGalleryModule,
    SmartadminLayoutModule,
    StatsModule,

  ],
  declarations: [GalleryDemoComponent]
})
export class GalleryDemoModule {
}
