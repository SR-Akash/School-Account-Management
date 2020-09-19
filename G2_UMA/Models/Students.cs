using System;
using System.Collections.Generic;

namespace G2_UMA.Models
{
    public partial class Students
    {
        public Students()
        {
            Payment = new HashSet<Payment>();
        }

        public string Std_Id { get; set; }
        public string Name { get; set; }
        public string Birth_Date { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Father_Name { get; set; }
        public string Mother_Name { get; set; }
        public int Class { get; set; }
        public int M_Id { get; set; }

        public Months Months { get; set; }

        public ICollection<Payment> Payment { get; set; }
    }
}