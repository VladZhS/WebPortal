import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AlertService } from 'src/app/alert/alert.service';
import { CustomerService } from 'src/app/customer-page/customer-service';
import { ProductService } from 'src/app/product-page/product-service';
import { CustomerModel } from 'src/app/models/customer.model';
import { OrderModel } from 'src/app/models/order.model';
import { ProductModel } from 'src/app/models/product.model';
import { OrderService } from '../order-service';

@Component({
  selector: 'app-new-order-page',
  templateUrl: './new-order-page.component.html',
  styleUrls: ['./new-order-page.component.css']
})
export class NewOrderPageComponent implements OnInit {
  selectedCustomer: CustomerModel
  selectedStatus: string
  selectedProduct: ProductModel
  totalCost: number
  createdDate: string
  orderID: number
  customersReceived: CustomerModel[] = []
  productsReceived: ProductModel[] = []
  productsSaved: ProductModel[] = []
  @ViewChild('qty') qty: ElementRef

  statuses: string[] = ["New", "Paid", "Shipped", "Delivered", "Closed"]
  tableHeaders: string[] = ["Product Id", "Product Name", "Product Category", "Product Size", "Quantity", "Price"]


  constructor(private customerService: CustomerService, private productSevice: ProductService, private alertService: AlertService, private orderService: OrderService) { }

  ngOnInit(): void {
    this.createdDate = "12.03.2022"
    this.totalCost = 0
    this.orderID = Math.floor(Math.random() * 10000000)
    this.productsReceived = this.productSevice.getProducts()
    this.customersReceived = this.customerService.getCustomers()
  }
  getTotalCost(quantity: number) {
    this.totalCost += this.selectedProduct.price * quantity

    console.log(this.totalCost)
  }
  onSave(description: string) {
    if (this.selectedCustomer !== undefined && this.selectedStatus !== undefined && this.totalCost != 0 && this.productsSaved.length != 0) {
      const order: OrderModel = new OrderModel(this.orderID, this.createdDate, this.selectedCustomer, this.productsSaved, this.totalCost, this.selectedStatus, description)
      this.orderService.addOrder(order)
      this.selectedCustomer.totalOrderedCost = this.totalCost
      this.selectedCustomer.ordersCount++
      this.customerService.setCustomer(this.selectedCustomer)
      this.alertService.success('Successfully created new order!')
    }
    else {

      this.alertService.error('Cannot create order, missing parameters');
    }
  }
  onAddProduct() {
    if (this.selectedProduct === undefined) {

      this.alertService.error('No product selected!');
    }
    else {
      const quantity: number = +this.qty.nativeElement.value || 0
      const orderedQuantity: number = this.selectedProduct?.orderedQuantity || 0
      const totalQuantity: number = this.selectedProduct?.quantity || 0
      console.log("Quantity: " + quantity + " orderedQuantity " + orderedQuantity + " totalQuantity " + totalQuantity)

      if (quantity + orderedQuantity <= totalQuantity) {
        this.selectedProduct.orderedQuantity += quantity
        this.replaceLocalProduct()
        this.productsSaved.push(new ProductModel(this.selectedProduct.productId, this.selectedProduct.productName, this.selectedProduct.productCategory, this.selectedProduct.productSize, 0, quantity, this.selectedProduct.price, this.selectedProduct.description, this.selectedProduct.createdDate))
        this.getTotalCost(quantity)

      }

      else {
        this.alertService.error('Product quantity must be smaller than availabe');
      }
    }
  }

  private replaceLocalProduct() {
    const index = this.productsReceived.findIndex(x => {
      return x.productId == this.selectedProduct.productId;
    })
    this.productsReceived[index] = this.selectedProduct
  }


}
