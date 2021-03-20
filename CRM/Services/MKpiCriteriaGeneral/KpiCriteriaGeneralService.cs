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

namespace CRM.Services.MKpiCriteriaGeneral
{
    public interface IKpiCriteriaGeneralService :  IServiceScoped
    {
        Task<int> Count(KpiCriteriaGeneralFilter KpiCriteriaGeneralFilter);
        Task<List<KpiCriteriaGeneral>> List(KpiCriteriaGeneralFilter KpiCriteriaGeneralFilter);
        Task<KpiCriteriaGeneral> Get(long Id);
    }

    public class KpiCriteriaGeneralService : BaseService, IKpiCriteriaGeneralService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public KpiCriteriaGeneralService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(KpiCriteriaGeneralFilter KpiCriteriaGeneralFilter)
        {
            try
            {
                int result = await UOW.KpiCriteriaGeneralRepository.Count(KpiCriteriaGeneralFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(KpiCriteriaGeneralService));
            }
            return 0;
        }

        public async Task<List<KpiCriteriaGeneral>> List(KpiCriteriaGeneralFilter KpiCriteriaGeneralFilter)
        {
            try
            {
                List<KpiCriteriaGeneral> KpiCriteriaGenerals = await UOW.KpiCriteriaGeneralRepository.List(KpiCriteriaGeneralFilter);
                return KpiCriteriaGenerals;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(KpiCriteriaGeneralService));
            }
            return null;
        }
        
        public async Task<KpiCriteriaGeneral> Get(long Id)
        {
            KpiCriteriaGeneral KpiCriteriaGeneral = await UOW.KpiCriteriaGeneralRepository.Get(Id);
            if (KpiCriteriaGeneral == null)
                return null;
            return KpiCriteriaGeneral;
        }
    }
}
