using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
    }
}
