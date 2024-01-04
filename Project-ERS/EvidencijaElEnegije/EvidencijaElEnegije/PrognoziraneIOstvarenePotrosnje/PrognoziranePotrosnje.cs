using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje;

namespace EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje
{
    class PrognoziranePotrosnje
    {
   
            static List<ConsumptionData> consumptionDataList = new List<ConsumptionData>();



        public void IspisPotrosnje()
        {
            // Korisnicki unos datuma
            Console.WriteLine("Unesite datum (yyyy-MM-dd): ");
            DateTime selectedDate = DateTime.Parse(Console.ReadLine());

            // Korisnicki unos geografske oblasti
            Console.WriteLine("Unesite geografsku oblast (npr. VOJ, BGD): ");
            string selectedArea = Console.ReadLine();

            // Citanje XML-a na osnovu korisnickih unosa
            LoadConsumptionData(selectedDate.ToString("yyyy_MM_dd"), selectedArea);

            // filtriranje podataka na osnovu korisnickog unosa
            var filteredData = consumptionDataList
                .Where(data => data.Date == selectedDate && data.GeographicalArea == selectedArea)
                .ToList();

            // CSV header
            Console.WriteLine("Sat | Prognozirana potrosnja | Ostvarena postrosnja | Relativno odstupanje (%)");

            // Prikazivanje podataka u tabularnom formatu
            foreach (var data in filteredData)
            {
                double relativeDeviation = CalculateRelativeDeviation(data);

                Console.WriteLine($"{data.Hour,4} | {data.ForecastedConsumption,22:N2} | {data.ActualConsumption,19:N2} | {relativeDeviation,24:P2}");
            }
            string putanjaDoDirektorijuma = DirektorijumPath.PathOdstupanja;
            string filename = selectedArea+selectedDate.ToString("yyyy_MM_dd");
            string relpath = Path.Combine(putanjaDoDirektorijuma, $"odstupanje_{filename}.csv");

            // Izvoz tabele u CSV fajl
            ExportToCsv(filteredData, relpath);
        }

        private double CalculateRelativeDeviation(ConsumptionData data)
        {
            double absoluteDeviation = Math.Abs(data.ActualConsumption - data.ForecastedConsumption);
            return absoluteDeviation  / data.ActualConsumption;
        }

        private void ExportToCsv(List<ConsumptionData> data, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Sat,Prognozirana potrosnja,Ostvarena postrosnja,Relativno odstupanje");

                foreach (var item in data)
                {
                    double relativeDeviation = CalculateRelativeDeviation(item);

                    writer.WriteLine($"{item.Hour},{item.ForecastedConsumption},{item.ActualConsumption},{relativeDeviation:P2}");
                }
            }

            Console.WriteLine($"Table exported to {filePath}");
        }

        private void LoadConsumptionData(string fileName, string selectedArea)
        {

            string putanjaDoDirektorijuma = DirektorijumPath.PathDatoteke;
            // Construct the XML file path based on the entered date and region
            string forecastedXmlFilePath = Path.Combine(putanjaDoDirektorijuma, $"prog_{fileName}.xml");
            string actualXmlFilePath = Path.Combine(putanjaDoDirektorijuma, $"ostv_{fileName}.xml");

          

            Dictionary<int, ConsumptionData> consumptionDataDict = new Dictionary<int, ConsumptionData>();

            // Ucitavanje podataka prognozirane potrosnje
            LoadXmlData(forecastedXmlFilePath, fileName, consumptionDataDict, true, selectedArea);

            // Ucitavanje podataka ostvarene potrosnje
            LoadXmlData(actualXmlFilePath, fileName, consumptionDataDict, false, selectedArea);

            // Dodavanje kombinovanih podataka u listu
            consumptionDataList.AddRange(consumptionDataDict.Values);
          
        }
        private void LoadXmlData(string xmlFilePath, string fileName, Dictionary<int, ConsumptionData> consumptionDataDict, bool isForecasted, string selectedArea)
        {
            try
            {
                XDocument xdoc = XDocument.Load(xmlFilePath);

                foreach (var stavka in xdoc.Descendants("STAVKA"))
                {
                    int hour = int.Parse(stavka.Element("SAT")?.Value);
                    if (stavka.Element("OBLAST")?.Value == selectedArea && (hour >= 0 && hour <= 24))
                    {
                        if (!consumptionDataDict.ContainsKey(hour))
                        {
                            consumptionDataDict[hour] = new ConsumptionData
                            {
                                Hour = hour,
                                GeographicalArea = stavka.Element("OBLAST")?.Value,
                            };
                        }

                        ConsumptionData data = consumptionDataDict[hour];

                        if (isForecasted)
                        {
                            data.ForecastedConsumption = double.Parse(stavka.Element("LOAD")?.Value);
                        }
                        else
                        {
                            data.ActualConsumption = double.Parse(stavka.Element("LOAD")?.Value);
                        }

                        DateTime.TryParseExact(fileName, "yyyy_MM_dd", null, DateTimeStyles.None, out DateTime parsedDate);

                        if (parsedDate != DateTime.MinValue)
                        {
                            data.Date = parsedDate;
                        }
                        else
                        {
                            Console.WriteLine($"Error: Invalid date format - {fileName}");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading XML file ({xmlFilePath}): {ex.Message}");
            }
        }

    }
    }

