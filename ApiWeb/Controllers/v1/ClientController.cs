using Application.Features.Clients.Commands.CreateClientCommand;
using Application.Features.Clients.Commands.DeleteClientCommand;
using Application.Features.Clients.Commands.UpdateClientCommand;
using Application.Features.Clients.Queries.GetAllClients;
using Application.Features.Clients.Queries.GetClientById;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiWeb.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetClient([FromQuery] GetAllClientsParameters filters)
        {
            return Ok(await Mediator.Send(new GetAllClientsQuery
            {
                PageNumber = filters.PageNumber,
                PageSize = filters.PageSize,
                Name = filters.Name,
                LastName = filters.LastName
            }));
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdClient(int id)
        {
            return Ok(await Mediator.Send(new GetClientByIdQuery { Id = id }));
        }
        [HttpPost]
        public async Task<IActionResult> PostClient(CreateClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutClient(int id, UpdateClientCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            return Ok(await Mediator.Send(new DeleteClientCommand { Id = id }));
        }
    }
}
