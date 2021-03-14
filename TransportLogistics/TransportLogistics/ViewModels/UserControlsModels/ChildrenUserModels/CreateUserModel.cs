using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TransportLogistics.Infrastructure;

namespace TransportLogistics.ViewModels.UserControlsModels.ChildrenUserModels
{
     public class CreateUserModel: BaseNotifyPropertyChanged
    {
        IService<UserDTO> usersService;
        private UserDTO user;
        public UserDTO User
        {

            get => user;
            set
            {
                user = value;
                Notify();
            }

        }
        public ICommand SaveOrCancelCommand { get; set; }
        public CreateUserModel(IService<UserDTO> usersService)
        {
            this.usersService = usersService;
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
                    usersService.CreateOrUpdate(User);
                }
                else if (param == "cancel")
                {
                    User = null;
                }

            });

        }
    }
}
