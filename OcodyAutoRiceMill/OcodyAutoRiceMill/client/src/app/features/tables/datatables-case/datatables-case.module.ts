import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {DatatablesCaseComponent} from "./datatables-case.component";
import { DatatablesRestDemoComponent } from './datatables-rest-demo.component';
import {DatatablesCaseRoutingModule} from "./datatables-case-routing.module";
import { SharedModule } from '@app/shared/shared.module';
import { SmartadminDatatableModule } from '@app/shared/ui/datatable/smartadmin-datatable.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    SmartadminDatatableModule,
    DatatablesCaseRoutingModule
  ],
  declarations: [
     DatatablesCaseComponent, DatatablesRestDemoComponent
  ]
})
export class DatatablesCaseModule { }
