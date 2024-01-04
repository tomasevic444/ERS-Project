using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje
{
    public class ConsumptionData
    {
        public DateTime Date { get; set; }
        public string GeographicalArea { get; set; }
        public int Hour { get; set; }
        public double ForecastedConsumption { get; set; }
        public double ActualConsumption { get; set; }
    }
}
