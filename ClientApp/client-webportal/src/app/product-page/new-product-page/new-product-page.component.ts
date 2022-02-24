import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from 'src/app/alert/alert.service';

import { ProductModel } from 'src/app/shared/product-model';
import { ProductService } from '../product-service';

@Component({
  selector: 'app-new-product-page',
  templateUrl: './new-product-page.component.html',
  styleUrls: ['./new-product-page.component.css']
})
export class NewProductPageComponent implements OnInit {

  status: boolean = false;
  selectedValue: string 
  selectedSize: string
  creationStatus: string
  productId: number
  createdDate: string

  @ViewChild('price') priceInputRef: ElementRef
  @ViewChild('qty') qty: ElementRef

  @Input() product: ProductModel

  categories: string[] = ["Foods","Drinks","Desserts", "Ice Cream"]
  sizes: string[] = ["Small", "Medium","Big"]

  constructor(private productService: ProductService, private route: ActivatedRoute, private alertService: AlertService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'] 
    if( id == "new"){
    this.creationStatus = "new"
    this.productId = Math.floor(Math.random()*10000000)
    this.createdDate = "12.03.2022"
    }
    else {
      this.product = this.productService.getProductById(id)!
      this.productId = this.product.productId
      this.selectedValue = this.product.productCategory
      this.selectedSize = this.product.productSize
      this.createdDate = this.product.createdDate
    }

  }
 
  onSave(productName: string, description: string){
    try{
      this.product = new ProductModel(this.productId, productName,this.selectedValue,  this.selectedSize,0, this.qty.nativeElement.value, this.priceInputRef.nativeElement.value, description, this.createdDate)
      if(productName !== "" && this.selectedSize !== "" && this.selectedValue !== "" && this.qty.nativeElement.value != 0){
        if(this.creationStatus == "new" ) 
        {
          this.productService.addProduct(this.product)
          this.alertService.success("New product successfully added ")
        }
        else
        {
          this.productService.replaceProduct(this.product)
          
          this.alertService.success("Product successfully edited ")
        }
      }
      else{
        throw new Error("Values cannot be empty")
      }
    }
    catch(error){
      if(error instanceof Error)
      this.alertService.error(error.message)
    }
  }
}