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

namespace CRM.Services.MEmailStatus
{
    public interface IEmailStatusService :  IServiceScoped
    {
        Task<int> Count(EmailStatusFilter EmailStatusFilter);
        Task<List<EmailStatus>> List(EmailStatusFilter EmailStatusFilter);
        Task<EmailStatus> Get(long Id);
    }

    public class EmailStatusService : BaseService, IEmailStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public EmailStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(EmailStatusFilter EmailStatusFilter)
        {
            try
            {
                int result = await UOW.EmailStatusRepository.Count(EmailStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(EmailStatusService));
            }
            return 0;
        }

        public async Task<List<EmailStatus>> List(EmailStatusFilter EmailStatusFilter)
        {
            try
            {
                List<EmailStatus> EmailStatuss = await UOW.EmailStatusRepository.List(EmailStatusFilter);
                return EmailStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(EmailStatusService));
            }
            return null;
        }
        
        public async Task<EmailStatus> Get(long Id)
        {
            EmailStatus EmailStatus = await UOW.EmailStatusRepository.Get(Id);
            if (EmailStatus == null)
                return null;
            return EmailStatus;
        }
    }
}
