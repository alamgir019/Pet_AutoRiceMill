import {Component} from '@angular/core';

import * as fromLayout from '@app/core/store/layout';
import { Store } from '@ngrx/store';

@Component({
  selector: 'sa-minify-menu',
  template: `<span class="minifyme" (click)="toggle()">
    <i class="fa fa-arrow-circle-left hit"></i>
</span>`,
})
export class MinifyMenuComponent {

  constructor(
   private store: Store<any>
  ) {
  }

  toggle() {
    this.store.dispatch(new fromLayout.MinifyMenu())
  }
}
