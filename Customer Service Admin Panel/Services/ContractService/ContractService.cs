using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.ModelView;
using Customer_Service_Admin_Panel.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Services.ContractService
{
    public class ContractService : IContractService
    {
       private ModelStateDictionary _ModelState;
       private UnitOfWork _unitOfWork;
       private ContractValidation ContractValidation = new ContractValidation();
        

        public ContractService(UnitOfWork unitOfWork, ModelStateDictionary modelState)
        {
            this._ModelState = modelState;
            this._unitOfWork = unitOfWork;
        }

        public void AddContract(AddContract model, int id)
        {
            if (ContractValidation.IsValid(model,_ModelState))
            {
                try
                {
                    _unitOfWork.ContractRepository.AddContract(model, id);
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    throw new HttpException(ex.Message);

                }
                
            }
           
        }

        public void DeleteContract(int id, int contractId)
        {
            _unitOfWork.ContractRepository.DeleteContract(id,contractId);
            _unitOfWork.Save();
        }

        public IEnumerable<AddContract> GetAllContracts(int id)
        {
            var getContracts = _unitOfWork.ContractRepository.GetContracts(id);

            return getContracts;
        }

        public AddContract GetContract(int id, int contractId)
        {
            var contract = _unitOfWork.ContractRepository.GetContract(id,contractId);
            return contract;
        }

        public void UpdateContract(AddContract model, int id)
        {
            _unitOfWork.ContractRepository.UpdateContract(model, id);
            _unitOfWork.Save();
        }
    }
}