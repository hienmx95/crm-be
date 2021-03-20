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

namespace CRM.Services.MContactStatus
{
    public interface IContactStatusService :  IServiceScoped
    {
        Task<int> Count(ContactStatusFilter ContactStatusFilter);
        Task<List<ContactStatus>> List(ContactStatusFilter ContactStatusFilter);
        Task<ContactStatus> Get(long Id);
    }

    public class ContactStatusService : BaseService, IContactStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public ContactStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(ContactStatusFilter ContactStatusFilter)
        {
            try
            {
                int result = await UOW.ContactStatusRepository.Count(ContactStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactStatusService));
            }
            return 0;
        }

        public async Task<List<ContactStatus>> List(ContactStatusFilter ContactStatusFilter)
        {
            try
            {
                List<ContactStatus> ContactStatuss = await UOW.ContactStatusRepository.List(ContactStatusFilter);
                return ContactStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactStatusService));
            }
            return null;
        }
        
        public async Task<ContactStatus> Get(long Id)
        {
            ContactStatus ContactStatus = await UOW.ContactStatusRepository.Get(Id);
            if (ContactStatus == null)
                return null;
            return ContactStatus;
        }
    }
}
