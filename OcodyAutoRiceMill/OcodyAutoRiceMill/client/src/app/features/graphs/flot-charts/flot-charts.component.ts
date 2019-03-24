import {Component, OnInit, OnDestroy} from '@angular/core';
import * as examples from "./flot-examples"
import {FakeDataSource} from "./flot-examples";
import { JsonApiService } from '@app/core/services';

@Component({
  selector: 'sa-flot-charts',
  templateUrl: './flot-charts.component.html',
})
export class FlotChartsComponent implements OnInit, OnDestroy {

  public flotData:any;
  public flotExamples:any;


  public updatingData: Array<any>;

  constructor(private jsonApiService:JsonApiService) {
  }

  ngOnInit() {
    this.jsonApiService.fetch( '/graphs/flot.json').subscribe(data => this.flotData = data);
    this.flotExamples = examples;

    this.interval = setInterval(()=>{
      this.updateStats()
    }, 1000);
    this.updateStats()
  }

  updateStats() {
    this.updatingData = [FakeDataSource.getRandomData()]
  }

  private interval;

  ngOnDestroy() {
    clearInterval(this.interval);
  }

}
