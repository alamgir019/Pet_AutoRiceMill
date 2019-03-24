import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { dropzoneShowcaseRouting } from './dropzone-showcase.routing';
import {DropzoneShowcaseComponent} from "./dropzone-showcase.component";
import { SharedModule } from '@app/shared/shared.module';
import { DropzoneModule } from '@app/shared/forms/dropzone/dropzone.module';

@NgModule({
  imports: [
    CommonModule,
    dropzoneShowcaseRouting,
    SharedModule,
    DropzoneModule
  ],
  declarations: [DropzoneShowcaseComponent]
})
export class DropzoneShowcaseModule { }
