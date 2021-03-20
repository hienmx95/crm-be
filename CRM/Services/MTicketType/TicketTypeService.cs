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

namespace CRM.Services.MTicketType
{
    public interface ITicketTypeService :  IServiceScoped
    {
        Task<int> Count(TicketTypeFilter TicketTypeFilter);
        Task<List<TicketType>> List(TicketTypeFilter TicketTypeFilter);
        Task<TicketType> Get(long Id);
        Task<TicketType> Create(TicketType TicketType);
        Task<TicketType> Update(TicketType TicketType);
        Task<TicketType> Delete(TicketType TicketType);
        Task<List<TicketType>> BulkDelete(List<TicketType> TicketTypes);
        Task<List<TicketType>> Import(List<TicketType> TicketTypes);
        TicketTypeFilter ToFilter(TicketTypeFilter TicketTypeFilter);
    }

    public class TicketTypeService : BaseService, ITicketTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ITicketTypeValidator TicketTypeValidator;

        public TicketTypeService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ITicketTypeValidator TicketTypeValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.TicketTypeValidator = TicketTypeValidator;
        }
        public async Task<int> Count(TicketTypeFilter TicketTypeFilter)
        {
            try
            {
                int result = await UOW.TicketTypeRepository.Count(TicketTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex.InnerException, nameof(TicketTypeService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }

        public async Task<List<TicketType>> List(TicketTypeFilter TicketTypeFilter)
        {
            try
            {
                List<TicketType> TicketTypes = await UOW.TicketTypeRepository.List(TicketTypeFilter);
                return TicketTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex.InnerException, nameof(TicketTypeService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }
        public async Task<TicketType> Get(long Id)
        {
            TicketType TicketType = await UOW.TicketTypeRepository.Get(Id);
            if (TicketType == null)
                return null;
            return TicketType;
        }
       
        public async Task<TicketType> Create(TicketType TicketType)
        {
            if (!await TicketTypeValidator.Create(TicketType))
                return TicketType;

            try
            {
                await UOW.Begin();
                await UOW.TicketTypeRepository.Create(TicketType);
                await UOW.Commit();

                await Logging.CreateAuditLog(TicketType, new { }, nameof(TicketTypeService));
                return await UOW.TicketTypeRepository.Get(TicketType.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await Logging.CreateSystemLog(ex.InnerException, nameof(TicketTypeService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }

        public async Task<TicketType> Update(TicketType TicketType)
        {
            if (!await TicketTypeValidator.Update(TicketType))
                return TicketType;
            try
            {
                var oldData = await UOW.TicketTypeRepository.Get(TicketType.Id);

                await UOW.Begin();
                await UOW.TicketTypeRepository.Update(TicketType);
                await UOW.Commit();

                var newData = await UOW.TicketTypeRepository.Get(TicketType.Id);
                await Logging.CreateAuditLog(newData, oldData, nameof(TicketTypeService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await Logging.CreateSystemLog(ex.InnerException, nameof(TicketTypeService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }

        public async Task<TicketType> Delete(TicketType TicketType)
        {
            if (!await TicketTypeValidator.Delete(TicketType))
                return TicketType;

            try
            {
                await UOW.Begin();
                await UOW.TicketTypeRepository.Delete(TicketType);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketType, nameof(TicketTypeService));
                return TicketType;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await Logging.CreateSystemLog(ex.InnerException, nameof(TicketTypeService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }

        public async Task<List<TicketType>> BulkDelete(List<TicketType> TicketTypes)
        {
            if (!await TicketTypeValidator.BulkDelete(TicketTypes))
                return TicketTypes;

            try
            {
                await UOW.Begin();
                await UOW.TicketTypeRepository.BulkDelete(TicketTypes);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketTypes, nameof(TicketTypeService));
                return TicketTypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await Logging.CreateSystemLog(ex.InnerException, nameof(TicketTypeService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }
        
        public async Task<List<TicketType>> Import(List<TicketType> TicketTypes)
        {
            if (!await TicketTypeValidator.Import(TicketTypes))
                return TicketTypes;
            try
            {
                await UOW.Begin();
                await UOW.TicketTypeRepository.BulkMerge(TicketTypes);
                await UOW.Commit();

                await Logging.CreateAuditLog(TicketTypes, new { }, nameof(TicketTypeService));
                return TicketTypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await Logging.CreateSystemLog(ex.InnerException, nameof(TicketTypeService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }     
        
        public TicketTypeFilter ToFilter(TicketTypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<TicketTypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                TicketTypeFilter subFilter = new TicketTypeFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ColorCode))
                        subFilter.ColorCode = FilterPermissionDefinition.StringFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;
                }
            }
            return filter;
        }
    }
}
