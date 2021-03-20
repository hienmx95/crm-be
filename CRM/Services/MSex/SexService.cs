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

namespace CRM.Services.MSex
{
    public interface ISexService :  IServiceScoped
    {
        Task<int> Count(SexFilter SexFilter);
        Task<List<Sex>> List(SexFilter SexFilter);
        Task<Sex> Get(long Id);
    }

    public class SexService : BaseService, ISexService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public SexService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(SexFilter SexFilter)
        {
            try
            {
                int result = await UOW.SexRepository.Count(SexFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(SexService));
            }
            return 0;
        }

        public async Task<List<Sex>> List(SexFilter SexFilter)
        {
            try
            {
                List<Sex> Sexs = await UOW.SexRepository.List(SexFilter);
                return Sexs;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(SexService));
            }
            return null;
        }
        
        public async Task<Sex> Get(long Id)
        {
            Sex Sex = await UOW.SexRepository.Get(Id);
            if (Sex == null)
                return null;
            return Sex;
        }
    }
}
