using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserLastName { get; set; }

        public string UserFirstName { get; set; }

        public string UserPatronymic { get; set; }

        public string UserDrivingLecense { get; set; }

        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public override string ToString() => $"{UserLastName} {UserFirstName}";
        
    }
}
