using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Services
{
    public class PurchaseService : Service<Purchase>
    {
        public PurchaseService(AppDbContext context) : base(context)
        { }

        public override IQueryable<Purchase> GetAll()
        {
            return base.GetAll()
                .Include(p => p.PurchaseDetails)
                .Include(p => p.Store)
                    .ThenInclude(store => store.Type)
                .Include(p => p.Plan)
                    .ThenInclude(p => p.Land)
                        .ThenInclude(l => l.Farmer);
        }
        public override async Task<bool> RemoveAsync(Purchase entity)
        {
            _context.RemoveRange(entity.PurchaseDetails);
            return await base.RemoveAsync(entity);
        }

        public IQueryable<Purchase> GetPendingPurchases()
        {
            Plan now = new() { FinishDate = DateOnly.FromDateTime(DateTime.Now) };
            return GetAll().Where(p => p.Plan.FinishDate > now.FinishDate);
        }
        //For the Store's page.
        public IQueryable<Purchase> GetPendingPurchasesByStoreId(int storeId)
        {
            return GetPendingPurchases().Where(p => p.StoreId == storeId);
        }
        //For the Farmer's plan's purchases page.
        public IQueryable<Purchase> GetPendingPurchasesByPlanId(int planId)
        {
            return GetPendingPurchases().Where(p =>  p.PlanId == planId);
        }
    }
}
