export class CustomerModel {
    constructor(
        public customerId: number,
        public customerName: string,
        public customerAddress: string,
        public totalOrderedCost: number,
        public ordersCount: number
        ){}
}