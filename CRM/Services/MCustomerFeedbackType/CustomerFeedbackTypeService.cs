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

namespace CRM.Services.MCustomerFeedbackType
{
    public interface ICustomerFeedbackTypeService :  IServiceScoped
    {
        Task<int> Count(CustomerFeedbackTypeFilter CustomerFeedbackTypeFilter);
        Task<List<CustomerFeedbackType>> List(CustomerFeedbackTypeFilter CustomerFeedbackTypeFilter);
        Task<CustomerFeedbackType> Get(long Id);
    }

    public class CustomerFeedbackTypeService : BaseService, ICustomerFeedbackTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public CustomerFeedbackTypeService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(CustomerFeedbackTypeFilter CustomerFeedbackTypeFilter)
        {
            try
            {
                int result = await UOW.CustomerFeedbackTypeRepository.Count(CustomerFeedbackTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerFeedbackTypeService));
            }
            return 0;
        }

        public async Task<List<CustomerFeedbackType>> List(CustomerFeedbackTypeFilter CustomerFeedbackTypeFilter)
        {
            try
            {
                List<CustomerFeedbackType> CustomerFeedbackTypes = await UOW.CustomerFeedbackTypeRepository.List(CustomerFeedbackTypeFilter);
                return CustomerFeedbackTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerFeedbackTypeService));
            }
            return null;
        }
        
        public async Task<CustomerFeedbackType> Get(long Id)
        {
            CustomerFeedbackType CustomerFeedbackType = await UOW.CustomerFeedbackTypeRepository.Get(Id);
            if (CustomerFeedbackType == null)
                return null;
            return CustomerFeedbackType;
        }
    }
}
