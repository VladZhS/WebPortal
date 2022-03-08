import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductModel } from '../shared/product-model';
import { ProductService } from './product-service';

@Component({selector: 'somecontet',  template: `
<div class="modal-header">
  <h4 class="modal-title">Are you sure?</h4>
  <button type="button" class="btn-close" aria-label="Close" (click)="activeModal.dismiss('Cross click')"></button>
</div>
<div class="modal-footer">
<button type="button" class="btn btn-outline-dark" (click)="activeModal.close(true)">OK</button>
  <button type="button" class="btn btn-outline-dark" (click)="activeModal.close(false)">Close</button>
</div>
`
})
export class NgbdModalContent {

  constructor(public activeModal: NgbActiveModal) {}
}
@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css']
})
export class ProductPageComponent implements OnInit {
  products: ProductModel[];
  tableHeaders: string[] = ["Product id", "Product Name", "Product Category", "Product Size", "Quantity", "Price", "Action"]

  constructor(private productService: ProductService, private modalService: NgbModal) { 

  }

  async onDelete(index: number){
    const modalRef = this.modalService.open(NgbdModalContent);
    if(await modalRef.result){
    this.productService.deleteProduct(index)
    this.products = this.productService.getProducts();
    }
  }

  ngOnInit(): void {
   

    this.products = this.productService.getProducts();
  }

}
