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

namespace CRM.Services.MScheduleMaster
{
    public interface IScheduleMasterService :  IServiceScoped
    {
        Task<int> Count(ScheduleMasterFilter ScheduleMasterFilter);
        Task<List<ScheduleMaster>> List(ScheduleMasterFilter ScheduleMasterFilter);
        Task<ScheduleMaster> Get(long Id);
        Task<ScheduleMaster> Create(ScheduleMaster ScheduleMaster);
        Task<ScheduleMaster> Update(ScheduleMaster ScheduleMaster);
        Task<ScheduleMaster> Delete(ScheduleMaster ScheduleMaster);
        Task<List<ScheduleMaster>> BulkDelete(List<ScheduleMaster> ScheduleMasters);
        Task<List<ScheduleMaster>> Import(List<ScheduleMaster> ScheduleMasters);
        ScheduleMasterFilter ToFilter(ScheduleMasterFilter ScheduleMasterFilter);
    }

    public class ScheduleMasterService : BaseService, IScheduleMasterService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IScheduleMasterValidator ScheduleMasterValidator;

        public ScheduleMasterService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IScheduleMasterValidator ScheduleMasterValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.ScheduleMasterValidator = ScheduleMasterValidator;
        }
        public async Task<int> Count(ScheduleMasterFilter ScheduleMasterFilter)
        {
            try
            {
                int result = await UOW.ScheduleMasterRepository.Count(ScheduleMasterFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex.InnerException, nameof(ScheduleMasterService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }

        public async Task<List<ScheduleMaster>> List(ScheduleMasterFilter ScheduleMasterFilter)
        {
            try
            {
                List<ScheduleMaster> ScheduleMasters = await UOW.ScheduleMasterRepository.List(ScheduleMasterFilter);
                return ScheduleMasters;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex.InnerException, nameof(ScheduleMasterService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }
        public async Task<ScheduleMaster> Get(long Id)
        {
            ScheduleMaster ScheduleMaster = await UOW.ScheduleMasterRepository.Get(Id);
            if (ScheduleMaster == null)
                return null;
            return ScheduleMaster;
        }
       
        public async Task<ScheduleMaster> Create(ScheduleMaster ScheduleMaster)
        {
            if (!await ScheduleMasterValidator.Create(ScheduleMaster))
                return ScheduleMaster;

            try
            {
                await UOW.Begin();
                await UOW.ScheduleMasterRepository.Create(ScheduleMaster);
                await UOW.Commit();

                await Logging.CreateAuditLog(ScheduleMaster, new { }, nameof(ScheduleMasterService));
                return await UOW.ScheduleMasterRepository.Get(ScheduleMaster.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await Logging.CreateSystemLog(ex.InnerException, nameof(ScheduleMasterService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }

        public async Task<ScheduleMaster> Update(ScheduleMaster ScheduleMaster)
        {
            if (!await ScheduleMasterValidator.Update(ScheduleMaster))
                return ScheduleMaster;
            try
            {
                var oldData = await UOW.ScheduleMasterRepository.Get(ScheduleMaster.Id);

                await UOW.Begin();
                await UOW.ScheduleMasterRepository.Update(ScheduleMaster);
                await UOW.Commit();

                var newData = await UOW.ScheduleMasterRepository.Get(ScheduleMaster.Id);
                await Logging.CreateAuditLog(newData, oldData, nameof(ScheduleMasterService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await Logging.CreateSystemLog(ex.InnerException, nameof(ScheduleMasterService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }

        public async Task<ScheduleMaster> Delete(ScheduleMaster ScheduleMaster)
        {
            if (!await ScheduleMasterValidator.Delete(ScheduleMaster))
                return ScheduleMaster;

            try
            {
                await UOW.Begin();
                await UOW.ScheduleMasterRepository.Delete(ScheduleMaster);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, ScheduleMaster, nameof(ScheduleMasterService));
                return ScheduleMaster;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await Logging.CreateSystemLog(ex.InnerException, nameof(ScheduleMasterService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }

        public async Task<List<ScheduleMaster>> BulkDelete(List<ScheduleMaster> ScheduleMasters)
        {
            if (!await ScheduleMasterValidator.BulkDelete(ScheduleMasters))
                return ScheduleMasters;

            try
            {
                await UOW.Begin();
                await UOW.ScheduleMasterRepository.BulkDelete(ScheduleMasters);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, ScheduleMasters, nameof(ScheduleMasterService));
                return ScheduleMasters;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await Logging.CreateSystemLog(ex.InnerException, nameof(ScheduleMasterService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }
        
        public async Task<List<ScheduleMaster>> Import(List<ScheduleMaster> ScheduleMasters)
        {
            if (!await ScheduleMasterValidator.Import(ScheduleMasters))
                return ScheduleMasters;
            try
            {
                await UOW.Begin();
                await UOW.ScheduleMasterRepository.BulkMerge(ScheduleMasters);
                await UOW.Commit();

                await Logging.CreateAuditLog(ScheduleMasters, new { }, nameof(ScheduleMasterService));
                return ScheduleMasters;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await Logging.CreateSystemLog(ex.InnerException, nameof(ScheduleMasterService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }     
        
        public ScheduleMasterFilter ToFilter(ScheduleMasterFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<ScheduleMasterFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                ScheduleMasterFilter subFilter = new ScheduleMasterFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                //foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                //{
                //    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                //        subFilter.Id = Map(subFilter.Id, FilterPermissionDefinition);
                //    if (FilterPermissionDefinition.Name == nameof(subFilter.ManagerId))
                //        subFilter.ManagerId = Map(subFilter.ManagerId, FilterPermissionDefinition);
                //    if (FilterPermissionDefinition.Name == nameof(subFilter.SalerId))
                //        subFilter.SalerId = Map(subFilter.SalerId, FilterPermissionDefinition);
                //    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                //        subFilter.Name = Map(subFilter.Name, FilterPermissionDefinition);
                //    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                //        subFilter.Code = Map(subFilter.Code, FilterPermissionDefinition);
                //    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                //        subFilter.StatusId = Map(subFilter.StatusId, FilterPermissionDefinition);
                //    if (FilterPermissionDefinition.Name == nameof(subFilter.RecurDays))
                //        subFilter.RecurDays = Map(subFilter.RecurDays, FilterPermissionDefinition);
                //    if (FilterPermissionDefinition.Name == nameof(subFilter.StartDate))
                //        subFilter.StartDate = Map(subFilter.StartDate, FilterPermissionDefinition);
                //    if (FilterPermissionDefinition.Name == nameof(subFilter.EndDate))
                //        subFilter.EndDate = Map(subFilter.EndDate, FilterPermissionDefinition);
                //    if (FilterPermissionDefinition.Name == nameof(subFilter.StartDayOfWeek))
                //        subFilter.StartDayOfWeek = Map(subFilter.StartDayOfWeek, FilterPermissionDefinition);
                //    if (FilterPermissionDefinition.Name == nameof(subFilter.DisplayOrder))
                //        subFilter.DisplayOrder = Map(subFilter.DisplayOrder, FilterPermissionDefinition);
                //    if (FilterPermissionDefinition.Name == nameof(subFilter.Description))
                //        subFilter.Description = Map(subFilter.Description, FilterPermissionDefinition);
                //}
            }
            return filter;
        }
    }
}
