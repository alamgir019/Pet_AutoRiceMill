import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './orders/orders.component';
import { ProductsViewComponent } from './products-view/products-view.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import {routing} from "./e-commerce.routing";
import {ShoppingCartComponent} from "./shopping-cart/shopping-cart.component";
import {CarouselModule} from "ngx-bootstrap";
import { SmartadminLayoutModule } from '@app/shared/layout';
import { SmartadminWidgetsModule } from '@app/shared/widgets/smartadmin-widgets.module';
import { StatsModule } from '@app/shared/stats/stats.module';
import { SmartadminDatatableModule } from '@app/shared/ui/datatable/smartadmin-datatable.module';

@NgModule({
  imports: [
    CommonModule,

    routing,

    SmartadminLayoutModule,
    SmartadminWidgetsModule,
    StatsModule,
    SmartadminDatatableModule,
    CarouselModule,

  ],
  declarations: [
    ShoppingCartComponent,
    OrdersComponent,
    ProductsViewComponent, ProductDetailsComponent
  ]
})
export class ECommerceModule { }
