using Plain.Infrastructure.Interfaces.Domain;
using Plain.Infrastructure.Interfaces.Services;
using Plain.Web.Mvc.Controllers;
using Plain.Web.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plain.Controllers
{
    public abstract class ServiceCrudFilterController<TEntity, TEV, TLV, TF> : CrudFilterController<int, TEntity, TEV, TLV, TF>
        where TEntity : class, IEntityKey<int>, new()
        where TEV : EditViewModel, new()
        where TLV : ListViewModel<TEntity>, new()
        where TF : FilterViewModel, new()
    {
        protected ICrudServices<TEntity> _service;

        public ServiceCrudFilterController(ICrudServices<TEntity> service)
        {
            _service = service;
        }

        protected override TEntity LoadEntiy(int id)
        {
            return _service.FindBy(id);
        }

        protected override Result ExecuteUpdate(TEntity entity, TEV viewModel)
        {
            _service.Update(entity);
            return new Result { Success = true };
        }

        protected override Result ExecuteDelete(int id)
        {
            _service.Delete(id);
            return new Result { Success = true };
        }

        protected override Result ExecuteInsert(TEntity entity, TEV viewModel)
        {
            _service.Add(entity);
            return new Result { Success = true };
        }
    }
}

