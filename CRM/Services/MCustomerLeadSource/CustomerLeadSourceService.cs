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

namespace CRM.Services.MCustomerLeadSource
{
    public interface ICustomerLeadSourceService :  IServiceScoped
    {
        Task<int> Count(CustomerLeadSourceFilter CustomerLeadSourceFilter);
        Task<List<CustomerLeadSource>> List(CustomerLeadSourceFilter CustomerLeadSourceFilter);
        Task<CustomerLeadSource> Get(long Id);
    }

    public class CustomerLeadSourceService : BaseService, ICustomerLeadSourceService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public CustomerLeadSourceService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(CustomerLeadSourceFilter CustomerLeadSourceFilter)
        {
            try
            {
                int result = await UOW.CustomerLeadSourceRepository.Count(CustomerLeadSourceFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadSourceService));
            }
            return 0;
        }

        public async Task<List<CustomerLeadSource>> List(CustomerLeadSourceFilter CustomerLeadSourceFilter)
        {
            try
            {
                List<CustomerLeadSource> CustomerLeadSources = await UOW.CustomerLeadSourceRepository.List(CustomerLeadSourceFilter);
                return CustomerLeadSources;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadSourceService));
            }
            return null;
        }
        
        public async Task<CustomerLeadSource> Get(long Id)
        {
            CustomerLeadSource CustomerLeadSource = await UOW.CustomerLeadSourceRepository.Get(Id);
            if (CustomerLeadSource == null)
                return null;
            return CustomerLeadSource;
        }
    }
}
