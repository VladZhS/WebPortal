import { Component, OnInit } from '@angular/core';
import { OrderModel } from '../models/order.model';
import { OrderService } from './order-service';

@Component({
  selector: 'app-order-page',
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css']
})
export class OrderPageComponent implements OnInit {
  orders: OrderModel[]
  tableHeaders = ["Order Number", "Customer Name", "Customer Addres", "Total Cost", "Status"]


  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.orders = this.orderService.getOrders()
  }


}
