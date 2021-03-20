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

namespace CRM.Services.MKpiPeriod
{
    public interface IKpiPeriodService :  IServiceScoped
    {
        Task<int> Count(KpiPeriodFilter KpiPeriodFilter);
        Task<List<KpiPeriod>> List(KpiPeriodFilter KpiPeriodFilter);
        Task<KpiPeriod> Get(long Id);
    }

    public class KpiPeriodService : BaseService, IKpiPeriodService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public KpiPeriodService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(KpiPeriodFilter KpiPeriodFilter)
        {
            try
            {
                int result = await UOW.KpiPeriodRepository.Count(KpiPeriodFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(KpiPeriodService));
            }
            return 0;
        }

        public async Task<List<KpiPeriod>> List(KpiPeriodFilter KpiPeriodFilter)
        {
            try
            {
                List<KpiPeriod> KpiPeriods = await UOW.KpiPeriodRepository.List(KpiPeriodFilter);
                return KpiPeriods;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(KpiPeriodService));
            }
            return null;
        }
        
        public async Task<KpiPeriod> Get(long Id)
        {
            KpiPeriod KpiPeriod = await UOW.KpiPeriodRepository.Get(Id);
            if (KpiPeriod == null)
                return null;
            return KpiPeriod;
        }
    }
}
