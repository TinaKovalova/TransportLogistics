using DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RoleRepository : GenericRepository<Role>
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}
