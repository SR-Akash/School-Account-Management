using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G2_UMA.Models
{
    public partial class Fees
    {
        public Fees()
        {
            Payment = new HashSet<Payment>();
        }

        public int Fee_Id { get; set; }
        public string F_Name { get; set; }

        public ICollection<Payment> Payment { get; set; }
    }
}
