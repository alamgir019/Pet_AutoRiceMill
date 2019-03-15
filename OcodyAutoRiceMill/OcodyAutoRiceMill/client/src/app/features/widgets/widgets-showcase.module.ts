import {NgModule} from "@angular/core";

import {routing} from "./widgets-showcase.routing";
import {WidgetsShowcaseComponent} from "./widgets-showcase.component";
import { SharedModule } from "@app/shared/shared.module";



@NgModule({
  declarations: [
    WidgetsShowcaseComponent
  ],
  imports: [
    SharedModule,
    routing
  ],
  providers: [],
})
export class WidgetsShowcaseModule {

}
