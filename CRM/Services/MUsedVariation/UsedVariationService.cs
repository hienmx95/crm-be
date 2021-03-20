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

namespace CRM.Services.MUsedVariation
{
    public interface IUsedVariationService :  IServiceScoped
    {
        Task<int> Count(UsedVariationFilter UsedVariationFilter);
        Task<List<UsedVariation>> List(UsedVariationFilter UsedVariationFilter);
        Task<UsedVariation> Get(long Id);
    }

    public class UsedVariationService : BaseService, IUsedVariationService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public UsedVariationService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(UsedVariationFilter UsedVariationFilter)
        {
            try
            {
                int result = await UOW.UsedVariationRepository.Count(UsedVariationFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(UsedVariationService));
            }
            return 0;
        }

        public async Task<List<UsedVariation>> List(UsedVariationFilter UsedVariationFilter)
        {
            try
            {
                List<UsedVariation> UsedVariations = await UOW.UsedVariationRepository.List(UsedVariationFilter);
                return UsedVariations;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(UsedVariationService));
            }
            return null;
        }
        
        public async Task<UsedVariation> Get(long Id)
        {
            UsedVariation UsedVariation = await UOW.UsedVariationRepository.Get(Id);
            if (UsedVariation == null)
                return null;
            return UsedVariation;
        }
    }
}
