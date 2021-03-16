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
        OrderDTO currentOrder;
        OrderStatusDTO currentStatus;
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
        public OrderDTO CurrentOrder
        {
            get => currentOrder;
            set
            {
                currentOrder = value;
                Notify();
            }

        }
        public OrderStatusDTO CurrentStatus
        {
            get => currentStatus;
            set
            {
                currentStatus = value;
                Notify();
            }

        }

        public ICommand SaveOrCancelCommand { get; set; }
        public CreateNewOrderModel(IService<OrderDTO> orderService, IService<UserDTO> userService, IService<OrderStatusDTO> statusService)
        {
            this.orderService = orderService;
            this.userService = userService;
            this.statusService = statusService;
            CurrentStatus = statusService.Get(2);
            CurrentOrder = new OrderDTO();
            CurrentOrder.Date = DateTime.Now;
            CurrentOrder.Status = CurrentStatus;
           
            

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
                    if (CurrentOrder != null)
                    {
                        CurrentOrder.StatusId = CurrentOrder.Status?.StatusId;
                        CurrentOrder.UserId = CurrentOrder.OrderUser?.UserId;
                    }
                    orderService.CreateOrUpdate(CurrentOrder);
                    MessageBox.Show("Заказ добавлен ");

                    CurrentOrder = new OrderDTO();
                    CurrentOrder.Date = DateTime.Now;


                }
                else if (param == "cancel")
                {
                    CurrentOrder = new OrderDTO();
                    CurrentOrder.Date = DateTime.Now;
                }

            });

        }
    }
}
