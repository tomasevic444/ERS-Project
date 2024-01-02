using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml;

public class DirektorijumPath
{
    public const string PathDatoteke = @"C:\Users\SRDJAN\Desktop\faks\Treca godina\ERS\Projekat\Project-ERS\Datoteke\";
    public const string PathBazaPodataka = @"C:\Users\SRDJAN\Desktop\faks\Treca godina\ERS\Projekat\Project-ERS\Baza Podataka\";
}


namespace EvidencijaElEnegije
{
    class Program
    {
        static void Main(string[] args)
        {
            string unosStr;
            Unos unos = new Unos();
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
                        administratorskiRezim();
                        break;
                }



            } while (!unosStr.ToUpper().Equals("X"));


        }


        static void administratorskiRezim()
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
                Console.WriteLine("X -> Izlaz");
                unosStr = Console.ReadLine();


                generisanjeDatoteka gd = new generisanjeDatoteka();
                switch (unosStr)
                {
                    case "1": gd.Generisanje(true);
                        break;
                    case "2": new generisanjeDatoteka().Generisanje(false);
                        break;
                    case "3":
                        break;
                    case "4": gd.ObrisiXMLDatoteke();
                        break;

                }

            } while (!unosStr.ToUpper().Equals("X"));

        }



        static void unos()
        {

            int datNum = 0;
            string unosStr;
            do
            {
                Console.WriteLine("\n1. Pretrazite datoteke u folderu");
                Console.WriteLine("X -> Izlaz");

                unosStr = Console.ReadLine();

                switch (unosStr)
                {
                    case "1": datNum = ispisDatoteka(); 
                        break;
                    case "x":
                        break;
                    case "X":
                        break;

                    default: Console.WriteLine("Pogresan unos!"); 
                        break;
                }



            } while (!unosStr.ToUpper().Equals("X"));

            int i = 0;
            List<int> datList = new List<int>();
            do
            {
                Console.WriteLine("\nIzaberi readni broj datoteke koju zelite da importujete u bazu podataka");
                Console.WriteLine("X -> Izlaz");

                unosStr = Console.ReadLine();

                if (!unosStr.ToUpper().Equals("X"))
                {

                    if (int.Parse(unosStr) < 0 || int.Parse(unosStr) > datNum)
                    {
                        Console.WriteLine("Datoteka sa rednim brojem " + unosStr + " ne postoji");
                    }
                    else
                    {
                        if (i == 0)
                        {
                            datList.Add(int.Parse(unosStr));
                        }
                        else
                        {
                            foreach (int listNum in datList)
                            {
                                if (int.Parse(unosStr) == listNum)
                                {
                                    Console.WriteLine("Datoteku pod rednim brojem " + " ste vec uneli!");
                                }
                                else
                                {
                                    Console.WriteLine("Datoteku pod rednim brojem " + " je uspesno uneta");
                                }
                            }
                        }


                    }
                }

            } while (!unosStr.ToUpper().Equals("X"));

            List<potrosnjaPoSatu> ppsList = new List<potrosnjaPoSatu>();
            List<Dan> danList = new List<Dan>();

            pravljenjeListe(datList, ppsList, danList);
            upisUBazuPodataka(ppsList, danList);




        }




        static int ispisDatoteka()
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

        void ispisSadrzajaDatoteke(string Path)
        {

            List<potrosnjaPoSatu> pList = new List<potrosnjaPoSatu>();
            string filePath = Path;

            // Učitavanje XML fajla koristeći XDocument
            XDocument xmlDoc = XDocument.Load(filePath);

            // Prolazak kroz svaki STAVKA element u XML-u

            foreach (XElement stavkaElement in xmlDoc.Descendants("STAVKA"))
            {
                int sat = int.Parse(stavkaElement.Element("SAT").Value);
                int load = int.Parse(stavkaElement.Element("LOAD").Value);
                string oblast = stavkaElement.Element("OBLAST").Value;

                pList.Add(new potrosnjaPoSatu(sat, load, oblast));

                // Prikazivanje učitanih podataka za svaki STAVKA element
                Console.WriteLine($"SAT: {sat}, LOAD: {load}, OBLAST: {oblast}");
            }

        }

        static void pravljenjeListe(List<int> datList, List<potrosnjaPoSatu> ppsList, List<Dan> danList)
        {

            string putanjaDoDirektorijuma = DirektorijumPath.PathDatoteke;


            // Provera da li direktorijum postoji
            if (Directory.Exists(putanjaDoDirektorijuma))
            {
                // Dobijanje svih fajlova iz direktorijuma
                string[] fajlovi = Directory.GetFiles(putanjaDoDirektorijuma);

                int i = 1;
                foreach (string fajl in fajlovi)
                {
                    foreach (int j in datList)
                    {
                        if (j == i)
                        {
                            Console.WriteLine(fajl);
                            dodavanjeUListu(fajl, ppsList, danList);
                        }

                    }
                    i++;
                }
            }

        }

        static void dodavanjeUListu(string Path, List<potrosnjaPoSatu> pList, List<Dan> danList)
        {

            XDocument xmlDoc = XDocument.Load(Path);

            string[] parts1 = Path.Split('\\');
            string result1 = parts1.Last();


            // Razdvoji niz koristeći donju crtu kao separator
            string[] parts = result1.Split('_');
            string result;
            // Ako postoje dijelovi nakon razdvajanja

            // Prvi dio će biti ono što je ispred prvog _
            result = parts[0];

            bool ostv = (result == "ostv") ? true : false;

            foreach (XElement stavkaElement in xmlDoc.Descendants("STAVKA"))
            {
                int sat = int.Parse(stavkaElement.Element("Sat").Value);
                int load = int.Parse(stavkaElement.Element("Load").Value);
                string oblast = stavkaElement.Element("Oblast").Value;

                pList.Add(new potrosnjaPoSatu(sat, load, oblast));
            }

            danList.Add(new Dan(pList, ostv, int.Parse(parts[3].Substring(0, 2)), int.Parse(parts[2]), int.Parse(parts[1])));

        }

        static void upisUBazuPodataka(List<potrosnjaPoSatu> ppsList, List<Dan> danList)
        {

            string putanjaDoXmlDatoteke = DirektorijumPath.PathBazaPodataka + "BazaPodataka.xml";


            using (XmlWriter writer = XmlWriter.Create(putanjaDoXmlDatoteke))
            {

                writer.WriteStartElement("BAZA_PODATAKA");
                foreach (Dan d in danList)
                {
                    writer.WriteStartElement("PROGNOZIRANI_LOAD_" + d.getDate());

                    foreach (potrosnjaPoSatu entity in d.ppsList)
                    {
                        writer.WriteStartElement("STAVKA");
                        DodajXmlElement(writer, "Sat", entity.sat.ToString());
                        DodajXmlElement(writer, "Load", entity.load.ToString());
                        DodajXmlElement(writer, "Oblast", entity.oblast);
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();


                Console.WriteLine($"Podaci su upisani u XML datoteku: {putanjaDoXmlDatoteke}");


            }
        }


        // Metoda za dodavanje XML elementa sa vrednošću
        static void DodajXmlElement(XmlWriter writer, string nazivElementa, string vrednost)
        {
            writer.WriteStartElement(nazivElementa);
            writer.WriteString(vrednost);
            writer.WriteEndElement();
        }
    }
}
