using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
        private ObservableCollection<OrderStatusDTO> orderStatuses;
        private ObservableCollection<CarDTO> cars;
        private ObservableCollection<OrderDTO> orders;
        private UserDTO selectedUser;
        private RoleDTO selectedRole;
        private OrderStatusDTO  selectedOrderStatus;
        private CarDTO selectedCar;
        private OrderDTO selectedOrder;
        private DateTime sortDate=DateTime.Now;
        private string findString;
       



        private IService<RoleDTO> rolesService;
        private IService<UserDTO> usersService;
        private IService<OrderStatusDTO> orderStatusService;
        private IService<CarDTO> carService;
        private IService<OrderDTO> orderService;

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
       /* public UserControl PrevView
        {
            get => prevView;
            set
            {
                prevView = value;
                Notify();
            }
        }*/

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
                            CurrentFirstChildView = new StatusView();
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
        public OrderDTO SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value;
               // selectedOrder.StatusId = value.Status?.StatusId;
               // selectedOrder.UserId = value.OrderUser?.UserId;

                Notify();
            }
        }
        public OrderStatusDTO SelectedOrderStatus
        {
            get => selectedOrderStatus;
            set
            {
                selectedOrderStatus = value;

                Notify();
            }
        }
        public CarDTO SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;

                Notify();
            }
        }
        public DateTime SortDate
        {
            get => sortDate;
            set
            {
                sortDate = value;
                Notify();
            }
        }
        public string FindString
        {
            get => findString;
            set
            {
                findString = value;
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
        public ObservableCollection<OrderStatusDTO> OrderStatuses
        {
            get => orderStatuses;
            set
            {
                orderStatuses = value;
                Notify();
            }
        }
        public ObservableCollection<CarDTO> Cars
        {
            get => cars;
            set
            {
                cars = value;
                Notify();
            }
        }
        public ObservableCollection<OrderDTO> Orders
        {
            get => orders;
            set
            {
                orders = value;
                Notify();
            }
        }


        #endregion


        #region Commands

        public ICommand ChangeViewCommand { get; set; }
        public ICommand CreateCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand SaveOrCancelCommand { get; set; }
        public ICommand SortCommand { get; set; }
        public ICommand PrintVisualCommand { get; set; }

        #endregion

        public MainViewModel(IService<RoleDTO> rolesService, IService<UserDTO>usersService, IService<OrderStatusDTO> orderStatusService, 
                                IService<CarDTO> carService, IService<OrderDTO> orderService)
        {
            
            this.rolesService = rolesService;
            this.usersService = usersService;
            this.orderStatusService = orderStatusService;
            this.carService = carService;
            this.orderService = orderService;
           
          
            // InitCollection();
            Roles = new ObservableCollection<RoleDTO>(rolesService.GetAll());
            Users = new ObservableCollection<UserDTO>(usersService.GetAll());
            OrderStatuses = new ObservableCollection<OrderStatusDTO>(orderStatusService.GetAll());
            Cars = new ObservableCollection<CarDTO>(carService.GetAll());
            Orders = new ObservableCollection<OrderDTO>(orderService.GetAll().OrderBy(x => x.Date));

            foreach(var i in Orders)
            {
                if (i.StatusId.HasValue)
                {
                    i.Status = orderStatusService.Get((int)(i.StatusId));
                    i.OrderUser = usersService.Get((int)(i.UserId));
                }
            }
            

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
                     case "User":
                         {
                            SelectedUser = new UserDTO();
                            CurrentFirstChildView =new CreateUserView();
                                                    
                             break;
                         }
                     case "Car":
                         {

                             SelectedCar = new CarDTO();
                             CurrentFirstChildView = new CreateCarView();
                             break;
                         }
                     case "Role":
                         {
                             SelectedRole = new RoleDTO();
                             CurrentFirstChildView = new CreateRoleView();
                             break;
                         }
                     case "Status":
                         {
                             SelectedOrderStatus = new OrderStatusDTO();
                             CurrentFirstChildView = new CreateStatusView();
                             break;
                         }
                     case "OrderList":
                         {
                             //SelectedOrderStatus = new OrderStatusDTO();
                             CurrentFirstChildView = new OrderListView();
                             break;
                         }
                     case "Order":
                         {
                             //SelectedOrder = new OrderDTO();
                             CurrentFirstChildView = new CreateNewOrderView();
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
                                 carService.CreateOrUpdate(SelectedCar);
                                 Cars = new ObservableCollection<CarDTO>(carService.GetAll());
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
                                 orderStatusService.CreateOrUpdate(SelectedOrderStatus);
                                 OrderStatuses = new ObservableCollection<OrderStatusDTO>(orderStatusService.GetAll());
                                 break;
                             }
                         case "order":
                             {
                                 orderService.CreateOrUpdate(SelectedOrder);
                                 Orders = new ObservableCollection<OrderDTO>(orderService.GetAll());

                                 foreach (var i in Orders)
                                 {
                                     if (i.StatusId.HasValue)
                                     {
                                         i.Status = orderStatusService.Get((int)(i.StatusId));
                                         i.OrderUser = usersService.Get((int)(i.UserId));
                                     }
                                 }

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
            SortCommand = new RelayCommand(obj =>
             {
                 var param = obj as String;
                 if (param == "byDate")
                 {
                     var select =orderService.GetAll().Where(order=>order.Date==SortDate);
                     Orders =new ObservableCollection<OrderDTO>(select);
                 }
                 else if (param == "byUser")
                 {

                     var select = Orders.Where(order => order.UserId == SelectedUser.UserId);
                     Orders = new ObservableCollection<OrderDTO>(select);
                 }
                 else if(param== "byCar")
                 {
                     var select = Orders.Where(order => order.CarId == SelectedCar.CarId);
                     Orders = new ObservableCollection<OrderDTO>(select);
                 }
                 else if (param == "find")
                 {
                     var select = Orders.Where(order => order.WhereFrom.Contains(FindString) || order.Where.Contains(FindString));
                     Orders = new ObservableCollection<OrderDTO>(select);
                 }
                 else if (param == "filling")
                 {
                     var select = orderService.GetAll().Where(order => order.Date == SortDate && order.CarId==SelectedCar?.CarId);
                     Orders = new ObservableCollection<OrderDTO>(select);
                 }

                 else if (param == "clear")
                 {
                     Orders = new ObservableCollection<OrderDTO>(orderService.GetAll().OrderBy(x => x.Date));

                     foreach (var i in Orders)
                     {
                         if (i.StatusId.HasValue)
                         {
                             i.Status = orderStatusService.Get((int)(i.StatusId));
                             i.OrderUser = usersService.Get((int)(i.UserId));
                         }
                     }
                     Orders.OrderBy(order => order.Date);
                 }
             });
            PrintVisualCommand = new RelayCommand(obj =>
             {
                 var viewDoc = obj as System.Windows.Media.Visual;
                
                 PrintDialog printDialog = new PrintDialog();
                 if (printDialog.ShowDialog() == true)
                 {
                     printDialog.PrintVisual(obj as System.Windows.Media.Visual, "Printing");
                 }
                




             });
        }

       
    }

    /*
  private async void InitCollection()
  {
      await Task.Run(() => Roles = new ObservableCollection<string>(roleService.GetAll());
  }*/


}

