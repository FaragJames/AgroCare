using AgroCare.Data.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services;
using AgroCare.ViewModels;
using System.Xml.Serialization;
using Microsoft.AspNetCore.SignalR;
using AgroCare.Hubs;
using Models.Models.Auxiliary;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class BuyerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IService<Item> _itemService;
        private readonly OrderService _orderService;
        private readonly UserIdService<Buyer> _userIdService;


        public BuyerController(IMapper mapper, IService<Item> itemService, OrderService orderService, UserIdService<Buyer> userIdService)
        {
            _mapper = mapper;
            _itemService = itemService;
            _orderService = orderService;
            _userIdService = userIdService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder(int? orderId)
        {
            var buyerId = await _userIdService.GetIdByUserNameAsync(User.Identity!.Name!);
            OrderDto? orderDto = orderId.HasValue ?
                _mapper.Map<OrderDto>(_orderService.GetPendingOrdersByBuyerId(buyerId).Where(o => o.Id == orderId).First())
                : null;
            var itemsDto = _itemService.GetAll().Select(i => _mapper.Map<ItemDto>(i)).ToList();
            return View(new ItemsOrderViewModel(orderDto, itemsDto));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(int? orderId, List<OrderDetail> items,
            [FromServices] IHubContext<BuyerAdminHub> hub,
            [FromServices] EngineerService engineerService)
        {
            if (ModelState.IsValid)
            {
                int adminId = 0;
                if (!orderId.HasValue)
                {
                    adminId = GetAdmin(engineerService);
                    Order newOrder = new()
                    {
                        BuyerId = await _userIdService.GetIdByUserNameAsync(User.Identity!.Name!),
                        AdminEngineerId = adminId,
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
                    if(oldOrder != null)
                    {
                        adminId = oldOrder.AdminEngineerId;
                        await _orderService.RemoveOrderDetailsAsync(oldOrder);
                        oldOrder.OrderDetails = items;
                        if (!await _orderService.EditAsync(oldOrder))
                        {
                            ModelState.AddModelError("", "Not Added Try Again..!");
                            return RedirectToAction(nameof(CreateOrder));
                        }
                    }
                }

                await hub.Clients.All.SendAsync("ChangeIcon", (await engineerService.GetOneAsync(adminId))!.UserName);
                return RedirectToAction(nameof(ShowPendingOrders));
            }

            return RedirectToAction(nameof(CreateOrder));
        }
        private int GetAdmin(EngineerService engineerService)
        {
            var adminsId = engineerService.GetAll().Where(e => e.EngineerType.Name.Contains("admin")).Select(e => e.Id).ToList();
            int lowest = int.MaxValue, adminId = 0;
            foreach (var admin in adminsId)
            {
                int tmp = _orderService.GetPendingOrdersByAdminId(admin).Count();
                if (tmp < lowest)
                {
                    lowest = tmp;
                    adminId = admin;
                }
            }
            return adminId;
        }

        public async Task<IActionResult> ShowPendingOrders()
        {
            var buyerId = await _userIdService.GetIdByUserNameAsync(User.Identity!.Name!);
            var pending = _orderService.GetPendingOrdersByBuyerId(buyerId).Select(i => _mapper.Map<OrderDto>(i));
            return View(pending.ToList());
        }
        public async Task<IActionResult> ShowUnderwayOrders()
        {
            var buyerId = await _userIdService.GetIdByUserNameAsync(User.Identity!.Name!);
            var underway = _orderService.GetUnderwayOrdersByBuyerId(buyerId).Select(i => _mapper.Map<OrderDto>(i));
            return View(underway.ToList());
        }

        [HttpPost]
        public async Task ClickedByBuyer([FromBody] dynamic body)
        {
            int orderId = (int)body.orderId;
            var order = await _orderService.GetOneAsync(orderId);
            if (order != null)
            {
                order.ClickedByBuyer = true;
                await _orderService.EditAsync(order);
            }
        }
    }
}