using Application.Interfaces;
using Application.wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clients.Commands.CreateClientCommand
{
    public class CreateClientCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }      
    }
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Client> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateClientCommandHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var newReport = _mapper.Map<Client>(request);
            var data = await _repositoryAsync.AddAsync(newReport);

            return new Response<int>(data.Id);
        }
    }
}
