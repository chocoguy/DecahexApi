namespace Api.Models.WardMax
{
        public class CreditCardOffer
        {
            public int Id { get; set; }
            public int CreditCardId { get; set; }
            public string OfferTitle { get; set; }
            public string OfferDescription { get; set; }
        }
}
