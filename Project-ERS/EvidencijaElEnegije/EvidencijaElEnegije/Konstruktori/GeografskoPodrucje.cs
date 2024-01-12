using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaElEnegije.Konstruktori
{
    public class GeografskoPodrucje
    {
        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public string Sirina { get; set; }

        public GeografskoPodrucje(string sifra)
        {
            Sifra = sifra;
            Naziv = sifra;

            Sirina = GenerateRandomSirina(); // Set a random value for Sirina during object creation
        }

        private string GenerateRandomSirina()
        {
            Random random = new Random();
            int randomSirina = random.Next(50, 101); // Generates a random number between 50 and 100
            return randomSirina.ToString();
        }
    }
}
