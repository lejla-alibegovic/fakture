namespace faktura.Data.Dtos.Responses
{
    public class StavkeFakture
    {
        public int Id { get; set; }

        public int FakturaId { get; set; }

        public string Opis { get; set; }

        public int Kolicina { get; set; }

        public double JedininaCijena { get; set; }

        public double UkupnaCijenaBezPDV { get; set; }
    }
}