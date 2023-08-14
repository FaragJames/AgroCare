using AgroCare.Data.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services;
using AgroCare.ViewModels;
using Microsoft.AspNetCore.JsonPatch;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class BuyerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IService<Item> _itemService;
        private readonly OrderService _orderService;


        public BuyerController(IMapper mapper, IService<Item> itemService, OrderService orderService)
        {
            _mapper = mapper;
            _itemService = itemService;
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult CreateOrder(int? orderId)
        {
            OrderDto? orderDto = orderId.HasValue ?
                _mapper.Map<OrderDto>(_orderService.GetPendingOrdersByBuyerId(39).Where(o => o.Id == orderId).First())
                : null;
            var itemsDto = _itemService.GetAll().Select(i => _mapper.Map<ItemDto>(i)).ToList();
            return View(new ItemsOrderViewModel(orderDto, itemsDto));
        }

        [HttpPost]
        public IActionResult CreateOrder(List<OrderDetail> items, int? orderId)
        {
            //Algorithm (Queue Theory) for assigning an admin to the order.
            return View(nameof(ShowPendingOrders));
        }

        public IActionResult ShowPendingOrders()
        {
            var pending = _orderService.GetPendingOrdersByBuyerId(39).Select(i => _mapper.Map<OrderDto>(i));
            return View(pending.ToList());
        }
        public IActionResult ShowUnderwayOrders()
        {
            var underway = _orderService.GetUnderwayOrdersByBuyerId(39).Select(i => _mapper.Map<OrderDto>(i));
            return View(underway.ToList());
        }
    }
}