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

namespace CRM.Services.MStoreStatus
{
    public interface IStoreStatusService :  IServiceScoped
    {
        Task<int> Count(StoreStatusFilter StoreStatusFilter);
        Task<List<StoreStatus>> List(StoreStatusFilter StoreStatusFilter);
        Task<StoreStatus> Get(long Id);
    }

    public class StoreStatusService : BaseService, IStoreStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public StoreStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(StoreStatusFilter StoreStatusFilter)
        {
            try
            {
                int result = await UOW.StoreStatusRepository.Count(StoreStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(StoreStatusService));
            }
            return 0;
        }

        public async Task<List<StoreStatus>> List(StoreStatusFilter StoreStatusFilter)
        {
            try
            {
                List<StoreStatus> StoreStatuss = await UOW.StoreStatusRepository.List(StoreStatusFilter);
                return StoreStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(StoreStatusService));
            }
            return null;
        }
        
        public async Task<StoreStatus> Get(long Id)
        {
            StoreStatus StoreStatus = await UOW.StoreStatusRepository.Get(Id);
            if (StoreStatus == null)
                return null;
            return StoreStatus;
        }
    }
}
