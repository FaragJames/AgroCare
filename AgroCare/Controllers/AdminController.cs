using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using AgroCare.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Models.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using AgroCare.Data.DTOs;
using System.Data;
using System;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly OrderService _orderService;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;


        public AdminController(UserManager<IdentityUser> manager, AppDbContext appDbContext, IMapper mapper, OrderService orderService)
        {
            _userManager = manager;
            _appDbContext = appDbContext;
            _mapper = mapper;
            _orderService = orderService;
        }

        public async Task<IActionResult> ReceivedOrders([FromServices] UserIdService<Engineer> userIdService)
        {
            return View(_orderService.GetAdminOrders(await userIdService.GetIdByUserNameAsync(User.Identity!.Name!))
                .Where(o => o.ExecutiveTeam == null).Select(o => _mapper.Map<OrderDto>(o)).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> ReceivedOrders([FromBody] dynamic body)
        {
            int teamId = (int)body.TeamId,
                orderId = (int)body.OrderId;
            Order order = (await _orderService.GetOneAsync(orderId))!;
            if(teamId > 0)
            {
                order.ExecutiveTeamId = teamId;
            }
            else
            {
                List<string> feedbacks = new();
                foreach (var item in body.Feedbacks)
                    feedbacks.Add((string)item.name);

                int i = 0;
                foreach (var orderDetail in order.OrderDetails)
                    orderDetail.Feedback = feedbacks[i++];
            }

            order.ClickedByBuyer = false;
            await _orderService.EditAsync(order);
            return RedirectToAction(nameof(ReceivedOrders));
        }

        [HttpPost]
        public async Task ClickedByAdmin([FromBody] dynamic body)
        {
            int orderId = (int)body.orderId;
            var order = await _orderService.GetOneAsync(orderId);
            if (order != null)
            {
                order.ClickedByAdmin = true;
                await _orderService.EditAsync(order);
            }
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new()
                {
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0]
                };

                string role = user.UserName.Split('_')[1];
                var createResult = await _userManager.CreateAsync(user, model.Password);
                var roleResult = await _userManager.AddToRoleAsync(user, role);

                if (!createResult.Succeeded || !roleResult.Succeeded)
                {
                    foreach (var error in createResult.Errors)
                        ModelState.AddModelError("", error.Description);

                    foreach (var error in roleResult.Errors)
                        ModelState.AddModelError("", error.Description);

                    return View(model);
                }

                return RedirectToAction("UserInfo",
                    new { role = role.Equals("Admin") || role.Equals("Executive") ? "Engineer" : role,
                    userName = user.UserName});
            }

            return View(model);
        }

        public IActionResult UserInfo([FromServices] IService<StoreType> storeTypeService,
            [FromServices] IService<EngineerType> engineerTypeService,
            [FromServices] IService<LandType> landTypeService,
            [FromServices] IService<SoilType> soilTypeService,
            string role, string userName)
        {
            return View(new UserInfoViewModel(storeTypeService.GetAll().Select(sType => _mapper.Map<StoreTypeDto>(sType)).ToList(),
                    engineerTypeService.GetAll().Select(eType => _mapper.Map<EngineerTypeDto>(eType)).ToList(),
                    landTypeService.GetAll().Select(lType => _mapper.Map<LandTypeDto>(lType)).ToList(),
                    soilTypeService.GetAll().Select(soilType => _mapper.Map<SoilTypeDto>(soilType)).ToList(),
                    role, userName));
        }

        [HttpPost]
        [ActionName($"UserInfo{nameof(Buyer)}")]
        public async Task<IActionResult> UserInfo(Buyer buyer)
        {
            if (ModelState.IsValid)
            {
                await _appDbContext.Buyers.AddAsync(buyer);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("UserInfo",
                new
                {
                    role = nameof(Buyer),
                    userName = buyer.UserName
                });
        }

        [HttpPost]
        [ActionName($"UserInfo{nameof(Engineer)}")]
        public async Task<IActionResult> UserInfo(Engineer engineer)
        {
            if (ModelState.IsValid)
            {
                await _appDbContext.Engineers.AddAsync(engineer);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("UserInfo",
                new
                {
                    role = nameof(Engineer),
                    userName = engineer.UserName
                });
        }

        [HttpPost]
        [ActionName($"UserInfo{nameof(Farmer)}")]
        public async Task<IActionResult> UserInfo(Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                await _appDbContext.Farmers.AddAsync(farmer);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("UserInfo",
                new
                {
                    role = nameof(Farmer),
                    userName = farmer.UserName
                });
        }

        [HttpPost]
        [ActionName($"UserInfo{nameof(Store)}")]
        public async Task<IActionResult> UserInfo(Store store)
        {
            if (ModelState.IsValid)
            {
                await _appDbContext.Stores.AddAsync(store);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("UserInfo",
                new
                {
                    role = nameof(Store),
                    userName = store.UserName
                });
        }
    }
}
