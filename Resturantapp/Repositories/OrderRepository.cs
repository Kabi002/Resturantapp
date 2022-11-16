using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppRestaurantApp.Models;
using WebAppRestaurantApp.ViewModel;

namespace WebAppRestaurantApp.Repositories
{

    public class OrderRepository
    {
        private mvcprojectdbEntities objRestaurantDbEntities;

        public OrderRepository()
        {
            objRestaurantDbEntities = new mvcprojectdbEntities();

        }

        public bool AddOrder(OrderViewModel objOrderViewModel)
        {
            try
            {
                Order objOrder = new Order();

                objOrder.CustomerId = objOrderViewModel.CustomerId;
                objOrder.FinalTotal = objOrderViewModel.FinalTotal;
                objOrder.OrderDate = DateTime.Now;
                objOrder.OrderNumber = String.Format("{0:ddmmmyyyyhhmmss}", DateTime.Now);
                objOrder.PaymentTypeId = objOrderViewModel.PaymentTypeId;

                objRestaurantDbEntities.Orders.Add(objOrder);
                objRestaurantDbEntities.SaveChanges();
                int OrderId = objOrder.OrderId;

                foreach (var item in objOrderViewModel.ListOfOrderDetailViewModel)
                {
                    OrderDetail objOrderDetail = new OrderDetail();
                    objOrderDetail.OrderId = OrderId;
                    objOrderDetail.Discount = item.Discount;
                    objOrderDetail.ItemId = item.ItemId;
                    objOrderDetail.Total = item.Total;  
                    objOrderDetail.UnitPrice = item.UnitPrice;
                    objOrderDetail.Quantity = item.Quantity;
                    objRestaurantDbEntities.OrderDetails.Add(objOrderDetail);
                    objRestaurantDbEntities.SaveChanges();


                    Transaction objTransaction = new Transaction();
                    objTransaction.ItemId = item.ItemId;
                    objTransaction.Quantity = (-1) * item.Quantity;
                    objTransaction.TransactionDate = DateTime.Now;
                    objTransaction.TypeId = 2;
                    objRestaurantDbEntities.Transactions.Add(objTransaction);
                    objRestaurantDbEntities.SaveChanges();

                }

                return true; 

            }
            catch
            {
                return false;
            }
}

    }
}