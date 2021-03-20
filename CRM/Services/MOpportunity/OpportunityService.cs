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
using CRM.Services.MOrganization;
using CRM.Services.MFile;

namespace CRM.Services.MOpportunity
{
    public interface IOpportunityService :  IServiceScoped
    {
        Task<int> Count(OpportunityFilter OpportunityFilter);
        Task<List<Opportunity>> List(OpportunityFilter OpportunityFilter);
        Task<Opportunity> Get(long Id);
        Task<Opportunity> Create(Opportunity Opportunity);
        Task<Opportunity> Update(Opportunity Opportunity);
        Task<Opportunity> Delete(Opportunity Opportunity);
        Task<List<Opportunity>> BulkDelete(List<Opportunity> Opportunities);
        Task<List<Opportunity>> Import(List<Opportunity> Opportunities);
        Task<Opportunity> ChangeStatus(long Id, long statusId);
        Task<Entities.File> UploadFile(Entities.File File);
        Task<OpportunityFilter> ToFilter(OpportunityFilter OpportunityFilter); 
    }

    public class OpportunityService : BaseService, IOpportunityService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IOpportunityValidator OpportunityValidator;
        private IFileService FileService;
        private IOrganizationService OrganizationService;

        public OpportunityService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IOrganizationService OrganizationService,
            IFileService FileService,
            IOpportunityValidator OpportunityValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.OpportunityValidator = OpportunityValidator;
            this.FileService = FileService;
            this.OrganizationService = OrganizationService;
        }
        public async Task<int> Count(OpportunityFilter OpportunityFilter)
        {
            try
            {
                int result = await UOW.OpportunityRepository.Count(OpportunityFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OpportunityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OpportunityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Opportunity>> List(OpportunityFilter OpportunityFilter)
        {
            try
            {
                List<Opportunity> Opportunitys = await UOW.OpportunityRepository.List(OpportunityFilter);
                return Opportunitys;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OpportunityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OpportunityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<Opportunity> Get(long Id)
        {
            Opportunity Opportunity = await UOW.OpportunityRepository.Get(Id);
            if (Opportunity == null)
                return null;
            return Opportunity;
        }
       
        public async Task<Opportunity> Create(Opportunity Opportunity)
        {
            if (!await OpportunityValidator.Create(Opportunity))
                return Opportunity;

            try
            {
                await UOW.Begin();
                var Creator = await UOW.AppUserRepository.Get(CurrentContext.UserId);
                Opportunity.CreatorId = Creator.Id;
                Opportunity.OrganizationId = Creator.OrganizationId;
                Opportunity.SaleStageId = OpportunityStatusEnum.MOI.Id;
                await UOW.OpportunityRepository.Create(Opportunity);

                await UOW.Commit();
                Opportunity = await UOW.OpportunityRepository.Get(Opportunity.Id);
                await Logging.CreateAuditLog(Opportunity, new { }, nameof(OpportunityService));
                return Opportunity;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OpportunityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OpportunityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Opportunity> Update(Opportunity Opportunity)
        {
            if (!await OpportunityValidator.Update(Opportunity))
                return Opportunity;
            try
            {
                var oldData = await UOW.OpportunityRepository.Get(Opportunity.Id);

                await UOW.Begin();
                await UOW.OpportunityRepository.Update(Opportunity);
                await UOW.Commit();

                Opportunity = await UOW.OpportunityRepository.Get(Opportunity.Id);
                await Logging.CreateAuditLog(Opportunity, oldData, nameof(OpportunityService));
                return Opportunity;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OpportunityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OpportunityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Opportunity> Delete(Opportunity Opportunity)
        {
            if (!await OpportunityValidator.Delete(Opportunity))
                return Opportunity;

            try
            {
                await UOW.Begin();
                await UOW.OpportunityRepository.Delete(Opportunity);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, Opportunity, nameof(OpportunityService));
                return Opportunity;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OpportunityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OpportunityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Opportunity>> BulkDelete(List<Opportunity> Opportunities)
        {
            if (!await OpportunityValidator.BulkDelete(Opportunities))
                return Opportunities;

            try
            {
                await UOW.Begin();
                await UOW.OpportunityRepository.BulkDelete(Opportunities);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, Opportunities, nameof(OpportunityService));
                return Opportunities;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OpportunityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OpportunityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<Opportunity>> Import(List<Opportunity> Opportunities)
        {
            if (!await OpportunityValidator.Import(Opportunities))
                return Opportunities;
            try
            {
                await UOW.Begin();
                await UOW.OpportunityRepository.BulkMerge(Opportunities);
                await UOW.Commit();

                await Logging.CreateAuditLog(Opportunities, new { }, nameof(OpportunityService));
                return Opportunities;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OpportunityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OpportunityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Opportunity> ChangeStatus(long Id, long statusId)
        {
            try
            {
                var oldData = await UOW.OpportunityRepository.Get(Id);

                await UOW.Begin();
                await UOW.OpportunityRepository.ChangeStatus(Id, statusId);
                await UOW.Commit();

                var Opportunity = await UOW.OpportunityRepository.Get(Id);
                await Logging.CreateAuditLog(Opportunity, oldData, nameof(OpportunityService));
                return Opportunity;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OpportunityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OpportunityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Entities.File> UploadFile(Entities.File File)
        {
            FileInfo fileInfo = new FileInfo(File.Name);
            string path = $"/opportunity/{StaticParams.DateTimeNow.ToString("yyyyMMdd")}/{Guid.NewGuid()}{fileInfo.Extension}";
            string thumbnailPath = $"/opportunity/{StaticParams.DateTimeNow.ToString("yyyyMMdd")}/{Guid.NewGuid()}{fileInfo.Extension}";
            File = await FileService.Create(File, path);
            return File;
        }

        public async Task<OpportunityFilter> ToFilter(OpportunityFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<OpportunityFilter>();
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
                OpportunityFilter subFilter = new OpportunityFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterBuilder.Merge(subFilter.AppUserId, FilterPermissionDefinition.IdFilter);
                }
            }
            return filter;
        } 
    }
}
