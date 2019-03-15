import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import * as fromCalendar from "@app/core/store/calendar";

@Component({
  selector: 'sa-analytics',
  templateUrl: './analytics.component.html',
})
export class AnalyticsComponent implements OnInit {

  public calendar$
  constructor(
    private store: Store<any>
  ) {
    this.calendar$ = this.store.select(fromCalendar.getCalendarState);
  }

  ngOnInit() {
  }

}
