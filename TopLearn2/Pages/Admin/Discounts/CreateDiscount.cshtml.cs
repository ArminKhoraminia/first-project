using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Order;

namespace TopLearn2.Web.Pages.Admin.Discounts
{
    public class CreateDiscountModel : PageModel
    {

        #region Create Context

        private IOrderService _orderService;
        public CreateDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        [BindProperty]
        public Discount Discount { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost(string stDate, string edDate)
        {
            if (!string.IsNullOrEmpty(stDate))
            {
                string[] std = stDate.Split('/');
                Discount.StartDate = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                    );
            }
            if (!string.IsNullOrEmpty(edDate))
            {
                string[] edd = edDate.Split('/');
                Discount.EndDate = new DateTime(int.Parse(edd[0]),
                    int.Parse(edd[1]),
                    int.Parse(edd[2]),
                    new PersianCalendar()
                    );
            }
            if (string.IsNullOrEmpty(Discount.DiscountCode))
                return Page();

            if (Discount.DiscountPercent == 0)
                return Page();

            if (_orderService.CheckExistDiscount(Discount.DiscountCode) == false)
                return Page();

            bool isSuccess = _orderService.AddDiscount(Discount);
            if (isSuccess)
                return Redirect("/Admin/Discounts/ShowDiscounts");
            else
                return Page();
        }

        public IActionResult OnGetCheckCode(string code)
        {
            return Content(_orderService.CheckExistDiscount(code).ToString());
        }
    }
}
