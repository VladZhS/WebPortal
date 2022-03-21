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

        private void UpdateProducts(Order order)
        {
            foreach (var item in order.OrderProduct)
            {
                item.OderId = order.Id;

                var product = context.Product.FirstOrDefault(x => x.Id == item.ProductId);
                product.Quantity -= item.Quantity;
                
                if (product.Quantity < 0)
                    throw new InvalidOperationException($"{product.Name} quantity was greater than total quantity");
                
                context.Product.Update(product); //changing each order product quantity 
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
            var orderProducts = context.OrderProduct.Where(op => op.OderId == order.Id).ToList();
            context.OrderProduct.RemoveRange(orderProducts);

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
                    UpdateProducts(order);

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
            var order = context.Order.FirstOrDefault(o => o.Id == model.Id);

            if (order == null) throw new InvalidOperationException("Invalid id");
            if (order.Archived) throw new InvalidOperationException("Can't update archived order");

            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    order = model.ToEntity(order);

                    RemoveOrderProducts(order);
                    UpdateProducts(order);
                    UpdateOrder(order);

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

        private void UpdateOrder(Order order)
        {
            context.Order.Update(order);

            context.SaveChanges();
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
