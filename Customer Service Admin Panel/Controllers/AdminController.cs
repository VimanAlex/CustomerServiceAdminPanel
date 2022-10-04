
using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Controllers
{
    public class AdminController : Controller
    {
        private CustomerServiceEntity dbEntity = new CustomerServiceEntity();
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {

            return View();
        }

        [Authorize]
        public ActionResult Customer()
        {
            var data = dbEntity.Customers.ToList();
            return View(data);
        }

        [Authorize]
        public ActionResult AddCustomer()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer(AddCustomerAndAdressContext add)
        {
            if (ModelState.IsValid)
            {

                Model.Customer customer = new Model.Customer();
                customer.OrganizationNumber = add.OrganizationNumber;
                customer.OrganizationName = add.OrganizationName;
                dbEntity.Customers.Add(customer);
                dbEntity.SaveChanges();

                Adress adress = new Adress();
                adress.CustomerId = customer.CustomerId;
                adress.StreetAddess1 = add.StreetAddess1;
                adress.StreetAddress2 = add.StreetAddress2;
                adress.Region = add.Region;
                adress.City = add.City;
                adress.ZipCode = add.ZipCode;
                dbEntity.Adresses.Add(adress);
                dbEntity.SaveChanges();

                Contact contact = new Contact();
                contact.Customer = customer;
                contact.FirstName = add.FirstName;
                contact.LastName = add.LastName;
                contact.Mobile = add.Mobile;
                contact.Email = add.Email;
                contact.IsMain = add.IsMain;
                dbEntity.Contacts.Add(contact);
                dbEntity.SaveChanges();




                return RedirectToAction("Customer");
            }
            return View();

        }
        [Authorize]
        public ActionResult DetailsCustomer(int id , string searchOrder,string searchContract)
        {

            var data = (from c in dbEntity.Customers
                        join a in dbEntity.Adresses on c.CustomerId equals a.CustomerId
                        join co in dbEntity.Contacts on c.CustomerId equals co.CustomerId
                        where c.CustomerId == id
                        select new
                        {
                            CustomerId = c.CustomerId,
                            OrganizationName = c.OrganizationName,
                            OrganizationNumber = c.OrganizationNumber,
                            Region = a.Region,
                            City = a.City,
                            StreetAddess1 = a.StreetAddess1,
                            StreetAddress2 = a.StreetAddress2,
                            ZipCode = a.ZipCode,
                            FirstName = co.FirstName,
                            LastName = co.LastName,
                            Mobile = co.Mobile,
                            Email = co.Email,
                            IsMain = co.IsMain,

                        }).AsEnumerable().Select(x => new AddCustomerAndAdressContext {
                            CustomerId = x.CustomerId,
                            OrganizationName = x.OrganizationName,
                            OrganizationNumber = x.OrganizationNumber,
                            Region = x.Region,
                            City = x.City,
                            StreetAddess1 = x.StreetAddess1,
                            StreetAddress2 = x.StreetAddress2,
                            ZipCode = x.ZipCode,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Mobile = x.Mobile,
                            Email = x.Email,
                            IsMain = x.IsMain,
                        }).ToList();

            List<AddCustomerAndAdressContext> list = new List<AddCustomerAndAdressContext>();

            list.AddRange(data);

            List<AddContract> listContracts = new List<AddContract>();

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

            listContracts.AddRange(dataListContract);

           

            //search textbox contracts

            ViewData["SearchContracts"] = searchContract;

            if (!String.IsNullOrEmpty(searchContract))
            {
                listContracts = listContracts.Where(s => s.ProductName.Contains(searchContract) || s.SignedDate.ToString().Contains(searchContract)).ToList();
            }

            ViewBag.ContractList = listContracts;



            List < AddOrder > listOrders = new List<AddOrder>();

            var dataListOrder = (from o in dbEntity.Orders
                                 join ot in dbEntity.OrderTypes on o.OrderTypeId equals ot.OrderTypeId
                                 join ol in dbEntity.OrderLines on o.OrderId equals ol.OrderId
                                 join emp in dbEntity.Employees on ol.EmployeeId equals emp.EmployeeId
                                 join cust in dbEntity.Customers on emp.CustomerId equals cust.CustomerId
                                 join os in dbEntity.OrderStatus on o.OrderStatusId equals os.OrderStatusId
                                 where cust.CustomerId == id
                                 
                                 select new
                                 {
                                     EmployeeId = emp.EmployeeId,
                                     OrderId=o.OrderId,
                                     CustomerId = cust.CustomerId,
                                     OrderStatusId = os.OrderStatusId,
                                     OrderStatus = os.OrderStatus,
                                    // OrderStatus = os.OrderStatus1,
                                     OrderTypeId = ot.OrderTypeId,
                                     OrderType = ot.OrderType1,
                                     CreateDate = o.CreateDate,
                                     Amount = ol.Amount,
                                     FirstName = emp.FirstName,
                                     LastName = emp.LastName,

                                 }).AsEnumerable().Select(x => new AddOrder
                                 {
                                     EmployeeId = x.EmployeeId,
                                     OrderId =x.OrderId,
                                     CustomerId = x.CustomerId,
                                     OrderStatus = x.OrderStatus,
                                     OrderStatusId = x.OrderStatusId,
                                     OrderTypeId = x.OrderTypeId,
                                     OrderType1 = x.OrderType,
                                     CreateDate = x.CreateDate,
                                     Amount =  x.Amount,
                                     FirstName = x.FirstName,
                                     LastName = x.LastName,
                                 }).ToList();






            listOrders.AddRange(dataListOrder);

            //search orders 

            ViewData["SearchOrders"] = searchOrder;

            if (!String.IsNullOrEmpty(searchOrder))
            {
                listOrders = listOrders.Where(x => x.OrderType1.Contains(searchOrder) || 
                                              x.FirstName.Contains(searchOrder) || x.LastName.Contains(searchOrder) ||x.Amount.ToString() == searchOrder.ToString() || x.CreateDate.ToString().Contains(searchOrder)).ToList();
            }

            ViewBag.ListOrders = listOrders;



            return View(list);
        }

       

        [Authorize]
        public ActionResult UpdateCustomer(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = (from c in dbEntity.Customers
                        join a in dbEntity.Adresses on c.CustomerId equals a.CustomerId
                        join co in dbEntity.Contacts on c.CustomerId equals co.CustomerId
                        where c.CustomerId == id
                        select new
                        {
                            CustomerId = c.CustomerId,
                            OrganizationName = c.OrganizationName,
                            OrganizationNumber = c.OrganizationNumber,
                            Region = a.Region,
                            City = a.City,
                            StreetAddess1 = a.StreetAddess1,
                            StreetAddress2 = a.StreetAddress2,
                            ZipCode = a.ZipCode,
                            FirstName = co.FirstName,
                            LastName = co.LastName,
                            Mobile = co.Mobile,
                            Email = co.Email,
                            IsMain = co.IsMain,

                        }).AsEnumerable().Select(x => new AddCustomerAndAdressContext
                        {
                            CustomerId = x.CustomerId,
                            OrganizationName = x.OrganizationName,
                            OrganizationNumber = x.OrganizationNumber,
                            Region = x.Region,
                            City = x.City,
                            StreetAddess1 = x.StreetAddess1,
                            StreetAddress2 = x.StreetAddress2,
                            ZipCode = x.ZipCode,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Mobile = x.Mobile,
                            Email = x.Email,
                            IsMain = x.IsMain,
                        }).ToList().First();


            return View(data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UpdateCustomer(AddCustomerAndAdressContext model, int id)
        {

            var customer = dbEntity.Customers.Where(x => x.CustomerId == id).SingleOrDefault();
            var address = dbEntity.Adresses.Where(x => x.CustomerId == id).SingleOrDefault();
            var contact = dbEntity.Contacts.Where(x => x.CustomerId == id).SingleOrDefault();



            customer.OrganizationNumber = model.OrganizationNumber;
            customer.OrganizationName = model.OrganizationName;
            address.Region = model.Region;
            address.City = model.City;
            address.StreetAddess1 = model.StreetAddess1;
            address.StreetAddress2 = model.StreetAddress2;
            address.ZipCode = model.ZipCode;

            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Mobile = model.Mobile;
            contact.Email = model.Email;
            contact.IsMain = model.IsMain;


            if (ModelState.IsValid)
            {

                dbEntity.Entry(customer).State = EntityState.Modified;
                dbEntity.Entry(address).State = EntityState.Modified;
                dbEntity.Entry(contact).State = EntityState.Modified;
                dbEntity.SaveChanges();

                //  return RedirectToAction("DetailsCustomer);
                return RedirectToAction("DetailsCustomer", new { id = id });
            }

            return View(model);
        }



        [Authorize]
        public ActionResult AddContract(int id)
        {
            

            List<Product> list = new List<Product>();
            var data = dbEntity.Products.ToList();

            var model = new AddContract();
            model.CustomerId = id;

            list.AddRange(data);

            ViewBag.ProductList = list;

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]

        public ActionResult AddContract(AddContract model, int id)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();
                Model.Customer customer = dbEntity.Customers.Find(id);

                if (customer != null)
                {
                    Contract contract = new Contract();
                    contract.ProductId = model.ProductId;

                    contract.Customer = dbEntity.Customers.Find(id);

                    contract.SignedDate = model.SignedDate;

                    dbEntity.Contracts.Add(contract);
                    dbEntity.SaveChanges();
                    return RedirectToAction("DetailsCustomer", new { id = id });
                }
                else
                {
                    return HttpNotFound();
                }


            }

            return View(model);
        }

        [Authorize]
        public ActionResult UpdateContract(int id)
        {
            var data = (from c in dbEntity.Contracts
                        join a in dbEntity.Customers on c.CustomerId equals a.CustomerId
                        join co in dbEntity.Products on c.ProductId equals co.ProductId
                        where c.CustomerId == id
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
                        }).ToList().First();

            List<Product> list = new List<Product>();
            var data1 = dbEntity.Products.ToList();

            list.AddRange(data1);
            ViewBag.ProductList = list;

            return View(data);
        }

        [HttpPost,ValidateAntiForgeryToken]

        public ActionResult UpdateContract(AddContract addContract , int id)
        {
           
            var contract = dbEntity.Contracts.Where(x => x.ContractId ==addContract.ContractId).SingleOrDefault();
           

            contract.ProductId = addContract.ProductId;
            contract.SignedDate = addContract.SignedDate;

            if (ModelState.IsValid)
            {
                dbEntity.Entry(contract).State = EntityState.Modified;
                
                dbEntity.SaveChanges();

                return RedirectToAction("DetailsCustomer", new { id = id });
            }

            return View(addContract);
            

        }

        public ActionResult DeleteContract(int id , int contractId)
        {
            var customer = dbEntity.Customers.Find(id);
            var order = dbEntity.Orders.Where(x=>x.OrderStatusId == 1).ToList();

            if(customer != null)
            {
                if (order.Count() == 0)
                {
                    var contract = dbEntity.Contracts.Where(x => x.ContractId == contractId).First();
                    dbEntity.Contracts.Remove(contract);
                    dbEntity.SaveChanges();

                }
                return RedirectToAction("DetailsCustomer", new { id = id });
            }

            return View();
           
        }

        [Authorize]
        public ActionResult UpdateOrder(int id ,  int orderId)
        {
           
            var data = (from emp in dbEntity.Employees
                        join ol in dbEntity.OrderLines on emp.EmployeeId equals ol.EmployeeId
                        join o in dbEntity.Orders on ol.OrderId equals o.OrderId
                        join ot in dbEntity.OrderTypes on o.OrderTypeId equals ot.OrderTypeId
                        join cust in dbEntity.Customers on emp.CustomerId equals cust.CustomerId
                        join os in dbEntity.OrderStatus on o.OrderStatusId equals os.OrderStatusId
                       
                        where cust.CustomerId == id
                        select new
                        {
                            EmployeeId = emp.EmployeeId,
                            CustomerId = cust.CustomerId,
                            OrderTypeId = ot.OrderTypeId, 
                            OrderStatusId = os.OrderStatusId,
                           // OrderStatus = os.OrderStatus,
                            OrderId = o.OrderId,
                            OrderLineId = ol.OrderLineId,
                            CreateDate = o.CreateDate,
                            Amount = ol.Amount,
                            FirstName = emp.FirstName,
                            LastName = emp.LastName
                        }).AsEnumerable().Select(x => new AddOrder
                        {
                            EmployeeId = x.EmployeeId,
                            CustomerId = x.CustomerId,
                            OrderTypeId = x.OrderTypeId,  
                            OrderStatusId = x.OrderStatusId,
                           // OrderStatus = x.OrderStatus,
                            OrderId = x.OrderId,
                            OrderLineId = x.OrderLineId,
                            CreateDate = x.CreateDate,
                            Amount = x.Amount,
                            FirstName = x.FirstName,
                            LastName = x.LastName
                        }).Where(x=>x.OrderId == orderId).ToList().First();


            List<OrderType> list = new List<OrderType>();
            var data1 = dbEntity.OrderTypes.ToList();
            list.AddRange(data1);
            ViewBag.OrderTypeList = list;

            List<OrderStatu> orderStatus = new List<OrderStatu>();
            var dataStatus = dbEntity.OrderStatus.ToList();
            orderStatus.AddRange(dataStatus);

            ViewBag.StatusOrderList = orderStatus;


            List<AddOrder> listOrderLine = new List<AddOrder>();

            var dataListOrderLine = (from emp in dbEntity.Employees
                                     join ol in dbEntity.OrderLines on emp.EmployeeId equals ol.EmployeeId
                                     join o in dbEntity.Orders on ol.OrderId equals o.OrderId
                                     join ot in dbEntity.OrderTypes on o.OrderTypeId equals ot.OrderTypeId
                                     join cust in dbEntity.Customers on emp.CustomerId equals cust.CustomerId
                                     join os in dbEntity.OrderStatus on o.OrderStatusId equals os.OrderStatusId
                                   
                                     where cust.CustomerId == id 
                                     select new
                                     {
                                         EmployeeId = emp.EmployeeId,
                                         CustomerId = cust.CustomerId,                                                                          
                                         OrderTypeId = ot.OrderTypeId,
                                         OrderStatusId = os.OrderStatusId,
                                         OrderStatus = os.OrderStatus,
                                         OrderId = o.OrderId,
                                         OrderLineId = ol.OrderLineId,
                                         CreateDate = o.CreateDate,
                                         Amount = ol.Amount,
                                         FirstName = emp.FirstName,
                                         LastName = emp.LastName
                                     }).AsEnumerable().Select(x => new AddOrder()
                                     {
                                         EmployeeId = x.EmployeeId,
                                         CustomerId = x.CustomerId,
                                         OrderTypeId = x.OrderTypeId, 
                                         OrderStatusId = x.OrderStatusId,
                                         OrderStatus = x.OrderStatus,
                                         OrderId = x.OrderId,
                                         OrderLineId = x.OrderLineId,
                                         CreateDate = x.CreateDate,
                                         Amount = x.Amount,
                                         FirstName = x.FirstName,
                                         LastName = x.LastName

                                     }).Where(x => x.OrderId == orderId).ToList();

          
            listOrderLine.AddRange(dataListOrderLine);

            ViewBag.ListOrderLine = listOrderLine;

            return View(data);
        }

        [HttpPost,ValidateAntiForgeryToken]

        public ActionResult UpdateOrder(AddOrder model , int id)
        {
            var order = dbEntity.Orders.Where(x => x.OrderId == model.OrderId).SingleOrDefault();
            var orderLine = dbEntity.OrderLines.Where(x => x.OrderId == model.OrderId);
           
        

                order.OrderId = model.OrderId;
                order.OrderTypeId = model.OrderTypeId;
                order.CreateDate = model.CreateDate;
                order.OrderStatusId = model.OrderStatusId;
               // orderLine.Amount = model.Amount;
               
            if (ModelState.IsValid)
            {
                dbEntity.Entry(order).State = EntityState.Modified;

                foreach(var item in orderLine)
                {
                    dbEntity.Entry(item).State = EntityState.Modified;
                }
               
                
                dbEntity.SaveChanges();

                return RedirectToAction("DetailsCustomer", new { id = id });
            }

            return View(model);

        }


        [Authorize]
        public ActionResult AddOrder(int id)
        {
            List<OrderStatu> orderStatus = new List<OrderStatu>();
            var dataStatus = dbEntity.OrderStatus.ToList();
            orderStatus.AddRange(dataStatus);

            ViewBag.StatusOrderList = orderStatus;

            List<OrderType> list = new List<OrderType>();
            var data = dbEntity.OrderTypes.ToList();
            list.AddRange(data);
            ViewBag.OrderTypeList = list;

            var model = new AddOrder();
            model.CustomerId = id;

            list.AddRange(data);

           
            List<Employee> listEmployees = new List<Employee>();

            var dataEmployee = dbEntity.Employees.Where(x=>x.CustomerId == id).ToList();

            listEmployees.AddRange(dataEmployee);

            model.ListOfEmployees = dataEmployee;

           

            return View(model);
            
        }

        [HttpPost, ValidateAntiForgeryToken]

        public ActionResult AddOrder(AddOrder model , int id)
        {

            if (ModelState.IsValid)
            {

                Customer customer = dbEntity.Customers.Find(id);
                
                if (customer != null)
                {

                        Order order = new Order();

                        order.OrderTypeId = model.OrderTypeId;
                        order.CreateDate = model.CreateDate;
                        order.OrderStatusId = model.OrderStatusId;
                   
                        dbEntity.Orders.Add(order);
                       
                        

                    order.OrderLines = new List<OrderLine>();
                    foreach(var emp in model.ListOfEmployees.Where(x=> x.IsChecked))
                    {
                        OrderLine orderLine = new OrderLine();
                        orderLine.OrderId = order.OrderId;
                        orderLine.EmployeeId = emp.EmployeeId;
                        orderLine.Order = order;
                        orderLine.Amount = model.Amount;
                      //  dbEntity.OrderLines.Add(orderLine);
                        order.OrderLines.Add(orderLine);
                    }
                       
                    dbEntity.SaveChanges();
                    return RedirectToAction("DetailsCustomer", new { id = id });
                }
                else
                {
                    return HttpNotFound();
                }

            }
            return View(model);
        }

       

        [ActionName("DeleteOrder")]
        public ActionResult DeleteOrders(int id, int orderId)
        {
            var customer = dbEntity.Customers.Find(id);

            if(customer != null)
            {
                var order = dbEntity.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();
                var orderLine = dbEntity.OrderLines.Where(x => x.OrderId == order.OrderId);

                dbEntity.OrderLines.RemoveRange(orderLine);
                dbEntity.Orders.Remove(order);

                dbEntity.SaveChanges();
                return RedirectToAction("DetailsCustomer", new { id = id });
            }


            return View();
            
        }

        

        public ActionResult CreateEmployee( int id)
        {
            List<Employee> list = new List<Employee>();
            var data = dbEntity.Employees.ToList();

            var model = new Employee();
            model.CustomerId = id;

            list.AddRange(data);

            ViewBag.ListEmployee = list;

            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult CreateEmployee(Employee emplopyeeModel , int id)
        {
            

            if (ModelState.IsValid)
            {

                Model.Customer customer = dbEntity.Customers.Find(id);
                Employee employee = dbEntity.Employees.Where(e => e.CustomerId == customer.CustomerId && e.FirstName == emplopyeeModel.FirstName && e.LastName == emplopyeeModel.LastName).FirstOrDefault();
               


                if (customer != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (employee == null)
                        {
                            employee = new Employee();

                            employee.FirstName = emplopyeeModel.FirstName;
                            employee.LastName = emplopyeeModel.LastName;
                            employee.Customer = customer;

                            dbEntity.Employees.Add(employee);
                            dbEntity.SaveChanges();

                            return RedirectToAction("DetailsCustomer", new { id = id });
                        }
                        else
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Employee Exist !!");
                        }
                        
                    }
                   
                     
                }
                else
                {
                    return HttpNotFound();
                }


            }

            return View(emplopyeeModel);

        }

      
    }
}