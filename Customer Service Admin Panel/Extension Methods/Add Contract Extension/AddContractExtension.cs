using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer_Service_Admin_Panel.Extension_Methods.AddCustomerAndAddressContext_Extension
{
    public static class AddContractExtension
    {
        public static Contract ContractModel(this AddContract contractModel,Customer customer,Contract contract = null,Product product=null)
        {
            if(contract == null)
            {
                contract = new Contract();
            }
            if(product == null)
            {
                product = new Product();
            }

            contract.Customer = customer;
            contract.ProductId = contractModel.ProductId;            
            contract.SignedDate = contractModel.SignedDate;

            return contract;
        }
    }
}