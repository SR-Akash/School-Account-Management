using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G2_UMA.Models
{
    public class Months
    {
        public Months()
        {
            Students = new HashSet<Students>();
            Payment = new HashSet<Payment>();
        }

        public int M_Id { get; set; }
        public string M_Name { get; set; }

        public ICollection<Students> Students { get; set; }
        public ICollection<Payment> Payment { get; set; }
    }
}
