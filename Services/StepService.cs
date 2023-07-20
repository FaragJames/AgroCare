using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Services  
{

    public class StepService : Service<Step>
    {
        public StepService(AppDbContext context) : base(context)
        { }

        public override IQueryable<Step> GetAll()
        {
            return base.GetAll()
                .Include(s => s.Action)
                .Include(s => s.StepDetails)
                    .ThenInclude(s => s.AgriculturalItem);
        }
        public override async Task<bool> RemoveAsync(Step entity)
        {
            _context.RemoveRange(entity.StepDetails);
            return await base.RemoveAsync(entity);
        }
    }
}
