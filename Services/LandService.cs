using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LandService : Service<Land>
    {
        public LandService(AppDbContext context) : base(context)
        { }

        public override IQueryable<Land> GetAll()
        {
            return base.GetAll()
                .Include(l => l.Farmer)
                .Include(l => l.Type)
                .Include(l => l.SoilType);
        }
    }
}
