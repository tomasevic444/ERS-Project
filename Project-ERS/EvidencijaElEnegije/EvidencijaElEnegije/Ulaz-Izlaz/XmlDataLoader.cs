using EvidencijaElEnegije.Logika;
using EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EvidencijaElEnegije.PrognoziraneelOstvarenePotrosnje
{
    public class XmlDataLoader : IXmlDataLoader
    {
        public  void LoadConsumptionData(string fileName, string selectedArea, List<ConsumptionData> consumptionDataList)
        {
            string putanjaDoDirektorijuma = DirektorijumPath.PathDatoteke;

            string forecastedXmlFilePath = Path.Combine(putanjaDoDirektorijuma, $"prog_{fileName}.xml");
            string actualXmlFilePath = Path.Combine(putanjaDoDirektorijuma, $"ostv_{fileName}.xml");

            Dictionary<int, ConsumptionData> consumptionDataDict = new Dictionary<int, ConsumptionData>();

            LoadXmlData(forecastedXmlFilePath, fileName, consumptionDataDict, true, selectedArea);
            LoadXmlData(actualXmlFilePath, fileName, consumptionDataDict, false, selectedArea);

            consumptionDataList.AddRange(consumptionDataDict.Values);
        }

        public  void LoadXmlData(string xmlFilePath, string fileName, Dictionary<int, ConsumptionData> consumptionDataDict, bool isForecasted, string selectedArea)
        {
            try
            {
                XDocument xdoc = XDocument.Load(xmlFilePath);

                foreach (var stavka in xdoc.Descendants("STAVKA"))
                {
                    int hour = int.Parse(stavka.Element("Sat")?.Value);
                    if (stavka.Element("Oblast")?.Value == selectedArea && (hour >= 0 && hour <= 24))
                    {
                        if (!consumptionDataDict.ContainsKey(hour))
                        {
                            consumptionDataDict[hour] = new ConsumptionData
                            {
                                Hour = hour,
                                GeographicalArea = stavka.Element("Oblast")?.Value,
                            };
                        }

                        ConsumptionData data = consumptionDataDict[hour];

                        if (isForecasted)
                        {
                            data.ForecastedConsumption = double.Parse(stavka.Element("Load")?.Value);
                        }
                        else
                        {
                            data.ActualConsumption = double.Parse(stavka.Element("Load")?.Value);
                        }

                        DateTime.TryParseExact(fileName, "yyyy_MM_dd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate);

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
