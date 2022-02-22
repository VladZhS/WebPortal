import { EventEmitter } from "@angular/core";
import { ProductModel } from "../shared/product-model";

export class ProductService{
    productSaved = new EventEmitter<ProductModel>();

    products: ProductModel[] = [ new ProductModel(1345123, "Fish", "Foods", "Small", 2, 150, "sdsdf", "12.03.2022")]

    getProducts(){
        return this.products.slice()
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
        this.products.push(product)
    }

    deleteProduct(index: number){
        const index2 = this.products.findIndex(x =>
            {
                return x.productId ==index;
            })
        this.products.splice(index2, 1)
    }

}