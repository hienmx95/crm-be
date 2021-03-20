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

namespace CRM.Services.MCallStatus
{
    public interface ICallStatusService :  IServiceScoped
    {
        Task<int> Count(CallStatusFilter CallStatusFilter);
        Task<List<CallStatus>> List(CallStatusFilter CallStatusFilter);
        Task<CallStatus> Get(long Id);
    }

    public class CallStatusService : BaseService, ICallStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public CallStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(CallStatusFilter CallStatusFilter)
        {
            try
            {
                int result = await UOW.CallStatusRepository.Count(CallStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CallStatusService));
            }
            return 0;
        }

        public async Task<List<CallStatus>> List(CallStatusFilter CallStatusFilter)
        {
            try
            {
                List<CallStatus> CallStatuss = await UOW.CallStatusRepository.List(CallStatusFilter);
                return CallStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CallStatusService));
            }
            return null;
        }
        
        public async Task<CallStatus> Get(long Id)
        {
            CallStatus CallStatus = await UOW.CallStatusRepository.Get(Id);
            if (CallStatus == null)
                return null;
            return CallStatus;
        }
    }
}
