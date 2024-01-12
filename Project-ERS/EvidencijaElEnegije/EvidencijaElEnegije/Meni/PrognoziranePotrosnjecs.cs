using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EvidencijaElEnegije.PrognoziraneelOstvarenePotrosnje;
using EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje;
using EvidencijaElEnegije.PrognoziranelOstvarenePotrosnje;

namespace EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje
{
   public  class PrognoziranePotrosnje
    {

        static List<ConsumptionData> consumptionDataList = new List<ConsumptionData>();
        private IXmlDataLoader xmlDataLoader;
        private ICsvExporter csvExporter;

        public PrognoziranePotrosnje(IXmlDataLoader xmlDataLoader, ICsvExporter csvExporter)
        {
            this.xmlDataLoader = xmlDataLoader;
            this.csvExporter = csvExporter;
        }


        public void IspisPotrosnje()
        {
            // Korisnicki unos datuma
            Console.WriteLine("Unesite datum (yyyy-MM-dd): ");
            DateTime selectedDate = DateTime.Parse(Console.ReadLine());

            // Korisnicki unos geografske oblasti
            Console.WriteLine("Unesite geografsku oblast (npr. VOJ, BGD): ");
            string selectedArea = Console.ReadLine();
            XmlDataLoader xmlDataLoaderInstance = new XmlDataLoader();
            // Citanje XML-a na osnovu korisnickih unosa
            xmlDataLoaderInstance.LoadConsumptionData(selectedDate.ToString("yyyy_MM_dd"), selectedArea, consumptionDataList);

            // filtriranje podataka na osnovu korisnickog unosa
            var filteredData = consumptionDataList
                .Where(data => data.Date == selectedDate && data.GeographicalArea == selectedArea)
                .ToList();

            // CSV header
            Console.WriteLine("Sat | Prognozirana potrosnja | Ostvarena postrosnja | Relativno odstupanje (%)");

            // Prikazivanje podataka u tabularnom formatu
            foreach (var data in filteredData)
            {
                double relativeDeviation = Formule.CalculateRelativeDeviation(data);

                Console.WriteLine($"{data.Hour,4} | {data.ForecastedConsumption,22:N2} | {data.ActualConsumption,19:N2} | {relativeDeviation,24:P2}");
            }
            string putanjaDoDirektorijuma = DirektorijumPath.PathOdstupanja;
            string filename = selectedArea + selectedDate.ToString("yyyy_MM_dd");
            string relpath = Path.Combine(putanjaDoDirektorijuma, $"odstupanje_{filename}.csv");

            // Izvoz tabele u CSV fajl

            CsvExporter csvExporterInstance = new CsvExporter();

            // Export data to CSV using the created instance
            csvExporterInstance.ExportToCsv(filteredData, relpath);
        }

   

      

    

    }
}

