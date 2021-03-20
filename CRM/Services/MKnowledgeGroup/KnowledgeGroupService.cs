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

namespace CRM.Services.MKnowledgeGroup
{
    public interface IKnowledgeGroupService : IServiceScoped
    {
        Task<int> Count(KnowledgeGroupFilter KnowledgeGroupFilter);
        Task<List<KnowledgeGroup>> List(KnowledgeGroupFilter KnowledgeGroupFilter);
        Task<KnowledgeGroup> Get(long Id);
        Task<KnowledgeGroup> Create(KnowledgeGroup KnowledgeGroup);
        Task<KnowledgeGroup> Update(KnowledgeGroup KnowledgeGroup);
        Task<KnowledgeGroup> Delete(KnowledgeGroup KnowledgeGroup);
        Task<List<KnowledgeGroup>> BulkDelete(List<KnowledgeGroup> KnowledgeGroups);
        Task<List<KnowledgeGroup>> Import(List<KnowledgeGroup> KnowledgeGroups);
        KnowledgeGroupFilter ToFilter(KnowledgeGroupFilter KnowledgeGroupFilter);
    }

    public class KnowledgeGroupService : BaseService, IKnowledgeGroupService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IKnowledgeGroupValidator KnowledgeGroupValidator;

        public KnowledgeGroupService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IKnowledgeGroupValidator KnowledgeGroupValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.KnowledgeGroupValidator = KnowledgeGroupValidator;
        }
        public async Task<int> Count(KnowledgeGroupFilter KnowledgeGroupFilter)
        {
            try
            {
                int result = await UOW.KnowledgeGroupRepository.Count(KnowledgeGroupFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<KnowledgeGroup>> List(KnowledgeGroupFilter KnowledgeGroupFilter)
        {
            try
            {
                List<KnowledgeGroup> KnowledgeGroups = await UOW.KnowledgeGroupRepository.List(KnowledgeGroupFilter);
                return KnowledgeGroups;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<KnowledgeGroup> Get(long Id)
        {
            KnowledgeGroup KnowledgeGroup = await UOW.KnowledgeGroupRepository.Get(Id);
            if (KnowledgeGroup == null)
                return null;
            return KnowledgeGroup;
        }

        public async Task<KnowledgeGroup> Create(KnowledgeGroup KnowledgeGroup)
        {
            if (!await KnowledgeGroupValidator.Create(KnowledgeGroup))
                return KnowledgeGroup;

            try
            {
                await UOW.Begin();
                await UOW.KnowledgeGroupRepository.Create(KnowledgeGroup);
                await UOW.Commit();
                KnowledgeGroup = await UOW.KnowledgeGroupRepository.Get(KnowledgeGroup.Id);
                await Logging.CreateAuditLog(KnowledgeGroup, new { }, nameof(KnowledgeGroupService));
                return KnowledgeGroup;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<KnowledgeGroup> Update(KnowledgeGroup KnowledgeGroup)
        {
            if (!await KnowledgeGroupValidator.Update(KnowledgeGroup))
                return KnowledgeGroup;
            try
            {
                var oldData = await UOW.KnowledgeGroupRepository.Get(KnowledgeGroup.Id);

                await UOW.Begin();
                await UOW.KnowledgeGroupRepository.Update(KnowledgeGroup);
                await UOW.Commit();

                KnowledgeGroup = await UOW.KnowledgeGroupRepository.Get(KnowledgeGroup.Id);
                await Logging.CreateAuditLog(KnowledgeGroup, oldData, nameof(KnowledgeGroupService));
                return KnowledgeGroup;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<KnowledgeGroup> Delete(KnowledgeGroup KnowledgeGroup)
        {
            if (!await KnowledgeGroupValidator.Delete(KnowledgeGroup))
                return KnowledgeGroup;

            try
            {
                await UOW.Begin();
                await UOW.KnowledgeGroupRepository.Delete(KnowledgeGroup);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, KnowledgeGroup, nameof(KnowledgeGroupService));
                return KnowledgeGroup;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<KnowledgeGroup>> BulkDelete(List<KnowledgeGroup> KnowledgeGroups)
        {
            if (!await KnowledgeGroupValidator.BulkDelete(KnowledgeGroups))
                return KnowledgeGroups;

            try
            {
                await UOW.Begin();
                await UOW.KnowledgeGroupRepository.BulkDelete(KnowledgeGroups);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, KnowledgeGroups, nameof(KnowledgeGroupService));
                return KnowledgeGroups;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<KnowledgeGroup>> Import(List<KnowledgeGroup> KnowledgeGroups)
        {
            if (!await KnowledgeGroupValidator.Import(KnowledgeGroups))
                return KnowledgeGroups;
            try
            {
                await UOW.Begin();
                await UOW.KnowledgeGroupRepository.BulkMerge(KnowledgeGroups);
                await UOW.Commit();

                await Logging.CreateAuditLog(KnowledgeGroups, new { }, nameof(KnowledgeGroupService));
                return KnowledgeGroups;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public KnowledgeGroupFilter ToFilter(KnowledgeGroupFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<KnowledgeGroupFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                KnowledgeGroupFilter subFilter = new KnowledgeGroupFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter; if (FilterPermissionDefinition.Name == nameof(subFilter.DisplayOrder))
                        subFilter.DisplayOrder = FilterPermissionDefinition.LongFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Description))
                        subFilter.Description = FilterPermissionDefinition.StringFilter;
                }
            }
            return filter;
        }
    }
}
