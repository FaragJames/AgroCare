using AgroCare.Data.DTOs;

namespace AgroCare.ViewModels
{
    public class ItemsOrderViewModel
    {
        public OrderDto? Order;
        public List<ItemDto> Items;

        public ItemsOrderViewModel(OrderDto? order, List<ItemDto> items)
        {
            Order = order;
            Items = items;
        }
    }
}
