import { OrderModel } from "../shared/order-model";

export class OrderService{
    orders: OrderModel[]= []

    getOrders(){
        return this.orders?.slice()
    }
    addOrder(order: OrderModel){
        this.orders?.push(order)
    }
    getOrderById(id: number){
        return this.orders.find(x => x.orderNumber == id);
    }
    
}