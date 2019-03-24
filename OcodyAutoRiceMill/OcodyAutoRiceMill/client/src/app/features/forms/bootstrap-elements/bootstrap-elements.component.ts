import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'sa-bootstrap-elements',
  templateUrl: './bootstrap-elements.component.html',
})
export class BootstrapElementsComponent implements OnInit {

  public styleTheme: string = 'style-0';

  public styleThemes: Array<string> = ['style-0', 'style-1', 'style-2', 'style-3'];

  constructor() {}

  ngOnInit() {
  }

}
