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

namespace CRM.Services.MInfulenceLevelMarket
{
    public interface IInfulenceLevelMarketService :  IServiceScoped
    {
        Task<int> Count(InfulenceLevelMarketFilter InfulenceLevelMarketFilter);
        Task<List<InfulenceLevelMarket>> List(InfulenceLevelMarketFilter InfulenceLevelMarketFilter);
        Task<InfulenceLevelMarket> Get(long Id);
    }

    public class InfulenceLevelMarketService : BaseService, IInfulenceLevelMarketService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public InfulenceLevelMarketService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(InfulenceLevelMarketFilter InfulenceLevelMarketFilter)
        {
            try
            {
                int result = await UOW.InfulenceLevelMarketRepository.Count(InfulenceLevelMarketFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(InfulenceLevelMarketService));
            }
            return 0;
        }

        public async Task<List<InfulenceLevelMarket>> List(InfulenceLevelMarketFilter InfulenceLevelMarketFilter)
        {
            try
            {
                List<InfulenceLevelMarket> InfulenceLevelMarkets = await UOW.InfulenceLevelMarketRepository.List(InfulenceLevelMarketFilter);
                return InfulenceLevelMarkets;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(InfulenceLevelMarketService));
            }
            return null;
        }
        
        public async Task<InfulenceLevelMarket> Get(long Id)
        {
            InfulenceLevelMarket InfulenceLevelMarket = await UOW.InfulenceLevelMarketRepository.Get(Id);
            if (InfulenceLevelMarket == null)
                return null;
            return InfulenceLevelMarket;
        }
    }
}
