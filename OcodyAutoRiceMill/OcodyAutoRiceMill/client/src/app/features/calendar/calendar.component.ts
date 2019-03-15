import { Component } from "@angular/core";
import { Store } from "@ngrx/store";
import * as fromCalendar from "@app/core/store/calendar";

@Component({
  selector: "sa-calendar",
  templateUrl: "./calendar.component.html"
})
export class CalendarComponent {
  calendar$;

  constructor(private store: Store<any>) {
    this.calendar$ = this.store.select(fromCalendar.getCalendarState);
  }

  public onAddSample($event) {
    this.store.dispatch(new fromCalendar.AddSample($event))
  }
  public onChangeSampleDrop($event) {
    this.store.dispatch(new fromCalendar.ChangeSampleDrop($event))
  }
  public onAddEvent($event) {
    this.store.dispatch(new fromCalendar.AddEvent($event))
  }
}
