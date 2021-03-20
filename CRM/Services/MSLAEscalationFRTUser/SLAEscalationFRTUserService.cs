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

namespace CRM.Services.MSLAEscalationFRTUser
{
    public interface ISLAEscalationFRTUserService :  IServiceScoped
    {
        Task<int> Count(SLAEscalationFRTUserFilter SLAEscalationFRTUserFilter);
        Task<List<SLAEscalationFRTUser>> List(SLAEscalationFRTUserFilter SLAEscalationFRTUserFilter);
        Task<SLAEscalationFRTUser> Get(long Id);
        Task<SLAEscalationFRTUser> Create(SLAEscalationFRTUser SLAEscalationFRTUser);
        Task<SLAEscalationFRTUser> Update(SLAEscalationFRTUser SLAEscalationFRTUser);
        Task<SLAEscalationFRTUser> Delete(SLAEscalationFRTUser SLAEscalationFRTUser);
        Task<List<SLAEscalationFRTUser>> BulkDelete(List<SLAEscalationFRTUser> SLAEscalationFRTUsers);
        Task<List<SLAEscalationFRTUser>> Import(List<SLAEscalationFRTUser> SLAEscalationFRTUsers);
        SLAEscalationFRTUserFilter ToFilter(SLAEscalationFRTUserFilter SLAEscalationFRTUserFilter);
    }

    public class SLAEscalationFRTUserService : BaseService, ISLAEscalationFRTUserService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAEscalationFRTUserValidator SLAEscalationFRTUserValidator;

        public SLAEscalationFRTUserService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAEscalationFRTUserValidator SLAEscalationFRTUserValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAEscalationFRTUserValidator = SLAEscalationFRTUserValidator;
        }
        public async Task<int> Count(SLAEscalationFRTUserFilter SLAEscalationFRTUserFilter)
        {
            try
            {
                int result = await UOW.SLAEscalationFRTUserRepository.Count(SLAEscalationFRTUserFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationFRTUser>> List(SLAEscalationFRTUserFilter SLAEscalationFRTUserFilter)
        {
            try
            {
                List<SLAEscalationFRTUser> SLAEscalationFRTUsers = await UOW.SLAEscalationFRTUserRepository.List(SLAEscalationFRTUserFilter);
                return SLAEscalationFRTUsers;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAEscalationFRTUser> Get(long Id)
        {
            SLAEscalationFRTUser SLAEscalationFRTUser = await UOW.SLAEscalationFRTUserRepository.Get(Id);
            if (SLAEscalationFRTUser == null)
                return null;
            return SLAEscalationFRTUser;
        }
       
        public async Task<SLAEscalationFRTUser> Create(SLAEscalationFRTUser SLAEscalationFRTUser)
        {
            if (!await SLAEscalationFRTUserValidator.Create(SLAEscalationFRTUser))
                return SLAEscalationFRTUser;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTUserRepository.Create(SLAEscalationFRTUser);
                await UOW.Commit();
                SLAEscalationFRTUser = await UOW.SLAEscalationFRTUserRepository.Get(SLAEscalationFRTUser.Id);
                await Logging.CreateAuditLog(SLAEscalationFRTUser, new { }, nameof(SLAEscalationFRTUserService));
                return SLAEscalationFRTUser;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationFRTUser> Update(SLAEscalationFRTUser SLAEscalationFRTUser)
        {
            if (!await SLAEscalationFRTUserValidator.Update(SLAEscalationFRTUser))
                return SLAEscalationFRTUser;
            try
            {
                var oldData = await UOW.SLAEscalationFRTUserRepository.Get(SLAEscalationFRTUser.Id);

                await UOW.Begin();
                await UOW.SLAEscalationFRTUserRepository.Update(SLAEscalationFRTUser);
                await UOW.Commit();

                SLAEscalationFRTUser = await UOW.SLAEscalationFRTUserRepository.Get(SLAEscalationFRTUser.Id);
                await Logging.CreateAuditLog(SLAEscalationFRTUser, oldData, nameof(SLAEscalationFRTUserService));
                return SLAEscalationFRTUser;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationFRTUser> Delete(SLAEscalationFRTUser SLAEscalationFRTUser)
        {
            if (!await SLAEscalationFRTUserValidator.Delete(SLAEscalationFRTUser))
                return SLAEscalationFRTUser;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTUserRepository.Delete(SLAEscalationFRTUser);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationFRTUser, nameof(SLAEscalationFRTUserService));
                return SLAEscalationFRTUser;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationFRTUser>> BulkDelete(List<SLAEscalationFRTUser> SLAEscalationFRTUsers)
        {
            if (!await SLAEscalationFRTUserValidator.BulkDelete(SLAEscalationFRTUsers))
                return SLAEscalationFRTUsers;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTUserRepository.BulkDelete(SLAEscalationFRTUsers);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationFRTUsers, nameof(SLAEscalationFRTUserService));
                return SLAEscalationFRTUsers;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAEscalationFRTUser>> Import(List<SLAEscalationFRTUser> SLAEscalationFRTUsers)
        {
            if (!await SLAEscalationFRTUserValidator.Import(SLAEscalationFRTUsers))
                return SLAEscalationFRTUsers;
            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTUserRepository.BulkMerge(SLAEscalationFRTUsers);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAEscalationFRTUsers, new { }, nameof(SLAEscalationFRTUserService));
                return SLAEscalationFRTUsers;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAEscalationFRTUserFilter ToFilter(SLAEscalationFRTUserFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAEscalationFRTUserFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAEscalationFRTUserFilter subFilter = new SLAEscalationFRTUserFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.SLAEscalationFRTId))
                        subFilter.SLAEscalationFRTId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
