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

namespace CRM.Services.MKpiCriteriaItem
{
    public interface IKpiCriteriaItemService :  IServiceScoped
    {
        Task<int> Count(KpiCriteriaItemFilter KpiCriteriaItemFilter);
        Task<List<KpiCriteriaItem>> List(KpiCriteriaItemFilter KpiCriteriaItemFilter);
        Task<KpiCriteriaItem> Get(long Id);
    }

    public class KpiCriteriaItemService : BaseService, IKpiCriteriaItemService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public KpiCriteriaItemService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(KpiCriteriaItemFilter KpiCriteriaItemFilter)
        {
            try
            {
                int result = await UOW.KpiCriteriaItemRepository.Count(KpiCriteriaItemFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(KpiCriteriaItemService));
            }
            return 0;
        }

        public async Task<List<KpiCriteriaItem>> List(KpiCriteriaItemFilter KpiCriteriaItemFilter)
        {
            try
            {
                List<KpiCriteriaItem> KpiCriteriaItems = await UOW.KpiCriteriaItemRepository.List(KpiCriteriaItemFilter);
                return KpiCriteriaItems;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(KpiCriteriaItemService));
            }
            return null;
        }
        
        public async Task<KpiCriteriaItem> Get(long Id)
        {
            KpiCriteriaItem KpiCriteriaItem = await UOW.KpiCriteriaItemRepository.Get(Id);
            if (KpiCriteriaItem == null)
                return null;
            return KpiCriteriaItem;
        }
    }
}
