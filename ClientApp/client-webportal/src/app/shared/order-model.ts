import { CustomerModel } from "./customer-model";
import { ProductModel } from "./product-model";

export class OrderModel {
    constructor(public orderNumber: number,
         public orderCreatedDate: string,
         public customer: CustomerModel,
         public products: ProductModel[],
         public totalCost: number,
         public status: string,
         public description: string
         ){}
}