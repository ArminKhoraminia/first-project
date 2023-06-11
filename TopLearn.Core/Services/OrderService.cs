using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Order;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.Order;
using TopLearn.DataLayer.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.Core.Services
{
    public class OrderService : IOrderService
    {
        #region Create Context

        private TopLearnContext _context;
        private IUserService _userService;
        private ICourseService _courseService;
        private IWalletService _walletService;
        public OrderService(TopLearnContext context, IUserService userService, ICourseService courseService, IWalletService walletService)
        {
            _context = context;
            _userService = userService;
            _courseService = courseService;
            _walletService = walletService;
        }

        #endregion

        #region Get Order Infirmations

        public Order GetOrderForValidation(string userName, int orderId)
        {
            int userId = _userService.GetIdByUserName(userName);
            Order order = _context.Order.Include(o => o.OrderDetails).ThenInclude(o => o.Course).SingleOrDefault(o => o.UserId == userId && o.OrderId == orderId);
            return order;
        }

        public List<Order> GetAllOrderForUser(string userName)
        {
            int userId = _userService.GetIdByUserName(userName);
            return _context.Order.Where(o => o.UserId == userId).ToList();
        }

        public Order FindOrderById(int orderId)
        {
            return _context.Order.Find(orderId);
        }

        #endregion

        #region Order Functions

        public int AddOrder(string userName, int courseId)
        {
            int userId = _userService.GetIdByUserName(userName);
            Course course = _courseService.GetCourseById(courseId);
            Order order = _context.Order.FirstOrDefault(o => o.UserId == userId && o.IsFinaly == false);

            using IDbContextTransaction transaction = _context.Database.BeginTransaction();

            try
            {
                if (order == null)
                {
                    order = new Order()
                    {
                        IsFinaly = false,
                        CreateDate = DateTime.Now,
                        UserId = userId,
                        OrderPrice = course.CoursePrice,
                        OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            Count = 1,
                            CourseId = courseId,
                            Price = course.CoursePrice,

                        }
                    }
                    };
                    _context.Order.Add(order);
                }
                else
                {
                    OrderDetail detail = _context.OrderDetail
                        .FirstOrDefault(d => d.CourseId == courseId && d.OrderId == order.OrderId);

                    if (detail == null)
                    {
                        detail = new OrderDetail()
                        {
                            OrderId = order.OrderId,
                            Count = 1,
                            CourseId = courseId,
                            Price = course.CoursePrice

                        };
                        _context.OrderDetail.Add(detail);
                    }
                    else
                    {
                        detail.Count++;
                        _context.OrderDetail.Update(detail);
                    }

                    _context.SaveChanges(); // SaveChanges For Order Detail

                    order.OrderPrice = ReCalculateOrderPrice(order.OrderId);
                    _context.Order.Update(order);
                }

                _context.SaveChanges(); // SaveChanges For Order

                transaction.Commit();

                return order.OrderId;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return 0;
            }

        }

        public int ReCalculateOrderPrice(int orderId)
        {
            return _context.OrderDetail.Where(d => d.OrderId == orderId).Sum((d => d.Price * d.Count));
        }

        public bool CheckOutFinalyOrder(string userName, int orderId)
        {

            int userId = _userService.GetIdByUserName(userName);
            Order order = _context.Order.Include(o => o.OrderDetails).SingleOrDefault(o => o.OrderId == orderId);
            int amountWallet = _walletService.GetWalletPriceUserName(userName);

            if (order == null) return false;
            if (order.UserId != userId || order.IsFinaly != false) return false;
            if (amountWallet < order.OrderPrice) return false;

            using IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                order.IsFinaly = true;
                _context.Order.Update(order);

                Wallet wallet = new Wallet()
                {
                    Amount = order.OrderPrice,
                    DateTime = DateTime.Now,
                    Description = "خرید دوره با شماره فاکتور : " + order.OrderId,
                    IsPayed = true,
                    TypeId = 2,
                    UserId = userId,
                };
                _context.Wallet.Add(wallet);

                foreach (var item in order.OrderDetails)
                {
                    _context.UserCourse.Add(new UserCourse()
                    {
                        CourseId = item.CourseId,
                        UserId = userId
                    });
                }

                _context.SaveChanges();

                transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
        }

        public bool UpdateOder(Order order)
        {
            _context.Order.Update(order);
            _context.SaveChanges();
            return true;
        }

        #endregion

        #region Discount

        public DiscountUseType UseDiscount(int orderid, string code)
        {
            Discount discount = _context.Discount.SingleOrDefault(d => d.DiscountCode == code);
            Order order = FindOrderById(orderid);
            bool usedDiscount = false;
            if (discount != null && order != null)
            {
                usedDiscount = _context.UserDiscount.Any(d => d.DiscountId == discount.DiscountId && d.UserId == order.UserId);
            }

            if (discount == null)
                return DiscountUseType.NotFound;
            if (discount.UsableCount <= 0)
                return DiscountUseType.FinishedUsable;
            if (discount.StartDate != null && discount.StartDate > DateTime.Now)
                return DiscountUseType.IsNotStarted;
            if (discount.EndDate != null && discount.EndDate < DateTime.Now)
                return DiscountUseType.ExpireDate;
            if (discount.DiscountPercent == 0)
                return DiscountUseType.IsPercentZero;
            if (order == null)
                return DiscountUseType.NotFoundOrder;
            if (usedDiscount == true)
                return DiscountUseType.IsUsedDiscountForThisOrder;


            using IDbContextTransaction transaction = _context.Database.BeginTransaction();

            try
            {
                int tmpPercent = 100 - discount.DiscountPercent;
                order.OrderPrice = (order.OrderPrice * tmpPercent) / 100;
                UpdateOder(order);

                discount.UsableCount--;
                _context.Discount.Update(discount);

                _context.UserDiscount.Add(new UserDiscount()
                {
                    DiscountId = discount.DiscountId,
                    UserId = order.UserId,
                });

                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return DiscountUseType.Failure;
            }



            return DiscountUseType.Success;
        }

        public List<Discount> GetAllDiscounts()
        {
            return _context.Discount.ToList();
        }

        public Discount FindDiscountById(int id)
        {
            return _context.Discount.Find(id);
        }

        public bool AddDiscount(Discount discount)
        {
            _context.Discount.Add(discount);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateDiscount(Discount discount)
        {
            bool flgExist = _context.Discount.Any(d => d.DiscountCode == discount.DiscountCode && d.DiscountId != discount.DiscountId);
            if (flgExist) return false;

            _context.Discount.Update(discount);
            _context.SaveChanges();

            return true;
        }

        public bool CheckExistDiscount(string code)
        {
            return _context.Discount.Any(d => d.DiscountCode == code);
        }


        #endregion

    }
}
