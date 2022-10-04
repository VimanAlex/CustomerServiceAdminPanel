using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.ModelView;
using Customer_Service_Admin_Panel.Extension_Methods.AddCustomerAndAddressContext_Extension;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Customer_Service_Admin_Panel.Repository
{
    public class ContractRepository : IContractRepository
    {
        CustomerServiceEntity dbEntity;
       
        public ContractRepository(CustomerServiceEntity context)
        {
            this.dbEntity = context;
        }

        public IEnumerable<AddContract> GetContracts(int id)
        {
            var dataListContract = (from c in dbEntity.Contracts
                                    join a in dbEntity.Customers on c.CustomerId equals a.CustomerId
                                    join co in dbEntity.Products on c.ProductId equals co.ProductId
                                    where c.CustomerId == id

                                    select new
                                    {
                                        ContractId = c.ContractId,
                                        ProductId = co.ProductId,
                                        CustomerId = c.CustomerId,
                                        SignedDate = c.SignedDate,
                                        ProductName = co.ProductName

                                    }).AsEnumerable().Select(x => new AddContract
                                    {
                                        ContractId = x.ContractId,
                                        ProductId = x.ProductId,
                                        CustomerId = x.CustomerId,
                                        SignedDate = x.SignedDate,
                                        ProductName = x.ProductName
                                    }).ToList();

            return dataListContract;
        }

        public void AddContract(AddContract model , int id)
        {           
            Customer customer = dbEntity.Customers.Find(id);

                if (customer != null)
                {
                     dbEntity.Contracts.Add(model.ContractModel(customer));
                }

        }

        public void DeleteContract(int id, int contractId)
        {
            var customer = dbEntity.Customers.Find(id);
            var order = dbEntity.Orders.Where(x => x.OrderStatusId == 1).ToList();

            if (customer != null)
            {
                if (order.Count() == 0)
                {
                    var contract = dbEntity.Contracts.Where(x => x.ContractId == contractId).First();
                    dbEntity.Contracts.Remove(contract);
                   
                }
            }

        }

        public void UpdateContract(AddContract model, int id)
        {
            var contract = dbEntity.Contracts.Where(x => x.ContractId == model.ContractId).First();
            var customer = dbEntity.Customers.Find(id);

                if (contract != null && customer !=null)
                {
                    var updateContract = model.ContractModel(customer, contract);

                    dbEntity.Entry(updateContract).State = EntityState.Modified;
                }
            
        }

        public void Save()
        {
            dbEntity.SaveChanges();
        }

        public AddContract GetContract(int id,int contractId)
        {
            var data = (from c in dbEntity.Contracts
                        join a in dbEntity.Customers on c.CustomerId equals a.CustomerId
                        join co in dbEntity.Products on c.ProductId equals co.ProductId
                        where c.CustomerId == id && c.ContractId == contractId
                        select new
                        {
                            ContractId = c.ContractId,
                            CustomerId = c.CustomerId,
                            ProductId = co.ProductId,
                            ProductName = co.ProductName,
                            SignedDate = c.SignedDate

                        }).AsEnumerable().Select(x => new AddContract
                        {
                            ContractId = x.ContractId,
                            CustomerId = x.CustomerId,
                            ProductId = x.ProductId,
                            ProductName = x.ProductName,
                            SignedDate = x.SignedDate
                        }).First();

            return data;
        }
    }
}