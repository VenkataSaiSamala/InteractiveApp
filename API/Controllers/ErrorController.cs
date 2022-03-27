using API.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ErrorController : BaseApiController
    {
        private readonly DataContext _dataContext;
        public ErrorController(DataContext dataContext) {
            _dataContext = dataContext;
        }

        [HttpGet("Not-found")]
        public string NotFound()
        {
            var data = _dataContext.Users.Find("-1");
            
            return data.UserName;
        }
    }
}
