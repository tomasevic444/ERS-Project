using EvidencijaElEnegije.Konstruktori;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace OsobaTest
{
    [TestFixture]
    public class GeografskoPodrucjeTests
    {
        [Test]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            string sifra = "TestSifra";

            // Act
            var geografskoPodrucje = new GeografskoPodrucje(sifra);

            // Assert
            Assert.AreEqual(sifra, geografskoPodrucje.Sifra);
            Assert.AreEqual(sifra, geografskoPodrucje.Naziv);

            // Since GenerateRandomSirina method generates a random value, we can only check if it's not null
            Assert.IsNotNull(geografskoPodrucje.Sirina);
        }
    }
}
