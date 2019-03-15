import { Component, OnInit } from '@angular/core';
import { DATA } from './data';

@Component({
  selector: 'sa-dygraphs',
  templateUrl: './dygraphs.component.html',
})
export class DygraphsComponent implements OnInit {

  constructor() {}

  ngOnInit() {
  }

  getDemoData(){
    return DATA;
  }
}
