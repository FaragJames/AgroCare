using Microsoft.EntityFrameworkCore;
using Models.Models;   

namespace Services
{

    public class FarmerService : Service<Farmer>
    {
        public FarmerService(AppDbContext context) : base(context)
        { }

        public override async Task<bool> RemoveAsync(Farmer farmer)
        {
            _context.RemoveRange(farmer.Lands);
            return await base.RemoveAsync(farmer);
        }
    
    }
}
