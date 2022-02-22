export class OrderModel {
    constructor(public orderNumber: number,
         public customerName: string,
         public customerAddres: string,
         public totalCost: number,
         public status: string
         ){}
}