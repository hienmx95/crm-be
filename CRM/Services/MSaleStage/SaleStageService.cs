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

namespace CRM.Services.MSaleStage
{
    public interface ISaleStageService :  IServiceScoped
    {
        Task<int> Count(SaleStageFilter SaleStageFilter);
        Task<List<SaleStage>> List(SaleStageFilter SaleStageFilter);
        Task<SaleStage> Get(long Id);
    }

    public class SaleStageService : BaseService, ISaleStageService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public SaleStageService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(SaleStageFilter SaleStageFilter)
        {
            try
            {
                int result = await UOW.SaleStageRepository.Count(SaleStageFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(SaleStageService));
            }
            return 0;
        }

        public async Task<List<SaleStage>> List(SaleStageFilter SaleStageFilter)
        {
            try
            {
                List<SaleStage> SaleStages = await UOW.SaleStageRepository.List(SaleStageFilter);
                return SaleStages;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(SaleStageService));
            }
            return null;
        }
        
        public async Task<SaleStage> Get(long Id)
        {
            SaleStage SaleStage = await UOW.SaleStageRepository.Get(Id);
            if (SaleStage == null)
                return null;
            return SaleStage;
        }
    }
}
