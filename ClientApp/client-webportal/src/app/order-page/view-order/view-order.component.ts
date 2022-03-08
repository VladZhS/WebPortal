import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderModel } from 'src/app/models/order.model';
import { OrderService } from '../order-service';

@Component({
  selector: 'app-view-order',
  templateUrl: './view-order.component.html',
  styleUrls: ['./view-order.component.css']
})
export class ViewOrderComponent implements OnInit {

  order: OrderModel
  tableHeaders: string[] = ["Product Id", "Product Name", "Product Category", "Product Size", "Quantity", "Price"]

  constructor(private route: ActivatedRoute, private orderService: OrderService) { }

  ngOnInit(): void {
    this.order = this.orderService.getOrderById(this.route.snapshot.params['id'])!
    console.log(this.order)
  }

}
