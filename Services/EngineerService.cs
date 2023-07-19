using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Services
{

    public class EngineerService : Service<Engineer>
    {
        public EngineerService(AppDbContext context) : base(context)
        { }

        public override IQueryable<Engineer> GetAll()
        {
            return base.GetAll().Include(e => e.EngineerType);
        }
    
        public IQueryable<Engineer> GetHeadEngineers()
        {
            return GetAll().Where(e => e.EngineerType.Name.Contains("executive")
                && !e.HeadEngineerId.HasValue);
        }
        public IQueryable<Engineer> GetEngineersByType(int engineerTypeId)
        {
            return GetAll().Where(e => e.EngineerTypeId == engineerTypeId);
        }
        public IQueryable<Engineer> GetExecutiveTeam(int headEngineerId)
        {
            return GetAll().Where(e => e.HeadEngineerId == headEngineerId);
        }
    }
} 
