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
                .Include(p => p.OrderDetails)
                .Include(p => p.Land)
                .Include(p => p.Steps)
                    .ThenInclude(s => s.Action)
                .Include(p => p.Steps)
                    .ThenInclude(s => s.StepDetails)
                        .ThenInclude(sd => sd.AgriculturalItem);
        }
        public override async Task<bool> RemoveAsync(Plan plan)
        {
            try
            {
                _context.RemoveRange(plan.Steps.SelectMany(s => s.StepDetails));
                _context.RemoveRange(plan.Steps);
                _context.Remove(plan);
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
