using Microsoft.AspNetCore.Mvc;
using Plain.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces.Domain;
using Plain.Infrastructure.Interfaces.Services;
using Plain.Web.Mvc.Controllers;
using Plain.Web.Mvc.Models;
using System;
using System.Linq;

namespace Plain.Controllers
{
    public class ListController<TEntity> : ListControllerBase<int, TEntity, ListViewModel<TEntity>>
        where TEntity : class, IEntityKey<int>, new()
    {
        private IServices<TEntity> _services;

        public ListController(IServices<TEntity> services)
        {
            _services = services;
        }

        public ActionResult Detail(int id)
        {
            TEntity entity = _services.FindBy(id);
            return View("Detail",entity);
        }

        protected override ListViewModel<TEntity> ExecuteList(int CurrentPage)
        {
            var result = _services.All(CurrentPage, _pageSize);
            return new ListViewModel<TEntity>
            {
                CurrentPage = CurrentPage,
                Items = result.PageOfResults.ToList(),
                PageSize = 20,
                TotalItems = result.TotalItems
            };
        }

        protected ListViewModel<TEntity> ExecuteList(Func<PagedResult<TEntity>> source, int itemsPerPage)
        {
            var result = source();
            return new ListViewModel<TEntity>
            {
                CurrentPage = CurrentPage,
                Items = result.PageOfResults.ToList(),
                PageSize = itemsPerPage,
                TotalItems = result.TotalItems
            };
        }

        protected virtual ListViewModel<TEntity> ExecuteList(int page, int itemsPerPage)
        {
            var result = _services.All(page, itemsPerPage);
            return new ListViewModel<TEntity>
            {
                CurrentPage = page,
                Items = result.PageOfResults.ToList(),
                PageSize = itemsPerPage,
                TotalItems = result.TotalItems
            };
        }
    }
}
