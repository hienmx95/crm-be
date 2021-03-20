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

namespace CRM.Services.MActivityStatus
{
    public interface IActivityStatusService :  IServiceScoped
    {
        Task<int> Count(ActivityStatusFilter ActivityStatusFilter);
        Task<List<ActivityStatus>> List(ActivityStatusFilter ActivityStatusFilter);
        Task<ActivityStatus> Get(long Id);
    }

    public class ActivityStatusService : BaseService, IActivityStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public ActivityStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(ActivityStatusFilter ActivityStatusFilter)
        {
            try
            {
                int result = await UOW.ActivityStatusRepository.Count(ActivityStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ActivityStatusService));
            }
            return 0;
        }

        public async Task<List<ActivityStatus>> List(ActivityStatusFilter ActivityStatusFilter)
        {
            try
            {
                List<ActivityStatus> ActivityStatuss = await UOW.ActivityStatusRepository.List(ActivityStatusFilter);
                return ActivityStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ActivityStatusService));
            }
            return null;
        }
        
        public async Task<ActivityStatus> Get(long Id)
        {
            ActivityStatus ActivityStatus = await UOW.ActivityStatusRepository.Get(Id);
            if (ActivityStatus == null)
                return null;
            return ActivityStatus;
        }
    }
}
