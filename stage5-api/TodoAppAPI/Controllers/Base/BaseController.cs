using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using MediatR;

namespace TodoAppAPI.Controllers.Base
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}")]

    public class BaseController : Controller
    {

        private IMediator _Mediator;
        protected IMediator Mediator
        {
            get
            {
                return _Mediator ?? (_Mediator = HttpContext.RequestServices.GetService<IMediator>());
            }
        }
    }
}
