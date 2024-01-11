using EvidencijaElEnegije.SQL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace EvidencijaElEnegije.Meni
{
    class ReadDat
    {
        private static readonly generisanjeDatoteka generisanjeDatoteka = new generisanjeDatoteka();
        private static readonly WriteDat writeHendler = new WriteDat();

        //Odabir datoteka preko rednog broja koji im je dodeljen
        public void odabirDatoteka(int datNum)
        {

            //lista izabranih datoteka po rednim brojevima
            List<int> datList = new List<int>();
            string unosStr;
            //List<potrosnjaPoSatu> ppsList = new List<potrosnjaPoSatu>();//lista informacija po satu: sat, load, oblast
            List<Dan> danList = new List<Dan>();//lista sa svim ucitanim fajlovima

            if (datNum < 1)
            {
                Console.WriteLine("Prvo morate ispisati sve datoteke kako bi se za svaku generisao redni broj");
            }
            else
                do
                {
                    Console.WriteLine("\nIzaberi redni broj datoteke koju zelite da importujete u bazu podataka");
                    Console.WriteLine("X -> Izlaz");

                    unosStr = Console.ReadLine();

                    if (!unosStr.ToUpper().Equals("X"))
                    {
                        if (int.TryParse(unosStr, out int selectedNumber) && selectedNumber >= 1 && selectedNumber <= datNum)
                        {
                            if (!datList.Contains(selectedNumber))
                            {
                                pravljenjeListe(new List<int> { selectedNumber }, danList);
                                writeHendler.upisUBazuPodataka(danList);
                                writeHendler.upisUBazuPodatakaSQL(danList);
                                datList.Add(selectedNumber);
                                Console.WriteLine("Datoteku pod rednim brojem " + selectedNumber + " je uspesno uneta");
                            }
                            else
                            {
                                Console.WriteLine("Datoteku pod rednim brojem " + selectedNumber + " ste vec uneli!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Pogresan unos!");
                        }
                    }

                } while (!unosStr.ToUpper().Equals("X"));

        }

        //Pretrazivanje koje su se liste izabrale po unosu rednih brojeva i pozivanje funkcije dodavanjeUListuXML
        public void pravljenjeListe(List<int> datList, List<Dan> danList)
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
                            Console.WriteLine("Unosenje: " + fajl);
                            if (Path.GetExtension(fajl).Trim('.') == "xml")
                            {
                                dodavanjeUListuXML(fajl, danList);
                            }
                            else if (Path.GetExtension(fajl).Trim('.') == "txt")
                            {
                                //TO DO
                            }
                        }

                    }
                    i++;
                }
            }

        }

        //Provera da li je validna datoteka ukoli ne generise se pokusaj i belezi u posebnom folderu GRESKE
        //Pravljenje liste tipa Dan (i liste tipa potrosnjaPoSatu <- polje u Dan klasi)
        public void dodavanjeUListuXML(string Path, List<Dan> danList)
        {
            List<potrosnjaPoSatu> pList = new List<potrosnjaPoSatu>();

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

            if (proveraIspravnosti(new Dan(pList, ostv, int.Parse(parts[3].Substring(0, 2)), int.Parse(parts[2]), int.Parse(parts[1])), Path, Datoteka))
            {
                danList.Add(new Dan(pList, ostv, int.Parse(parts[3].Substring(0, 2)), int.Parse(parts[2]), int.Parse(parts[1])));
            }
            else
            {
                writeHendler.datotekaGreske(new Dan(pList, ostv, int.Parse(parts[3].Substring(0, 2)), int.Parse(parts[2]), int.Parse(parts[1])), Path, Datoteka);

            }



        }

        //Provera validalnosti datoteke
        public bool proveraIspravnosti(Dan d, string Path, string Datoteka)
        {
            DateTime trenutnoVremeLokalno = DateTime.Now;
            if (d.mesec > 12 || d.mesec < 1)
            {
                writeHendler.datotekaGreske(d, Path, Datoteka);
                throw new ArgumentException("Mesec mora da ima vrednost veću od 0 i manju od 13");
            }

            if (d.dan < 1 || d.dan > DateTime.DaysInMonth(d.godina, d.mesec))
            {
                writeHendler.datotekaGreske(d, Path, Datoteka);
                throw new ArgumentException("Dan mora imati vrednost veću od 0 a manju od " + DateTime.DaysInMonth(d.godina, d.mesec) + 1);
            }


            if (d.ostvarena)
                if (trenutnoVremeLokalno.Year < d.godina)
                {
                    writeHendler.datotekaGreske(d, Path, Datoteka);
                    throw new ArgumentException("Ostvarena potrošnja ne može da bude za datum koji još nije bio - loša godina");
                }
                else
                {
                    if (trenutnoVremeLokalno.Year == d.godina && trenutnoVremeLokalno.Month < d.mesec)
                    {
                        writeHendler.datotekaGreske(d, Path, Datoteka);
                        throw new ArgumentException("Ostvarena potrošnja ne može da bude za datum koji još nije bio - loš mesec");
                    }
                    else
                    {
                        if (trenutnoVremeLokalno.Year == d.godina && trenutnoVremeLokalno.Month == d.mesec && trenutnoVremeLokalno.Day < d.dan)
                        {
                            writeHendler.datotekaGreske(d, Path, Datoteka);
                            throw new ArgumentException("Ostvarena potrošnja ne može da bude za datum koji još nije bio - loš dan");
                        }
                    }
                }

            //Kada pomeramo sat unapred dan ima 23 sata
            if (d.dan == PomeranjeSata.PomeranjeUNApred_dan && d.mesec == PomeranjeSata.PomeranjeUNApred_mesec)
            {
                foreach (potrosnjaPoSatu pps in d.ppsList)
                {
                    if (pps.sat > 23 || pps.sat < 1)
                    {
                        writeHendler.datotekaGreske(d, Path, Datoteka);
                        throw new ArgumentException("Sat mora biti u granici [1,23]");
                    }
                    if (pps.oblast == Oblast.GRESKA)
                    {
                        writeHendler.datotekaGreske(d, Path, Datoteka);
                        throw new ArgumentException("Nepostojeca oblast");
                    }
                }
            }

            //Kada pomeramo sat unazad dan ima 25 sata
            if (d.dan == PomeranjeSata.PomeranjeUNazad_Mesec && d.mesec == PomeranjeSata.PomeranjeUNazad_dan)
            {
                foreach (potrosnjaPoSatu pps in d.ppsList)
                {
                    if (pps.sat > 25 || pps.sat < 1)
                    {
                        writeHendler.datotekaGreske(d, Path, Datoteka);
                        throw new ArgumentException("Sat mora biti u granici [1,25]");
                    }
                    if (pps.oblast == Oblast.GRESKA)
                    {
                        writeHendler.datotekaGreske(d, Path, Datoteka);
                        throw new ArgumentException("Nepostojeca oblast");
                    }
                }
            }

            foreach (potrosnjaPoSatu pps in d.ppsList)
            {
                if (pps.sat > 24 || pps.sat < 1)
                {
                    writeHendler.datotekaGreske(d, Path, Datoteka);
                    throw new ArgumentException("Sat mora biti u granici [1,24]");
                }
                if (pps.oblast == Oblast.GRESKA)
                {
                    writeHendler.datotekaGreske(d, Path, Datoteka);
                    throw new ArgumentException("Nepostojeca oblast");
                }
            }

            return true;
        }

    }
}
