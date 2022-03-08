import { CategoryModel } from "./category.model";
import { SizeModel } from "./size.model";

export class ProductModel{
    constructor(
        public Id: number,
        public Name: string,
        public Quantity: number,
        public Price: number,
        public Description: string,
        public Category: CategoryModel,
        public Size: SizeModel,
        public createdDate: string
         ) {}
}