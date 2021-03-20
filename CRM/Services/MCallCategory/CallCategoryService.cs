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

namespace CRM.Services.MCallCategory
{
    public interface ICallCategoryService :  IServiceScoped
    {
        Task<int> Count(CallCategoryFilter CallCategoryFilter);
        Task<List<CallCategory>> List(CallCategoryFilter CallCategoryFilter);
        Task<CallCategory> Get(long Id);
    }

    public class CallCategoryService : BaseService, ICallCategoryService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public CallCategoryService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(CallCategoryFilter CallCategoryFilter)
        {
            try
            {
                int result = await UOW.CallCategoryRepository.Count(CallCategoryFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CallCategoryService));
            }
            return 0;
        }

        public async Task<List<CallCategory>> List(CallCategoryFilter CallCategoryFilter)
        {
            try
            {
                List<CallCategory> CallCategorys = await UOW.CallCategoryRepository.List(CallCategoryFilter);
                return CallCategorys;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CallCategoryService));
            }
            return null;
        }
        
        public async Task<CallCategory> Get(long Id)
        {
            CallCategory CallCategory = await UOW.CallCategoryRepository.Get(Id);
            if (CallCategory == null)
                return null;
            return CallCategory;
        }
    }
}
