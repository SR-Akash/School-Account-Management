using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G2_UMA.Models
{
    public class Payment
    {
        public int id { get; set; }
        public string Std_Id { get; set; }
        public int Fee_Id { get; set; }
        public int Amount { get; set; }
        public int M_Id { get; set; }

        public Students Students { get; set; }
        public Fees Fees { get; set; }
        public Months Months { get; set; }
    }
}
