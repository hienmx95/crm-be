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

namespace CRM.Services.MPhoneType
{
    public interface IPhoneTypeService :  IServiceScoped
    {
        Task<int> Count(PhoneTypeFilter PhoneTypeFilter);
        Task<List<PhoneType>> List(PhoneTypeFilter PhoneTypeFilter);
        Task<PhoneType> Get(long Id);
        Task<PhoneType> Create(PhoneType PhoneType);
        Task<PhoneType> Update(PhoneType PhoneType);
        Task<PhoneType> Delete(PhoneType PhoneType);
        Task<List<PhoneType>> BulkDelete(List<PhoneType> PhoneTypes);
        Task<List<PhoneType>> Import(List<PhoneType> PhoneTypes);
        Task<PhoneTypeFilter> ToFilter(PhoneTypeFilter PhoneTypeFilter);
    }

    public class PhoneTypeService : BaseService, IPhoneTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IPhoneTypeValidator PhoneTypeValidator;

        public PhoneTypeService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            IPhoneTypeValidator PhoneTypeValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.PhoneTypeValidator = PhoneTypeValidator;
        }
        public async Task<int> Count(PhoneTypeFilter PhoneTypeFilter)
        {
            try
            {
                int result = await UOW.PhoneTypeRepository.Count(PhoneTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(PhoneTypeService));
            }
            return 0;
        }

        public async Task<List<PhoneType>> List(PhoneTypeFilter PhoneTypeFilter)
        {
            try
            {
                List<PhoneType> PhoneTypes = await UOW.PhoneTypeRepository.List(PhoneTypeFilter);
                return PhoneTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(PhoneTypeService));
            }
            return null;
        }
        
        public async Task<PhoneType> Get(long Id)
        {
            PhoneType PhoneType = await UOW.PhoneTypeRepository.Get(Id);
            if (PhoneType == null)
                return null;
            return PhoneType;
        }
        public async Task<PhoneType> Create(PhoneType PhoneType)
        {
            if (!await PhoneTypeValidator.Create(PhoneType))
                return PhoneType;

            try
            {
                await UOW.PhoneTypeRepository.Create(PhoneType);
                PhoneType = await UOW.PhoneTypeRepository.Get(PhoneType.Id);
                await Logging.CreateAuditLog(PhoneType, new { }, nameof(PhoneTypeService));
                return PhoneType;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(PhoneTypeService));
            }
            return null;
        }

        public async Task<PhoneType> Update(PhoneType PhoneType)
        {
            if (!await PhoneTypeValidator.Update(PhoneType))
                return PhoneType;
            try
            {
                var oldData = await UOW.PhoneTypeRepository.Get(PhoneType.Id);

                await UOW.PhoneTypeRepository.Update(PhoneType);

                PhoneType = await UOW.PhoneTypeRepository.Get(PhoneType.Id);
                await Logging.CreateAuditLog(PhoneType, oldData, nameof(PhoneTypeService));
                return PhoneType;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(PhoneTypeService));
            }
            return null;
        }

        public async Task<PhoneType> Delete(PhoneType PhoneType)
        {
            if (!await PhoneTypeValidator.Delete(PhoneType))
                return PhoneType;

            try
            {
                await UOW.PhoneTypeRepository.Delete(PhoneType);
                await Logging.CreateAuditLog(new { }, PhoneType, nameof(PhoneTypeService));
                return PhoneType;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(PhoneTypeService));
            }
            return null;
        }

        public async Task<List<PhoneType>> BulkDelete(List<PhoneType> PhoneTypes)
        {
            if (!await PhoneTypeValidator.BulkDelete(PhoneTypes))
                return PhoneTypes;

            try
            {
                await UOW.PhoneTypeRepository.BulkDelete(PhoneTypes);
                await Logging.CreateAuditLog(new { }, PhoneTypes, nameof(PhoneTypeService));
                return PhoneTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(PhoneTypeService));
            }
            return null;

        }
        
        public async Task<List<PhoneType>> Import(List<PhoneType> PhoneTypes)
        {
            if (!await PhoneTypeValidator.Import(PhoneTypes))
                return PhoneTypes;
            try
            {
                await UOW.PhoneTypeRepository.BulkMerge(PhoneTypes);

                await Logging.CreateAuditLog(PhoneTypes, new { }, nameof(PhoneTypeService));
                return PhoneTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(PhoneTypeService));
            }
            return null;
        }     
        
        public async Task<PhoneTypeFilter> ToFilter(PhoneTypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<PhoneTypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                PhoneTypeFilter subFilter = new PhoneTypeFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        subFilter.Code = FilterBuilder.Merge(subFilter.Code, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        subFilter.Name = FilterBuilder.Merge(subFilter.Name, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterBuilder.Merge(subFilter.StatusId, FilterPermissionDefinition.IdFilter);
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
