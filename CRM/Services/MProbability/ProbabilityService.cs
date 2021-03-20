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

namespace CRM.Services.MProbability
{
    public interface IProbabilityService :  IServiceScoped
    {
        Task<int> Count(ProbabilityFilter ProbabilityFilter);
        Task<List<Probability>> List(ProbabilityFilter ProbabilityFilter);
        Task<Probability> Get(long Id);
    }

    public class ProbabilityService : BaseService, IProbabilityService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public ProbabilityService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(ProbabilityFilter ProbabilityFilter)
        {
            try
            {
                int result = await UOW.ProbabilityRepository.Count(ProbabilityFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ProbabilityService));
            }
            return 0;
        }

        public async Task<List<Probability>> List(ProbabilityFilter ProbabilityFilter)
        {
            try
            {
                List<Probability> Probabilitys = await UOW.ProbabilityRepository.List(ProbabilityFilter);
                return Probabilitys;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ProbabilityService));
            }
            return null;
        }
        
        public async Task<Probability> Get(long Id)
        {
            Probability Probability = await UOW.ProbabilityRepository.Get(Id);
            if (Probability == null)
                return null;
            return Probability;
        }
    }
}
