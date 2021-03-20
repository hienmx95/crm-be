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

namespace CRM.Services.MStatus
{
    public interface IStatusService :  IServiceScoped
    {
        Task<int> Count(StatusFilter StatusFilter);
        Task<List<Status>> List(StatusFilter StatusFilter);
        Task<Status> Get(long Id);
    }

    public class StatusService : BaseService, IStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public StatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(StatusFilter StatusFilter)
        {
            try
            {
                int result = await UOW.StatusRepository.Count(StatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(StatusService));
            }
            return 0;
        }

        public async Task<List<Status>> List(StatusFilter StatusFilter)
        {
            try
            {
                List<Status> Statuss = await UOW.StatusRepository.List(StatusFilter);
                return Statuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(StatusService));
            }
            return null;
        }
        
        public async Task<Status> Get(long Id)
        {
            Status Status = await UOW.StatusRepository.Get(Id);
            if (Status == null)
                return null;
            return Status;
        }
    }
}
