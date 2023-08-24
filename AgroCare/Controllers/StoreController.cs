using AgroCare.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Store")]
    public class StoreController : Controller
    {
        readonly FarmerService farmerService;
        readonly StoreService storeService;
        readonly PurchaseService purchaseService;
        readonly UserIdService<Store> userIdService;

        public StoreController(StoreService storeService, FarmerService farmerService, PurchaseService purchaseService, UserIdService<Store> userIdService)
        {
            this.storeService = storeService;
            this.farmerService = farmerService;
            this.purchaseService = purchaseService;
            this.userIdService = userIdService;
        }

        [HttpGet]
        public IActionResult CreatePurchase()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchase(PurchaseAllDataView purchaseAllDataView)
        {
            if (await farmerService.DoesExist(purchaseAllDataView.FarmerId))
            {
                var F_ID = purchaseAllDataView.FarmerId;
                if (Program.PlansCodes.TryGetValue(F_ID, out (string, int) result))
                {
                    if (result.Item1 == purchaseAllDataView.Code)
                    {
                        List<PurchaseDetail> purchaseDetails = new();
                        foreach (var pd in purchaseAllDataView.PurchaseDetailsViews)
                        {
                            purchaseDetails.Add(new PurchaseDetail { Item = pd.Item, Quantity = pd.Quantity, Price = pd.Price, Details = pd.Details, IsRented = pd.IsRented });
                        }
                        Purchase purchase = new()
                        {
                            Store = (await storeService.GetOneAsync(await userIdService.GetIdByUserNameAsync(User.Identity!.Name!)))!,
                            Date = DateOnly.FromDateTime(DateTime.Now),
                            PurchaseDetails = purchaseDetails,
                            PlanId = result.Item2
                        };
                        if (!await purchaseService.AddAsync(purchase))
                        {
                            return BadRequest(new { error = "Not Added Try Again..!" });
                        }
                        else
                            return Ok();
                    }
                    else
                    {
                        return BadRequest(new { error = "There is no such Code for this Farmer.." });
                    }
                }
            }
            return BadRequest(new { error = "There is no such this Farmer.." });

        }

        [HttpGet]
        public async Task<IActionResult> ShowPurchases([Bind("SearchId")] string SearchId)
        {
            var PurchasesOfStore = await purchaseService.GetPendingPurchasesByStoreId(await userIdService.GetIdByUserNameAsync(User.Identity!.Name!)).ToListAsync();
            List<PurchaseStoreDtai> purchaseStore = new();
            var cost = 0f;
            foreach (var unp in PurchasesOfStore)
            {
                cost = unp.PurchaseDetails.Sum(n => n.Price * n.Quantity);
                purchaseStore.Add(new PurchaseStoreDtai { Date = unp.Date, FarmerId = unp.Plan.Land.FarmerId, Farmer = unp.Plan.Land.Farmer.Name, Cost = cost });
            }
            if (SearchId is null)
            {
                return View(new PurchaseStore { PurchaseStoreDtais = purchaseStore });
            }
            purchaseStore = purchaseStore.Where(p => p.FarmerId.ToString() == SearchId).ToList();
            PurchaseStore purchase = new()
            {
                SearchId = SearchId,
                PurchaseStoreDtais = purchaseStore,
                IsOneFarmer = true
            };
            return View(purchase);
        }
    }
}