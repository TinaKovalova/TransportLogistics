using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TransportLogistics.Infrastructure;

namespace TransportLogistics.ViewModels.UserControlsModels.ChildrenUserModels
{
    public class CreateStatusViewModel : BaseNotifyPropertyChanged
    {
        IService<OrderStatusDTO> orderService;
        OrderStatusDTO selectedStatusDTO;
        public OrderStatusDTO SelectedStatusDTO
        {
            get => selectedStatusDTO;
            set
            {
                selectedStatusDTO = value;
                Notify();
            }

        }
        public ICommand SaveOrCancelCommand { get; set; }
        public CreateStatusViewModel(IService<OrderStatusDTO> orderService)
        {
            this.orderService = orderService;
            selectedStatusDTO = new OrderStatusDTO();
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
                        orderService.CreateOrUpdate(SelectedStatusDTO);
                        MessageBox.Show("Статус создан");
                        SelectedStatusDTO = new OrderStatusDTO();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                }
                else if (param == "cancel")
                {
                    SelectedStatusDTO = null;
                }

            });

        }
    }
}
