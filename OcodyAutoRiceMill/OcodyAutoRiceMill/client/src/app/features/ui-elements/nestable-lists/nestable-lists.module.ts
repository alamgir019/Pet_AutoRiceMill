import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { nestableListsRouting } from './nestable-lists.routing';
import {NestableListsComponent} from "./nestable-lists.component";
import { SharedModule } from '@app/shared/shared.module';
import { NestableListModule } from '@app/shared/ui/nestable-list/nestable-list.module';


@NgModule({
  imports: [
    CommonModule,
    nestableListsRouting,
    SharedModule,
    NestableListModule,
  ],
  declarations: [NestableListsComponent]
})
export class NestableListsModule { }
