import { CustomerModel } from "./customer.model";
import { ProductModel } from "./product.model";

export class OrderModel {
    constructor(
        public customer: CustomerModel,
        public orderNumber: number,
        public orderCreatedDate: string,
        public products: ProductModel[],
        public totalCost: number,
        public status: string,
        public description: string
    ) { }
}