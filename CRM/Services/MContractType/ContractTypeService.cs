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

namespace CRM.Services.MContractType
{
    public interface IContractTypeService :  IServiceScoped
    {
        Task<int> Count(ContractTypeFilter ContractTypeFilter);
        Task<List<ContractType>> List(ContractTypeFilter ContractTypeFilter);
        Task<ContractType> Get(long Id);
    }

    public class ContractTypeService : BaseService, IContractTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public ContractTypeService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(ContractTypeFilter ContractTypeFilter)
        {
            try
            {
                int result = await UOW.ContractTypeRepository.Count(ContractTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContractTypeService));
            }
            return 0;
        }

        public async Task<List<ContractType>> List(ContractTypeFilter ContractTypeFilter)
        {
            try
            {
                List<ContractType> ContractTypes = await UOW.ContractTypeRepository.List(ContractTypeFilter);
                return ContractTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContractTypeService));
            }
            return null;
        }
        
        public async Task<ContractType> Get(long Id)
        {
            ContractType ContractType = await UOW.ContractTypeRepository.Get(Id);
            if (ContractType == null)
                return null;
            return ContractType;
        }
    }
}
