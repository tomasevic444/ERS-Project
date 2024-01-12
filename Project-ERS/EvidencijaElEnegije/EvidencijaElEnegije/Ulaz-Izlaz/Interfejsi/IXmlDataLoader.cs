using EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaElEnegije.PrognoziraneelOstvarenePotrosnje
{
    public interface IXmlDataLoader
    {
        void LoadConsumptionData(string fileName, string selectedArea, List<ConsumptionData> consumptionDataList);
    }
}
