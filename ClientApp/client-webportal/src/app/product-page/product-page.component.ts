import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../shared/product-model';
import { ProductService } from './product-service';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css']
})
export class ProductPageComponent implements OnInit {
  products: ProductModel[];
  tableHeaders: string[] = ["Product id", "Product Name", "Product Category", "Product Size", "Quantity", "Price", "Action"]

  constructor(private productService: ProductService) { 

  }

  onDelete(index: number){
    this.productService.deleteProduct(index)
    this.products = this.productService.getProducts();
  }

  ngOnInit(): void {

    this.products = this.productService.getProducts();
  }

}
