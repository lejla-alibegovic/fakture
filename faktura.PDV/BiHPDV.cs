using System.ComponentModel.Composition;

namespace faktura.PDV
{
    [Export(typeof(IPDV))]
    [ExportMetadata("PDV", "17 %")]
    public class BiHPDV : IPDV
    {
        public double Izracunaj(double value)
        {
            return value * 0.17;
        }
    }
}
