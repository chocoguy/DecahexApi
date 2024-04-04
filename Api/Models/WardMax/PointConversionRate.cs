namespace Api.Models.WardMax
{
    public class PointConversionRate
    {
        public int Id { get; set; }
        public CreditCard CreditCard { get; set; }
        public int PointsPerDollar { get; set; }
    }
}
