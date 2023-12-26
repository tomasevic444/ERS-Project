using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaElEnegije
{
    class potrosnjaPoSatu
    {
        public int sat { get; set; }
        public int load { get; set; }
        public string oblast { get; set; }

        // ostv -> true | prog -> flase
        public bool ostvarena { get; set; }

        public potrosnjaPoSatu(int SAT, int LOAD, string OBLAST)
        {
            sat = SAT; load = LOAD; oblast = OBLAST;
        }

        public potrosnjaPoSatu(int SAT, int LOAD, string OBLAST, bool OSTV)
        {
            sat = SAT; load = LOAD; oblast = OBLAST; ostvarena = OSTV;
        }

    }
}
