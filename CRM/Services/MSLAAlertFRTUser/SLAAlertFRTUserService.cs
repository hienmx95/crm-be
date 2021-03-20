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

namespace CRM.Services.MSLAAlertFRTUser
{
    public interface ISLAAlertFRTUserService :  IServiceScoped
    {
        Task<int> Count(SLAAlertFRTUserFilter SLAAlertFRTUserFilter);
        Task<List<SLAAlertFRTUser>> List(SLAAlertFRTUserFilter SLAAlertFRTUserFilter);
        Task<SLAAlertFRTUser> Get(long Id);
        Task<SLAAlertFRTUser> Create(SLAAlertFRTUser SLAAlertFRTUser);
        Task<SLAAlertFRTUser> Update(SLAAlertFRTUser SLAAlertFRTUser);
        Task<SLAAlertFRTUser> Delete(SLAAlertFRTUser SLAAlertFRTUser);
        Task<List<SLAAlertFRTUser>> BulkDelete(List<SLAAlertFRTUser> SLAAlertFRTUsers);
        Task<List<SLAAlertFRTUser>> Import(List<SLAAlertFRTUser> SLAAlertFRTUsers);
        SLAAlertFRTUserFilter ToFilter(SLAAlertFRTUserFilter SLAAlertFRTUserFilter);
    }

    public class SLAAlertFRTUserService : BaseService, ISLAAlertFRTUserService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAAlertFRTUserValidator SLAAlertFRTUserValidator;

        public SLAAlertFRTUserService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAAlertFRTUserValidator SLAAlertFRTUserValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAAlertFRTUserValidator = SLAAlertFRTUserValidator;
        }
        public async Task<int> Count(SLAAlertFRTUserFilter SLAAlertFRTUserFilter)
        {
            try
            {
                int result = await UOW.SLAAlertFRTUserRepository.Count(SLAAlertFRTUserFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertFRTUser>> List(SLAAlertFRTUserFilter SLAAlertFRTUserFilter)
        {
            try
            {
                List<SLAAlertFRTUser> SLAAlertFRTUsers = await UOW.SLAAlertFRTUserRepository.List(SLAAlertFRTUserFilter);
                return SLAAlertFRTUsers;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAAlertFRTUser> Get(long Id)
        {
            SLAAlertFRTUser SLAAlertFRTUser = await UOW.SLAAlertFRTUserRepository.Get(Id);
            if (SLAAlertFRTUser == null)
                return null;
            return SLAAlertFRTUser;
        }
       
        public async Task<SLAAlertFRTUser> Create(SLAAlertFRTUser SLAAlertFRTUser)
        {
            if (!await SLAAlertFRTUserValidator.Create(SLAAlertFRTUser))
                return SLAAlertFRTUser;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTUserRepository.Create(SLAAlertFRTUser);
                await UOW.Commit();
                SLAAlertFRTUser = await UOW.SLAAlertFRTUserRepository.Get(SLAAlertFRTUser.Id);
                await Logging.CreateAuditLog(SLAAlertFRTUser, new { }, nameof(SLAAlertFRTUserService));
                return SLAAlertFRTUser;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertFRTUser> Update(SLAAlertFRTUser SLAAlertFRTUser)
        {
            if (!await SLAAlertFRTUserValidator.Update(SLAAlertFRTUser))
                return SLAAlertFRTUser;
            try
            {
                var oldData = await UOW.SLAAlertFRTUserRepository.Get(SLAAlertFRTUser.Id);

                await UOW.Begin();
                await UOW.SLAAlertFRTUserRepository.Update(SLAAlertFRTUser);
                await UOW.Commit();

                SLAAlertFRTUser = await UOW.SLAAlertFRTUserRepository.Get(SLAAlertFRTUser.Id);
                await Logging.CreateAuditLog(SLAAlertFRTUser, oldData, nameof(SLAAlertFRTUserService));
                return SLAAlertFRTUser;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertFRTUser> Delete(SLAAlertFRTUser SLAAlertFRTUser)
        {
            if (!await SLAAlertFRTUserValidator.Delete(SLAAlertFRTUser))
                return SLAAlertFRTUser;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTUserRepository.Delete(SLAAlertFRTUser);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertFRTUser, nameof(SLAAlertFRTUserService));
                return SLAAlertFRTUser;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertFRTUser>> BulkDelete(List<SLAAlertFRTUser> SLAAlertFRTUsers)
        {
            if (!await SLAAlertFRTUserValidator.BulkDelete(SLAAlertFRTUsers))
                return SLAAlertFRTUsers;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTUserRepository.BulkDelete(SLAAlertFRTUsers);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertFRTUsers, nameof(SLAAlertFRTUserService));
                return SLAAlertFRTUsers;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAAlertFRTUser>> Import(List<SLAAlertFRTUser> SLAAlertFRTUsers)
        {
            if (!await SLAAlertFRTUserValidator.Import(SLAAlertFRTUsers))
                return SLAAlertFRTUsers;
            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTUserRepository.BulkMerge(SLAAlertFRTUsers);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAAlertFRTUsers, new { }, nameof(SLAAlertFRTUserService));
                return SLAAlertFRTUsers;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAAlertFRTUserFilter ToFilter(SLAAlertFRTUserFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAAlertFRTUserFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAAlertFRTUserFilter subFilter = new SLAAlertFRTUserFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.SLAAlertFRTId))
                        subFilter.SLAAlertFRTId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
