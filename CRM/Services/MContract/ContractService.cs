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
using CRM.Handlers;
using CRM.Services.MOrganization;
using CRM.Services.MFile;

namespace CRM.Services.MContract
{
    public interface IContractService : IServiceScoped
    {
        Task<int> Count(ContractFilter ContractFilter);
        Task<List<Contract>> List(ContractFilter ContractFilter);
        Task<Contract> Get(long Id);
        Task<Contract> Create(Contract Contract);
        Task<Contract> Update(Contract Contract);
        Task<Contract> Delete(Contract Contract);
        Task<List<Contract>> BulkDelete(List<Contract> Contracts);
        Task<List<Contract>> Import(List<Contract> Contracts);
        Task<ContractFilter> ToFilter(ContractFilter ContractFilter);
        Task<List<Item>> ListItem(ItemFilter ItemFilter);
        Task<Entities.File> UploadFile(Entities.File File);
    }

    public class ContractService : BaseService, IContractService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IContractValidator ContractValidator;
        private IRabbitManager RabbitManager;
        private IOrganizationService OrganizationService;
        private IFileService FileService;
        public ContractService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IContractValidator ContractValidator,
            IOrganizationService OrganizationService,
            IFileService FileService,
            IRabbitManager RabbitManager
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.ContractValidator = ContractValidator;
            this.OrganizationService = OrganizationService;
            this.FileService = FileService;
            this.RabbitManager = RabbitManager;
        }
        public async Task<int> Count(ContractFilter ContractFilter)
        {
            try
            {
                int result = await UOW.ContractRepository.Count(ContractFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Contract>> List(ContractFilter ContractFilter)
        {
            try
            {
                List<Contract> Contracts = await UOW.ContractRepository.List(ContractFilter);
                return Contracts;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<Contract> Get(long Id)
        {
            Contract Contract = await UOW.ContractRepository.Get(Id);
            if (Contract == null)
                return null;
            return Contract;
        }

        public async Task<Contract> Create(Contract Contract)
        {
            if (!await ContractValidator.Create(Contract))
                return Contract;

            try
            {
                var CurrentUser = await UOW.AppUserRepository.Get(CurrentContext.UserId);
                Contract.CreatorId = CurrentUser.Id;
                Contract.OrganizationId = CurrentUser.OrganizationId;
                await UOW.Begin();
                await UOW.ContractRepository.Create(Contract);
                await UOW.Commit();
                Contract = await UOW.ContractRepository.Get(Contract.Id);
                await Logging.CreateAuditLog(Contract, new { }, nameof(ContractService));
                return Contract;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Contract> Update(Contract Contract)
        {
            if (!await ContractValidator.Update(Contract))
                return Contract;
            try
            {
                var oldData = await UOW.ContractRepository.Get(Contract.Id);

                await UOW.Begin();
                await UOW.ContractRepository.Update(Contract);
                await UOW.Commit();

                Contract = await UOW.ContractRepository.Get(Contract.Id);
                await Logging.CreateAuditLog(Contract, oldData, nameof(ContractService));
                return Contract;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Contract> Delete(Contract Contract)
        {
            if (!await ContractValidator.Delete(Contract))
                return Contract;

            try
            {
                await UOW.Begin();
                await UOW.ContractRepository.Delete(Contract);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, Contract, nameof(ContractService));
                return Contract;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Contract>> BulkDelete(List<Contract> Contracts)
        {
            if (!await ContractValidator.BulkDelete(Contracts))
                return Contracts;

            try
            {
                await UOW.Begin();
                await UOW.ContractRepository.BulkDelete(Contracts);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, Contracts, nameof(ContractService));
                return Contracts;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Contract>> Import(List<Contract> Contracts)
        {
            if (!await ContractValidator.Import(Contracts))
                return Contracts;
            try
            {
                await UOW.Begin();
                await UOW.ContractRepository.BulkMerge(Contracts);
                await UOW.Commit();

                await Logging.CreateAuditLog(Contracts, new { }, nameof(ContractService));
                return Contracts;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Item>> ListItem(ItemFilter ItemFilter)
        {
            try
            {
                List<Item> Items = await UOW.ItemRepository.List(ItemFilter);
                return Items;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContractService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContractService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Entities.File> UploadFile(Entities.File File)
        {
            FileInfo fileInfo = new FileInfo(File.Name);
            string path = $"/contract/{StaticParams.DateTimeNow.ToString("yyyyMMdd")}/{Guid.NewGuid()}{fileInfo.Extension}";
            string thumbnailPath = $"/contract/{StaticParams.DateTimeNow.ToString("yyyyMMdd")}/{Guid.NewGuid()}{fileInfo.Extension}";
            File = await FileService.Create(File, path);
            return File;
        }

        public async Task<ContractFilter> ToFilter(ContractFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<ContractFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            List<Organization> Organizations = await OrganizationService.List(new OrganizationFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OrganizationSelect.ALL,
                OrderBy = OrganizationOrder.Id,
                OrderType = OrderType.ASC
            });
            foreach (var currentFilter in CurrentContext.Filters)
            {
                ContractFilter subFilter = new ContractFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterBuilder.Merge(subFilter.AppUserId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrganizationId))
                    {
                        var organizationIds = FilterOrganization(Organizations, FilterPermissionDefinition.IdFilter);
                        IdFilter IdFilter = new IdFilter { In = organizationIds };
                        subFilter.OrganizationId = FilterBuilder.Merge(subFilter.OrganizationId, IdFilter);
                    }
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        {
                            if (subFilter.AppUserId == null) subFilter.AppUserId = new IdFilter { };
                            subFilter.AppUserId.Equal = CurrentContext.UserId;
                        }
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        {
                            if (subFilter.AppUserId == null) subFilter.AppUserId = new IdFilter { };
                            subFilter.AppUserId.NotEqual = CurrentContext.UserId;
                        }
                    }
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CompanyId))
                        subFilter.CompanyId = FilterBuilder.Merge(subFilter.CompanyId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ContractTypeId))
                        subFilter.ContractTypeId = FilterBuilder.Merge(subFilter.ContractTypeId, FilterPermissionDefinition.IdFilter);
                }
            }
            return filter;
        }

        private void NotifyUsed(Contract Contract)
        {
        }
    }
}
