using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TransportLogistics.Infrastructure;
using TransportLogistics.Views.UserControls;


namespace TransportLogistics.ViewModels
{
    public class MainViewModel:BaseNotifyPropertyChanged
    {
        #region 
        private UserControl currentView = null;
        public UserControl CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                Notify();
            }
        }
        public string SelectedRole { get; set; }

        private ObservableCollection<string> roles;

        public ObservableCollection<string> Roles
        {
            get => roles;
            set
            {
                roles = value;
                Notify();
            }
        }


        private IService<RoleDTO> roleService;

        #endregion
        #region Commands
        public ICommand DirectoryCommand { get; set; }
        public ICommand OrderCommand { get; set; }
        public ICommand AccountCommand { get; set; }
        #endregion

        public MainViewModel(IService<RoleDTO> roleService)
        {
            this.roleService = roleService;
            this.roleService = roleService;
           // CurrentView = new DirectoryView();
            // InitCollection();
            Roles = new ObservableCollection<string>(roleService.GetAll().Select(x => x.ToString()));
            InitCommands();
          
        }
       
        private void InitCommands()
        {
            DirectoryCommand = new RelayCommand(obj =>
            {
                CurrentView = new DirectoryView();
            });
            OrderCommand = new RelayCommand(obj =>
            {
               CurrentView = new OrderView();
            });
            AccountCommand = new RelayCommand(obj =>
            {
               CurrentView = new AccountView();
            });


        }
        /*
      private async void InitCollection()
      {
          await Task.Run(() => Roles = new ObservableCollection<string>(roleService.GetAll().Select(x => x.ToString())));
      }*/
       

    }
}
