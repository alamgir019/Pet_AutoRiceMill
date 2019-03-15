import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
})
export class ProductDetailsComponent implements OnInit {

  product = {
    slides: [
      {
        src: 'assets/img/demo/e-comm/detail-1.png',
      },
      {
        src: 'assets/img/demo/e-comm/detail-2.png'
      },
      {
        src: 'assets/img/demo/e-comm/detail-3.png'
      }
    ]
  }


  constructor() { }

  ngOnInit() {
  }

}
