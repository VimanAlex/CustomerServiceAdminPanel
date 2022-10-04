using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer_Service_Admin_Panel.DAL
{
    public class UnitOfWork :IUnitOfWork, IDisposable
    {
        private CustomerServiceEntity context = new CustomerServiceEntity();

        private CustomerRepository _customerRepository;
        private ContractRepository _contractRepository;
        private EmployeesRepository _employeesRepository;
        private OrderRepository _orderRepository;
        private OrderLineRepository _orderLineRepository;
        private OrderStatusRepository _orderStatusRepository;
        private OrderTypeRepository _orderTypeRepository;
        private ProductRepository _productRepository;

        public CustomerRepository CustomerRepository
        {
            get
            {
                if(this._customerRepository == null)
                {
                    this._customerRepository = new CustomerRepository(context);
                }
                return this._customerRepository;
            }
        }

        public ContractRepository ContractRepository
        {
            get
            {
                if(this._contractRepository == null)
                {
                    this._contractRepository = new ContractRepository(context);
                }
                return this._contractRepository;
            }
        }

        public EmployeesRepository EmployeesRepository
        {
            get
            {
                if (this._employeesRepository == null)
                {
                    this._employeesRepository = new EmployeesRepository(context);
                }
                return this._employeesRepository;
            }
        }

        public OrderLineRepository OrderLineRepository
        {
            get
            {
                if (this._orderLineRepository == null)
                {
                    this._orderLineRepository = new OrderLineRepository(context);
                }
                return this._orderLineRepository;
            }
        }

        public OrderRepository OrderRepository
        {
            get
            {
                if(this._orderRepository == null)
                {
                    this._orderRepository = new OrderRepository(context);
                }
                return this._orderRepository;
            }
        }

        public OrderStatusRepository OrderStatusRepository
        {
            get
            {
                if(this._orderStatusRepository == null)
                {
                    this._orderStatusRepository = new OrderStatusRepository(context);
                }
                return this._orderStatusRepository;                 
            }
        }

        public OrderTypeRepository OrderTypeRepository
        {
            get
            {
                if(this._orderTypeRepository == null)
                {
                    this._orderTypeRepository = new OrderTypeRepository(context);
                }
                return this._orderTypeRepository;
            }
        }

        public ProductRepository ProductRepository
        {
            get
            {
                if(this._productRepository == null)
                {
                    this._productRepository = new ProductRepository(context);
                }
                return this._productRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}