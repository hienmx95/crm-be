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

namespace CRM.Services.MTicketOfDepartment
{
    public interface ITicketOfDepartmentService :  IServiceScoped
    {
        Task<int> Count(TicketOfDepartmentFilter TicketOfDepartmentFilter);
        Task<List<TicketOfDepartment>> List(TicketOfDepartmentFilter TicketOfDepartmentFilter);
        Task<TicketOfDepartment> Get(long Id);
        Task<TicketOfDepartment> Create(TicketOfDepartment TicketOfDepartment);
        Task<TicketOfDepartment> Update(TicketOfDepartment TicketOfDepartment);
        Task<TicketOfDepartment> Delete(TicketOfDepartment TicketOfDepartment);
        Task<List<TicketOfDepartment>> BulkDelete(List<TicketOfDepartment> TicketOfDepartments);
        Task<List<TicketOfDepartment>> Import(List<TicketOfDepartment> TicketOfDepartments);
        TicketOfDepartmentFilter ToFilter(TicketOfDepartmentFilter TicketOfDepartmentFilter);
    }

    public class TicketOfDepartmentService : BaseService, ITicketOfDepartmentService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ITicketOfDepartmentValidator TicketOfDepartmentValidator;

        public TicketOfDepartmentService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ITicketOfDepartmentValidator TicketOfDepartmentValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.TicketOfDepartmentValidator = TicketOfDepartmentValidator;
        }
        public async Task<int> Count(TicketOfDepartmentFilter TicketOfDepartmentFilter)
        {
            try
            {
                int result = await UOW.TicketOfDepartmentRepository.Count(TicketOfDepartmentFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketOfDepartment>> List(TicketOfDepartmentFilter TicketOfDepartmentFilter)
        {
            try
            {
                List<TicketOfDepartment> TicketOfDepartments = await UOW.TicketOfDepartmentRepository.List(TicketOfDepartmentFilter);
                return TicketOfDepartments;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<TicketOfDepartment> Get(long Id)
        {
            TicketOfDepartment TicketOfDepartment = await UOW.TicketOfDepartmentRepository.Get(Id);
            if (TicketOfDepartment == null)
                return null;
            return TicketOfDepartment;
        }
       
        public async Task<TicketOfDepartment> Create(TicketOfDepartment TicketOfDepartment)
        {
            if (!await TicketOfDepartmentValidator.Create(TicketOfDepartment))
                return TicketOfDepartment;

            try
            {
                await UOW.Begin();
                await UOW.TicketOfDepartmentRepository.Create(TicketOfDepartment);
                await UOW.Commit();
                TicketOfDepartment = await UOW.TicketOfDepartmentRepository.Get(TicketOfDepartment.Id);
                await Logging.CreateAuditLog(TicketOfDepartment, new { }, nameof(TicketOfDepartmentService));
                return TicketOfDepartment;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketOfDepartment> Update(TicketOfDepartment TicketOfDepartment)
        {
            if (!await TicketOfDepartmentValidator.Update(TicketOfDepartment))
                return TicketOfDepartment;
            try
            {
                var oldData = await UOW.TicketOfDepartmentRepository.Get(TicketOfDepartment.Id);

                await UOW.Begin();
                await UOW.TicketOfDepartmentRepository.Update(TicketOfDepartment);
                await UOW.Commit();

                TicketOfDepartment = await UOW.TicketOfDepartmentRepository.Get(TicketOfDepartment.Id);
                await Logging.CreateAuditLog(TicketOfDepartment, oldData, nameof(TicketOfDepartmentService));
                return TicketOfDepartment;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketOfDepartment> Delete(TicketOfDepartment TicketOfDepartment)
        {
            if (!await TicketOfDepartmentValidator.Delete(TicketOfDepartment))
                return TicketOfDepartment;

            try
            {
                await UOW.Begin();
                await UOW.TicketOfDepartmentRepository.Delete(TicketOfDepartment);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketOfDepartment, nameof(TicketOfDepartmentService));
                return TicketOfDepartment;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketOfDepartment>> BulkDelete(List<TicketOfDepartment> TicketOfDepartments)
        {
            if (!await TicketOfDepartmentValidator.BulkDelete(TicketOfDepartments))
                return TicketOfDepartments;

            try
            {
                await UOW.Begin();
                await UOW.TicketOfDepartmentRepository.BulkDelete(TicketOfDepartments);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketOfDepartments, nameof(TicketOfDepartmentService));
                return TicketOfDepartments;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<TicketOfDepartment>> Import(List<TicketOfDepartment> TicketOfDepartments)
        {
            if (!await TicketOfDepartmentValidator.Import(TicketOfDepartments))
                return TicketOfDepartments;
            try
            {
                await UOW.Begin();
                await UOW.TicketOfDepartmentRepository.BulkMerge(TicketOfDepartments);
                await UOW.Commit();

                await Logging.CreateAuditLog(TicketOfDepartments, new { }, nameof(TicketOfDepartmentService));
                return TicketOfDepartments;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfDepartmentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public TicketOfDepartmentFilter ToFilter(TicketOfDepartmentFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<TicketOfDepartmentFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                TicketOfDepartmentFilter subFilter = new TicketOfDepartmentFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Notes))
                        
                        
                        
                        
                        
                        
                        subFilter.Notes = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DepartmentId))
                        subFilter.DepartmentId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.TicketId))
                        subFilter.TicketId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.TicketStatusId))
                        subFilter.TicketStatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
