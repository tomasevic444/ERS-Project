using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaElEnegije
{

   public enum Oblast { VOJ = 1, BEO, KOSOVO, GRESKA }

   public  class potrosnjaPoSatu
    {
        public int sat { get; set; }
        public int load { get; set; }
        public Oblast oblast { get; set; }

        // ostv -> true | prog -> flase
        public bool ostvarena { get; set; }

        public potrosnjaPoSatu(int SAT, int LOAD, string OBLAST)
        {
            sat = SAT; load = LOAD; oblast = StringToEnum(OBLAST);
        }

        public potrosnjaPoSatu(int SAT, int LOAD, string OBLAST, bool OSTV)
        {
            sat = SAT; load = LOAD; oblast = StringToEnum(OBLAST); ostvarena = OSTV;
        }


        public Oblast StringToEnum(string oblast)
        {
            Oblast ret;
            switch (oblast)
            {
                case "VOJ":
                    ret = Oblast.VOJ;
                    break;
                case "BEO":
                    ret = Oblast.BEO;
                    break;
                case "KOSOVO":
                    ret = Oblast.KOSOVO;
                    break;

                default: ret = Oblast.GRESKA;
                        break;
            }
            return ret;
        }
        public string getStringOblast(Oblast oblast)
        {
            string ret;
            switch (oblast)
            {
                case Oblast.VOJ:
                    ret = "VOJ";
                    break;
                case Oblast.KOSOVO:
                    ret = "KOSOVO";
                    break;
                case Oblast.BEO:
                    ret = "BEOGRAD";
                    break;

                default: ret = "greska";
                    break;
            }
            return ret;
        }

    }
}
