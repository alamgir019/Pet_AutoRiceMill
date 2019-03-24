import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { treeViewsRouting } from './tree-views.routing';
import {TreeViewsComponent} from "./tree-views.component";
import { SharedModule } from '@app/shared/shared.module';
import { TreeViewModule } from '@app/shared/ui/tree-view/tree-view.module';


@NgModule({
  imports: [
    CommonModule,
    treeViewsRouting,
    SharedModule,
    TreeViewModule
  ],
  declarations: [TreeViewsComponent]
})
export class TreeViewsModule { }
