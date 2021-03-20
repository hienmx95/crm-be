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

namespace CRM.Services.MActivityPriority
{
    public interface IActivityPriorityService :  IServiceScoped
    {
        Task<int> Count(ActivityPriorityFilter ActivityPriorityFilter);
        Task<List<ActivityPriority>> List(ActivityPriorityFilter ActivityPriorityFilter);
        Task<ActivityPriority> Get(long Id);
    }

    public class ActivityPriorityService : BaseService, IActivityPriorityService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public ActivityPriorityService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(ActivityPriorityFilter ActivityPriorityFilter)
        {
            try
            {
                int result = await UOW.ActivityPriorityRepository.Count(ActivityPriorityFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ActivityPriorityService));
            }
            return 0;
        }

        public async Task<List<ActivityPriority>> List(ActivityPriorityFilter ActivityPriorityFilter)
        {
            try
            {
                List<ActivityPriority> ActivityPrioritys = await UOW.ActivityPriorityRepository.List(ActivityPriorityFilter);
                return ActivityPrioritys;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ActivityPriorityService));
            }
            return null;
        }
        
        public async Task<ActivityPriority> Get(long Id)
        {
            ActivityPriority ActivityPriority = await UOW.ActivityPriorityRepository.Get(Id);
            if (ActivityPriority == null)
                return null;
            return ActivityPriority;
        }
    }
}
