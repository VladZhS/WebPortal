import { Component, OnInit } from '@angular/core';
import { OrderModel } from '../shared/order-model';

@Component({
  selector: 'app-order-page',
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css']
})
export class OrderPageComponent implements OnInit {
  orders: OrderModel[] = [new OrderModel(12345, "John Sina", "5th Avenu New York", 200, "New"), 
  new OrderModel(12345, "John Sina", "5th Avenu New York", 200, "New"), 
  new OrderModel(12345, "John Sina", "5th Avenu New York", 200, "New"), 
  new OrderModel(12345, "John Sina", "5th Avenu New York", 200, "New"), 
  new OrderModel(12345, "John Sina", "5th Avenu New York", 200, "New")]
  tableHeaders = ["Order Number", "Customer Name", "Customer Addres", "Total Cost", "Status"]
  

  constructor() { }

  ngOnInit(): void {
  }


}
