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
     public class CreateCarViewModel: BaseNotifyPropertyChanged
    {
        IService<CarDTO> carService;
        CarDTO selectedCar;
        public CarDTO SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;
                Notify();
            }

        }
        public ICommand SaveOrCancelCommand { get; set; }
        public CreateCarViewModel(IService<CarDTO> carService)
        {
            this.carService = carService;
            selectedCar = new CarDTO();
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
                        carService.CreateOrUpdate(SelectedCar);
                        MessageBox.Show("Автомобиль создан");
                        SelectedCar = new CarDTO();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                }
                else if (param == "cancel")
                {
                    SelectedCar = null;
                }

            });

        }
    }
}
