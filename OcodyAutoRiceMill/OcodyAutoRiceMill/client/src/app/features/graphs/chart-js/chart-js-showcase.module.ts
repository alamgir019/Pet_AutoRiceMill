import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { chartJsShowcaseRouting } from './chart-js-showcase.routing';
import { ChartJsShowcaseComponent } from './chart-js-showcase.component';
import { SharedModule } from '@app/shared/shared.module';
import { ChartJsModule } from '@app/shared/graphs/chart-js/chart-js.module';

@NgModule({
  imports: [
    CommonModule,
    chartJsShowcaseRouting,
    SharedModule,
    ChartJsModule,
  ],
  declarations: [ChartJsShowcaseComponent]
})
export class ChartJsShowcaseModule { }
