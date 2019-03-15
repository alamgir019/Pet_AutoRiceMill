import {NgModule} from '@angular/core';

import {routing} from './dashboard.routing';
import { SharedModule } from '@app/shared/shared.module';


@NgModule({
  imports: [
    SharedModule,
    routing,
  ],
  declarations: [],
  providers: [],
})
export class DashboardModule {

}
