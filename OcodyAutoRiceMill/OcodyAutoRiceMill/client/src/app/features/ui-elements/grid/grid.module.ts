import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { gridRouting } from './grid.routing';
import {GridComponent} from "./grid.component";
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    gridRouting,
    SharedModule,
  ],
  declarations: [GridComponent]
})
export class GridModule { }
