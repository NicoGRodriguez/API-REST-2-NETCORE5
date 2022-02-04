using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<PagedResponse<List<ClientDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public class GetAllClientQueryHandler : IRequestHandler<GetAllClientsQuery, PagedResponse<List<ClientDTO>>>
        {
            private readonly IRepositoryAsync<Client> _repositoryAsync;
            private readonly IDistributedCache _distributedCache;
            private readonly IMapper _mapper;

            public GetAllClientQueryHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper, IDistributedCache distributedCache)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _distributedCache = distributedCache;
            }

            public async Task<PagedResponse<List<ClientDTO>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
            {
                var cacheKey = $"listClient_{request.PageSize}_{request.PageNumber}_{request.Name}_{request.LastName}";
                string serializedListClients;
                var listClients = new List<Client>();
                var redisListClients = await _distributedCache.GetAsync(cacheKey);

                if (redisListClients != null)
                {
                    serializedListClients = Encoding.UTF8.GetString(redisListClients);
                    listClients = JsonConvert.DeserializeObject<List<Client>>(serializedListClients);
                }
                else
                {
                    listClients = await _repositoryAsync.ListAsync(new PagedClientsSpecification(request.PageSize, request.PageNumber, request.Name, request.LastName));
                    serializedListClients = JsonConvert.SerializeObject(listClients);
                    redisListClients = Encoding.UTF8.GetBytes(serializedListClients);

                    var options = new DistributedCacheEntryOptions()
                                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                                .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await _distributedCache.SetAsync(cacheKey,redisListClients,options);
                }             
                var clientDTO = _mapper.Map<List<ClientDTO>>(listClients);

                return new PagedResponse<List<ClientDTO>>(clientDTO, request.PageNumber, request.PageSize);
            }
        }
    }
}
