import { Component, Input, OnInit } from '@angular/core';
import { AlertService } from 'src/app/alert/alert.service';
import { CustomerModel } from 'src/app/shared/customer-model';
import { CustomerService } from '../customer-service';

@Component({
  selector: 'app-new-customer-page',
  templateUrl: './new-customer-page.component.html',
  styleUrls: ['./new-customer-page.component.css']
})
export class NewCustomerPageComponent implements OnInit {

  createdDate: string = "12.03.2022"


  @Input() customer: CustomerModel

  constructor(private customerService: CustomerService, private alertService: AlertService) { }

  ngOnInit(): void {
    
  }
  onSave(customerName: string, customerAddress: string){
    if(customerName !== "" && customerAddress !== ""){
      
      const customerId = Math.floor(Math.random()*10000000)
      this.customer = new CustomerModel(customerId, customerName, customerAddress, 0, 0)
      this.customerService.addCustomer(this.customer)
      this.alertService.success("Added new customer with name " + customerName)
    }
    else{
      this.alertService.error("Values cannot be empty")
    }
  }

}
