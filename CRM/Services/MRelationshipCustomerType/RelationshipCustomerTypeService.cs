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

namespace CRM.Services.MRelationshipCustomerType
{
    public interface IRelationshipCustomerTypeService :  IServiceScoped
    {
        Task<int> Count(RelationshipCustomerTypeFilter RelationshipCustomerTypeFilter);
        Task<List<RelationshipCustomerType>> List(RelationshipCustomerTypeFilter RelationshipCustomerTypeFilter);
        Task<RelationshipCustomerType> Get(long Id);
        Task<RelationshipCustomerType> Create(RelationshipCustomerType RelationshipCustomerType);
        Task<RelationshipCustomerType> Update(RelationshipCustomerType RelationshipCustomerType);
        Task<RelationshipCustomerType> Delete(RelationshipCustomerType RelationshipCustomerType);
        Task<List<RelationshipCustomerType>> BulkDelete(List<RelationshipCustomerType> RelationshipCustomerTypes);
        Task<List<RelationshipCustomerType>> Import(List<RelationshipCustomerType> RelationshipCustomerTypes);
        Task<RelationshipCustomerTypeFilter> ToFilter(RelationshipCustomerTypeFilter RelationshipCustomerTypeFilter);
    }

    public class RelationshipCustomerTypeService : BaseService, IRelationshipCustomerTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IRelationshipCustomerTypeValidator RelationshipCustomerTypeValidator;

        public RelationshipCustomerTypeService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IRelationshipCustomerTypeValidator RelationshipCustomerTypeValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.RelationshipCustomerTypeValidator = RelationshipCustomerTypeValidator;
        }
        public async Task<int> Count(RelationshipCustomerTypeFilter RelationshipCustomerTypeFilter)
        {
            try
            {
                int result = await UOW.RelationshipCustomerTypeRepository.Count(RelationshipCustomerTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<RelationshipCustomerType>> List(RelationshipCustomerTypeFilter RelationshipCustomerTypeFilter)
        {
            try
            {
                List<RelationshipCustomerType> RelationshipCustomerTypes = await UOW.RelationshipCustomerTypeRepository.List(RelationshipCustomerTypeFilter);
                return RelationshipCustomerTypes;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<RelationshipCustomerType> Get(long Id)
        {
            RelationshipCustomerType RelationshipCustomerType = await UOW.RelationshipCustomerTypeRepository.Get(Id);
            if (RelationshipCustomerType == null)
                return null;
            return RelationshipCustomerType;
        }
       
        public async Task<RelationshipCustomerType> Create(RelationshipCustomerType RelationshipCustomerType)
        {
            if (!await RelationshipCustomerTypeValidator.Create(RelationshipCustomerType))
                return RelationshipCustomerType;

            try
            {
                await UOW.Begin();
                await UOW.RelationshipCustomerTypeRepository.Create(RelationshipCustomerType);
                await UOW.Commit();
                RelationshipCustomerType = await UOW.RelationshipCustomerTypeRepository.Get(RelationshipCustomerType.Id);
                await Logging.CreateAuditLog(RelationshipCustomerType, new { }, nameof(RelationshipCustomerTypeService));
                return RelationshipCustomerType;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<RelationshipCustomerType> Update(RelationshipCustomerType RelationshipCustomerType)
        {
            if (!await RelationshipCustomerTypeValidator.Update(RelationshipCustomerType))
                return RelationshipCustomerType;
            try
            {
                var oldData = await UOW.RelationshipCustomerTypeRepository.Get(RelationshipCustomerType.Id);

                await UOW.Begin();
                await UOW.RelationshipCustomerTypeRepository.Update(RelationshipCustomerType);
                await UOW.Commit();

                RelationshipCustomerType = await UOW.RelationshipCustomerTypeRepository.Get(RelationshipCustomerType.Id);
                await Logging.CreateAuditLog(RelationshipCustomerType, oldData, nameof(RelationshipCustomerTypeService));
                return RelationshipCustomerType;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<RelationshipCustomerType> Delete(RelationshipCustomerType RelationshipCustomerType)
        {
            if (!await RelationshipCustomerTypeValidator.Delete(RelationshipCustomerType))
                return RelationshipCustomerType;

            try
            {
                await UOW.Begin();
                await UOW.RelationshipCustomerTypeRepository.Delete(RelationshipCustomerType);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, RelationshipCustomerType, nameof(RelationshipCustomerTypeService));
                return RelationshipCustomerType;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<RelationshipCustomerType>> BulkDelete(List<RelationshipCustomerType> RelationshipCustomerTypes)
        {
            if (!await RelationshipCustomerTypeValidator.BulkDelete(RelationshipCustomerTypes))
                return RelationshipCustomerTypes;

            try
            {
                await UOW.Begin();
                await UOW.RelationshipCustomerTypeRepository.BulkDelete(RelationshipCustomerTypes);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, RelationshipCustomerTypes, nameof(RelationshipCustomerTypeService));
                return RelationshipCustomerTypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<RelationshipCustomerType>> Import(List<RelationshipCustomerType> RelationshipCustomerTypes)
        {
            if (!await RelationshipCustomerTypeValidator.Import(RelationshipCustomerTypes))
                return RelationshipCustomerTypes;
            try
            {
                await UOW.Begin();
                await UOW.RelationshipCustomerTypeRepository.BulkMerge(RelationshipCustomerTypes);
                await UOW.Commit();

                await Logging.CreateAuditLog(RelationshipCustomerTypes, new { }, nameof(RelationshipCustomerTypeService));
                return RelationshipCustomerTypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(RelationshipCustomerTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public async Task<RelationshipCustomerTypeFilter> ToFilter(RelationshipCustomerTypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<RelationshipCustomerTypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                RelationshipCustomerTypeFilter subFilter = new RelationshipCustomerTypeFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        subFilter.Name = FilterBuilder.Merge(subFilter.Name, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        {
                        }
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        {
                        }
                    }
                }
            }
            return filter;
        }
    }
}
