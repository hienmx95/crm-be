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

namespace CRM.Services.MEmailType
{
    public interface IEmailTypeService :  IServiceScoped
    {
        Task<int> Count(EmailTypeFilter EmailTypeFilter);
        Task<List<EmailType>> List(EmailTypeFilter EmailTypeFilter);
        Task<EmailType> Get(long Id);
    }

    public class EmailTypeService : BaseService, IEmailTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public EmailTypeService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(EmailTypeFilter EmailTypeFilter)
        {
            try
            {
                int result = await UOW.EmailTypeRepository.Count(EmailTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(EmailTypeService));
            }
            return 0;
        }

        public async Task<List<EmailType>> List(EmailTypeFilter EmailTypeFilter)
        {
            try
            {
                List<EmailType> EmailTypes = await UOW.EmailTypeRepository.List(EmailTypeFilter);
                return EmailTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(EmailTypeService));
            }
            return null;
        }
        
        public async Task<EmailType> Get(long Id)
        {
            EmailType EmailType = await UOW.EmailTypeRepository.Get(Id);
            if (EmailType == null)
                return null;
            return EmailType;
        }
    }
}
