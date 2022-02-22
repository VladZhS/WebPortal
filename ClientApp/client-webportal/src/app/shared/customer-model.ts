export class CustomerModel {
    constructor(public customerName: string,
         public customerAddres: string,
         public totalOrderedCost: number,
         public ordersCount: number
         ){}
}