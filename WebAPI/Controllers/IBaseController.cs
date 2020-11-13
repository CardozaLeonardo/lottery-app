using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public interface IBaseController<TEntity, TManager, TCreateCommand>
    {
        public Task<ActionResult<IEnumerable<TEntity>>> Get();

        public Task<ActionResult<IEnumerable<TEntity>>> Get(int id);

        public Task<IActionResult> Put(int id, TEntity entity);

        public Task<ActionResult<TEntity>> Post(TEntity entity);

        public Task<ActionResult<TEntity>> PostJson(TEntity entity);

        public Task<ActionResult<TEntity>> Delete(int id);

        public Task<ActionResult<TEntity>> Search(int id);

        public Task<ActionResult<TEntity>> SearchAllFields(int pageSize, string filterValue = null, string sortProperty = null, int? page = null);
    }
}
