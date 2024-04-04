namespace Api.Models.WardMax
{
    public class PointsRate
    {
        public int Id { get; set; }
        public CreditCard CreditCard { get; set; }
        public MerchantType MerchantType { get; set; }
        public int Pointsx { get; set; }
    }
}
