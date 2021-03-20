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

namespace CRM.Services.MContractPaymentHistory
{
    public interface IContractPaymentHistoryService :  IServiceScoped
    {
        Task<int> Count(ContractPaymentHistoryFilter ContractPaymentHistoryFilter);
        Task<List<ContractPaymentHistory>> List(ContractPaymentHistoryFilter ContractPaymentHistoryFilter);
        Task<ContractPaymentHistory> Get(long Id);
        Task<ContractPaymentHistory> Create(ContractPaymentHistory ContractPaymentHistory);
        Task<ContractPaymentHistory> Update(ContractPaymentHistory ContractPaymentHistory);
        Task<ContractPaymentHistory> Delete(ContractPaymentHistory ContractPaymentHistory);
        Task<List<ContractPaymentHistory>> BulkDelete(List<ContractPaymentHistory> ContractPaymentHistories);
        Task<List<ContractPaymentHistory>> Import(List<ContractPaymentHistory> ContractPaymentHistories);
        ContractPaymentHistoryFilter ToFilter(ContractPaymentHistoryFilter ContractPaymentHistoryFilter);
    }

    public class ContractPaymentHistoryService : BaseService, IContractPaymentHistoryService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IContractPaymentHistoryValidator ContractPaymentHistoryValidator;

        public ContractPaymentHistoryService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IContractPaymentHistoryValidator ContractPaymentHistoryValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.ContractPaymentHistoryValidator = ContractPaymentHistoryValidator;
        }
        public async Task<int> Count(ContractPaymentHistoryFilter ContractPaymentHistoryFilter)
        {
            try
            {
                int result = await UOW.ContractPaymentHistoryRepository.Count(ContractPaymentHistoryFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<ContractPaymentHistory>> List(ContractPaymentHistoryFilter ContractPaymentHistoryFilter)
        {
            try
            {
                List<ContractPaymentHistory> ContractPaymentHistorys = await UOW.ContractPaymentHistoryRepository.List(ContractPaymentHistoryFilter);
                return ContractPaymentHistorys;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<ContractPaymentHistory> Get(long Id)
        {
            ContractPaymentHistory ContractPaymentHistory = await UOW.ContractPaymentHistoryRepository.Get(Id);
            if (ContractPaymentHistory == null)
                return null;
            return ContractPaymentHistory;
        }
       
        public async Task<ContractPaymentHistory> Create(ContractPaymentHistory ContractPaymentHistory)
        {
            if (!await ContractPaymentHistoryValidator.Create(ContractPaymentHistory))
                return ContractPaymentHistory;

            try
            {
                await UOW.Begin();
                await UOW.ContractPaymentHistoryRepository.Create(ContractPaymentHistory);
                await UOW.Commit();
                ContractPaymentHistory = await UOW.ContractPaymentHistoryRepository.Get(ContractPaymentHistory.Id);
                await Logging.CreateAuditLog(ContractPaymentHistory, new { }, nameof(ContractPaymentHistoryService));
                return ContractPaymentHistory;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<ContractPaymentHistory> Update(ContractPaymentHistory ContractPaymentHistory)
        {
            if (!await ContractPaymentHistoryValidator.Update(ContractPaymentHistory))
                return ContractPaymentHistory;
            try
            {
                var oldData = await UOW.ContractPaymentHistoryRepository.Get(ContractPaymentHistory.Id);

                await UOW.Begin();
                await UOW.ContractPaymentHistoryRepository.Update(ContractPaymentHistory);
                await UOW.Commit();

                ContractPaymentHistory = await UOW.ContractPaymentHistoryRepository.Get(ContractPaymentHistory.Id);
                await Logging.CreateAuditLog(ContractPaymentHistory, oldData, nameof(ContractPaymentHistoryService));
                return ContractPaymentHistory;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<ContractPaymentHistory> Delete(ContractPaymentHistory ContractPaymentHistory)
        {
            if (!await ContractPaymentHistoryValidator.Delete(ContractPaymentHistory))
                return ContractPaymentHistory;

            try
            {
                await UOW.Begin();
                await UOW.ContractPaymentHistoryRepository.Delete(ContractPaymentHistory);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, ContractPaymentHistory, nameof(ContractPaymentHistoryService));
                return ContractPaymentHistory;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<ContractPaymentHistory>> BulkDelete(List<ContractPaymentHistory> ContractPaymentHistories)
        {
            if (!await ContractPaymentHistoryValidator.BulkDelete(ContractPaymentHistories))
                return ContractPaymentHistories;

            try
            {
                await UOW.Begin();
                await UOW.ContractPaymentHistoryRepository.BulkDelete(ContractPaymentHistories);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, ContractPaymentHistories, nameof(ContractPaymentHistoryService));
                return ContractPaymentHistories;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<ContractPaymentHistory>> Import(List<ContractPaymentHistory> ContractPaymentHistories)
        {
            if (!await ContractPaymentHistoryValidator.Import(ContractPaymentHistories))
                return ContractPaymentHistories;
            try
            {
                await UOW.Begin();
                await UOW.ContractPaymentHistoryRepository.BulkMerge(ContractPaymentHistories);
                await UOW.Commit();

                await Logging.CreateAuditLog(ContractPaymentHistories, new { }, nameof(ContractPaymentHistoryService));
                return ContractPaymentHistories;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractPaymentHistoryService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public ContractPaymentHistoryFilter ToFilter(ContractPaymentHistoryFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<ContractPaymentHistoryFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                ContractPaymentHistoryFilter subFilter = new ContractPaymentHistoryFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.ContractId))
                        subFilter.ContractId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.PaymentMilestone))
                        
                        
                        
                        
                        
                        
                        subFilter.PaymentMilestone = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.PaymentPercentage))
                        
                        
                        subFilter.PaymentPercentage = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.PaymentAmount))
                        
                        
                        subFilter.PaymentAmount = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Description))
                        
                        
                        
                        
                        
                        
                        subFilter.Description = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
