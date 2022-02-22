import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/customer-page/customer.service';
import { ProductService } from 'src/app/product-page/product-service';
import { ProductModel } from 'src/app/shared/product-model';

@Component({
  selector: 'app-new-order-page',
  templateUrl: './new-order-page.component.html',
  styleUrls: ['./new-order-page.component.css']
})
export class NewOrderPageComponent implements OnInit {

  

  constructor(private customerService:CustomerService, private productSevice: ProductService) { }

  ngOnInit(): void {

  }

}
