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
        private UserControl currentView;
        ObservableCollection<UserDTO> users;

        public UserControl CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                Notify();
            }
        }
        public ObservableCollection<UserDTO> Users
        {
            get => users;
            set
            {
                users = value;
                Notify();
            }
        }
        
        IService<UserDTO> userService;
        
        #endregion
        public MainViewModel(IService<UserDTO>userService)
        {
            this.userService = userService;

            InitCommands();
            currentView = new StartView();
        }
        private void InitCommands()
        {
            AuthorizationCommand = new RelayCommand(obj =>
             {

             });

        }
        #region Command
        public ICommand AuthorizationCommand { get; set; }
        #endregion
    }
}
