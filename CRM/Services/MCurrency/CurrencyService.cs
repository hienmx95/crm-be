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

namespace CRM.Services.MCurrency
{
    public interface ICurrencyService :  IServiceScoped
    {
        Task<int> Count(CurrencyFilter CurrencyFilter);
        Task<List<Currency>> List(CurrencyFilter CurrencyFilter);
        Task<Currency> Get(long Id);
    }

    public class CurrencyService : BaseService, ICurrencyService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public CurrencyService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(CurrencyFilter CurrencyFilter)
        {
            try
            {
                int result = await UOW.CurrencyRepository.Count(CurrencyFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CurrencyService));
            }
            return 0;
        }

        public async Task<List<Currency>> List(CurrencyFilter CurrencyFilter)
        {
            try
            {
                List<Currency> Currencys = await UOW.CurrencyRepository.List(CurrencyFilter);
                return Currencys;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CurrencyService));
            }
            return null;
        }
        
        public async Task<Currency> Get(long Id)
        {
            Currency Currency = await UOW.CurrencyRepository.Get(Id);
            if (Currency == null)
                return null;
            return Currency;
        }
    }
}
