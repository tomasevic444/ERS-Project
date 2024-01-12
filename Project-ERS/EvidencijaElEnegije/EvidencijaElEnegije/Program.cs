using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using EvidencijaElEnegije.Meni;
using System.Web;
using EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje;
using EvidencijaElEnegije.PrognoziraneelOstvarenePotrosnje;

namespace EvidencijaElEnegije
{
    class Program
    {
        private static readonly AdministratorskiRezim aRezim = new AdministratorskiRezim();
        private static readonly PrognoziranePotrosnje ispisPotrosnje = new PrognoziranePotrosnje(new XmlDataLoader(), new CsvExporter());
        static void Main(string[] args)
        {
            string unosStr;
            Unos unos = new Unos();
            Console.SetWindowSize(170, 30);

            do
            {
                Console.WriteLine("1. Unos");
                Console.WriteLine("2. Ispis prognozirane i ostvarene potrošnje");
                Console.WriteLine("10. Administratorski rezim");
                Console.WriteLine("X -> Izlaz");
                unosStr = Console.ReadLine();

                switch (unosStr)
                {
                    case "1":
                        unos.meni();
                        break;
                    case "2":
                        // Call the IspisPotrosnje method when option 2 is selected
                        ispisPotrosnje.IspisPotrosnje();
                        break;
                    case "10":
                        aRezim.administratorskiRezim();
                        break;
                }

            } while (!unosStr.ToUpper().Equals("X"));
        }

    }
}
