using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoCondo.GerarPlanilha.Entities
{
    internal class WaterRead
    {
        int Id { get; set; }
        int UnitId { get; set; }
        bool Reading { get; set; }
        DateTime Date { get; set; }
    }
}
