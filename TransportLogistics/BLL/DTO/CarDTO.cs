using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CarDTO
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string CarNumber { get; set; }
        public decimal FuelConsumption { get; set; }
        public override string ToString()
        {
            return $"{CarName} {CarNumber}";
        }
    }
}
