using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoCondo.GerarPlanilha.Entities
{
    internal class Unit
    {
        int Id { get; set; }
        int CondoId { get; set; }
        String UnitKey { get; set; }
        String BlockOrStreet { get; set; }
    }
}
