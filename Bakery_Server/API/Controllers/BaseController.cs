using API.Core.Contracts;
using API.Core.CustomExceptions;
using API.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class BaseController<T> : ControllerBase where T : BaseEntity
    {
        protected readonly IRepository<T> _context;
        protected readonly ILogger<BaseController<T>> _logger;

        public BaseController(IRepository<T> repository, ILogger<BaseController<T>> logger)
        {
            _context = repository;
            _logger = logger;
        }

        public async virtual Task<IActionResult> DeleteEntity(string id)
        {
            try
            {
                await _context.DeleteAsync(id);
                return Ok();
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
