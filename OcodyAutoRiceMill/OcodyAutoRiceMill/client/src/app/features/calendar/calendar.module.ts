
import {NgModule} from "@angular/core";
import {CalendarComponent} from "./calendar.component";
import { RouterModule } from "@angular/router";
import { SharedModule } from "@app/shared/shared.module";

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild([{
      path: '',
      component: CalendarComponent
    }]),
  ],
  declarations: [CalendarComponent],
})
export class CalendarFeatureModule{}
