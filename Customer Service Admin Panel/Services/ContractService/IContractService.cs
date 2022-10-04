using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Service_Admin_Panel.Services.ContractService
{
    public interface IContractService
    {
        IEnumerable<AddContract> GetAllContracts(int id);
        void AddContract(AddContract model, int id);
        void DeleteContract(int id,int contractId);
        void UpdateContract(AddContract model, int id);
        AddContract GetContract(int id, int contractId);
    }
}
