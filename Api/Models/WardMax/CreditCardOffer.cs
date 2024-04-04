namespace Api.Models.WardMax
{
        public class CreditCardOffer
        {
            public int Id { get; set; }
            public CreditCard CreditCard { get; set; }
            public string OfferTitle { get; set; }
            public string OfferDescription { get; set; }
        }
}
