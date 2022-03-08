import { CustomerModel } from "../shared/customer-model";


export class CustomerService{
    customers: CustomerModel[] = []
    addCustomer(customer: CustomerModel) {
        this.customers?.push(customer)
    }
    getCustomers(){
        return this.customers?.slice()
    }

    setCustomer(customer: CustomerModel){
        const index = this.customers.findIndex(x =>
            {
                return x.customerId == customer.customerId;
            })
            this.customers[index] = customer
    }

}