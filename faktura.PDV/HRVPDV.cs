using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faktura.PDV
{
    [Export(typeof(IPDV))]
    [ExportMetadata("PDV", "25 %")]
    public class HRVPDV : IPDV
    {
        public double Izracunaj(double value)
        {
            return value * 0.25;
        }
    }
}
