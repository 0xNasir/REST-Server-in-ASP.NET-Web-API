using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleRESTServer.Models
{
    public class Person
    {
        public long ID { get; set; }
        public String lastname { get; set; }
        public String firstname { get; set; }
        public Double payrate { get; set; }
        public String startdate { get; set; }
        public String enddate { get; set; }
    }
}