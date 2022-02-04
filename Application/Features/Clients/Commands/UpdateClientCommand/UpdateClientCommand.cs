using Application.Exceptions;
using Application.Interfaces;
using Application.wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clients.Commands.UpdateClientCommand
{
    public class UpdateClientCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }

        public class CreateClientCommandHandler : IRequestHandler<UpdateClientCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Client> _repositoryAsync;
            private readonly IMapper _mapper;

            public CreateClientCommandHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
            {
                var client = await _repositoryAsync.GetByIdAsync(request.Id);

                if (client == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}"); 
                }
                else
                {
                    client.Name = request.Name;
                    client.LastName = request.LastName;
                    client.birthdate = request.birthdate;
                    client.PhoneNumber = request.PhoneNumber;
                    client.Email = request.Email;
                    client.Adress = request.Adress;

                    await _repositoryAsync.UpdateAsync(client);

                    return new Response<int>(client.Id);
                }
            }
        }
    }
}
