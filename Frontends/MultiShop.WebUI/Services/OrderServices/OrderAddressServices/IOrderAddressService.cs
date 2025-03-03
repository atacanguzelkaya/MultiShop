using MultiShop.WebUI.Dtos.OrderDtos.OrderAddressDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderAddressServices
{
    public interface IOrderAddressService
    {
        Task CreateOrderAddressAsync(CreateOrderAddressDto createOrderAddressDto);
        // Task<List<ResultOrderAddressDto>> GetAllOrderAddressAsync();
        //    Task UpdateOrderAddressAsync(UpdateOrderAddressDto updateOrderAddressDto);
        //    Task DeleteOrderAddressAsync(string id);
        //    Task<UpdateOrderAddressDto> GetByIdOrderAddressAsync(string id);
    }
}