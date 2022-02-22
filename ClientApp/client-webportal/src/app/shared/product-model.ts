export class ProductModel{
    constructor(
         public productId: number,
         public productName: string, 
         public productCategory: string,
         public productSize: string,
         public quantity: number,
         public price: number,
         public description: string,
         public createdDate: string) {}
}