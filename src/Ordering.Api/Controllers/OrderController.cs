
using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Api.Command;
using Ordering.Domain.Core;

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
            return base.Ok(response.Result);
        }

        [HttpGet("Exception")]
        public ActionResult Get(int id) => throw new InvalidOperationException("Throw Exception");
    }
}
