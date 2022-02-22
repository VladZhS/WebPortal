import { CustomerModel } from "../shared/customer-model";


export class CustomerService{
    customers: CustomerModel[] =  [
        new CustomerModel("John Smith", "5th Ave, New York", 150, 3),
        new CustomerModel("John Smith", "5th Ave, New York", 150, 3)
      ]
    addCustomer(customer: CustomerModel) {
        this.customers.push(customer)
    }
    getCustomers(){
        return this.customers.slice()
    }
}