using EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidencijaElEnegije.PrognoziraneelOstvarenePotrosnje;
using EvidencijaElEnegije.PrognoziranelOstvarenePotrosnje;

namespace EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje
{
    public class CsvExporter : ICsvExporter
    {


        public  void ExportToCsv(List<ConsumptionData> data, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Sat,Prognozirana potrosnja,Ostvarena postrosnja,Relativno odstupanje");

                foreach (var item in data)
                {
                    double relativeDeviation = Formule.CalculateRelativeDeviation(item);

                    writer.WriteLine($"{item.Hour},{item.ForecastedConsumption},{item.ActualConsumption},{relativeDeviation:P2}");
                }
            }

           
        }
     
    }
}
