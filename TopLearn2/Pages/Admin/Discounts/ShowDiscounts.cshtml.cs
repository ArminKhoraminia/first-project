using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Order;

namespace TopLearn2.Web.Pages.Admin.Discounts
{
    public class ShowDiscountsModel : PageModel
    {
        #region Create Context

        private IOrderService _orderService;
        public ShowDiscountsModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        [BindProperty]
        public List<Discount> Discounts { get; set; }
        public void OnGet()
        {
            Discounts = _orderService.GetAllDiscounts();
        }
    }
}
