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

namespace EvidencijaElEnegije
{
    class Unos
    {
        private static readonly Skriptovi s = new Skriptovi();
        public void meni()
        {

            int datNum = 0;
            string unosStr;
            do
            {
                Console.WriteLine("\n1. Pretrazite datoteke u folderu");
                Console.WriteLine("2. Upisite datoteku u XML i SQL bazu podataka");
                Console.WriteLine("X -> Izlaz");

                unosStr = Console.ReadLine();

                switch (unosStr)
                {
                    case "1":
                        datNum = ispisDatoteka();
                        break;
                    case "2":
                        odabirDatoteka(datNum);
                        break;

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

        public void odabirDatoteka(int datNum)
        {

            int i = 0;
            List<int> datList = new List<int>();
            string unosStr;

            if (datNum < 1)
            {
                Console.WriteLine("Prvo morate ispisati sve datoteke kako bi se za svaku generisao redni broj");
            }
            else
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
                    List<potrosnjaPoSatu> ppsList = new List<potrosnjaPoSatu>();
                    List<Dan> danList = new List<Dan>();

                    pravljenjeListe(datList, ppsList, danList);
                    upisUBazuPodataka(ppsList, danList);
                    upisUBazuPodatakaSQL(danList);

                } while (!unosStr.ToUpper().Equals("X"));
        }

        public void upisUBazuPodataka(List<potrosnjaPoSatu> ppsList, List<Dan> danList)
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

        public void DodajXmlElement(XmlWriter writer, string nazivElementa, string vrednost)
        {
            writer.WriteStartElement(nazivElementa);
            writer.WriteString(vrednost);
            writer.WriteEndElement();
        }

        public void upisUBazuPodatakaSQL(List<Dan> danList)
        {

            foreach (Dan d in danList)
            {

                foreach (potrosnjaPoSatu entity in d.ppsList)
                {
                    try
                    {
                        s.dodajElement(entity, d);
                    }
                    catch (DbException db)
                    {
                        Console.WriteLine(db.Message);
                    }
                }


            }
        }


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

            public void pravljenjeListe(List<int> datList, List<potrosnjaPoSatu> ppsList, List<Dan> danList)
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

            public void dodavanjeUListu(string Path, List<potrosnjaPoSatu> pList, List<Dan> danList)
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

        }
    }
