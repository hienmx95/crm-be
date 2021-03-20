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

namespace CRM.Services.MKpiYear
{
    public interface IKpiYearService :  IServiceScoped
    {
        Task<int> Count(KpiYearFilter KpiYearFilter);
        Task<List<KpiYear>> List(KpiYearFilter KpiYearFilter);
        Task<KpiYear> Get(long Id);
    }

    public class KpiYearService : BaseService, IKpiYearService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public KpiYearService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(KpiYearFilter KpiYearFilter)
        {
            try
            {
                int result = await UOW.KpiYearRepository.Count(KpiYearFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(KpiYearService));
            }
            return 0;
        }

        public async Task<List<KpiYear>> List(KpiYearFilter KpiYearFilter)
        {
            try
            {
                List<KpiYear> KpiYears = await UOW.KpiYearRepository.List(KpiYearFilter);
                return KpiYears;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(KpiYearService));
            }
            return null;
        }
        
        public async Task<KpiYear> Get(long Id)
        {
            KpiYear KpiYear = await UOW.KpiYearRepository.Get(Id);
            if (KpiYear == null)
                return null;
            return KpiYear;
        }
    }
}
