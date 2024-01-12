using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using EvidencijaElEnegije.PrognoziraneelOstvarenePotrosnje;
using EvidencijaElEnegije.Klase;
using EvidencijaElEnegije.PrognoziranelOstvarenePotrosnje;
using System.Xml.Linq;
using System.IO;
using EvidencijaElEnegije.Meni;
using EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje;
using EvidencijaElEnegije.Logika;

namespace OsobaTest
{
    [TestFixture]
    public class PrognoziranePotrosnjeTests
    {
    /*    [Test]
        public void IspisPotrosnje_ShouldLoadDataAndExportToCsv()
        {

            var xmlDataLoaderMock = new Mock<IXmlDataLoader>();
            var csvExporterMock = new Mock<ICsvExporter>();

            var prognoziranePotrosnje = new PrognoziranePotrosnje(xmlDataLoaderMock.Object, csvExporterMock.Object);

            Console.SetIn(new System.IO.StringReader("2023-05-07\nVOJ\n"));


            var consumptionDataList = new List<ConsumptionData>
        {
            new ConsumptionData
            {
                Date = new DateTime(2023, 1, 1),
                GeographicalArea = "VOJ",
                Hour = 1,
                ForecastedConsumption = 50.0,
                ActualConsumption = 45.0
            },

        };

            xmlDataLoaderMock.Setup(x => x.LoadConsumptionData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ConsumptionData>>()))
                .Callback<string, string, List<ConsumptionData>>((fileName, area, list) => list.AddRange(consumptionDataList));


            string customCsvDirectory = DirektorijumPath.PathOdstupanja;
            string customCsvFileName = "odstupanje_VOJ2023_01_01.csv";
            string customCsvFilePath = Path.Combine(customCsvDirectory, customCsvFileName);


            prognoziranePotrosnje.IspisPotrosnje();



            xmlDataLoaderMock.Verify(x => x.LoadConsumptionData("2023_05_01", "VOJ", It.IsAny<List<ConsumptionData>>()), Times.Once);


            csvExporterMock.Verify(x => x.ExportToCsv(It.IsAny<List<ConsumptionData>>(), customCsvFilePath), Times.Once);


            Directory.Delete(customCsvFilePath, true);
        } */
    }
}
