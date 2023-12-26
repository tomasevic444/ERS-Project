using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaElEnegije
{
    internal class Dan
    {
        public List<potrosnjaPoSatu> pps = new List<potrosnjaPoSatu>();

        // ostv -> true | prog -> flase
        public bool ostvarena { get; set; }

        public int dan { get; set; }
        public int mesec { get; set; }
        public int godina { get; set; }

        public Dan(List<potrosnjaPoSatu> PPS, bool OSTVARENA)
        {
            ostvarena = OSTVARENA;
            pps = PPS;
        }

        public Dan(List<potrosnjaPoSatu> PPS, bool OSTVARENA, int DAN, int MESEC, int GODINA)
        {
            ostvarena = OSTVARENA;
            pps = PPS;
            dan = DAN;
            mesec = MESEC;
            godina = GODINA;

        }

        public string getDate()
        {
            return godina + "_" + mesec + "_" + dan;
        }


    }
}
