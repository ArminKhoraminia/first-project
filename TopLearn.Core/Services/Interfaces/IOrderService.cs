using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Order;
using TopLearn.DataLayer.Entities.Order;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IOrderService
    {
        #region Get Order Infirmations

        Order GetOrderForValidation(string userName, int orderId);
        List<Order> GetAllOrderForUser(string userName);
        Order FindOrderById(int orderId);

        #endregion

        #region Order Functions

        int AddOrder(string userName, int courseId);
        int ReCalculateOrderPrice(int orderId);
        bool CheckOutFinalyOrder(string userName, int orderId);
        bool UpdateOder(Order order);

        #endregion

        #region Discount

        DiscountUseType UseDiscount(int orderid, string code);
        List<Discount> GetAllDiscounts();
        Discount FindDiscountById(int id);
        bool AddDiscount(Discount discount);
        bool UpdateDiscount(Discount discount);
        bool CheckExistDiscount(string code);

        #endregion

    }
}
