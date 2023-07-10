using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Services  
{

    public class StoreService : Service<Store>
    {
        public StoreService(AppDbContext context) : base(context)
        { }

        public override IQueryable<Store> GetAll()
        {
            return base.GetAll().Include(s => s.Type);
        }
    }
}
