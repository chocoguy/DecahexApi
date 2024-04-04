namespace Api.Models.WardMax
{
    public class CashbackRate
    {
        public int Id { get; set; }
        public CreditCard CreditCard { get; set; }
        public MerchantType MerchantType { get; set; }
        public decimal CashBackPercent { get; set; }
    }
}
