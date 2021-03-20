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

namespace CRM.Services.MSLATimeUnit
{
    public interface ISLATimeUnitService :  IServiceScoped
    {
        Task<int> Count(SLATimeUnitFilter SLATimeUnitFilter);
        Task<List<SLATimeUnit>> List(SLATimeUnitFilter SLATimeUnitFilter);
        Task<SLATimeUnit> Get(long Id);
    }

    public class SLATimeUnitService : BaseService, ISLATimeUnitService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public SLATimeUnitService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(SLATimeUnitFilter SLATimeUnitFilter)
        {
            try
            {
                int result = await UOW.SLATimeUnitRepository.Count(SLATimeUnitFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(SLATimeUnitService));
            }
            return 0;
        }

        public async Task<List<SLATimeUnit>> List(SLATimeUnitFilter SLATimeUnitFilter)
        {
            try
            {
                List<SLATimeUnit> SLATimeUnits = await UOW.SLATimeUnitRepository.List(SLATimeUnitFilter);
                return SLATimeUnits;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(SLATimeUnitService));
            }
            return null;
        }
        
        public async Task<SLATimeUnit> Get(long Id)
        {
            SLATimeUnit SLATimeUnit = await UOW.SLATimeUnitRepository.Get(Id);
            if (SLATimeUnit == null)
                return null;
            return SLATimeUnit;
        }
    }
}
