import { Component, Input, OnInit } from '@angular/core';
import { CustomerModel } from 'src/app/shared/customer-model';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'app-new-customer-page',
  templateUrl: './new-customer-page.component.html',
  styleUrls: ['./new-customer-page.component.css']
})
export class NewCustomerPageComponent implements OnInit {

  createdDate: string = "12.03.2022"


  @Input() product: CustomerModel

  constructor(private productService: CustomerService) { }

  ngOnInit(): void {

  }
  onSave(customerName: string, customerAddress: string){
    this.product = new CustomerModel(customerName, customerAddress, 0, 0)
    this.productService.addCustomer(this.product)
  }

}
