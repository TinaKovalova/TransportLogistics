using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TransportLogistics.Infrastructure;

namespace TransportLogistics.ViewModels.UserControlsModels.ChildrenUserModels
{
    public class CreateNewOrderModel : BaseNotifyPropertyChanged
    { 
        IService<OrderDTO> orderService;
        IService<UserDTO> userService;
        IService<OrderStatusDTO> statusService;
        ObservableCollection<UserDTO> users;
        ObservableCollection<OrderStatusDTO> statuses;
        OrderDTO selectedOrder;
        OrderStatusDTO selectedStatus;
        UserDTO selectedUser;
        DateTime currentDate;
        public ObservableCollection<UserDTO> Users
        {
            get => users;
            set
            {
                users = value;
                Notify();
            }
        }
        public ObservableCollection<OrderStatusDTO> Statuses
        {
            get => statuses;
            set
            {
                statuses = value;
                Notify();
            }
        }
        public OrderDTO SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value;
                Notify();
            }

        }
        public OrderStatusDTO SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
                Notify();
            }

        }
        public UserDTO SelestedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                Notify();
            }

        }
        public DateTime CurrentDate
        {
            get => currentDate;
            set
            {
                currentDate = value;
                Notify();
            }

        }
        public ICommand SaveOrCancelCommand { get; set; }
        public CreateNewOrderModel(IService<OrderDTO> orderService, IService<UserDTO> userService, IService<OrderStatusDTO> statusService)
        {
            this.orderService = orderService;
            this.userService = userService;
            this.statusService = statusService;
            SelectedOrder = new OrderDTO();
            // SelectedOrder.OrderStatus = "новый";
            CurrentDate = DateTime.Now;

            Users = new ObservableCollection<UserDTO>(userService.GetAll());
            Statuses = new ObservableCollection<OrderStatusDTO>(statusService.GetAll());
            InitCommand();

        }
        private void InitCommand()
        {
            SaveOrCancelCommand = new RelayCommand(obj =>
            {
                var param = obj as String;
                if (param == "save")
                {
                    /*
                    SelectedOrder.User = selectedUser;
                    SelectedOrder.OrderStatus = SelectedStatus;
                    SelectedOrder.StatusId = selectedStatus.StatusId;
                    SelectedOrder.UserId = selectedOrder.User.UserId;
                  */
                   
                    SelectedOrder.Date = $"{CurrentDate.Year}-{CurrentDate.Month}-{CurrentDate.Day}";
                    orderService.CreateOrUpdate(SelectedOrder);
                    MessageBox.Show("Заказ создана");
                    SelectedOrder = new OrderDTO();


                }
                else if (param == "cancel")
                {
                    SelectedOrder = null;
                }

            });

        }
    }
}
