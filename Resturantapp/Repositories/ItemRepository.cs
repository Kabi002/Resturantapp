using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppRestaurantApp.Models;

namespace WebAppRestaurantApp.Repositories
{
    public class ItemRepository
    {
        private mvcprojectdbEntities objRestaurantDbEntities;

        public ItemRepository()
        {
            objRestaurantDbEntities = new mvcprojectdbEntities();
        }

        public IEnumerable<SelectListItem> GetAllItems()
        {
            var objSelectListItems = new List<SelectListItem>();
            objSelectListItems = (from obj in objRestaurantDbEntities.Items
                                  select new SelectListItem()
                                  {
                                      Text = obj.ItemName,
                                      Value = obj.ItemId.ToString(),
                                      Selected = true

                                  }).ToList();
            return objSelectListItems;
        }
    }
}