import { NgModule } from "@angular/core";

import { SparklinesComponent } from "./sparklines/sparklines.component";
import { EasyPieChartsComponent } from "./easy-pie-charts/easy-pie-charts.component";

import { routing } from "./graphs-showcase.routing";
import { SharedModule } from "@app/shared/shared.module";

@NgModule({
  declarations: [SparklinesComponent, EasyPieChartsComponent],
  imports: [SharedModule, routing],
  providers: []
})
export class GraphsShowcaseModule {}
