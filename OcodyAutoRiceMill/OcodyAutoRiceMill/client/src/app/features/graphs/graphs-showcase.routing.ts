import {ModuleWithProviders} from "@angular/core"
import {Routes, RouterModule} from '@angular/router';

import {SparklinesComponent} from "./sparklines/sparklines.component";
import {EasyPieChartsComponent} from "./easy-pie-charts/easy-pie-charts.component";


export const routes: Routes = [
  {
    path: 'flot-charts',
    loadChildren: './flot-charts/flot-charts.module#FlotChartsModule'
  },
  {
    path: 'morris-charts',
    loadChildren: './morris-charts/morris-charts.module#MorrisChartsModule'
  },
  {
    path: 'sparklines',
    component: SparklinesComponent
  },
  {
    path: 'easy-pie-charts',
    component: EasyPieChartsComponent
  },
  {
    path: 'dygraphs',
    loadChildren: './dygraphs/dygraphs.module#DygraphsModule'
  },
  {
    path: 'chart-js',
    loadChildren: './chart-js/chart-js-showcase.module#ChartJsShowcaseModule'
  },
  {
    path: 'highchart-tables',
    loadChildren: './highchart-tables/highchart-tables.module#HighchartTablesModule'
  }

];

export const routing = RouterModule.forChild(routes);
