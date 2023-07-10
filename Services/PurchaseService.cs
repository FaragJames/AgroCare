using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    .ThenInclude(store => store.Type);
        }
        public override async Task<bool> RemoveAsync(Purchase entity)
        {
            _context.RemoveRange(entity.PurchaseDetails);
            return await base.RemoveAsync(entity);
        }

        public IQueryable<Purchase> GetPendingPurchases()
        {
            return GetAll().Where(p => p.Plan.FinishDate < DateTime.Now);
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
