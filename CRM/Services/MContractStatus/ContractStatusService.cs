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

namespace CRM.Services.MContractStatus
{
    public interface IContractStatusService :  IServiceScoped
    {
        Task<int> Count(ContractStatusFilter ContractStatusFilter);
        Task<List<ContractStatus>> List(ContractStatusFilter ContractStatusFilter);
        Task<ContractStatus> Get(long Id);
    }

    public class ContractStatusService : BaseService, IContractStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public ContractStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(ContractStatusFilter ContractStatusFilter)
        {
            try
            {
                int result = await UOW.ContractStatusRepository.Count(ContractStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContractStatusService));
            }
            return 0;
        }

        public async Task<List<ContractStatus>> List(ContractStatusFilter ContractStatusFilter)
        {
            try
            {
                List<ContractStatus> ContractStatuss = await UOW.ContractStatusRepository.List(ContractStatusFilter);
                return ContractStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContractStatusService));
            }
            return null;
        }
        
        public async Task<ContractStatus> Get(long Id)
        {
            ContractStatus ContractStatus = await UOW.ContractStatusRepository.Get(Id);
            if (ContractStatus == null)
                return null;
            return ContractStatus;
        }
    }
}
