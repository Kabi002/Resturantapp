using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppRestaurantApp.Models;
using WebAppRestaurantApp.Repositories;
using WebAppRestaurantApp.ViewModel;

namespace WebAppRestaurantApp.Controllers
{
    public class HomeController : Controller
    {
        private mvcprojectdbEntities objRestaurantDbEntities;
        public HomeController()
        {
            objRestaurantDbEntities = new mvcprojectdbEntities();

        }
        public ActionResult Index()
        {
           /* var objCustomerRepository = new CustomerRepository();
            var objItemRepository = new ItemRepository();
            var objPaymentRepository = new PaymentTypeRepository();*/

            CustomerRepository objCustomerRepository = new CustomerRepository();
            ItemRepository objItemRepository = new ItemRepository();
            PaymentTypeRepository objPaymentRepository = new PaymentTypeRepository();

            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>(
                    objCustomerRepository.GetAllCustomers(), objItemRepository.GetAllItems(), objPaymentRepository.GetAllPaymentType());

            return View(objMultipleModels);
        }
        [HttpGet]

       public JsonResult getItemUnitPrice(int itemId)
        {
            decimal UnitPrice = (decimal)objRestaurantDbEntities.Items.Single(model => model.ItemId == itemId).ItemPrice;
            return Json(UnitPrice, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Index(OrderViewModel objOrderViewModel)
        {
            OrderRepository objOrderRepository  = new OrderRepository();
           bool isstatus= objOrderRepository.AddOrder(objOrderViewModel);
            string SuccessMessage = String.Empty;

            if (isstatus)
            {
                SuccessMessage = "Hi!! Your Order has  been Successfully Placed.";
            }
            else
            {
                SuccessMessage = "sorry!! There is some issue while placing order.";
            }
            return Json(SuccessMessage, JsonRequestBehavior.AllowGet);
        }

    }
}