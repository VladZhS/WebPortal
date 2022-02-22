import { Component, OnInit } from '@angular/core';
import { CustomerModel } from '../shared/customer-model';
import { CustomerService } from './customer.service';

@Component({
  selector: 'app-customer-page',
  templateUrl: './customer-page.component.html',
  styleUrls: ['./customer-page.component.css']
})
export class CustomerPageComponent implements OnInit {
  customers: CustomerModel[] 
  tableHeaders: string[] = ["Customer Name", "Customer Address", "Total Ordered Cost", "Orders Count"]

  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
    this.customers = this.customerService.getCustomers();
  }

}
