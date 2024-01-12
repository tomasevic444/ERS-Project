
using EvidencijaElEnegije.Konstruktori;
using EvidencijaElEnegije.Logika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace EvidencijaElEnegije.Klase
{
    public class GeoAreasMenager
    {
        private List<GeografskoPodrucje> geografskaPodrucja = new List<GeografskoPodrucje>();

        public void UcitajGeografskaPodrucjaIzXml()
        {
            XDocument xmlDoc = XDocument.Load(DirektorijumPath.PathBazaPodataka + "\\GeografskaPodrucja.xml");

            foreach (XElement podrucjeElement in xmlDoc.Descendants("PODRUCJE"))
            {
                string sifra = podrucjeElement.Element("Sifra").Value;
                geografskaPodrucja.Add(new GeografskoPodrucje(sifra));
            }
        }

        public void ProveraGeografskog(string sifra)
        {
            string path = DirektorijumPath.PathBazaPodataka + "\\GeografskaPodrucja.xml";

          UcitajGeografskaPodrucjaIzXml();

            if (!geografskaPodrucja.Any(gp => gp.Sifra == sifra))
            {
                geografskaPodrucja.Add(new GeografskoPodrucje(sifra));

                using (XmlWriter writer = XmlWriter.Create(path))
                {
                    writer.WriteStartElement("GEOGRAFSKA_PODRUCJA");

                    foreach (GeografskoPodrucje gp in geografskaPodrucja)
                    {
                        writer.WriteStartElement("PODRUCJE");
                        DodajXmlElement(writer, "Sifra", gp.Sifra);
                        DodajXmlElement(writer, "Naziv", gp.Naziv);
                        DodajXmlElement(writer, "Sirina", gp.Sirina);
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                Console.WriteLine($"Geografsko područje sa šifrom {sifra} je uspešno dodato.");
            }
        }

        private void DodajXmlElement(XmlWriter writer, string nazivElementa, string vrednost)
        {
            writer.WriteStartElement(nazivElementa);
            writer.WriteString(vrednost);
            writer.WriteEndElement();
        }
    }
}
