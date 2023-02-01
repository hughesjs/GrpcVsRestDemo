using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLibrary.REST
{
    public class MeteoriteLandingRest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Fall { get; set; }
        public GeoLocationRest GeoLocationRest { get; set; }
        public double Mass { get; set; }
        public string NameType { get; set; }
        public string RecClass { get; set; }
        public double RecLAT { get; set; }
        public double RecLONG { get; set; }
        public DateTime Year { get; set; }
    }
}
