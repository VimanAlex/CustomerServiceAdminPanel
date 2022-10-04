using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Validation
{
    public interface IValidation<TEntity> where TEntity : class
    {
        bool IsValid(TEntity entity,ModelStateDictionary modelState);
        bool IsValid(TEntity entity, ModelStateDictionary modelState, int id);
    }
}
