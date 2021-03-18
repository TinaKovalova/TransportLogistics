using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public string WhereFrom { get; set; }
        public string Where { get; set; }
        public string Note { get; set; }
        public int? StatusId { get; set; }

        public int? UserId { get; set; }
        public int? CarId { get; set; }
        public float? Distance { get; set; }
        public CarDTO Car { get; set; }
        public OrderStatusDTO  Status { get; set; }
        public UserDTO OrderUser { get; set; }
        
    }
}
