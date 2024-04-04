using Api.Models.WardMax;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Api.Data.WardMax
{
    public class DataPortal
    {

        string connString = "";

        public void sqlTest()
        {
            using (var connection = new SqlConnection(connString))
            {
                string sampleSql = "SELECT * FROM CreditCard";

                // Use the Query method to execute the query and return a list of objects    
                //var books = connection.Query<Book>(sql, new { authorName = "John Smith" }).ToList();
                var creditCards = connection.QueryAsync<CreditCard>(sampleSql);

                foreach (var creditCard in creditCards.Result)
                {
                    Console.WriteLine("ID: " + creditCard.Id);
                    Console.WriteLine("Card Name: " + creditCard.Name);
                }


            }
        }

        public async Task<CreditCard> GetCreditCardById(int id)
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = @$"SELECT c.Id, c.Name, c.FTFee, c.Picture, c.PictureRetina, c.Color, n.Id, n.Name, n.Picture, n.PictureRetina, r.Id, r.Name FROM CreditCard c 
                                 INNER JOIN NetworkType n ON c.NetworkId = n.Id
                                 INNER JOIN RewardType r ON c.RewardTypeId = r.Id
                                 WHERE c.Id = {id}";

                var queryCard = await connection.QueryAsync<CreditCard, NetworkType, RewardType, CreditCard>(query, (cc, nt, rt) =>
                {
                   cc.NetworkType = nt;
                   cc.RewardType = rt;
                   return cc;
                });

                return queryCard.FirstOrDefault();

            }
        }

        public async Task<List<CreditCard>> SearchCreditCardsByName(string nameQuery)
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = $@"SELECT c.Id, c.Name, c.FTFee, c.Picture, c.PictureRetina, c.Color, n.Id, n.Name, n.Picture, n.PictureRetina, r.Id, r.Name FROM CreditCard c 
                                 INNER JOIN NetworkType n ON c.NetworkId = n.Id
                                 INNER JOIN RewardType r ON c.RewardTypeId = r.Id
                                 WHERE c.Name LIKE '%{nameQuery}%'";

                var queryCards = await connection.QueryAsync<CreditCard, NetworkType, RewardType, CreditCard>(query, (cc, nt, rt) =>
                {
                    cc.NetworkType = nt;
                    cc.RewardType = rt;
                    return cc;
                });

                return queryCards.ToList();

            }
        }

        public async Task<List<CreditCard>> GetAllCreditCards()
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = $@"SELECT c.Id, c.Name, c.FTFee, c.Picture, c.PictureRetina, c.Color, n.Id, n.Name, n.Picture, n.PictureRetina, r.Id, r.Name FROM CreditCard c 
                                 INNER JOIN NetworkType n ON c.NetworkId = n.Id
                                 INNER JOIN RewardType r ON c.RewardTypeId = r.Id";

                var queryCards = await connection.QueryAsync<CreditCard, NetworkType, RewardType, CreditCard>(query, (cc, nt, rt) =>
                {
                    cc.NetworkType = nt;
                    cc.RewardType = rt;
                    return cc;
                });

                return queryCards.ToList();

            }
        }

        public async Task<Merchant> GetMerchantById(int id)
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = $@"SELECT m.Id, m.Name, mt.Id, mt.Name FROM Merchant m 
                                  INNER JOIN MerchantType mt ON m.MerchantTypeId = mt.Id
                                  WHERE m.Id = {id}";

                var queryMerchant = await connection.QueryAsync<Merchant, MerchantType, Merchant>(query, (me, mt) =>
                {
                    me.MerchantType = mt;
                    return me;
                });

                return queryMerchant.FirstOrDefault();

            }
        }

        public async Task<List<Merchant>> SearchMerchantsByName(string nameQuery)
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = $@"SELECT m.Id, m.Name, mt.Id, mt.Name FROM Merchant m 
                                  INNER JOIN MerchantType mt ON m.MerchantTypeId = mt.Id
                                  WHERE m.Name LIKE '%{nameQuery}%'";

                var queryMerchant = await connection.QueryAsync<Merchant, MerchantType, Merchant>(query, (me, mt) =>
                {
                    me.MerchantType = mt;
                    return me;
                });

                return queryMerchant.ToList();

            }
        }

        public async Task<List<Merchant>> GetAllMerchants()
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = $@"SELECT m.Id, m.Name, mt.Id, mt.Name FROM Merchant m 
                                  INNER JOIN MerchantType mt ON m.MerchantTypeId = mt.Id";

                var queryMerchant = await connection.QueryAsync<Merchant, MerchantType, Merchant>(query, (me, mt) =>
                {
                    me.MerchantType = mt;
                    return me;
                });

                return queryMerchant.ToList();

            }
        }

        public async Task<MerchantType> GetMerchantTypeById(int id)
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = $@"SELECT * FROM MerchantType WHERE Id = {id}";
                MerchantType selectedMerchantType = await connection.QuerySingleAsync<MerchantType>(query);
                return selectedMerchantType;
            }
        }

        public async Task<List<MerchantType>> SearchMerchantTypesByName(string nameQuery)
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = $@"SELECT * FROM MerchantType WHERE Name LIKE '%{nameQuery}%'";

                var queryMerchantType = await connection.QueryAsync<MerchantType>(query);

                return queryMerchantType.ToList();

            }
        }

        public async Task<List<MerchantType>> GetAllMerchantTypes()
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = $@"SELECT * FROM MerchantType";

                var queryMerchantType = await connection.QueryAsync<MerchantType>(query);

                return queryMerchantType.ToList();

            }
        }


        public async Task<NetworkType> GetNetworkTypeById(int id)
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = $"SELECT * FROM NetworkType WHERE Id = {id}";
                NetworkType selectedNetworkType = await connection.QuerySingleAsync<NetworkType>(query);
                return selectedNetworkType;
            }
        }

        public async Task<List<CashbackRate>> GetCreditCardCashBackRateByCardId(int ccid)
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = $@"SELECT cr.Id, cr.CashBackPercent, mt.Id, mt.Name FROM CashbackRate cr
                                INNER JOIN MerchantType mt ON cr.MerchantTypeId = mt.Id
                                WHERE cr.CreditCardId = {ccid}";
                var queryCashback = await connection.QueryAsync<CashbackRate, MerchantType, CashbackRate>(query, (cr, mt) =>
                {
                    cr.MerchantType = mt;
                    return cr;
                });

                return queryCashback.ToList();
            }
        }

        public async Task<List<PointsRate>> GetCreditCardPointsRateByCardId(int ccid)
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = $@"SELECT pr.Id, pr.Pointsx, mt.Id, mt.Name FROM PointsRate pr
                                INNER JOIN MerchantType mt ON pr.MerchantTypeId = mt.Id
                                WHERE pr.CreditCardId = {ccid}";
                var queryCashback = await connection.QueryAsync<PointsRate, MerchantType, PointsRate>(query, (pr, mt) =>
                {
                    pr.MerchantType = mt;
                    return pr;
                });

                return queryCashback.ToList();
            }
        }

        public async Task<PointConversionRate> GetCreditCardPointRateByCardId(int ccid)
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = $"SELECT * FROM PointConversionRate WHERE CreditCardId = {ccid}";
                PointConversionRate selectedCreditCardPointRate = await connection.QuerySingleAsync<PointConversionRate>(query);
                return selectedCreditCardPointRate;
            }
        }

        public async Task<List<CreditCardOffer>> GetCreditCardOffersByCardId(int ccid)
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = $"SELECT * FROM CreditCardOffer WHERE CreditCardId = {ccid}";
                var offers = await connection.QueryAsync<CreditCardOffer>(query);
                return offers.ToList();
            }
        }





    }
}
