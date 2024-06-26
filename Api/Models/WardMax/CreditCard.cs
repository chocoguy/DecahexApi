﻿namespace Api.Models.WardMax
{
    public class CreditCard
    {
        public int Id { get; set; }
        public RewardType? RewardType { get; set; }
        public NetworkType NetworkType { get; set; }
        public string Name { get; set; }
        public decimal FTFee { get; set; }
        public string Picture { get; set; }
        public string PictureRetina { get; set; }
        public string Color { get; set; }
    }
}
