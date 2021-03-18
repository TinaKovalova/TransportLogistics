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
     public class CreateUserModel: BaseNotifyPropertyChanged
    {
        IService<UserDTO> usersService;
        IService<RoleDTO> rolesService;
        UserDTO user;
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
        ObservableCollection<RoleDTO> roles;
        public UserDTO User
        {

            get => user;
            set
            {
                user = value;
                Notify();
            }

        }
        public ObservableCollection<RoleDTO> Roles
        {
            get => roles;
            set
            {
                roles = value;
                Notify();
            }
        }
        public ICommand SaveOrCancelCommand { get; set; }

        public CreateUserModel(IService<UserDTO> usersService, IService<RoleDTO> rolesService)
        {
            this.usersService = usersService;
            this.rolesService = rolesService;
            roles = new ObservableCollection<RoleDTO>(rolesService.GetAll());
            User = new UserDTO();
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
                        usersService.CreateOrUpdate(User);
                        MessageBox.Show("Пользователь создан");
                        User = new UserDTO();

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                  

                }
                else if (param == "cancel")
                {
                    User = new UserDTO();
                }

            });

        }
    }
}
