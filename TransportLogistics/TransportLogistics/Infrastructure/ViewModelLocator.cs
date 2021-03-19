using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using TransportLogistics.Modules;
using TransportLogistics.ViewModels;
using TransportLogistics.ViewModels.UserControlsModels;
using TransportLogistics.ViewModels.UserControlsModels.ChildrenUserModels;

namespace TransportLogistics.Infrastructure
{
    public class ViewModelLocator
    {
        IKernel kernel;
        public ViewModelLocator() => kernel = new StandardKernel(new LogisticsNinjectModule());
        public MainViewModel MainViewModel => kernel.Get<MainViewModel>();
        public DirectoryViewModel DirectoryViewModel => kernel.Get<DirectoryViewModel>();
        public AccountViewModel AccountViewModel => kernel.Get<AccountViewModel>();
        public OrderViewModel OrderViewModel => kernel.Get<OrderViewModel>();
        public UserViewModel UserViewModel => kernel.Get<UserViewModel>();
        public CreateUserModel CreateUserModel => kernel.Get<CreateUserModel>();
        public RoleViewModel RoleViewModel => kernel.Get<RoleViewModel>();
        public CarViewModel CarViewModel => kernel.Get<CarViewModel>();
        public StatusViewModel StatusViewModel => kernel.Get<StatusViewModel>();
        public CreateRoleViewModel CreateRoleViewModel => kernel.Get<CreateRoleViewModel>();
        public CreateStatusViewModel CreateStatusViewModel => kernel.Get<CreateStatusViewModel>();
        public CreateCarViewModel CreateCarViewModel => kernel.Get<CreateCarViewModel>();
        public OrderListViewModel OrderListViewModel => kernel.Get<OrderListViewModel>();
        public CreateNewOrderModel CreateNewOrderModel => kernel.Get<CreateNewOrderModel>();
     

    }
}
