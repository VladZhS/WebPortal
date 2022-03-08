import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { OrderPageComponent } from './order-page/order-page.component';
import { ProductPageComponent } from './product-page/product-page.component';
import { CustomerPageComponent } from './customer-page/customer-page.component';
import { NewOrderPageComponent } from './order-page/new-order-page/new-order-page.component';
import { NewProductPageComponent } from './product-page/new-product-page/new-product-page.component';
import { NewCustomerPageComponent } from './customer-page/new-customer-page/new-customer-page.component';
import { ViewProductComponent } from './product-page/view-product/view-product.component';
import { AlertComponent } from './alert/alert.component';
import { ViewOrderComponent } from './order-page/view-order/view-order.component';

const appRoutes: Routes = [
  { path: 'orders', component: OrderPageComponent },
  { path: 'orders/new-order', component: NewOrderPageComponent },
  { path: 'products', component: ProductPageComponent},
  { path: 'products/new-product/:id', component: NewProductPageComponent},
  { path: 'products/view-product/:id', component: ViewProductComponent},
  { path: 'customers', component: CustomerPageComponent},
  { path: 'customers/new-customer', component: NewCustomerPageComponent},
  { path: 'orders/view-order/:id', component: ViewOrderComponent},
  { path: '**', redirectTo: 'orders' }
]

@NgModule({
  declarations: [
    AppComponent,
    OrderPageComponent,
    ProductPageComponent,
    CustomerPageComponent,
    NewOrderPageComponent,
    NewProductPageComponent,
    NewCustomerPageComponent,
    ViewProductComponent,
    AlertComponent,
    ViewOrderComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
