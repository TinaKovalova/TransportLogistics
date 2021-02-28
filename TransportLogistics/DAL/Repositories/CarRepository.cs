using DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CarRepository : GenericRepository<Car>
    {
        public CarRepository(DbContext context) : base(context)
        {
        }
    }
}
