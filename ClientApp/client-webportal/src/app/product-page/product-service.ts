import { EventEmitter } from "@angular/core";
import { ProductModel } from "../shared/product-model";

export class ProductService{

    products: ProductModel[] = []

    getProducts(){
        return this.products?.slice()
    }
    getProductById(id: number){
        return this.products.find(x => x.productId == id);
    }
    replaceProduct(product: ProductModel){
        const index = this.products.findIndex(x =>
            {
                return x.productId == product.productId;
            })
            this.products[index] = product
    }

    addProduct(product: ProductModel){
        this.products?.push(product)
    }

    deleteProduct(index: number){
        const index2 = this.products.findIndex(x =>
            {
                return x.productId ==index;
            })
        this.products.splice(index2, 1)
    }

}