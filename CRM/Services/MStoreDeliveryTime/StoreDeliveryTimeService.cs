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

namespace CRM.Services.MStoreDeliveryTime
{
    public interface IStoreDeliveryTimeService :  IServiceScoped
    {
        Task<int> Count(StoreDeliveryTimeFilter StoreDeliveryTimeFilter);
        Task<List<StoreDeliveryTime>> List(StoreDeliveryTimeFilter StoreDeliveryTimeFilter);
        Task<StoreDeliveryTime> Get(long Id);
    }

    public class StoreDeliveryTimeService : BaseService, IStoreDeliveryTimeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public StoreDeliveryTimeService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(StoreDeliveryTimeFilter StoreDeliveryTimeFilter)
        {
            try
            {
                int result = await UOW.StoreDeliveryTimeRepository.Count(StoreDeliveryTimeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(StoreDeliveryTimeService));
            }
            return 0;
        }

        public async Task<List<StoreDeliveryTime>> List(StoreDeliveryTimeFilter StoreDeliveryTimeFilter)
        {
            try
            {
                List<StoreDeliveryTime> StoreDeliveryTimes = await UOW.StoreDeliveryTimeRepository.List(StoreDeliveryTimeFilter);
                return StoreDeliveryTimes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(StoreDeliveryTimeService));
            }
            return null;
        }
        
        public async Task<StoreDeliveryTime> Get(long Id)
        {
            StoreDeliveryTime StoreDeliveryTime = await UOW.StoreDeliveryTimeRepository.Get(Id);
            if (StoreDeliveryTime == null)
                return null;
            return StoreDeliveryTime;
        }
    }
}
