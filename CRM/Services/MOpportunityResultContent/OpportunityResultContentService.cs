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

namespace CRM.Services.MOpportunityResultType
{
    public interface IOpportunityResultTypeService :  IServiceScoped
    {
        Task<int> Count(OpportunityResultTypeFilter OpportunityResultTypeFilter);
        Task<List<OpportunityResultType>> List(OpportunityResultTypeFilter OpportunityResultTypeFilter);
        Task<OpportunityResultType> Get(long Id);
    }

    public class OpportunityResultTypeService : BaseService, IOpportunityResultTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public OpportunityResultTypeService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(OpportunityResultTypeFilter OpportunityResultTypeFilter)
        {
            try
            {
                int result = await UOW.OpportunityResultTypeRepository.Count(OpportunityResultTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityResultTypeService));
            }
            return 0;
        }

        public async Task<List<OpportunityResultType>> List(OpportunityResultTypeFilter OpportunityResultTypeFilter)
        {
            try
            {
                List<OpportunityResultType> OpportunityResultTypes = await UOW.OpportunityResultTypeRepository.List(OpportunityResultTypeFilter);
                return OpportunityResultTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityResultTypeService));
            }
            return null;
        }
        
        public async Task<OpportunityResultType> Get(long Id)
        {
            OpportunityResultType OpportunityResultType = await UOW.OpportunityResultTypeRepository.Get(Id);
            if (OpportunityResultType == null)
                return null;
            return OpportunityResultType;
        }
    }
}
