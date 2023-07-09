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
            try
            {
                _context.RemoveRange(entity.PurchaseDetails);
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
