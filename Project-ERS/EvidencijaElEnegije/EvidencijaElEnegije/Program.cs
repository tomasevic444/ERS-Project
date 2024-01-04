using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using EvidencijaElEnegije.Meni;
using EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje;

public class DirektorijumPath
{
    public static readonly string TrenutniDirektorijum = Directory.GetCurrentDirectory();//...Projekat\Project-ERS\EvidencijaElEnegije\EvidencijaElEnegije\bin\Debug
    public static readonly string PathProjekta = Path.GetFullPath(Path.Combine(TrenutniDirektorijum, "..", "..", ".."));//vracam se 3 foldera u nazad
    public static readonly string PathDatoteke = Path.Combine(PathProjekta, "Datoteke");
    public static readonly string PathBazaPodataka = Path.Combine(PathProjekta, "Baza Podataka");
    public static readonly string PathGreske = Path.Combine(PathProjekta, "Datoteke", "GRESKE");
    public static readonly string PathOdstupanja = Path.Combine(PathProjekta, "Datoteke", "RELATIVNA-ODSTUPANJA");
}


namespace EvidencijaElEnegije
{
    class Program
    {
        private static readonly AdministratorskiRezim aRezim = new AdministratorskiRezim();
        private static readonly PrognoziranePotrosnje ispisPotrosnje = new PrognoziranePotrosnje();

        static void Main(string[] args)
        {
            Console.WriteLine(DirektorijumPath.TrenutniDirektorijum);
            Console.WriteLine(DirektorijumPath.PathDatoteke);
            Console.WriteLine(DirektorijumPath.PathBazaPodataka);
            Console.WriteLine(DirektorijumPath.PathGreske);
            string unosStr;
            Unos unos = new Unos();
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
