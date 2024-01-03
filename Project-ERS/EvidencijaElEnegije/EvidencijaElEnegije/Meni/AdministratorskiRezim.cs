using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaElEnegije.Meni
{
    class AdministratorskiRezim
    {

        public void administratorskiRezim()
        {

            string sifra;
            int brPokusaja = 5;
            do
            {
                if (brPokusaja < 5)
                {
                    Console.WriteLine("POGRESNA LOZINKA\tPreostalo pokusaja: " + brPokusaja);
                }
                Console.WriteLine("Sifra:");
                sifra = Console.ReadLine();
                brPokusaja--;
            } while (!sifra.ToUpper().Equals("123") & brPokusaja != 0);
            if (brPokusaja == 0)
                return;

            Console.WriteLine("\t======================================================================================================\t");
            Console.WriteLine("\t=                                       ADMINISTRATORSKI REZIM                                       =\t");
            Console.WriteLine("\t======================================================================================================\t");

            string unosStr;
            do
            {
                Console.WriteLine("1. Ponovo generisanje datoteka ostvarene potrosnje");
                Console.WriteLine("2. Ponovo generisanje datoteka prognozirane potrosnje");
                Console.WriteLine("3. Brisanje pojedinacnih datoteka");
                Console.WriteLine("4. Brisanje svih datoteka");
                Console.WriteLine("5. Generisanje neispravnih datoteka ostvarene potrosnje");
                Console.WriteLine("6. Generisanje neispravnih datoteka prognozirane potrosnje");
                Console.WriteLine("X -> Izlaz");
                unosStr = Console.ReadLine();


                generisanjeDatoteka gd = new generisanjeDatoteka();
                switch (unosStr)
                {
                    case "1":
                        gd.Generisanje(true);
                        break;
                    case "2":
                        new generisanjeDatoteka().Generisanje(false);
                        break;
                    case "3":
                        break;
                    case "4":
                        gd.ObrisiXMLDatoteke();
                        break;
                    case "5":
                        gd.GenerisanjeNeispravnihDatoteka(true);
                        break;
                    case "6":
                        gd.GenerisanjeNeispravnihDatoteka(false);
                        break;

                }

            } while (!unosStr.ToUpper().Equals("X"));

        }

    }
}
