using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using EvidencijaElEnegije.PrognoziraneelOstvarenePotrosnje;
using EvidencijaElEnegije.Klase;
using EvidencijaElEnegije.PrognoziranelOstvarenePotrosnje;
using System.Xml.Linq;

namespace EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje.Tests
{
    public class FormuleTests
    {


        [Test]
        public void CalculateRelativeDeviation_WithEqualActualAndForecasted_ShouldReturnZero()
        {
            // Arrange
            ConsumptionData testData = new ConsumptionData
            {
                ActualConsumption = 20,
                ForecastedConsumption = 20
            };

            // Act
            double result = Formule.CalculateRelativeDeviation(testData);

            // Assert
            Assert.AreEqual(0, result);
        }



        [Test]
        public void CalculateRelativeDeviation_WithLargeValues_ShouldReturnCorrectValue()
        {
            // Arrange
            ConsumptionData testData = new ConsumptionData
            {
                ActualConsumption = 1000000,
                ForecastedConsumption = 900000
            };

            // Act
            double result = Formule.CalculateRelativeDeviation(testData);

            // Assert
            Assert.AreEqual(0.1, result, 0.0001);
        }
    }
    [TestFixture]
    public class CsvExporterTests
    {
        [Test]
        public void ExportToCsv_ShouldGenerateCorrectCsvContent()
        {
            // Arrange
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
            // Add more ConsumptionData objects as needed for your test cases
        };

            // Act
            string filePath = "test.csv"; // Provide a valid file path or use a temporary file
            csvExporter.ExportToCsv(consumptionDataList, filePath);

            // Assert
            string[] lines = System.IO.File.ReadAllLines(filePath);

            // Assuming that the CSV file has a header
            Assert.AreEqual("Sat,Prognozirana potrosnja,Ostvarena postrosnja,Relativno odstupanje", lines[0]);

            // Assuming that there are two rows of data in the CSV file
            Assert.AreEqual("1,50,45,11.11%", lines[1]);
            Assert.AreEqual("2,60,55,9.09%", lines[2]);

            // Clean up: Delete the test file
            System.IO.File.Delete(filePath);
        }
    }
}