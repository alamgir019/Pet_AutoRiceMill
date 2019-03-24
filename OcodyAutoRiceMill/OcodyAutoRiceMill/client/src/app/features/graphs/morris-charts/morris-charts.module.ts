import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { morrisChartsRouting } from './morris-charts.routing';
import { MorrisChartsComponent } from './morris-charts.component';
import { SharedModule } from '@app/shared/shared.module';
import { MorrisGraphModule } from '@app/shared/graphs/morris-graph/morris-graph.module';

@NgModule({
  imports: [
    CommonModule,
    morrisChartsRouting,
    SharedModule,
    MorrisGraphModule
  ],
  declarations: [MorrisChartsComponent]
})
export class MorrisChartsModule { }
