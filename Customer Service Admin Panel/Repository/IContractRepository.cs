using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Service_Admin_Panel.Repository
{
    internal interface IContractRepository
    {
        IEnumerable<AddContract> GetContracts(int id);
        AddContract GetContract(int id, int contractId);
        void AddContract(AddContract contract , int id);
        void UpdateContract(AddContract contract , int id);
        void DeleteContract(int id , int contractId);
        void Save();

    }
}
