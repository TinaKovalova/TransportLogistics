using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using TransportLogistics.Modules;
using TransportLogistics.ViewModels;
using TransportLogistics.ViewModels.UserControlsModels;

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

    }
}
