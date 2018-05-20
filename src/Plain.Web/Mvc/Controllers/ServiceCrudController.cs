using Plain.Infrastructure.Interfaces.Domain;
using Plain.Infrastructure.Interfaces.Services;
using Plain.Web.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plain.Controllers
{
    public class ServiceCrudController<TEntity, TEV, TLV> : ServiceController<TEntity, TEV, TLV>
        where TEntity : class, IEntityKey<int>, new()
        where TEV : EditViewModel, new()
        where TLV : ListViewModel<TEntity>, new()
    {
        protected ICrudServices<TEntity> _crudService;

        public ServiceCrudController(ICrudServices<TEntity> service)
            : base(service)
        {
            _crudService = service;
        }

        protected override Result ExecuteUpdate(TEntity entity, TEV viewModel)
        {
            _crudService.Update(entity);
            return new Result { Success = true };
        }

        protected override Result ExecuteDelete(int id)
        {
            _crudService.Delete(id);
            return new Result { Success = true };
        }

        protected override Result ExecuteInsert(TEntity entity, TEV viewModel)
        {
            _crudService.Add(entity);
            return new Result { Success = true };
        }
    }
}

