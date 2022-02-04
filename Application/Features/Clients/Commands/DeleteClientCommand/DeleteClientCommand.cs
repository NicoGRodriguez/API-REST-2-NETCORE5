using Application.Exceptions;
using Application.Interfaces;
using Application.wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clients.Commands.DeleteClientCommand
{
    public class DeleteClientCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class CreateClientCommandHandler : IRequestHandler<DeleteClientCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Client> _repositoryAsync;

            public CreateClientCommandHandler(IRepositoryAsync<Client> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
            {
                var client = await _repositoryAsync.GetByIdAsync(request.Id);

                if (client == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {                  
                    await _repositoryAsync.DeleteAsync(client);

                    return new Response<int>(client.Id);
                }
            }
        }
    }
}
