using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using EvidencijaElEnegije.SQL;
using EvidencijaElEnegije.Meni;
public class DirektorijumPath
{
    public static readonly string TrenutniDirektorijum = Directory.GetCurrentDirectory();//...Projekat\Project-ERS\EvidencijaElEnegije\EvidencijaElEnegije\bin\Debug
    public static readonly string PathProjekta = Path.GetFullPath(Path.Combine(TrenutniDirektorijum, "..", "..", ".."));//vracam se 3 foldera u nazad
    public static readonly string PathDatoteke = Path.Combine(PathProjekta, "Datoteke");
    public static readonly string PathBazaPodataka = Path.Combine(PathProjekta, "Baza Podataka");
    public static readonly string PathGreske = Path.Combine(PathProjekta, "Datoteke", "GRESKE");
}

public class PomeranjeSata
{
    public static readonly int PomeranjeUNApred_mesec = 3;//mart
    public static readonly int PomeranjeUNApred_dan = 26;
    public static readonly int PomeranjeUNazad_Mesec = 10;//oktobar
    public static readonly int PomeranjeUNazad_dan = 29;//oktobar
}

namespace EvidencijaElEnegije
{
    class Unos
    {
        private static readonly ReadDat readHendler = new ReadDat();
        private static readonly WriteDat writeHendler = new WriteDat();
        public void meni()
        {

            int datNum = 0;
            string unosStr;
            do
            {
                Console.WriteLine("\n1. Pretrazite datoteke u folderu");
                Console.WriteLine("2. Upisite datoteku u XML i SQL bazu podataka");
                Console.WriteLine("3. Rucno kreiranje datoteke");
                Console.WriteLine("X -> Izlaz");

                unosStr = Console.ReadLine();

                switch (unosStr)
                {
                    case "1":
                        datNum = ispisDatoteka();
                        break;
                    case "2":
                        readHendler.odabirDatoteka(datNum);
                        break;
                    case "3":
                        writeHendler.rucnoKreiranjeDatoteke();
                        break;

                    //kako se ne bi izvrsavao default prilikom izlaza - unosa x|X
                    case "x":
                        break;
                    case "X":
                        break;

                    default:
                        Console.WriteLine("Pogresan unos!");
                        break;
                }
            } while (!unosStr.ToUpper().Equals("X"));



        }

        //Ispis svih datoteka sa PathDatoteke putanje i postavljanje rednog broja ispred svake
        public int ispisDatoteka()
        {
            // Putanja do direktorijuma
            string putanjaDoDirektorijuma = DirektorijumPath.PathDatoteke;

            // Provera da li direktorijum postoji
            if (Directory.Exists(putanjaDoDirektorijuma))
            {
                // Dobijanje svih fajlova iz direktorijuma
                string[] fajlovi = Directory.GetFiles(putanjaDoDirektorijuma);

                // Ispisivanje naziva fajlova
                Console.WriteLine("Izaberi redni broj fajla koji zelis da uneses");

                int i = 1;
                foreach (string fajl in fajlovi)
                {
                    Console.WriteLine((i > 9 ? i.ToString() : ("0" + i)) + ". " + Path.GetFileName(fajl));
                    i++;
                }
                return i;
            }
            else
            {
                Console.WriteLine("Direktorijum ne postoji.");
                return 1;
            }


        }
    }
}
