using EvidencijaElEnegije.Logika;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace EvidencijaElEnegije
{
    class generisanjeDatoteka
    {

        public generisanjeDatoteka() { }
        //Generisanje ispravnih datoteka
        public void Generisanje(bool ostvarena)
        {

            for (int i = 1; i <= 31; i++)
            {

                //string putanjaDoXmlDatoteke = @"C:\Users\SRDJAN\Desktop\faks\Treca godina\ERS\Projekat\Project-ERS\Datoteke\prog_2023_05_" + (i > 9 ? i.ToString() : ("0" + i)) + ".xml";
                string putanjaDoXmlDatoteke = DirektorijumPath.PathDatoteke + "\\"+ (ostvarena ? "ostv" : "prog") + "_2023_05_" + (i > 9 ? i.ToString() : ("0" + i)) + ".xml";


                using (XmlWriter writer = XmlWriter.Create(putanjaDoXmlDatoteke))
                {
                    writer.WriteStartElement((ostvarena ? "OSTVARENA_LOAD" : "PROGNOZIRANI_LOAD"));

                    for (int j = 1; j <= 24; j++)
                    {
                        // Pisanje početnog elementa
                        writer.WriteStartElement("STAVKA");


                        Random random = new Random();
                        int load = 0;

                        switch (j)
                        {
                            case 1:
                                load = random.Next(3150, 3200);
                                break;
                            case 2:
                                load = random.Next(2830, 2890);
                                break;
                            case 3:
                                load = random.Next(2600, 2650);
                                break;
                            case 4:
                                load = random.Next(2530, 2590);
                                break;
                            case 5:
                                load = random.Next(2550, 2560);
                                break;
                            case 6:
                                load = random.Next(2610, 2660);
                                break;
                            case 7:
                                load = random.Next(3000, 3050);
                                break;
                            case 8:
                                load = random.Next(3525, 3575);
                                break;
                            case 9:
                                load = random.Next(3740, 3760);
                                break;
                            case 10:
                                load = random.Next(3790, 3800);
                                break;
                            case 11:
                                load = random.Next(3790, 3790);
                                break;
                            case 12:
                                load = random.Next(3820, 3830);
                                break;
                            case 13:
                                load = random.Next(3740, 3745);
                                break;
                            case 14:
                                load = random.Next(3600, 3700); // u datim datotekama ostv->3707 prog->3607 u odnosu na ostale prognoze odstupanje je preveliko
                                break;
                            case 15:
                                load = random.Next(3540, 3550);
                                break;
                            case 16:
                                load = random.Next(3490, 3500);
                                break;
                            case 17:
                                load = random.Next(3490, 3510);
                                break;
                            case 18:
                                load = random.Next(3420, 3470);
                                break;
                            case 19:
                                load = random.Next(3545, 3580);
                                break;
                            case 20:
                                load = random.Next(3720, 3790);
                                break;
                            case 21:
                                load = random.Next(4000, 4050);
                                break;
                            case 22:
                                load = random.Next(4050, 4200);
                                break;
                            case 23:
                                load = random.Next(3900, 3920);
                                break;
                            case 24:
                                load = random.Next(3700, 3720);
                                break;


                        }


                        // Dodavanje elemenata
                        DodajXmlElement(writer, "SAT", j.ToString());
                        DodajXmlElement(writer, "LOAD", load.ToString());
                        DodajXmlElement(writer, "OBLAST", "VOJ");

                        writer.WriteEndElement();
                    }

                    for (int j = 1; j <= 24; j++)
                    {
                        // Pisanje početnog elementa
                        writer.WriteStartElement("STAVKA");


                        Random random = new Random();
                        int load = 0;


                        switch (j)
                        {
                            case 1:
                                load = random.Next(3150, 3200);
                                break;
                            case 2:
                                load = random.Next(3250, 3300);
                                break;
                            case 3:
                                load = random.Next(2950, 3150);
                                break;
                            case 4:
                                load = random.Next(2400, 2600);
                                break;
                            case 5:
                                load = random.Next(2600, 2700);
                                break;
                            case 6:
                                load = random.Next(2740, 2780);
                                break;
                            case 7:
                                load = random.Next(3100, 3450);
                                break;
                            case 8:
                                load = random.Next(3500, 3575);
                                break;
                            case 9:
                                load = random.Next(3700, 3800);
                                break;
                            case 10:
                                load = random.Next(3700, 3800);
                                break;
                            case 11:
                                load = random.Next(3300, 3400);
                                break;
                            case 12:
                                load = random.Next(3800, 3900);
                                break;
                            case 13:
                                load = random.Next(3200, 3300);
                                break;
                            case 14:
                                load = random.Next(3750, 3800); // u datim datotekama ostv->3707 prog->3607 u odnosu na ostale prognoze odstupanje je preveliko
                                break;
                            case 15:
                                load = random.Next(3125, 3175);
                                break;
                            case 16:
                                load = random.Next(3100, 3400);
                                break;
                            case 17:
                                load = random.Next(3175, 3225);
                                break;
                            case 18:
                                load = random.Next(3410, 3415);
                                break;
                            case 19:
                                load = random.Next(3650, 3690);
                                break;
                            case 20:
                                load = random.Next(3600, 3620);
                                break;
                            case 21:
                                load = random.Next(4150, 4200);
                                break;
                            case 22:
                                load = random.Next(4100, 4400);
                                break;
                            case 23:
                                load = random.Next(3900, 4000);
                                break;
                            case 24:
                                load = random.Next(3800, 3850);
                                break;


                        }

                        // Dodavanje elemenata
                        DodajXmlElement(writer, "Sat", j.ToString());
                        DodajXmlElement(writer, "Load", load.ToString());
                        DodajXmlElement(writer, "Oblast", "BEO");

                        writer.WriteEndElement();
                    }




                    writer.WriteEndElement();

                    Console.WriteLine($"Podaci su upisani u XML datoteku: {putanjaDoXmlDatoteke}");


                }
            }



        }
        //F-ja ja lakse dodavanje Elemenata u XML datoteku
        public void DodajXmlElement(XmlWriter writer, string nazivElementa, string vrednost)
        {
            writer.WriteStartElement(nazivElementa);
            writer.WriteString(vrednost);
            writer.WriteEndElement();
        }
        //Brisanje svih datoteka sa putanje PathDatoteke
        public void ObrisiXMLDatoteke()
        {
            try
            {
                string putanjaDoDirektorijuma = DirektorijumPath.PathDatoteke;
                // Provera da li direktorijum postoji
                if (Directory.Exists(putanjaDoDirektorijuma))
                {
                    // Prikupljanje svih XML datoteka u direktorijumu
                    string[] xmlDatoteke = Directory.GetFiles(putanjaDoDirektorijuma, "*.xml");

                    // Brisanje svake XML datoteke
                    foreach (string xmlDatoteka in xmlDatoteke)
                    {
                        File.Delete(xmlDatoteka);
                        Console.WriteLine($"Obrisana je datoteka: {xmlDatoteka}");
                    }
                }
                else
                {
                    Console.WriteLine("Direktorijum ne postoji.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Došlo je do greške: {ex.Message}");
            }
        }
        //Brisanje samo jedne datoteke
        public void brisanjePojedinacnih()
        {
            string unosStr;
            do
            {

                //ispisDatoteka();

                Console.Write("Unesi redni broj datoteke za brisanje: ");
                unosStr = Console.ReadLine();



            } while (!unosStr.ToUpper().Equals("X"));
        }

        //Generisanje datoteka koje nisu validne
        public void GenerisanjeNeispravnihDatoteka(bool ostvarena)
        {

            for (int i = 1; i <= 31; i++)
            {

                //string putanjaDoXmlDatoteke = @"C:\Users\SRDJAN\Desktop\faks\Treca godina\ERS\Projekat\Project-ERS\Datoteke\prog_2023_05_" + (i > 9 ? i.ToString() : ("0" + i)) + ".xml";
                string putanjaDoXmlDatoteke = DirektorijumPath.PathDatoteke + (ostvarena ? "ostv" : "prog") + "_2026_05_" + (i > 9 ? i.ToString() : ("0" + i)) + ".xml";


                using (XmlWriter writer = XmlWriter.Create(putanjaDoXmlDatoteke))
                {
                    writer.WriteStartElement((ostvarena ? "OSTVARENA_LOAD" : "PROGNOZIRANI_LOAD"));

                    for (int j = 1; j <= 24; j++)
                    {
                        // Pisanje početnog elementa
                        writer.WriteStartElement("STAVKA");


                        Random random = new Random();
                        int load = 0;

                        switch (j)
                        {
                            case 1:
                                load = random.Next(3150, 3200);
                                break;
                            case 2:
                                load = random.Next(2830, 2890);
                                break;
                            case 3:
                                load = random.Next(2600, 2650);
                                break;
                            case 4:
                                load = random.Next(2530, 2590);
                                break;
                            case 5:
                                load = random.Next(2550, 2560);
                                break;
                            case 6:
                                load = random.Next(2610, 2660);
                                break;
                            case 7:
                                load = random.Next(3000, 3050);
                                break;
                            case 8:
                                load = random.Next(3525, 3575);
                                break;
                            case 9:
                                load = random.Next(3740, 3760);
                                break;
                            case 10:
                                load = random.Next(3790, 3800);
                                break;
                            case 11:
                                load = random.Next(3790, 3790);
                                break;
                            case 12:
                                load = random.Next(3820, 3830);
                                break;
                            case 13:
                                load = random.Next(3740, 3745);
                                break;
                            case 14:
                                load = random.Next(3600, 3700); // u datim datotekama ostv->3707 prog->3607 u odnosu na ostale prognoze odstupanje je preveliko
                                break;
                            case 15:
                                load = random.Next(3540, 3550);
                                break;
                            case 16:
                                load = random.Next(3490, 3500);
                                break;
                            case 17:
                                load = random.Next(3490, 3510);
                                break;
                            case 18:
                                load = random.Next(3420, 3470);
                                break;
                            case 19:
                                load = random.Next(3545, 3580);
                                break;
                            case 20:
                                load = random.Next(3720, 3790);
                                break;
                            case 21:
                                load = random.Next(4000, 4050);
                                break;
                            case 22:
                                load = random.Next(4050, 4200);
                                break;
                            case 23:
                                load = random.Next(3900, 3920);
                                break;
                            case 24:
                                load = random.Next(3700, 3720);
                                break;


                        }


                        // Dodavanje elemenata
                        DodajXmlElement(writer, "Sat", j.ToString());
                        DodajXmlElement(writer, "Load", load.ToString());
                        DodajXmlElement(writer, "Oblast", "VOJ");

                        writer.WriteEndElement();
                    }

                    for (int j = 1; j <= 24; j++)
                    {
                        // Pisanje početnog elementa
                        writer.WriteStartElement("STAVKA");


                        Random random = new Random();
                        int load = 0;


                        switch (j)
                        {
                            case 1:
                                load = random.Next(3150, 3200);
                                break;
                            case 2:
                                load = random.Next(3250, 3300);
                                break;
                            case 3:
                                load = random.Next(2950, 3150);
                                break;
                            case 4:
                                load = random.Next(2400, 2600);
                                break;
                            case 5:
                                load = random.Next(2600, 2700);
                                break;
                            case 6:
                                load = random.Next(2740, 2780);
                                break;
                            case 7:
                                load = random.Next(3100, 3450);
                                break;
                            case 8:
                                load = random.Next(3500, 3575);
                                break;
                            case 9:
                                load = random.Next(3700, 3800);
                                break;
                            case 10:
                                load = random.Next(3700, 3800);
                                break;
                            case 11:
                                load = random.Next(3300, 3400);
                                break;
                            case 12:
                                load = random.Next(3800, 3900);
                                break;
                            case 13:
                                load = random.Next(3200, 3300);
                                break;
                            case 14:
                                load = random.Next(3750, 3800); // u datim datotekama ostv->3707 prog->3607 u odnosu na ostale prognoze odstupanje je preveliko
                                break;
                            case 15:
                                load = random.Next(3125, 3175);
                                break;
                            case 16:
                                load = random.Next(3100, 3400);
                                break;
                            case 17:
                                load = random.Next(3175, 3225);
                                break;
                            case 18:
                                load = random.Next(3410, 3415);
                                break;
                            case 19:
                                load = random.Next(3650, 3690);
                                break;
                            case 20:
                                load = random.Next(3600, 3620);
                                break;
                            case 21:
                                load = random.Next(4150, 4200);
                                break;
                            case 22:
                                load = random.Next(4100, 4400);
                                break;
                            case 23:
                                load = random.Next(3900, 4000);
                                break;
                            case 24:
                                load = random.Next(3800, 3850);
                                break;


                        }

                        // Dodavanje elemenata
                        DodajXmlElement(writer, "Sat", j.ToString());
                        DodajXmlElement(writer, "Load", load.ToString());
                        DodajXmlElement(writer, "Oblast", "BEO");

                        writer.WriteEndElement();
                    }




                    writer.WriteEndElement();

                    Console.WriteLine($"Podaci su upisani u XML datoteku: {putanjaDoXmlDatoteke}");


                }
            }



        }


    }


}




