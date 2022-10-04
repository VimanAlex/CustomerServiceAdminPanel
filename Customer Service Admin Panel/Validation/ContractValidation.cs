using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Validation
{
    public class ContractValidation : IValidation<AddContract>
    {
        public bool IsValid(AddContract validationContract, ModelStateDictionary _ModelState)
        {
            if (String.IsNullOrEmpty(validationContract.SignedDate.ToString()))
            {
                _ModelState.AddModelError("SignedDate", "Choise the date ");
            }

            if (validationContract.ProductId == null)
            {
                _ModelState.AddModelError("ProductId", "Choise a product");
            }

            return _ModelState.IsValid;
        }

        public bool IsValid(AddContract entity, ModelStateDictionary modelState, int id)
        {
            throw new NotImplementedException();
        }
    }
}