

import {NgModule} from "@angular/core";
import {MapsComponent} from "./maps.component";
import {MapStyleService} from "./shared/map-style.service";
import {GoogleAPIService} from "./shared/google-api.service";
import {routing} from "./maps.routing";
import { SharedModule } from "@app/shared/shared.module";

@NgModule({
  imports: [routing, SharedModule],
  declarations: [MapsComponent],
  exports: [MapsComponent],
  providers: [GoogleAPIService, MapStyleService],
})
export class MapsModule{}
