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

namespace CRM.Services.MCustomerLeadLevel
{
    public interface ICustomerLeadLevelService :  IServiceScoped
    {
        Task<int> Count(CustomerLeadLevelFilter CustomerLeadLevelFilter);
        Task<List<CustomerLeadLevel>> List(CustomerLeadLevelFilter CustomerLeadLevelFilter);
        Task<CustomerLeadLevel> Get(long Id);
    }

    public class CustomerLeadLevelService : BaseService, ICustomerLeadLevelService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public CustomerLeadLevelService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(CustomerLeadLevelFilter CustomerLeadLevelFilter)
        {
            try
            {
                int result = await UOW.CustomerLeadLevelRepository.Count(CustomerLeadLevelFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadLevelService));
            }
            return 0;
        }

        public async Task<List<CustomerLeadLevel>> List(CustomerLeadLevelFilter CustomerLeadLevelFilter)
        {
            try
            {
                List<CustomerLeadLevel> CustomerLeadLevels = await UOW.CustomerLeadLevelRepository.List(CustomerLeadLevelFilter);
                return CustomerLeadLevels;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadLevelService));
            }
            return null;
        }
        
        public async Task<CustomerLeadLevel> Get(long Id)
        {
            CustomerLeadLevel CustomerLeadLevel = await UOW.CustomerLeadLevelRepository.Get(Id);
            if (CustomerLeadLevel == null)
                return null;
            return CustomerLeadLevel;
        }
    }
}
