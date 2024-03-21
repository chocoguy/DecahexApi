namespace Api.Models.WardMax
{
    public class CashbackRate
    {
        public int Id { get; set; }
        public int CreditCardId { get; set; }
        public int MerchantTypeId { get; set; }
        public decimal CashBackPercent { get; set; }
    }
}
