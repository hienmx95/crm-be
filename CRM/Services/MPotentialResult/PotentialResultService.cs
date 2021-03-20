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

namespace CRM.Services.MPotentialResult
{
    public interface IPotentialResultService :  IServiceScoped
    {
        Task<int> Count(PotentialResultFilter PotentialResultFilter);
        Task<List<PotentialResult>> List(PotentialResultFilter PotentialResultFilter);
        Task<PotentialResult> Get(long Id);
    }

    public class PotentialResultService : BaseService, IPotentialResultService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public PotentialResultService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(PotentialResultFilter PotentialResultFilter)
        {
            try
            {
                int result = await UOW.PotentialResultRepository.Count(PotentialResultFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(PotentialResultService));
            }
            return 0;
        }

        public async Task<List<PotentialResult>> List(PotentialResultFilter PotentialResultFilter)
        {
            try
            {
                List<PotentialResult> PotentialResults = await UOW.PotentialResultRepository.List(PotentialResultFilter);
                return PotentialResults;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(PotentialResultService));
            }
            return null;
        }
        
        public async Task<PotentialResult> Get(long Id)
        {
            PotentialResult PotentialResult = await UOW.PotentialResultRepository.Get(Id);
            if (PotentialResult == null)
                return null;
            return PotentialResult;
        }
    }
}
