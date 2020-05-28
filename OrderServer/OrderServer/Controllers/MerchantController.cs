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
    public class MerchantController : ControllerBase
    {
        private readonly OrderContext orderDb;

        private Merchant merchant=new Merchant();

        public MerchantController(OrderContext orderContext)
        {
            orderDb = orderContext;
        }

        //商家注册
        [HttpPost]
        public ActionResult<Merchant> Register(Merchant merchant)
        {
            try
            {
                orderDb.Merchants.Add(merchant);
                orderDb.SaveChanges();
            } catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return merchant;
        }

        //商家登录
        [HttpPost("login")]
        public ActionResult<Merchant> Login(Merchant merchant)
        {
            var mer = orderDb.Merchants.Where(op => op.Id == merchant.Id)
                .Where(op => op.Password == merchant.Password).ToList<Merchant>();
            if (mer.Count == 0)
                return Content("账号或密码错误");
            merchant = mer[0];
            return mer[0];
        }

        //商家账号修改
        [HttpPut]
        public ActionResult<Merchant> SetAccount(Merchant merchant)
        {
            try
            {
                orderDb.Entry(merchant).State = EntityState.Modified;
                orderDb.SaveChanges();
            }
            catch (Exception e)
            {
                return Content("修改失败");
            }
            return merchant;
        }

        //获取该店所有菜品
        [HttpGet("{id}")]
        public ActionResult<List<Cuisine>> GetAllCuisines(int id)
        {
            var cuisines = orderDb.Cuisines.Where(op =>op.MerchantId== id).ToList<Cuisine>();
            return cuisines;
        }

        //上架新菜品
        [HttpPost("addcuisine")]
        public ActionResult AddCuisine(Cuisine cuisine)
        {
            try
            {
                orderDb.Cuisines.Add(cuisine);
                orderDb.SaveChanges();
            }catch(Exception e)
            {
                return Content(e.InnerException.Message);
            }
            return Content("添加成功");
        }

        //下架菜品
        [HttpDelete]
        public ActionResult DeleteCuisine(Cuisine cuisine)
        {
            try
            {
                orderDb.Cuisines.Remove(cuisine);
                orderDb.SaveChanges();
            }catch(Exception e)
            {
                return Content(e.InnerException.Message);
            }
            return Content("删除成功");
        }

        //修改菜品信息
        [HttpPut("setcuisine")]
        public ActionResult SetCuisine(Cuisine cuisine)
        {
            try
            {
                orderDb.Entry(cuisine).State = EntityState.Modified;
                orderDb.SaveChanges();
            }catch(Exception e)
            {
                return Content(e.InnerException.Message);
            }
            return Content("修改成功");
        }

        //获取所有待完成订单
        [HttpGet]
        public ActionResult<List<Order>> GetOrders()
        {
            var orders = orderDb.Orders.Include("Customer").Include("OrderItems")
                .Where(op => op.OrderState == "准备中").ToList<Order>();
            return orders;
        }

        //完成交易
        [HttpPut("finish")]
        public ActionResult FinishDeal(Order order)
        {
            try
            {
                order.OrderState = "交易已完成";
                order.Customer.Balance -= order.TotalPrice;
                merchant.Balance += order.TotalPrice;
                orderDb.Entry(order).State = EntityState.Modified;
                orderDb.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(e.InnerException.Message);
            }
            return Content("已确认收货，交易成功");
        }
    }

}