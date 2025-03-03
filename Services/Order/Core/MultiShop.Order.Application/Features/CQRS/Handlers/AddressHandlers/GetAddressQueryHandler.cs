using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class GetAddressQueryHandler
    {
        private readonly IRepository<Address> _repository;
        public GetAddressQueryHandler(IRepository<Address> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetAddressQueryResult>> Handle()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetAddressQueryResult
            {
                AddressId = x.AddressId,
                UserId = x.UserId,
                City = x.City,
                District = x.District,
                Country = x.Country,
                Description = x.Description,
                Detail1 = x.Detail1,
                Detail2 = x.Detail2,
                Email = x.Email,
                Name = x.Name,
                Surname = x.Surname,
                Phone = x.Phone,
                ZipCode = x.ZipCode
            }).ToList();
        }
    }
}