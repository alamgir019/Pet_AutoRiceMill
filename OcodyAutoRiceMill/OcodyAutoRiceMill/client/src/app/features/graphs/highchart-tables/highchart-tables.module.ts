import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { highchartTablesRouting } from './highchart-tables.routing';
import { HighchartTablesComponent } from './highchart-tables.component';
import { SharedModule } from '@app/shared/shared.module';
import { HighchartsModule } from '@app/shared/graphs/highcharts/highcharts.module';

@NgModule({
  imports: [
    CommonModule,
    highchartTablesRouting,
    SharedModule,
    HighchartsModule
  ],
  declarations: [HighchartTablesComponent]
})
export class HighchartTablesModule { }
