import { Component } from '@angular/core';
import { CustomerService } from './customer-page/customer.service';
import { ProductService } from './product-page/product-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ProductService, CustomerService]
})
export class AppComponent {
  title = 'client-webportal';
}
