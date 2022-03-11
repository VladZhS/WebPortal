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

        private void AddOrder(Order order)
        {
            order.Archived = false;

            order.Status = null;

            context.Order.Add(order);

            context.SaveChanges();
        }

        private void AddOrderProducts(Order order)
        {
            foreach (var item in order.OrderProduct)
            {
                var product = context.Product.FirstOrDefault(x => x.Id == item.Product.Id);
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
                var product = context.Product.FirstOrDefault(x => x.Id == item.Product.Id);
                product.Quantity += item.Quantity;
            }

            //removing all the orderproducts of this order
            context.OrderProduct = (DbSet<OrderProduct>)context.OrderProduct.Where(x => x.Id != order.Id);
        }

        public Order ArchiveOrder(int id)
        {
            var order = context.Order.FirstOrDefault(x => x.Id == id);
            if (order == null)
                throw new InvalidOperationException("Invalid id");

            order.Archived = true;

            context.Order.Update(order);
            context.SaveChanges(true);

            return order;
        }

        public Order UnarchiveOrder(int id)
        {
            var order = context.Order.FirstOrDefault(x => x.Id == id);
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
            var order = context.Order.FirstOrDefault(x => x.Id == model.Id);
            if (order != null)
                throw new InvalidOperationException("Invalid id");
            if (order.Archived)
                throw new InvalidOperationException("Can't update archived order");


            order = model.ToEntity(order);

            RemoveOrderProducts(order);

            try { AddOrderProducts(order); }
            catch (Exception ex) { throw ex; }

            context.Order.Update(order);
            context.SaveChanges();

            return order;
        }
        public Order DeleteOrder(int id)
        {
            var order = context.Order.FirstOrDefault(x => x.Id == id);
            if (order == null)
                throw new InvalidOperationException("Invalid id");

            RemoveOrderProducts(order);

            context.Order.Remove(order);
            return order;
        }

        public IEnumerable<OrderModel> ConvertList(IEnumerable<Order> collection)
        {
            return collection.ConvertModel<Order, OrderModel>();
        }
    }
}
