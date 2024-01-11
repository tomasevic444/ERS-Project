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


namespace EvidencijaElEnegije
{
    class Program
    {
        private static readonly AdministratorskiRezim aRezim = new AdministratorskiRezim();
        static void Main(string[] args)
        {
            string unosStr;
            Unos unos = new Unos();
            Console.SetWindowSize(170, 30);

            do
            {
                Console.WriteLine("1. Unos");
                Console.WriteLine("10. Administratorski rezim");
                Console.WriteLine("X -> Izlaz");
                unosStr = Console.ReadLine();

                switch (unosStr)
                {
                    case "1":
                        unos.meni();
                        break;
                    case "10":
                        aRezim.administratorskiRezim();
                        break;
                }

            } while (!unosStr.ToUpper().Equals("X"));
        }

    }
}
