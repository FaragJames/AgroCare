using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : Service<Order>
    {
        public OrderService(AppDbContext context) : base(context)
        { }

        public override IQueryable<Order> GetAll()
        {
            return base.GetAll()
                .Include(o => o.Buyer)
                .Include(o => o.AdminEngineer)
                    .ThenInclude(admin => admin.EngineerType)
                .Include(o => o.ExecutiveTeam)
                    .ThenInclude(executive => executive!.EngineerType)
                .Include(o => o.OrderDetails)
                    .ThenInclude(orderDetails => orderDetails.Item);

            //This will not cause any issues when executing the query,
            //even if the ExecutiveTeam navigation property is null for some Order entities.
            //The related data will simply not be loaded for those Order entities.
        }
        public override async Task<bool> RemoveAsync(Order entity)
        {
            try
            {
                _context.RemoveRange(entity.OrderDetails);
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
