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
using System.Data.Entity.Validation;

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
                currentOrder.StatusId =statusService.Get(1).StatusId; ;
                currentOrder.Date = DateTime.Now;
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
            CurrentOrder = new OrderDTO();
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
                    try
                    {
                        CurrentOrder.UserId = CurrentOrder.OrderUser?.UserId;
                        if (CurrentOrder.Where != null & CurrentOrder.WhereFrom != null)
                        {
                            orderService.CreateOrUpdate(CurrentOrder);
                            MessageBox.Show("Заказ добавлен ");
                            CurrentOrder = new OrderDTO();
                        }
                        else
                            MessageBox.Show("Некорректно заполнены данные!\nЗапись не сохранена.");
                    }
                    catch(DbEntityValidationException)
                    {
                        MessageBox.Show("Некорректно заполнены данные!\nЗапись невалидна.");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (param == "cancel")
                {
                    CurrentOrder = new OrderDTO();
                }
            });
        }
    }
}
