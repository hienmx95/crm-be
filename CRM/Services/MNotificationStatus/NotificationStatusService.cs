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

namespace CRM.Services.MNotificationStatus
{
    public interface INotificationStatusService :  IServiceScoped
    {
        Task<int> Count(NotificationStatusFilter NotificationStatusFilter);
        Task<List<NotificationStatus>> List(NotificationStatusFilter NotificationStatusFilter);
        Task<NotificationStatus> Get(long Id);
    }

    public class NotificationStatusService : BaseService, INotificationStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public NotificationStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(NotificationStatusFilter NotificationStatusFilter)
        {
            try
            {
                int result = await UOW.NotificationStatusRepository.Count(NotificationStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(NotificationStatusService));
            }
            return 0;
        }

        public async Task<List<NotificationStatus>> List(NotificationStatusFilter NotificationStatusFilter)
        {
            try
            {
                List<NotificationStatus> NotificationStatuss = await UOW.NotificationStatusRepository.List(NotificationStatusFilter);
                return NotificationStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(NotificationStatusService));
            }
            return null;
        }
        
        public async Task<NotificationStatus> Get(long Id)
        {
            NotificationStatus NotificationStatus = await UOW.NotificationStatusRepository.Get(Id);
            if (NotificationStatus == null)
                return null;
            return NotificationStatus;
        }
    }
}
