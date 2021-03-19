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
    public class CreateRoleViewModel: BaseNotifyPropertyChanged
    {
        IService<RoleDTO> rolesService;
        RoleDTO currentRoleDTO;
        public RoleDTO CurrentRoleDTO
        {
            get => currentRoleDTO;
            set
            {
                currentRoleDTO = value;
                Notify();
            }
        }
        public ICommand SaveOrCancelCommand { get; set; }
        public CreateRoleViewModel(IService<RoleDTO> rolesService)
        {
            this.rolesService = rolesService;
            CurrentRoleDTO = new RoleDTO();
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
                        if (CurrentRoleDTO.RoleName != null)
                        {
                            rolesService.CreateOrUpdate(CurrentRoleDTO);
                            MessageBox.Show("Роль создана");
                            CurrentRoleDTO = new RoleDTO();
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
                    CurrentRoleDTO = new RoleDTO();
                }
            });
        }
    }
}
