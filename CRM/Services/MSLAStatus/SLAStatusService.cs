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

namespace CRM.Services.MSLAStatus
{
    public interface ISLAStatusService :  IServiceScoped
    {
        Task<int> Count(SLAStatusFilter SLAStatusFilter);
        Task<List<SLAStatus>> List(SLAStatusFilter SLAStatusFilter);
        Task<SLAStatus> Get(long Id);
    }

    public class SLAStatusService : BaseService, ISLAStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public SLAStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(SLAStatusFilter SLAStatusFilter)
        {
            try
            {
                int result = await UOW.SLAStatusRepository.Count(SLAStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(SLAStatusService));
            }
            return 0;
        }

        public async Task<List<SLAStatus>> List(SLAStatusFilter SLAStatusFilter)
        {
            try
            {
                List<SLAStatus> SLAStatuss = await UOW.SLAStatusRepository.List(SLAStatusFilter);
                return SLAStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(SLAStatusService));
            }
            return null;
        }
        
        public async Task<SLAStatus> Get(long Id)
        {
            SLAStatus SLAStatus = await UOW.SLAStatusRepository.Get(Id);
            if (SLAStatus == null)
                return null;
            return SLAStatus;
        }
    }
}
