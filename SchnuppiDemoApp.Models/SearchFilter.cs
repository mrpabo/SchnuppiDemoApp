using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchnuppiDemoApp.Models
{
    public class SearchFilter
    {
        public string ProductionOrder { get; set; }
        public string ProducerNumber { get; set; }

        public DateTime? DateFrom { get; set; } // Datum "von"
        public DateTime? DateTo { get; set; }   // Datum "bis"

    }
}
