using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
        OrderStatusDTO currentStatusDTO;
        public OrderStatusDTO CurrentStatusDTO
        {
            get => currentStatusDTO;
            set
            {
                currentStatusDTO = value;
                Notify();
            }
        }
        public ICommand SaveOrCancelCommand { get; set; }
        public CreateStatusViewModel(IService<OrderStatusDTO> orderService)
        {
            this.orderService = orderService;
            CurrentStatusDTO = new OrderStatusDTO();
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
                        if (CurrentStatusDTO.StatusName != null)
                        {
                            orderService.CreateOrUpdate(CurrentStatusDTO);
                            MessageBox.Show("Статус создан");
                            CurrentStatusDTO = new OrderStatusDTO();
                        }
                        else
                            MessageBox.Show("Некорректно заполнены данные!\nЗапись не сохранена.");
                    }
                    catch (DbEntityValidationException)
                    {
                        MessageBox.Show("Некорректно заполнены данные!\nЗапись невалидна.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (param == "cancel")
                {
                    CurrentStatusDTO = new OrderStatusDTO();
                }
            });
        }
    }
}
