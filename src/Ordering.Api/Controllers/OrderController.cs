
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Api.Command;

namespace Ordering.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;
        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(CreateOrderCommand value)
        {
            var response = await this.mediator.Send(value);

            if (response.Errors.Any())
                return base.BadRequest(response.Errors);
            return base.Ok(response.Result);
        }
    }
}
