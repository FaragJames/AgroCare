using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserIdService<T> where T : class, IBaseProperties, IUserName
    {
        readonly AppDbContext _context;


        public UserIdService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<int> GetIdByUserNameAsync(string userName) {
            return (await _context.Set<T>().FirstAsync(e => e.UserName == userName)).Id;
        }
    }
}
