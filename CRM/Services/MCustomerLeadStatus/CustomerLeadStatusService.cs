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

namespace CRM.Services.MCustomerLeadStatus
{
    public interface ICustomerLeadStatusService :  IServiceScoped
    {
        Task<int> Count(CustomerLeadStatusFilter CustomerLeadStatusFilter);
        Task<List<CustomerLeadStatus>> List(CustomerLeadStatusFilter CustomerLeadStatusFilter);
        Task<CustomerLeadStatus> Get(long Id);
    }

    public class CustomerLeadStatusService : BaseService, ICustomerLeadStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public CustomerLeadStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(CustomerLeadStatusFilter CustomerLeadStatusFilter)
        {
            try
            {
                int result = await UOW.CustomerLeadStatusRepository.Count(CustomerLeadStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadStatusService));
            }
            return 0;
        }

        public async Task<List<CustomerLeadStatus>> List(CustomerLeadStatusFilter CustomerLeadStatusFilter)
        {
            try
            {
                List<CustomerLeadStatus> CustomerLeadStatuss = await UOW.CustomerLeadStatusRepository.List(CustomerLeadStatusFilter);
                return CustomerLeadStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadStatusService));
            }
            return null;
        }
        
        public async Task<CustomerLeadStatus> Get(long Id)
        {
            CustomerLeadStatus CustomerLeadStatus = await UOW.CustomerLeadStatusRepository.Get(Id);
            if (CustomerLeadStatus == null)
                return null;
            return CustomerLeadStatus;
        }
    }
}
