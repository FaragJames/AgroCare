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

            //If the ExecutiveTeam navigation property is null for some Order entities,
            //this will not cause any issues when executing the query,
            //The related data will simply not be loaded for those Order entities.
        }
        public override async Task<bool> RemoveAsync(Order entity)
        {
            _context.RemoveRange(entity.OrderDetails);
            return await base.RemoveAsync(entity);
        }

        public IQueryable<Order> GetPendingOrders()
        {
            return GetAll().Where(o => !o.ExecutiveTeamId.HasValue);
        }
        //For the Buyer's page.
        public IQueryable<Order> GetPendingOrdersByBuyerId(int buyerId)
        {
            return GetPendingOrders().Where(o => o.BuyerId == buyerId);
        }
        //For the Admin's orders list.
        public IQueryable<Order> GetPendingOrdersByAdminId(int adminId)
        {
            return GetPendingOrders().Where(o => o.AdminEngineerId == adminId);
        }
        public IQueryable<Order> GetUnderwayOrders()
        {
            return GetAll().Where(o =>
                o.ExecutiveTeamId.HasValue &&
                o.OrderDetails.Where(oD => oD.DeliveryDate < DateTime.Now).Any());
        }
        //For the Buyer's page.
        public IQueryable<Order> GetUnderwayOrdersByBuyerId(int buyerId)
        {
            return GetUnderwayOrders().Where(o => o.BuyerId ==  buyerId);
        }
        //For the Executive Team's tasks list.
        public IQueryable<Order> GetUnderwayOrdersByExecutiveId(int executiveId)
        {
            return GetUnderwayOrders().Where(o => o.ExecutiveTeamId == executiveId);
        }

        public IQueryable<Order> GetExecutiveTeamOrders(int headEngineerId)
        {
            return GetAll().Where(o => o.ExecutiveTeamId == headEngineerId);
        }
        public IQueryable<Order> GetAdminOrders(int adminId)
        {
            return GetAll().Where(o => o.AdminEngineerId == adminId);
        }
    }
}
