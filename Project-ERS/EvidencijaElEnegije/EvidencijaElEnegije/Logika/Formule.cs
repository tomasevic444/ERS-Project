using EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaElEnegije.PrognoziranelOstvarenePotrosnje
{
    public class Formule
    {
        public static double CalculateRelativeDeviation(ConsumptionData data)
        {
            double absoluteDeviation = Math.Abs(data.ActualConsumption - data.ForecastedConsumption);
            return absoluteDeviation / data.ActualConsumption;
        }
    }
}
