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
using TransportLogistics.Views.UserControls.ChildrenUserControls;

namespace TransportLogistics.ViewModels
{
    public class MainViewModel:BaseNotifyPropertyChanged
    {
        #region Fields & Propertyes
        private UserControl currentView;
        private UserControl currentFirstChildView;
        private UserControl currentLastChildView;

        private TabItem selectedTabItem;
        private ObservableCollection<RoleDTO> roles;
        private ObservableCollection<UserDTO> users;
        private UserDTO selectedUser;
        private RoleDTO selectedRole;
       
       
        
        private IService<RoleDTO> rolesService;
        private IService<UserDTO> usersService;
      
        public UserControl CurrentFirstChildView
        {
            get => currentFirstChildView;
            set
            {
                currentFirstChildView = value;
                Notify();
            }
        }
        public UserControl CurrentLastChildView
        {
            get => currentLastChildView;
            set
            {
                currentLastChildView = value;
                Notify();
            }
        }
        // Создается главное представление на TabItem -----------------------------------------
        public TabItem SelectedTabItem
        {
            get => selectedTabItem;
            set
            {
                selectedTabItem = value;
                var param = selectedTabItem.Header.ToString();
              
                switch (param)
                {
                    case "Пользователи":
                        {
                            CurrentFirstChildView = new UserView();
                            break;
                        }
                    case "Автомобили":
                        {
                           CurrentFirstChildView = new CarView();
                            break;
                        }
                    case "Роли":
                        {
                           CurrentFirstChildView = new RoleView();
                            break;
                        }
                    case "Статус заказа":
                        {
                            CurrentFirstChildView = new OrderStatusView();
                            break;
                        }
                    case "Топливо":
                        {
                            //CurrentFirstChildView = new AccountView();
                            break;
                        }
                }

                Notify();
            }
        }
        public UserControl CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                Notify();
            }
        }
        public RoleDTO SelectedRole {
            get => selectedRole;
            set
            {
                selectedRole = value;
                Notify();
            }
        }
       
        public UserDTO SelectedUser
        {
            get => selectedUser;
            set
            {
               selectedUser = value;
            
                Notify();
            }
        }
       
        public ObservableCollection<RoleDTO> Roles
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
           
          
            // InitCollection();
            Roles = new ObservableCollection<RoleDTO>(rolesService.GetAll());
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
                     case "CreateUser":
                         {
                            SelectedUser = new UserDTO();
                            CurrentFirstChildView =new CreateUserView();
                                                    
                             break;
                         }
                     case "EditUser":
                         {


                             // CurrentFirstChildView = new CreateUserView();
                             break;
                         }
                     case "Role":
                         {
                             SelectedRole = new RoleDTO();
                             CurrentFirstChildView = new CreateRoleView();
                             break;
                         }
                 }

             });
            SaveOrCancelCommand = new RelayCommand(obj =>
             {
                 var param = obj as String;
                 if (param.StartsWith ("save"))
                 {
                     string key = param.Split(' ')[1];

                     switch (key)
                     {
                         case "user":
                             {
                                 usersService.CreateOrUpdate(SelectedUser);
                                 Users = new ObservableCollection<UserDTO>(usersService.GetAll());
                                 break;
                             }
                         case "car":
                             {
                                 CurrentFirstChildView = new CarView();
                                 break;
                             }
                         case "role":
                             {
                                 rolesService.CreateOrUpdate(SelectedRole);
                                 Roles = new ObservableCollection<RoleDTO>(rolesService.GetAll());
                                 break;
                             }
                         case "status":
                             {
                                 CurrentFirstChildView = new OrderStatusView();
                                 break;
                             }
                         case "Топливо":
                             {
                                 //CurrentFirstChildView = new AccountView();
                                 break;
                             }
                     }
                     
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
                                //usersService.Remove(SelectedUser);
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

