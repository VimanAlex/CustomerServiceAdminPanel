using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Validation
{
    public class OrderValidation : IValidation<AddOrder>
    {
        public bool IsValid(AddOrder validationOrder, ModelStateDictionary _modelState)
        {
            if (validationOrder.OrderTypeId == null)
            {
                _modelState.AddModelError("OrderTypeId", "Select Order Type");
            }

            if (String.IsNullOrEmpty(validationOrder.CreateDate.ToString()))
            {
                _modelState.AddModelError("CreateDate", "Choise the date ");
            }


            if (validationOrder.Amount < 1000)
            {

                _modelState.AddModelError("Amount", "Minim amount 1000 ");
            }


            if (Convert.ToInt32(validationOrder.OrderStatusId) != 1)
            {
                _modelState.AddModelError("OrderStatusId", "Order must be in pending");
            }

            if(validationOrder.ListOfEmployees.Where(x=>x.IsChecked).Count() == 0)
            {
                _modelState.AddModelError("IsChecked", "You must select minim one Employee");
            }

            return _modelState.IsValid;
        }

        public bool IsValid(AddOrder entity, ModelStateDictionary modelState, int id)
        {
            throw new NotImplementedException();
        }
    }
}