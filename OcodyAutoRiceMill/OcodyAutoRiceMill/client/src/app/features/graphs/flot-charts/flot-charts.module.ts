import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { flotChartsRouting } from './flot-charts.routing';
import { FlotChartsComponent } from './flot-charts.component';
import { FlotChartModule } from '@app/shared/graphs/flot-chart/flot-chart.module';
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    flotChartsRouting,
    SharedModule,
    FlotChartModule
  ],
  declarations: [FlotChartsComponent]
})
export class FlotChartsModule { }
