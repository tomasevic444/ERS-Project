using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaElEnegije
{
    public class Dan
    {
        public List<potrosnjaPoSatu> ppsList = new List<potrosnjaPoSatu>();

        // ostv -> true | prog -> flase
        public bool ostvarena { get; set; }

        public int dan { get; set; }
        public int mesec { get; set; }
        public int godina { get; set; }

        public Dan(List<potrosnjaPoSatu> PPS, bool OSTVARENA)
        {
            ostvarena = OSTVARENA;
            ppsList = PPS;
        }


        public Dan(int DAN, int MESEC, int GODINA, List<potrosnjaPoSatu> PPSLIST)
        {
            dan = DAN;
            mesec = MESEC;
            godina = GODINA;
            ppsList = PPSLIST;
        }

        public Dan(int DAN, int MESEC, int GODINA, bool OSTVARENA, List<potrosnjaPoSatu> PPSLIST)
        {
            dan = DAN;
            mesec = MESEC;
            godina = GODINA;
            ppsList = PPSLIST;
            ostvarena = OSTVARENA;
        }

        public Dan(List<potrosnjaPoSatu> PPS, bool OSTVARENA, int DAN, int MESEC, int GODINA)
        {
            ostvarena = OSTVARENA;
            ppsList = PPS;
            dan = DAN;
            mesec = MESEC;
            godina = GODINA;

        }

        public string getDate()
        {
            return godina + "_" + mesec + "_" + dan;
        }
        public Dan(int DAN, int MESEC, int GODINA, bool OSTVARENA)
        {
            dan = DAN;
            mesec = MESEC;
            godina = GODINA;

            ostvarena = OSTVARENA;
        }

    }

}
