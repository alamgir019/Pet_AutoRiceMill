import {Component, OnInit, Input} from '@angular/core';
import { Store } from '@ngrx/store';



@Component({
  selector: 'image-editor-default-panel',
  template: `
            <section>
                <jcrop
                    [storeId]="storeId"
                    src="assets/img/superbox/superbox-full-11.jpg"
                    [width]="600" [height]="400"></jcrop>
            </section>
`,
})
export class DefaultPanelComponent implements OnInit {


  public storeId = 'defaultPanel';

  @Input() active: boolean;

  constructor(public store: Store<any>) {

  }

  ngOnInit() {

  }


}
