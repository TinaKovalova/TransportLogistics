using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        PrintDialog printDialog;
        Dictionary<string, Action> viewChangeMethods;
        Dictionary<string, Action> saveMethods;
        Dictionary<string, Func<IEnumerable<OrderDTO>>> sortMethods;
        bool loading = true;




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
       
       /* public UserControl PrevView
        {
            get => prevView;
            set
            {
                prevView = value;
                Notify();
            }
        }*/

       
        public TabItem SelectedTabItem
        {
            get => selectedTabItem;
            set
            {
                selectedTabItem = value;
                var param = selectedTabItem.Header.ToString();
                viewChangeMethods[param]();
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
        public ICommand SaveOrCancelCommand { get; set; }
        public ICommand SortCommand { get; set; }
        public ICommand PrintVisualCommand { get; set; }
        public ICommand SaveVisualCommand { get; set; }

        #endregion

        public MainViewModel(IService<RoleDTO> rolesService, IService<UserDTO>usersService, IService<OrderStatusDTO> orderStatusService, 
                                IService<CarDTO> carService, IService<OrderDTO> orderService)
        {
            this.rolesService = rolesService;
            this.usersService = usersService;
            this.orderStatusService = orderStatusService;
            this.carService = carService;
            this.orderService = orderService;
            printDialog = new PrintDialog();
            InitCollection();
            InitDictionary();
            InitCommands();
           

        }
        private void InitCollection()
        {
            Task.Run(() => {
                Roles = new ObservableCollection<RoleDTO>(rolesService.GetAll());
                Users = new ObservableCollection<UserDTO>(usersService.GetAll());
                OrderStatuses = new ObservableCollection<OrderStatusDTO>(orderStatusService.GetAll());
                Cars = new ObservableCollection<CarDTO>(carService.GetAll());
                InitOrdersCollection();
                loading = false;
            } );
          
        }
        private void InitOrdersCollection()
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
        private void InitDictionary()
        {
            viewChangeMethods = new Dictionary<string, Action>
            {
                ["DirectoryView"] = () => CurrentView = new DirectoryView(),
                ["OrderView"] = () => CurrentView = new OrderView(),
                ["AccountView"] = () => CurrentView = new AccountView(),
                ["User"] = () => CurrentFirstChildView = new CreateUserView(),
                ["Car"] = () => CurrentFirstChildView = new CreateCarView(),
                ["Role"] = () => CurrentFirstChildView = new CreateRoleView(),
                ["Status"] = () => CurrentFirstChildView = new CreateStatusView(),
                ["OrderList"] = () => CurrentFirstChildView = new OrderListView(),
                ["Order"] = () => CurrentFirstChildView = new CreateNewOrderView(),
                ["Пользователи"] = () => CurrentFirstChildView = new UserView(),
                ["Автомобили"] = () => CurrentFirstChildView = new CarView(),
                ["Роли"] = () => CurrentFirstChildView = new RoleView(),
                ["Статус заказа"] = () => CurrentFirstChildView = new StatusView()
            };

            saveMethods = new Dictionary<string, Action>
            {
                ["user"] = () =>
                {
                    usersService.CreateOrUpdate(SelectedUser);
                    Users = new ObservableCollection<UserDTO>(usersService.GetAll());
                },
                ["car"] = () =>
                {
                    carService.CreateOrUpdate(SelectedCar);
                    Cars = new ObservableCollection<CarDTO>(carService.GetAll());
                },
                ["role"] = () =>
                {
                    rolesService.CreateOrUpdate(SelectedRole);
                    Roles = new ObservableCollection<RoleDTO>(rolesService.GetAll());
                },
                ["status"] = () =>
                {
                    orderStatusService.CreateOrUpdate(SelectedOrderStatus);
                    OrderStatuses = new ObservableCollection<OrderStatusDTO>(orderStatusService.GetAll());
                },
                ["order"] = () =>
                {
                    orderService.CreateOrUpdate(SelectedOrder);
                    InitOrdersCollection();
                }
            };
            sortMethods= new Dictionary<string, Func<IEnumerable<OrderDTO>>>
            {
                ["byDate"] = () => Orders.Where(order => order.Date == SortDate),
                ["byUser"] = () => Orders.Where(order => order.UserId == SelectedUser?.UserId),
                ["byCar"] = () => Orders.Where(order => order.CarId == SelectedCar?.CarId),
                ["find"] = () => Orders.Where(order => order.WhereFrom.Contains(FindString) || order.Where.Contains(FindString)),
                ["filling"] = () => orderService.GetAll().Where(order => order.Date == SortDate && order.CarId == SelectedCar?.CarId && order.StatusId == 2),
                ["clear"] = () =>
                {
                    InitOrdersCollection();
                    return Orders;
                }
            };
        }
        private void InitCommands()
        {
            ChangeViewCommand = new RelayCommand(obj =>
            {
                var param = obj as String;
                viewChangeMethods[param]();
              
            },obj=>loading!=true);
            CreateCommand = new RelayCommand(obj =>
             {
                 var param = obj as String;
                 viewChangeMethods[param]();
                
             });
            SaveOrCancelCommand = new RelayCommand(obj =>
             {
                 var param = obj as String;
                 if (param.StartsWith ("save"))
                 {
                     string key = param.Split(' ')[1];
                     saveMethods[key]();
                 }
                 else if (param.StartsWith("cancel"))
                 {
                     SelectedUser = null;
                     SelectedCar = null;
                     SelectedOrder = null;
                     SelectedOrderStatus = null;
                     SelectedRole = null;
                     //перезалить вьюху////////////////////////////////////////////////////////
                 }

             });
        
            SortCommand = new RelayCommand(obj =>
             {
                 var param = obj as String;
                 Orders = new ObservableCollection<OrderDTO>(sortMethods[param]());
            });
            PrintVisualCommand = new RelayCommand(obj =>
             {
                 var viewDoc = obj as System.Windows.Media.Visual;
                
                 if (printDialog.ShowDialog() == true)
                 {
                     printDialog.PrintVisual(obj as System.Windows.Media.Visual, "Печать...");
                 }
             });
            SaveVisualCommand= new RelayCommand(obj =>
            {
                var viewDoc = obj as System.Windows.Media.Visual;
                printDialog.PrintVisual(obj as System.Windows.Media.Visual, "Сохранение...");
                
            });
        }

       
    }

  

}

