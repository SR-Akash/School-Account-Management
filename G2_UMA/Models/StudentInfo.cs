using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G2_UMA.Models
{
    public class StudentInfo
    {
        public int id { get; set; }
        public string std_id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public int Class { get; set; }
        public int paid { get; set; }
        public int due { get; set; }
    }
}
