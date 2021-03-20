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

namespace CRM.Services.MActivityType
{
    public interface IActivityTypeService :  IServiceScoped
    {
        Task<int> Count(ActivityTypeFilter ActivityTypeFilter);
        Task<List<ActivityType>> List(ActivityTypeFilter ActivityTypeFilter);
        Task<ActivityType> Get(long Id);
    }

    public class ActivityTypeService : BaseService, IActivityTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public ActivityTypeService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(ActivityTypeFilter ActivityTypeFilter)
        {
            try
            {
                int result = await UOW.ActivityTypeRepository.Count(ActivityTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ActivityTypeService));
            }
            return 0;
        }

        public async Task<List<ActivityType>> List(ActivityTypeFilter ActivityTypeFilter)
        {
            try
            {
                List<ActivityType> ActivityTypes = await UOW.ActivityTypeRepository.List(ActivityTypeFilter);
                return ActivityTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ActivityTypeService));
            }
            return null;
        }
        
        public async Task<ActivityType> Get(long Id)
        {
            ActivityType ActivityType = await UOW.ActivityTypeRepository.Get(Id);
            if (ActivityType == null)
                return null;
            return ActivityType;
        }
    }
}
