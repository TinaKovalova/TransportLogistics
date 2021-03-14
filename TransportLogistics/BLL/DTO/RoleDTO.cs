using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class RoleDTO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

       // public  ObservableCollection<UserDTO> Users { get; set; }

        public override string ToString() => $"{RoleName}";
       


    }
}
