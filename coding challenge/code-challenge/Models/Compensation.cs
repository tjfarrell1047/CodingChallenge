using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class Compensation
    {
        public string id {get;set;}
        public Employee employee { get; set; }
        public double salary { get; set; }
        public DateTime effectiveDate { get; set; }

    }
}
