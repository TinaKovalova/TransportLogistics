using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string Date { get; set; }
        public string FromWhere { get; set; }
        public string Where { get; set; }
        public string Note { get; set; }
        public string  OrderStatus { get; set; }
        public UserDTO User { get; set; }
        
    }
}
