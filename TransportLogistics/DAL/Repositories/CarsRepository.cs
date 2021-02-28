using DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CarsRepository : GenericRepository<Cars>
    {
        public CarsRepository(DbContext context) : base(context)
        {
        }
    }
}
