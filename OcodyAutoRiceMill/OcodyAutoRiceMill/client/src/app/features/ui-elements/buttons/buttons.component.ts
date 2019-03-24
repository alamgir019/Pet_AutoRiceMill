import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'sa-buttons',
  templateUrl: './buttons.component.html',
})
export class ButtonsComponent implements OnInit {

  public singleModel:string = '1';
  public radioModel:string = 'Middle';
  public checkModel:any = {left: false, middle: true, right: false};

  constructor() { }

  ngOnInit() {
  }

}
