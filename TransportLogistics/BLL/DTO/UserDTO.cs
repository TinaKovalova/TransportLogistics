﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class UserDTO
    {
        public string UserLastName { get; set; }

        public string UserFirstName { get; set; }

        public string UserPatronymic { get; set; }

        public string UserDrivingLecense { get; set; }

        public int? UserRole { get; set; }

    }
}