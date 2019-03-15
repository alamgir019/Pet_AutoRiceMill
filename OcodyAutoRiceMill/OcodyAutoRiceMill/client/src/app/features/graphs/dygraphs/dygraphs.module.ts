import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { dygraphsRouting } from './dygraphs.routing';
import { DygraphsComponent } from './dygraphs.component';
import { SharedModule } from '@app/shared/shared.module';
import { DygraphModule } from '@app/shared/graphs/dygraph/dygraph.module';

@NgModule({
  imports: [
    CommonModule,
    dygraphsRouting,
    SharedModule,
    DygraphModule
  ],
  declarations: [DygraphsComponent]
})
export class DygraphsModule { }
