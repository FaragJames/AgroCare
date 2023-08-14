using AgroCare.Data.DTOs;

namespace AgroCare.ViewModels
{
    public class ItemsOrderViewModel
    {
        public OrderDto? OrderDto;
        public List<ItemDto> ItemsDto;

        public ItemsOrderViewModel(OrderDto? order, List<ItemDto> items)
        {
            OrderDto = order;
            ItemsDto = items;
        }
    }
}
