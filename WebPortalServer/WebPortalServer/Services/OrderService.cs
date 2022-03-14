using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services
{
    public class OrderService : IOrderService
    {
        private readonly WebPortalDBContext context;

        public OrderService(WebPortalDBContext context)
        {
            this.context = context;
        }
        private IQueryable<Order> GetOrders()
        {
            return context.Order
                .Include(o => o.Customer)
                .Include(o => o.OrderProduct).ThenInclude(p => p.Product);
        }

        private void AddOrder(Order order)
        {

            Order safeOrder = new Order()
            {
                Date = order.Date,
                StatusId = order.StatusId,
                CustomerId = order.CustomerId,
                Description = order.Description,
                Archived = false
            };

            context.Order.Add(safeOrder);

            context.SaveChanges();

            order.Id = safeOrder.Id;
        }

        private void AddOrderProducts(Order order)
        {
            foreach (var item in order.OrderProduct)
            {
                var product = context.Product.FirstOrDefault(x => x.Id == item.ProductId);
                product.Quantity -= item.Quantity; 
                if (product.Quantity < 0)
                    throw new InvalidOperationException($"{product.Name} quantity was greater than total quantity");
                context.Product.Update(product); //changing each order product quantity 

                
                context.OrderProduct.Add(item); //adding orderproduct links
            }
            context.SaveChanges();
        }
       
        private void RemoveOrderProducts(Order order)
        {
            //returning quantities
            foreach (var item in order.OrderProduct)
            {
                var product = context.Product.FirstOrDefault(x => x.Id == item.ProductId);
                product.Quantity += item.Quantity;
            }

            //removing all the orderproducts of this order
            context.OrderProduct.RemoveRange(context.OrderProduct.Where(x => x.OderId == order.Id));


            context.SaveChanges();
        }

        public Order ArchiveOrder(int id)
        {
            var order = GetOrders().FirstOrDefault(x => x.Id == id);
            if (order == null)
                throw new InvalidOperationException("Invalid id");

            order.Archived = true;

            context.Order.Update(order);
            context.SaveChanges();

            return order;
        }

        public Order UnarchiveOrder(int id)
        {
            var order = GetOrders().FirstOrDefault(x => x.Id == id);
            if (order == null)
                throw new InvalidOperationException("Invalid id");

            foreach (var item in order.OrderProduct)
            {
                var product = context.Product.FirstOrDefault(x => x.Id == item.Product.Id);
                product.Quantity -= item.Quantity;
                if (product.Quantity < 0)
                    throw new InvalidOperationException($"{product.Name} quantity was greater than total quantity");
                context.Product.Update(product); //changing each order product quantity
            }

            order.Archived = false;
            
            context.Order.Update(order);
            context.SaveChanges();

            return order;
        }
        public Order CreateOrder(OrderModel model)
        {
            var order = model.ToEntity(
                new Order()
                {
                    Status = new OrderStatus(),
                    Customer = new Customer()
                });

            using (var tran = context.Database.BeginTransaction())
            {
                try 
                {
                    AddOrder(order);
                    AddOrderProducts(order);

                    tran.Commit();
                }
                catch
                { 
                    tran.Rollback();
                    throw; 
                }
            }

            return order;
        }
        public Order UpdateOrder(OrderModel model)
        {
            var order = GetOrders()
                .FirstOrDefault(x => x.Id == model.Id);
            if (order == null)
                throw new InvalidOperationException("Invalid id");
            if (order.Archived)
                throw new InvalidOperationException("Can't update archived order");


            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    RemoveOrderProducts(order);

                    order = model.ToEntity(order);

                    AddOrderProducts(order);

                    context.Order.Update(order);
                    context.SaveChanges();

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }

            return order;
        }
        public Order DeleteOrder(int id)
        {
            var order = GetOrders()
                .FirstOrDefault(x => x.Id == id);

            if (order == null)
                throw new InvalidOperationException("Invalid id");

            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    RemoveOrderProducts(order);

                    context.Order.Remove(order);
                    context.SaveChanges();
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
            return order;
        }

        public IEnumerable<OrderModel> ConvertList(IEnumerable<Order> collection)
        {
            var orders = collection.ToList();
            orders.ForEach(x =>
            {
                foreach (var product in x.OrderProduct)
                    product.Product.Quantity = product.Quantity;
            });
            return orders.ConvertModel<Order, OrderModel>().ToList();
        }
    }
}
