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

namespace CRM.Services.MRepairStatus
{
    public interface IRepairStatusService :  IServiceScoped
    {
        Task<int> Count(RepairStatusFilter RepairStatusFilter);
        Task<List<RepairStatus>> List(RepairStatusFilter RepairStatusFilter);
        Task<RepairStatus> Get(long Id);
    }

    public class RepairStatusService : BaseService, IRepairStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public RepairStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(RepairStatusFilter RepairStatusFilter)
        {
            try
            {
                int result = await UOW.RepairStatusRepository.Count(RepairStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(RepairStatusService));
            }
            return 0;
        }

        public async Task<List<RepairStatus>> List(RepairStatusFilter RepairStatusFilter)
        {
            try
            {
                List<RepairStatus> RepairStatuss = await UOW.RepairStatusRepository.List(RepairStatusFilter);
                return RepairStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(RepairStatusService));
            }
            return null;
        }
        
        public async Task<RepairStatus> Get(long Id)
        {
            RepairStatus RepairStatus = await UOW.RepairStatusRepository.Get(Id);
            if (RepairStatus == null)
                return null;
            return RepairStatus;
        }
    }
}
