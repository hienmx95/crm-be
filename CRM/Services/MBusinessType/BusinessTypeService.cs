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

namespace CRM.Services.MBusinessType
{
    public interface IBusinessTypeService :  IServiceScoped
    {
        Task<int> Count(BusinessTypeFilter BusinessTypeFilter);
        Task<List<BusinessType>> List(BusinessTypeFilter BusinessTypeFilter);
        Task<BusinessType> Get(long Id);
    }

    public class BusinessTypeService : BaseService, IBusinessTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public BusinessTypeService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(BusinessTypeFilter BusinessTypeFilter)
        {
            try
            {
                int result = await UOW.BusinessTypeRepository.Count(BusinessTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(BusinessTypeService));
            }
            return 0;
        }

        public async Task<List<BusinessType>> List(BusinessTypeFilter BusinessTypeFilter)
        {
            try
            {
                List<BusinessType> BusinessTypes = await UOW.BusinessTypeRepository.List(BusinessTypeFilter);
                return BusinessTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(BusinessTypeService));
            }
            return null;
        }
        
        public async Task<BusinessType> Get(long Id)
        {
            BusinessType BusinessType = await UOW.BusinessTypeRepository.Get(Id);
            if (BusinessType == null)
                return null;
            return BusinessType;
        }
    }
}
