using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TransportLogistics.Infrastructure;
using TransportLogistics.Views.UserControls;


namespace TransportLogistics.ViewModels
{
    public class MainViewModel:BaseNotifyPropertyChanged
    {
        #region Fields & Propertyes
        private UserControl currentView = null;
        private ObservableCollection<string> roles;
        private ObservableCollection<UserDTO> users;
        private UserDTO selectedUser;
        private IService<RoleDTO> rolesService;
        private IService<UserDTO> usersService;
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
       
        public UserDTO SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                Notify();
            }
        }
        public ObservableCollection<string> Roles
        {
            get => roles;
            set
            {
                roles = value;
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

        #endregion


        #region Commands
       
        public ICommand ChangeViewCommand { get; set; }
        public ICommand CreateCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand SaveOrCancelCommand { get; set; }

        #endregion

        public MainViewModel(IService<RoleDTO> rolesService, IService<UserDTO>usersService)
        {
            this.rolesService = rolesService;
            this.usersService = usersService;
           
           // CurrentView = new DirectoryView();
            // InitCollection();
            Roles = new ObservableCollection<string>(rolesService.GetAll().Select(x => x.ToString()));
            Users = new ObservableCollection<UserDTO>(usersService.GetAll().ToList());
            InitCommands();
          
        }

        private void InitCommands()
        {
            ChangeViewCommand = new RelayCommand(obj =>
            {
                var param = obj as String;
                switch (param)
                {
                    case "DirectoryView":
                        {
                            CurrentView = new DirectoryView();
                            break;
                        }
                    case "OrderView":
                        {
                            CurrentView = new OrderView();
                            break;
                        }
                    case "AccountView":
                        {
                            CurrentView = new AccountView();
                            break;
                        }
                }
            });
            CreateCommand = new RelayCommand(obj =>
             {
                 var param = obj as String;
                 switch (param)
                 {
                     case "user":
                         {
                             SelectedUser = new UserDTO();
                             break;
                         }
                     case "":
                         {
                             CurrentView = new OrderView();
                             break;
                         }
                     case "2":
                         {
                             CurrentView = new AccountView();
                             break;
                         }
                 }

             });
            SaveOrCancelCommand = new RelayCommand(obj =>
             {
                 var param = obj as String;
                 if (param == "save")
                 {
                     usersService.CreateOrUpdate(SelectedUser);
                 }
                 else if (param == "cancel")
                 {
                     SelectedUser = null;
                 }

             });
            RemoveCommand = new RelayCommand(obj => {
                var param = obj as String;
               
                MessageBox.Show(param);
                switch (param)
                {
                    case "user":
                        {

                            MessageBoxResult result=MessageBox.Show($"Вы действительно хотите удалить {SelectedUser.UserLastName} {SelectedUser.UserFirstName} {SelectedUser.UserPatronymic}","?",MessageBoxButton.YesNo) ;
                            if (result == MessageBoxResult.Yes)
                            {
                                usersService.Remove(SelectedUser);
                            }
                            //usersService.Remove(SelectedUser);
                                break;
                        }
                    case "":
                        {
                            CurrentView = new OrderView();
                            break;
                        }
                    case "2":
                        {
                            CurrentView = new AccountView();
                            break;
                        }
                }
            },(obj)=>SelectedUser!=null);
        }



    }
    /*
  private async void InitCollection()
  {
      await Task.Run(() => Roles = new ObservableCollection<string>(roleService.GetAll().Select(x => x.ToString())));
  }*/


}

