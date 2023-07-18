using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PlanService : Service<Plan>
    {
        public PlanService(AppDbContext context) : base(context)
        { }

        public override IQueryable<Plan> GetAll()
        {
            return base.GetAll()
                //.Include(p => p.OrderDetails)
                //.Include(p => p.Land)
                //    .ThenInclude(l => l.Farmer)
                .Include(p => p.Steps)
                    .ThenInclude(s => s.Action)
                .Include(p => p.Steps)
                    .ThenInclude(s => s.StepDetails)
                        .ThenInclude(sd => sd.AgriculturalItem);
        }
        public override async Task<bool> RemoveAsync(Plan plan)
        {
            _context.RemoveRange(plan.Steps.SelectMany(s => s.StepDetails));
            _context.RemoveRange(plan.Steps);
            return await base.RemoveAsync(plan);
        }

        public IQueryable<Plan> GetAllByLandId(int landId)
        {
            return GetAll().Where(p => p.LandId == landId);
        }
        //For the Farmer's page.
        public IQueryable<Plan> GetAllByFarmerId(int farmerId)
        {
            return GetAll().Where(p => p.Land.FarmerId == farmerId);
        }
    }
}
