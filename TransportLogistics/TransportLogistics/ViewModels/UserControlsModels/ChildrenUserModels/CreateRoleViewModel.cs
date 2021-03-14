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
    public class CreateRoleViewModel: BaseNotifyPropertyChanged
    {
        IService<RoleDTO> rolesService;
        RoleDTO selectedRoleDTO;
        public RoleDTO SelectedRoleDTO
        {
            get => selectedRoleDTO;
            set
            {
                selectedRoleDTO = value;
                Notify();
            }

        }
        public ICommand SaveOrCancelCommand { get; set; }
        public CreateRoleViewModel(IService<RoleDTO> rolesService)
        {
            this.rolesService = rolesService;
            selectedRoleDTO = new RoleDTO();
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
                        rolesService.CreateOrUpdate(SelectedRoleDTO);
                        MessageBox.Show("Роль создана");
                        SelectedRoleDTO = new RoleDTO();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                }
                else if (param == "cancel")
                {
                    SelectedRoleDTO = null;
                }

            });

        }
    }
}
