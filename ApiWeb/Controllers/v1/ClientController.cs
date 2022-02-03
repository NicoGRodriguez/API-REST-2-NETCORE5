using Application.Features.Clients.Commands.CreateClientCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWeb.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> PostClient(CreateClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
