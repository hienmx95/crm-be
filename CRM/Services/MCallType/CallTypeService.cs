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

namespace CRM.Services.MCallType
{
    public interface ICallTypeService :  IServiceScoped
    {
        Task<int> Count(CallTypeFilter CallTypeFilter);
        Task<List<CallType>> List(CallTypeFilter CallTypeFilter);
        Task<CallType> Get(long Id);
        Task<CallType> Create(CallType CallType);
        Task<CallType> Update(CallType CallType);
        Task<CallType> Delete(CallType CallType);
        Task<List<CallType>> BulkDelete(List<CallType> CallTypes);
        Task<List<CallType>> Import(List<CallType> CallTypes);
        CallTypeFilter ToFilter(CallTypeFilter CallTypeFilter);
    }

    public class CallTypeService : BaseService, ICallTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICallTypeValidator CallTypeValidator;

        public CallTypeService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ICallTypeValidator CallTypeValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CallTypeValidator = CallTypeValidator;
        }
        public async Task<int> Count(CallTypeFilter CallTypeFilter)
        {
            try
            {
                int result = await UOW.CallTypeRepository.Count(CallTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<CallType>> List(CallTypeFilter CallTypeFilter)
        {
            try
            {
                List<CallType> CallTypes = await UOW.CallTypeRepository.List(CallTypeFilter);
                return CallTypes;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<CallType> Get(long Id)
        {
            CallType CallType = await UOW.CallTypeRepository.Get(Id);
            if (CallType == null)
                return null;
            return CallType;
        }
       
        public async Task<CallType> Create(CallType CallType)
        {
            if (!await CallTypeValidator.Create(CallType))
                return CallType;

            try
            {
                await UOW.Begin();
                await UOW.CallTypeRepository.Create(CallType);
                await UOW.Commit();
                CallType = await UOW.CallTypeRepository.Get(CallType.Id);
                await Logging.CreateAuditLog(CallType, new { }, nameof(CallTypeService));
                return CallType;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<CallType> Update(CallType CallType)
        {
            if (!await CallTypeValidator.Update(CallType))
                return CallType;
            try
            {
                var oldData = await UOW.CallTypeRepository.Get(CallType.Id);

                await UOW.Begin();
                await UOW.CallTypeRepository.Update(CallType);
                await UOW.Commit();

                CallType = await UOW.CallTypeRepository.Get(CallType.Id);
                await Logging.CreateAuditLog(CallType, oldData, nameof(CallTypeService));
                return CallType;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<CallType> Delete(CallType CallType)
        {
            if (!await CallTypeValidator.Delete(CallType))
                return CallType;

            try
            {
                await UOW.Begin();
                await UOW.CallTypeRepository.Delete(CallType);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, CallType, nameof(CallTypeService));
                return CallType;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<CallType>> BulkDelete(List<CallType> CallTypes)
        {
            if (!await CallTypeValidator.BulkDelete(CallTypes))
                return CallTypes;

            try
            {
                await UOW.Begin();
                await UOW.CallTypeRepository.BulkDelete(CallTypes);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, CallTypes, nameof(CallTypeService));
                return CallTypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<CallType>> Import(List<CallType> CallTypes)
        {
            if (!await CallTypeValidator.Import(CallTypes))
                return CallTypes;
            try
            {
                await UOW.Begin();
                await UOW.CallTypeRepository.BulkMerge(CallTypes);
                await UOW.Commit();

                await Logging.CreateAuditLog(CallTypes, new { }, nameof(CallTypeService));
                return CallTypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public CallTypeFilter ToFilter(CallTypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CallTypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CallTypeFilter subFilter = new CallTypeFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ColorCode))
                        
                        
                        
                        
                        
                        
                        subFilter.ColorCode = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
