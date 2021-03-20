using CRM.Common;
using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using CRM.Repositories;
using CRM.Entities;
using CRM.Enums;

namespace CRM.Services.MMarketPrice
{
    public interface IMarketPriceService :  IServiceScoped
    {
        Task<int> Count(MarketPriceFilter MarketPriceFilter);
        Task<List<MarketPrice>> List(MarketPriceFilter MarketPriceFilter);
        Task<MarketPrice> Get(long Id);
    }

    public class MarketPriceService : BaseService, IMarketPriceService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public MarketPriceService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(MarketPriceFilter MarketPriceFilter)
        {
            try
            {
                int result = await UOW.MarketPriceRepository.Count(MarketPriceFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(MarketPriceService));
            }
            return 0;
        }

        public async Task<List<MarketPrice>> List(MarketPriceFilter MarketPriceFilter)
        {
            try
            {
                List<MarketPrice> MarketPrices = await UOW.MarketPriceRepository.List(MarketPriceFilter);
                return MarketPrices;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(MarketPriceService));
            }
            return null;
        }
        
        public async Task<MarketPrice> Get(long Id)
        {
            MarketPrice MarketPrice = await UOW.MarketPriceRepository.Get(Id);
            if (MarketPrice == null)
                return null;
            return MarketPrice;
        }
    }
}
