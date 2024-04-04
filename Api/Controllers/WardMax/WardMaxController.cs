using Api.Data.WardMax;
using Api.Models.WardMax;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Api.Controllers.WardMax
{
    [ApiController]
    [Route("[controller]")]
    public class WardMaxController : ControllerBase
    {
        private readonly ILogger<WardMaxController> _logger;
        private DataPortal _dataPortal { get; set; }


        public WardMaxController(ILogger<WardMaxController> logger) 
        { 
            _logger = logger;
            _dataPortal = new DataPortal();
        }

        [HttpGet("Testo")]
        public object Testo()
        {
            DataPortal dp = new();

            dp.sqlTest();


            return Ok();
        }

        [HttpGet("testof")]
        public async Task<CreditCard> GetCreditCardTestAsync()
        {
            DataPortal dp = new();

            CreditCard cc = await dp.GetCreditCardById(1);

            return cc;
        }

        [HttpGet("cashbackrates")]
        public async Task<List<CashbackRate>> GetCashBackRates()
        {
            DataPortal dp = new();

            return await dp.GetCreditCardCashBackRateByCardId(1);
        }

        [HttpGet("cc/{ccId}")]
        public async Task<object> GetCreditCardById(int ccId)
        {
            try
            {
                CreditCard creditCard = await _dataPortal.GetCreditCardById(ccId);
                if(creditCard != null)
                {
                    return creditCard;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("cc/cashback/{ccid}")]
        public async Task<object> GetCashBackRatesForCard(int ccid)
        {
            try
            {
                List<CashbackRate> rates = await _dataPortal.GetCreditCardCashBackRateByCardId(ccid);
                if(rates.Count > 0)
                {
                    return rates;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("cc/points/{ccid}")]
        public async Task<object> GetPointsRatesForCard(int ccid)
        {
            try
            {
                List<PointsRate> rates = await _dataPortal.GetCreditCardPointsRateByCardId(ccid);
                if (rates.Count > 0)
                {
                    return rates;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("cc/pointconversion/{ccid}")]
        public async Task<object> GetPointConversionRateForCard(int ccid)
        {
            try
            {
                PointConversionRate rate = await _dataPortal.GetCreditCardPointRateByCardId(ccid);
                if(rate != null)
                {
                    return rate;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("cc/offers/{ccid}")]
        public async Task<object> GetCardOffers(int ccid)
        {
            try
            {
                List<CreditCardOffer> offers = await _dataPortal.GetCreditCardOffersByCardId(ccid);
                if (offers.Count > 0)
                {
                    return offers;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("cc/all")]
        public async Task<object> GetAllCreditCards()
        {
            try
            {
                List<CreditCard> creditCards = await _dataPortal.GetAllCreditCards();
                if(creditCards.Count > 0)
                {
                    return creditCards;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("cc/search")]
        public async Task<object> SearchCreditCards()
        {
            try
            {
                string query = await GetTextFromBody(Request);
                List<CreditCard> results = await _dataPortal.SearchCreditCardsByName(query);
                if(results.Count > 0) 
                {
                    return results;
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("merchant/{merchantId}")]
        public async Task<object> GetMerchantById(int merchantId)
        {
            try
            {
                Merchant merchant = await _dataPortal.GetMerchantById(merchantId);
                if (merchant != null)
                {
                    return merchant;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("merchant/all")]
        public async Task<object> GetAllMerchants()
        {
            try
            {
                List<Merchant> merchants = await _dataPortal.GetAllMerchants();
                if (merchants.Count > 0)
                {
                    return merchants;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("merchant/search")]
        public async Task<object> SearchMerchants()
        {
            try
            {
                string query = await GetTextFromBody(Request);
                List<Merchant> results = await _dataPortal.SearchMerchantsByName(query);
                if (results.Count > 0)
                {
                    return results;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("merchanttype/{merchantTypeId}")]
        public async Task<object> GetMerchantTypeById(int merchantTypeId)
        {
            try
            {
                MerchantType merchantType = await _dataPortal.GetMerchantTypeById(merchantTypeId);
                if (merchantType != null)
                {
                    return merchantType;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("merchanttype/all")]
        public async Task<object> GetAllMerchantTypes()
        {
            try
            {
                List<MerchantType> merchantTypes = await _dataPortal.GetAllMerchantTypes();
                if (merchantTypes.Count > 0)
                {
                    return merchantTypes;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("merchanttype/search")]
        public async Task<object> SearchMerchantTypes()
        {
            try
            {
                string query = await GetTextFromBody(Request);
                List<MerchantType> results = await _dataPortal.SearchMerchantTypesByName(query);
                if (results.Count > 0)
                {
                    return results;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }



        private static async Task<string> GetTextFromBody(HttpRequest req)
        {
            if (!req.Body.CanSeek) { req.EnableBuffering(); }
            req.Body.Position = 0;

            var reader = new StreamReader(req.Body, Encoding.UTF8);
            var reqString = await reader.ReadToEndAsync().ConfigureAwait(false);

            return reqString;
        }


    }
}
