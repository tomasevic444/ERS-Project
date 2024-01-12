using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje;
using NUnit.Framework;
namespace OsobaTest
{
    [TestFixture]
    public class CsvExporterTests
    {
        [Test]
        public void ExportToCsv_ShouldGenerateCorrectCsvContent()
        {

            var csvExporter = new CsvExporter();
            var consumptionDataList = new List<ConsumptionData>
        {
            new ConsumptionData
            {
                Hour = 1,
                ForecastedConsumption = 50.0,
                ActualConsumption = 45.0
            },
            new ConsumptionData
            {
                Hour = 2,
                ForecastedConsumption = 60.0,
                ActualConsumption = 55.0
            }

        };


            string filePath = "test.csv";
            csvExporter.ExportToCsv(consumptionDataList, filePath);


            string[] lines = System.IO.File.ReadAllLines(filePath);

            Assert.AreEqual("Sat,Prognozirana potrosnja,Ostvarena postrosnja,Relativno odstupanje", lines[0]);

            Assert.AreEqual("1,50,45,11.11%", lines[1]);
            Assert.AreEqual("2,60,55,9.09%", lines[2]);

            // Clean up: Delete the test file
            System.IO.File.Delete(filePath);
        }
    }
}