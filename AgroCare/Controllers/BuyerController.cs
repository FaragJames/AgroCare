using AgroCare.Data.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services;
using AgroCare.ViewModels;
using System.Xml.Serialization;

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
        public async Task<IActionResult> CreateOrder(int? orderId, List<OrderDetail> items,
            [FromServices] UserIdService<Buyer> userId)
        {
            if (ModelState.IsValid)
            {
                if (!orderId.HasValue)
                {
                    Order newOrder = new()
                    {
                        BuyerId = await userId.GetIdByUserNameAsync(User.Identity!.Name!),
                        AdminEngineerId = GetAdmin(),
                        OrderDate = DateOnly.FromDateTime(DateTime.Now),
                        OrderDetails = items
                    };
                    if (!await _orderService.AddAsync(newOrder))
                    {
                        ModelState.AddModelError("", "Not Added Try Again..!");
                        return RedirectToAction(nameof(CreateOrder));
                    }
                }
                else
                {
                    var oldOrder = await _orderService.GetOneAsync(orderId.Value);
                    await _orderService.RemoveOrderDetailsAsync(oldOrder!);
                    oldOrder!.OrderDetails = items;

                    if (!await _orderService.EditAsync(oldOrder))
                    {
                        ModelState.AddModelError("", "Not Added Try Again..!");
                        return RedirectToAction(nameof(CreateOrder));
                    }
                }

                return RedirectToAction(nameof(ShowPendingOrders));
            }

            return RedirectToAction(nameof(CreateOrder));
        }

        public int GetAdmin()
        {
            var Admins = _orderService.GetPendingOrders().GroupBy(order => order.AdminEngineerId)
                .Select(group => new { adminId = group.Key, pendingOrdersCount = group.Count() })
                .OrderBy(admin => admin.pendingOrdersCount).ToList();
            return Admins[0].adminId;
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