﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortalServer.Model.WebEnities;
using WebPortalServer.Services;
using WebPortalServer.Services.Validators;

namespace WebPortalServer.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly WebPortalDBContext context;
        private readonly IOrderService service;
        private readonly IOrderValidator validator;

        public OrdersController(WebPortalDBContext context, IOrderService service, IOrderValidator validator)
        {
            this.context = context;
            this.service = service;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderModel>> GetAll()
        {
            var tmp = await context.Order.Where(x => !x.Archived).ToListAsync();
            
            tmp.ForEach(order => {
                order.Customer = context.Customer.FirstOrDefault(x => x.Id == order.CustomerId);
                order.OrderProduct = context.OrderProduct.Where(x => x.OderId == order.Id).ToList();
                foreach (var orderProduct in order.OrderProduct)
                {
                    orderProduct.Product = context.Product.FirstOrDefault(x => x.Id == orderProduct.Id);
                }
            }
            );
            
            return service.ConvertList(tmp);

            //return service.ConvertList(await context.Order
            //    .Where(x => !x.Archived)
            //    .ToListAsync());
        }
        [HttpGet("archived")]
        public async Task<IEnumerable<OrderModel>> GetAllArchived()
        {
            return service.ConvertList(await context.Order
                .Where(x => x.Archived)
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> Get(int id)
        {
            try
            {
                var order = new OrderModel(await context.Order.FirstOrDefaultAsync(x => x.Id == id));
                return Ok(order);
            }
            catch
            {
                return BadRequest(new ModelError("id", "Invalid id"));
            }
        }
        [HttpGet("byCustomer/{userId}")]
        public async Task<IEnumerable<OrderModel>> GetByUser(int id)
        {
            return service.ConvertList(await context.Order
                .Where(x => x.CustomerId == id)
                .ToListAsync());
        }
        [HttpGet("archived/{id}")]
        public async Task<ActionResult<OrderModel>> GetArchived(int id)
        {
            try
            {
                var order = new OrderModel(await context.Order.FirstOrDefaultAsync(x => x.Id == id));
                return Ok(order);
            }
            catch
            {
                return BadRequest(new ModelError("id", "Invalid id"));
            }
        }

        [HttpPost]
        public ActionResult Post(OrderModel model)
        {
            ModelError error = validator.IsValid(model);
            if (error.Count > 0)
                return BadRequest(error);
            try
            {
                var order = service.CreateOrder(model);
                return Accepted(new OrderModel(order));
            }
            catch (Exception ex)
            {
                error.AddError("order", ex.Message);
                return BadRequest(error);
            }
        }

        [HttpPut]
        public ActionResult Put(OrderModel model)
        {
            ModelError error = validator.IsValid(model);
            if (error.Count > 0)
                return BadRequest(error);

            try
            {
                var order = service.UpdateOrder(model);
                return Accepted(new OrderModel(order));
            }
            catch (Exception ex)
            {
                error.AddError("order", ex.Message);
                return BadRequest(error);
            }
        }
        [HttpDelete("archive/{id}")]
        public ActionResult Remove(int id)
        {
            try
            {
                var order = service.ArchiveOrder(id);
                return Accepted(new OrderModel(order));
            }
            catch (Exception ex)
            {
                return BadRequest(new ModelError("order", ex.Message));
            }
        }
        [HttpPut("unarchive/{id}")]
        public ActionResult Unarchive(int id)
        {
            try
            {
                return Accepted(new OrderModel(service.UnarchiveOrder(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(new ModelError("order", ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var order = service.DeleteOrder(id);
                return Accepted(new OrderModel(order));
            }
            catch (Exception ex)
            {
                return BadRequest(new ModelError("id", ex.Message));
            }
        }

        
    }
}
