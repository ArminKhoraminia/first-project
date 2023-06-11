using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Order;
using TopLearn.Core.DTOs.Order;
using System.Diagnostics;

namespace TopLearn2.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class MyOrdersController : Controller
    {
        #region Create Context

        private IOrderService _orderService;
        public MyOrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        public IActionResult Index()
        {
            return View(_orderService.GetAllOrderForUser(User.Identity.Name));
        }

        [Route("UserPanel/ShowOrder/{id}")]
        public IActionResult ShowOrder(int id, bool finaly = false,string errorType="")
        {
            Order order = _orderService.GetOrderForValidation(User.Identity.Name, id);

            if (order == null) return NotFound();

            ViewBag.Finaly = finaly;
            ViewBag.ErrorType = errorType;
            return View(order);
        }

        [Route("UserPanel/FinalyOrder/{id}")]
        public IActionResult FinalyOrder(int id)
        {
            bool isSucces = _orderService.CheckOutFinalyOrder(User.Identity.Name, id);
            if (isSucces)
            {
                return Redirect("/UserPanel/ShowOrder/" + id + "?Finaly=true");
            }
            else
            {
                return BadRequest("به دلیل مشکلاتی خرید شما با موافقیت انجام نشد ، لطفل دوباره تلاش کنید");
            }
        }

        [Route("UserPanel/UseDisCount/")]
        [HttpPost]
        public IActionResult UseDisCount(int orderId , string code)
        {
            bool ddd = true;
            DiscountUseType errorType = _orderService.UseDiscount(orderId, code);
            return Redirect("/UserPanel/ShowOrder/" + orderId + "?errorType=" + errorType.ToString());  
        }
    }
}
