using Microsoft.AspNetCore.Mvc;

namespace Ambev.Api.Controllers.Base
{
    [ApiController]
    [Route("v1/api/[controller]")]
    [Produces("application/json")]
    public abstract class BaseController : Controller { }
   
}
