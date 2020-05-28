using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderServer.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext orderDb;

        public OrderController(OrderContext orderContext)
        {
            orderDb = orderContext;
        }

        //下单
        [HttpPost]
        public ActionResult<Order> AddOrder(Order order)
        {
            try
            {
                order.OrderState = "准备中";
                foreach (OrderItem orderItem in order.OrderItems)
                {
                    var cuisine = orderDb.Cuisines.FirstOrDefault(op => op.Id == orderItem.CuisineId);
                    order.TotalPrice += cuisine.UnitPrice * orderItem.Amount;
                }
                orderDb.Orders.Add(order);
                orderDb.SaveChanges();
            }catch(Exception e)
            {
                return Content(e.InnerException.Message);
            }
            return Content("订餐成功");
        }

        //商家确认完成订单
        [HttpPut]
        public ActionResult FinishOrder(Order order)
        {
            try
            {
                order.OrderState = "以完成，等待配送";
                orderDb.Entry(order).State = EntityState.Modified;
                orderDb.SaveChanges();
            }catch(Exception e)
            {
                return Content(e.InnerException.Message);
            }
            return Content("订单已完成");
        }
    }
}