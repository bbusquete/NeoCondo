namespace NeoCondo.GerarBoleto.Entities
{
    public class Cobranca
    {
        public string Customer { get; set; }
        public string BillingType { get; set; }
        public Decimal Value { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public string ExternalReference { get; set; }
        public bool PostalService { get; set; }

        public CobrancaFine Fine { get; set; }
        public CobrancaInterest Interest { get; set; }
        
    }

    public class CobrancaFine
    {
        public float value { get; set; }
        //public string type { get; set; }
    }

    public class CobrancaInterest
    {
        public float value { get; set; }
        //public string type { get; set; }
    }

}
