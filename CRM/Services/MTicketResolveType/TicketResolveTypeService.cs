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

namespace CRM.Services.MTicketResolveType
{
    public interface ITicketResolveTypeService :  IServiceScoped
    {
        Task<int> Count(TicketResolveTypeFilter TicketResolveTypeFilter);
        Task<List<TicketResolveType>> List(TicketResolveTypeFilter TicketResolveTypeFilter);
        Task<TicketResolveType> Get(long Id);
    }

    public class TicketResolveTypeService : BaseService, ITicketResolveTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public TicketResolveTypeService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(TicketResolveTypeFilter TicketResolveTypeFilter)
        {
            try
            {
                int result = await UOW.TicketResolveTypeRepository.Count(TicketResolveTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(TicketResolveTypeService));
            }
            return 0;
        }

        public async Task<List<TicketResolveType>> List(TicketResolveTypeFilter TicketResolveTypeFilter)
        {
            try
            {
                List<TicketResolveType> TicketResolveTypes = await UOW.TicketResolveTypeRepository.List(TicketResolveTypeFilter);
                return TicketResolveTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(TicketResolveTypeService));
            }
            return null;
        }
        
        public async Task<TicketResolveType> Get(long Id)
        {
            TicketResolveType TicketResolveType = await UOW.TicketResolveTypeRepository.Get(Id);
            if (TicketResolveType == null)
                return null;
            return TicketResolveType;
        }
    }
}
