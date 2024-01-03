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

namespace EvidencijaElEnegije
{
    class Unos
    {
        private static readonly Skriptovi s = new Skriptovi();
        private static readonly generisanjeDatoteka generisanjeDatoteka = new generisanjeDatoteka();
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
                        odabirDatoteka(datNum);
                        break;
                    case "3":
                        rucnoKreiranjeDatoteke();
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

        //Odabir datoteka preko rednog broja koji im je dodeljen
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

        //Baza Podataka -> Pravljenje XML datoteke u kojoj ce biti smestene odabrane datoteke
        public void upisUBazuPodataka(List<potrosnjaPoSatu> ppsList, List<Dan> danList)
        {

            string putanjaDoXmlDatoteke = DirektorijumPath.PathBazaPodataka + "BazaPodataka.xml";


            using (XmlWriter writer = XmlWriter.Create(putanjaDoXmlDatoteke))
            {

                writer.WriteStartElement("BAZA_PODATAKA");
                foreach (Dan d in danList)
                {
                    writer.WriteStartElement((d.ostvarena ? "OSTVARENA_LOAD" : "PROGNOZIRANI_LOAD") + d.getDate());

                    foreach (potrosnjaPoSatu entity in d.ppsList)
                    {
                        writer.WriteStartElement("STAVKA");
                        DodajXmlElement(writer, "Sat", entity.sat.ToString());
                        DodajXmlElement(writer, "Load", entity.load.ToString());
                        DodajXmlElement(writer, "Oblast", entity.getStringOblast(entity.oblast));
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();


                Console.WriteLine($"Podaci su upisani u XML datoteku: {putanjaDoXmlDatoteke}");


            }
        }
        //F-ja za lakse dodavanje elemenata u XML datoteci
        public void DodajXmlElement(XmlWriter writer, string nazivElementa, string vrednost)
        {
            writer.WriteStartElement(nazivElementa);
            writer.WriteString(vrednost);
            writer.WriteEndElement();
        }
        //Upis u sql bazu podataka
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
        //Pretrazivanje koje su se liste izabrale po unosu rednih brojeva i pozivanje funkcije dodavanjeUListu
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
        //Provera da li je validna datoteka ukoli ne generise se pokusaj i belezi u posebnom folderu GRESKE
        //Pravljenje liste tipa Dan (i liste tipa potrosnjaPoSatu <- polje u Dan klasi)
        public void dodavanjeUListu(string Path, List<potrosnjaPoSatu> pList, List<Dan> danList)
        {

            XDocument xmlDoc = XDocument.Load(Path);

            string[] PathSplit = Path.Split('\\');
            string Datoteka = PathSplit.Last();

            // Razdvoji niz koristeći donju crtu kao separator
            string[] parts = Datoteka.Split('_');
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

            if (proveraIspravnosti(new Dan(pList, ostv, int.Parse(parts[3].Substring(0, 2)), int.Parse(parts[2]), int.Parse(parts[1]))))
            {
                danList.Add(new Dan(pList, ostv, int.Parse(parts[3].Substring(0, 2)), int.Parse(parts[2]), int.Parse(parts[1])));
            }
            else
            {
                generisanjeDatoteka.datotekaGreske(new Dan(pList, ostv, int.Parse(parts[3].Substring(0, 2)), int.Parse(parts[2]), int.Parse(parts[1])), Path, Datoteka);
            }



        }
        //Ispis sadrzaja jedne Datoteke
        public void ispisSadrzajaDatoteke(string Path)
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
        //Rucan unos datoteke
        public void rucnoKreiranjeDatoteke()
        {
            string unosStr = "";
            do
            {
                Console.WriteLine("Izaberi vrstu evidencije potrosnje: ");
                Console.WriteLine("1. Evidencija OSTVARENE potrosnje.");
                Console.WriteLine("2. Evidencija PROGNOZIRANE potrosnje.");
                int vrstaPotrosnje = int.Parse(Console.ReadLine());

                Console.WriteLine("Godina merenja");
                int godina = int.Parse(Console.ReadLine());

                Console.WriteLine("Mesec merenja");
                int mesec = int.Parse(Console.ReadLine());

                Console.WriteLine("Dan merenja");
                int dan = int.Parse(Console.ReadLine());

                Console.WriteLine("Unesi Geografsko podrucje: ");
                string podrucje = Console.ReadLine().ToUpper();

                List<potrosnjaPoSatu> sati = new List<potrosnjaPoSatu>(25);
                for (int i = 1; i <= 24; i++)
                {
                    Console.WriteLine("Unesite potrosnju u mW/h za " + i + "h :");
                    int load = int.Parse(Console.ReadLine());
                    sati.Add(new potrosnjaPoSatu(i, load, podrucje));
                }

                if (!generisanjeDatoteka.rucnoKreiranjeDatoteke(new Dan(dan, mesec, godina, sati), vrstaPotrosnje))
                {
                    Console.WriteLine("Greska prilikom dodavanja");
                }



            } while (!unosStr.ToUpper().Equals("X"));
        }
        //Provera validalnosti datoteke
        public bool proveraIspravnosti(Dan d)
        {
            DateTime trenutnoVremeLokalno = DateTime.Now;
            if (d.mesec > 12 || d.mesec < 1 || d.dan < 1 || d.dan > DateTime.DaysInMonth(d.godina, d.mesec))
                return false;

            if (d.ostvarena)
                if (trenutnoVremeLokalno.Year < d.godina)
                    return false;
                else
                {
                    if (trenutnoVremeLokalno.Year == d.godina && trenutnoVremeLokalno.Month < d.mesec)
                        return false;
                    else
                    {
                        if (trenutnoVremeLokalno.Year == d.godina && trenutnoVremeLokalno.Month == d.mesec && trenutnoVremeLokalno.Day < d.dan)
                            return false;
                    }
                }

            foreach (potrosnjaPoSatu pps in d.ppsList)
            {
                if (pps.sat > 25 || pps.sat <= 0)
                    return false;
                if (pps.oblast == Oblast.GRESKA)
                    return false;
            }

            return true;
        }


    }
}
