using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using BLL.Services;
using BLL.DTO;
using DAL.Context;
using DAL.Repositories;

namespace TransportLogistics.Modules
{
    public class LogisticsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IService<CarDTO>>().To<CarSevice>();
            Bind<IService<FuelDTO>>().To<FuelService>();
            Bind<IService<OrderDTO>>().To<OrderService>();
            Bind<IService<OrderStatusDTO>>().To<OrderStatusService>();
            Bind<IService<UserDTO>>().To<UserService>();
            Bind<IService<RoleDTO>>().To<RoleService>();
            Bind<IRepository<Car>>().To<CarRepository>();
            Bind<IRepository<Fuel>>().To<FuelRepository>();
            Bind<IRepository<User>>().To<UserRepository>();
            Bind<IRepository<Order>>().To<OrderRepository>();
            Bind<IRepository<Role>>().To<RoleRepository>();
            Bind<IRepository<OrderStatus>>().To<OrderStatusRepository>();
        }
    }
}
