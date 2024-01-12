using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaElEnegije.Logika
{
    public class DirektorijumPath
    {
        public static readonly string TrenutniDirektorijum = Directory.GetCurrentDirectory();//...Projekat\Project-ERS\EvidencijaElEnegije\EvidencijaElEnegije\bin\Debug
        public static readonly string PathProjekta = Path.GetFullPath(Path.Combine(TrenutniDirektorijum, "..", "..", ".."));//vracam se 3 foldera u nazad
        public static readonly string PathDatoteke = Path.Combine(PathProjekta, "Datoteke");
        public static readonly string PathBazaPodataka = Path.Combine(PathProjekta, "Baza Podataka");
        public static readonly string PathGreske = Path.Combine(PathProjekta, "Datoteke", "GRESKE");
        public static readonly string PathOdstupanja = Path.Combine(PathProjekta, "Datoteke", "RELATIVNA-ODSTUPANJA");
    }
}
