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
     public class CreateCarViewModel: BaseNotifyPropertyChanged
    {
        IService<CarDTO> carService;
        CarDTO currentCar;
        public CarDTO CurrentCar
        {
            get => currentCar;
            set
            {
                currentCar = value;
                Notify();
            }
        }
        public ICommand SaveOrCancelCommand { get; set; }
        public CreateCarViewModel(IService<CarDTO> carService)
        {
            this.carService = carService;
            CurrentCar = new CarDTO();
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
                        if (CurrentCar.CarName!= null & CurrentCar.CarNumber!=null)
                        {
                            carService.CreateOrUpdate(CurrentCar);
                            MessageBox.Show("Автомобиль создан");
                            CurrentCar = new CarDTO();
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
                    CurrentCar = new CarDTO();
                }
            });
        }
    }
}
