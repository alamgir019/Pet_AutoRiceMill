import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NormalTablesComponent } from './normal-tables.component';
import { SharedModule } from '@app/shared/shared.module';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild([{
      path: '', component: NormalTablesComponent
    }])
  ],
  declarations: [
    NormalTablesComponent
  ]
})
export class NormalTablesModule { }
