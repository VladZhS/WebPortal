import { Component } from '@angular/core';
import { CustomerService } from './customer-page/customer-service';
import { OrderService } from './order-page/order-service';
import { ProductService } from './product-page/product-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ProductService, CustomerService, OrderService]
})
export class AppComponent {
  title = 'client-webportal';
}
