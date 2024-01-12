using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EvidencijaElEnegije.Ulaz_Izlaz.Interfejsi
{
   public  interface IXDocumentProvider
    {
        IEnumerable<XElement> Descendants(XName name);
    }
}
