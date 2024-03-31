using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OnlineWallet.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        public readonly ISender _mediator;

        public BaseController(ISender mediator)
        {
            _mediator = mediator;
        }
        
    }
}
