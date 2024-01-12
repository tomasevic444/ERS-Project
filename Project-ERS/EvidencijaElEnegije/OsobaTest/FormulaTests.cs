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

namespace OsobaTest
{
    public class FormuleTests
    {


        [Test]
        public void CalculateRelativeDeviation_WithEqualActualAndForecasted_ShouldReturnZero()
        {

            ConsumptionData testData = new ConsumptionData
            {
                ActualConsumption = 20,
                ForecastedConsumption = 20
            };


            double result = Formule.CalculateRelativeDeviation(testData);


            Assert.AreEqual(0, result);
        }



        [Test]
        public void CalculateRelativeDeviation_WithLargeValues_ShouldReturnCorrectValue()
        {

            ConsumptionData testData = new ConsumptionData
            {
                ActualConsumption = 1000000,
                ForecastedConsumption = 900000
            };


            double result = Formule.CalculateRelativeDeviation(testData);


            Assert.AreEqual(0.1, result, 0.0001);
        }
    }
}
