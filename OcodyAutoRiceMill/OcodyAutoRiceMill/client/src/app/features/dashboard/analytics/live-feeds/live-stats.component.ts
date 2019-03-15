import {Component, OnInit, Input, OnDestroy, NgZone} from '@angular/core';

const FakeDataSource = {
  data: [],
  total: 200,
  getRandomData: function(){
    if (this.data.length > 0)
      this.data = this.data.slice(1);

    // do a random walk
    while (this.data.length < this.total) {
      var prev = this.data.length > 0 ? this.data[this.data.length - 1] : 50;
      var y = prev + Math.random() * 10 - 5;
      if (y < 0)
        y = 0;
      if (y > 100)
        y = 100;
      this.data.push(y);
    }

    // zip the generated y values with the x values
    var res = [];
    for (var i = 0; i < this.data.length; ++i)
      res.push([i, this.data[i]])
    return res;
  }
};


@Component({
  selector: 'live-stats-feed',
  templateUrl: './live-stats.component.html',
  styles: []
})
export class LiveStatsComponent implements OnInit, OnDestroy {

  constructor(private zone: NgZone) {
  }

  ngOnInit() {
    this.updateStats()
  }

  @Input() public liveSwitch = false;


  public liveStats: Array<any>;


  updateStats() {
    this.liveStats = [FakeDataSource.getRandomData()]
  }

  private interval;

  toggleSwitch() {

    if (this.liveSwitch) {
      this.interval = setInterval(()=>{
        this.updateStats()
      }, 1000)
    } else {
      clearInterval(this.interval);
    }
  }

  ngOnDestroy() {
    this.interval &&  clearInterval(this.interval);
  }


  liveStatsChartOptions = {
    yaxis: {
      min: 0,
      max: 100
    },
    xaxis: {
      min: 0,
      max: 100
    },
    colors: ['rgb(87, 136, 156)'],
    grid: {
      show: true,
      hoverable: true,
      clickable: true,
      borderWidth: 0,
    },
    series: {
      lines: {
        lineWidth: 1,
        fill: true,
        fillColor: {
          colors: [
            {
              opacity: 0.4
            },
            {
              opacity: 0
            }
          ]
        },
        steps: false

      }
    }
  }


}



