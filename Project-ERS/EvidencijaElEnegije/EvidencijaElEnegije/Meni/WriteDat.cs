using EvidencijaElEnegije.Klase;
using EvidencijaElEnegije.Konstruktori;
using EvidencijaElEnegije.SQL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace EvidencijaElEnegije.Meni
{
    class WriteDat
    {
        private static readonly generisanjeDatoteka generisanjeHendler = new generisanjeDatoteka();
        private static readonly ReadDat readHendler = new ReadDat();
        private static readonly Skriptovi s = new Skriptovi();
        private static List<GeografskoPodrucje> geografskaPodrucja = new List<GeografskoPodrucje>();
        private readonly GeoAreasMenager geospatialManager = new GeoAreasMenager();

        //Baza Podataka -> Pravljenje XML datoteke u kojoj ce biti smestene odabrane datoteke
        public void upisUBazuPodataka(List<Dan> danList)
        {
            string putanjaDoXmlDatoteke = DirektorijumPath.PathBazaPodataka + "\\BazaPodataka.xml";
            DateTime trenutnoVremeLokalno = DateTime.Now;

            using (XmlWriter writer = XmlWriter.Create(putanjaDoXmlDatoteke))
            {

                writer.WriteStartElement("BAZA_PODATAKA");
                foreach (Dan d in danList)
                {
                    string datName = (d.ostvarena ? "OSTVARENA_LOAD_" : "PROGNOZIRANI_LOAD_") + d.getDate();
                    writer.WriteStartElement(datName);

                    writer.WriteStartElement("info");
                    DodajXmlElement(writer, "LoadTime", trenutnoVremeLokalno.ToString());
                    DodajXmlElement(writer, "DatNAme", datName + ".xml");
                    DodajXmlElement(writer, "Path", DirektorijumPath.PathDatoteke);
                    writer.WriteEndElement();

                    foreach (potrosnjaPoSatu entity in d.ppsList)
                    {
                        writer.WriteStartElement("STAVKA");
                        DodajXmlElement(writer, "Sat", entity.sat.ToString());
                        DodajXmlElement(writer, "Load", entity.load.ToString());
                        DodajXmlElement(writer, "Oblast", entity.getStringOblast(entity.oblast));
                        geospatialManager.ProveraGeografskog(entity.getStringOblast(entity.oblast));
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

        //Generisanje XML datoteke pokusaja ubacivanja ne validnih datoteka u BP
        public void datotekaGreske(Dan d, string FIlePAth, string FileName)
        {
            DateTime trenutnoVremeLokalno = DateTime.Now;
            string putanjaDoXmlDatoteke = DirektorijumPath.PathGreske + "\\GRESKA_" + trenutnoVremeLokalno.ToString().Replace(' ', '_').Replace(':', '_').Replace('.', '_') + "_" + trenutnoVremeLokalno.Millisecond + ".xml";

            string putanjaDoXmlDatotekebrRedova = FIlePAth;
            XDocument xmlDoc = XDocument.Load(putanjaDoXmlDatotekebrRedova);

            // Broj elemenata u XML datoteci (pretpostavljamo da svaki red predstavlja jedan element)
            int brojRedova = xmlDoc.Descendants().Count();

            using (XmlWriter writer = XmlWriter.Create(putanjaDoXmlDatoteke))
            {
                writer.WriteStartElement("GRESKA");
                DodajXmlElement(writer, "Vreme_pokusaja_ucitavanja", trenutnoVremeLokalno.ToString());
                DodajXmlElement(writer, "Ime_fajla", FileName);
                DodajXmlElement(writer, "Lokacija_fajla", FIlePAth);
                DodajXmlElement(writer, "Broj_redova_fajla", brojRedova.ToString());
                writer.WriteEndElement();

                Console.WriteLine($"Podaci su upisani u XML datoteku: {putanjaDoXmlDatoteke}");
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

                int brSati;
                bool sati23 = false;
                if (dan == PomeranjeSata.PomeranjeUNazad_Mesec && mesec == PomeranjeSata.PomeranjeUNazad_dan)
                {
                    brSati = 25;
                }
                else if (dan == PomeranjeSata.PomeranjeUNApred_dan && mesec == PomeranjeSata.PomeranjeUNApred_mesec)
                {
                    sati23 = true;
                    brSati = 24;

                }
                else
                {
                    brSati = 24;
                }

                List<potrosnjaPoSatu> sati = new List<potrosnjaPoSatu>(25);

                for (int i = 1; i <= brSati; i++)
                {
                    if (brSati == 25)
                    {
                        Console.WriteLine("Unesite potrosnju u mW/h za vraceni sat");
                    }
                    else if (sati23 == true && brSati == 2)
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Unesite potrosnju u mW/h za " + i + "h :");
                    }
                    int load = int.Parse(Console.ReadLine());
                    sati.Add(new potrosnjaPoSatu(i, load, podrucje));
                }

                bool ostvarena;
                if (vrstaPotrosnje == 1)
                {
                    ostvarena = true;
                }
                else if (vrstaPotrosnje == 2)
                {
                    ostvarena = false;
                }
                else
                {
                    throw new ArgumentException("Pogresan unos za vrstu evidencije potrosnje");
                }

                if (readHendler.proveraIspravnosti((new Dan(dan, mesec, godina, ostvarena, sati)), null, null))
                {
                    if (!rucnoKreiranjeDatotekeXML(new Dan(dan, mesec, godina, sati), vrstaPotrosnje))
                    {
                        Console.WriteLine("Greska prilikom dodavanja");
                    }
                }
                else
                {
                    throw new ArgumentException("Neispravni podaci");
                }


            } while (!unosStr.ToUpper().Equals("X"));
        }

        //Generisanje datoteke -> F-ja se pozove nakon ispravno unestih podataka prilikom rucnog unosenja datoteke -> Unos.rucnoKreiranjeDatoteke()
        public bool rucnoKreiranjeDatotekeXML(Dan d, int ostvarena)
        {
            if(ostvarena == 1)
            {
                d.ostvarena = true;
            }else if (ostvarena == 2)
            {
                d.ostvarena = false;
            }
            else
            {
                throw new ArgumentException("Pogresan unos za vrstu evidencije potrosnje");
            }

            string putanjaDoXmlDatoteke = DirektorijumPath.PathDatoteke + (d.ostvarena ? "ostv" : "prog") + "_" + d.godina + "_" + d.mesec + "_" + (d.dan > 9 ? d.dan.ToString() : ("0" + d.dan)) + ".xml";

            using (XmlWriter writer = XmlWriter.Create(putanjaDoXmlDatoteke))
            {
                writer.WriteStartElement((d.ostvarena ? "OSTVARENA_LOAD_" : "PROGNOZIRANI_LOAD_"));


                // Pisanje početnog elementa
                writer.WriteStartElement("STAVKA");

                foreach (potrosnjaPoSatu pps in d.ppsList)
                {
                    DodajXmlElement(writer, "Sat", pps.sat.ToString());
                    DodajXmlElement(writer, "Load", pps.load.ToString());
                    DodajXmlElement(writer, "Oblast", pps.getStringOblast(pps.oblast));
                }

                writer.WriteEndElement();

                writer.WriteEndElement();

                Console.WriteLine($"Podaci su upisani u XML datoteku: {putanjaDoXmlDatoteke}");


            }


            return true;
        }

    }
}
