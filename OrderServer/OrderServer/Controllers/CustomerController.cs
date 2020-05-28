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
    public class CustomerController : ControllerBase
    {
        private readonly OrderContext orderDb;
        public CustomerController(OrderContext orderContext)
        {
            orderDb = orderContext;
        }

        //用户注册
        [HttpPost("register")]
        public ActionResult<Customer> Register(Customer customer)
        {
            Customer customer1 = null;
            try
            {
                orderDb.Customers.Add(customer);
                orderDb.SaveChanges();
            }catch(Exception e)
            {
                return customer1;
            }
            return customer;
        }

        //用户登录
        [HttpPost("login")]
        public ActionResult<Customer> Login(Customer customer)
        {
            Customer customer1 = null;
            var cus = orderDb.Customers.Where(op => op.Id == customer.Id)
                .Where(op => op.Password == customer.Password).ToList<Customer>();
            if (cus.Count == 0)
                return customer1;
            return cus[0];
        }

        //用户账号设置
        [HttpPut]
        public ActionResult<Customer> SetAccount(Customer customer)
        {
            try
            {
                orderDb.Entry(customer).State = EntityState.Modified;
                orderDb.SaveChanges();
            }catch(Exception e)
            {
                return Content("修改失败");
            }
            return customer;
        }

        //获取该用户的所有订单
        [HttpGet("{id}")]
        public ActionResult<List<Order>> GetAllOrders(int id)
        {
            var orders = orderDb.Orders.Include("OrderItems").Where(op => op.CustomerId == id).ToList<Order>();
            return orders;
        }

    }
}