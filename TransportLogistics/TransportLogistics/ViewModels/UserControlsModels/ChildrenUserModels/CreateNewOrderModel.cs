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
        ObservableCollection<UserDTO> users;
        OrderDTO selectedOrder;
        public ObservableCollection<UserDTO> Users
        {
            get => users;
            set
            {
                users = value;
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
        public ICommand SaveOrCancelCommand { get; set; }
        public CreateNewOrderModel(IService<OrderDTO> orderService, IService<UserDTO> userService)
        {
            this.orderService = orderService;
            this.userService = userService;
            SelectedOrder = new OrderDTO();
           // SelectedOrder.OrderStatus.StatusName = "новый";

            Users = new ObservableCollection<UserDTO>(userService.GetAll());
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
                       
                        orderService.CreateOrUpdate(SelectedOrder);
                        MessageBox.Show("Роль создана");
                        SelectedOrder = new OrderDTO();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                }
                else if (param == "cancel")
                {
                    SelectedOrder = null;
                }

            });

        }
    }
}
