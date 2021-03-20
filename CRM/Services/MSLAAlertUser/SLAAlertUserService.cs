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

namespace CRM.Services.MSLAAlertUser
{
    public interface ISLAAlertUserService :  IServiceScoped
    {
        Task<int> Count(SLAAlertUserFilter SLAAlertUserFilter);
        Task<List<SLAAlertUser>> List(SLAAlertUserFilter SLAAlertUserFilter);
        Task<SLAAlertUser> Get(long Id);
        Task<SLAAlertUser> Create(SLAAlertUser SLAAlertUser);
        Task<SLAAlertUser> Update(SLAAlertUser SLAAlertUser);
        Task<SLAAlertUser> Delete(SLAAlertUser SLAAlertUser);
        Task<List<SLAAlertUser>> BulkDelete(List<SLAAlertUser> SLAAlertUsers);
        Task<List<SLAAlertUser>> Import(List<SLAAlertUser> SLAAlertUsers);
        SLAAlertUserFilter ToFilter(SLAAlertUserFilter SLAAlertUserFilter);
    }

    public class SLAAlertUserService : BaseService, ISLAAlertUserService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAAlertUserValidator SLAAlertUserValidator;

        public SLAAlertUserService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAAlertUserValidator SLAAlertUserValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAAlertUserValidator = SLAAlertUserValidator;
        }
        public async Task<int> Count(SLAAlertUserFilter SLAAlertUserFilter)
        {
            try
            {
                int result = await UOW.SLAAlertUserRepository.Count(SLAAlertUserFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertUser>> List(SLAAlertUserFilter SLAAlertUserFilter)
        {
            try
            {
                List<SLAAlertUser> SLAAlertUsers = await UOW.SLAAlertUserRepository.List(SLAAlertUserFilter);
                return SLAAlertUsers;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAAlertUser> Get(long Id)
        {
            SLAAlertUser SLAAlertUser = await UOW.SLAAlertUserRepository.Get(Id);
            if (SLAAlertUser == null)
                return null;
            return SLAAlertUser;
        }
       
        public async Task<SLAAlertUser> Create(SLAAlertUser SLAAlertUser)
        {
            if (!await SLAAlertUserValidator.Create(SLAAlertUser))
                return SLAAlertUser;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertUserRepository.Create(SLAAlertUser);
                await UOW.Commit();
                SLAAlertUser = await UOW.SLAAlertUserRepository.Get(SLAAlertUser.Id);
                await Logging.CreateAuditLog(SLAAlertUser, new { }, nameof(SLAAlertUserService));
                return SLAAlertUser;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertUser> Update(SLAAlertUser SLAAlertUser)
        {
            if (!await SLAAlertUserValidator.Update(SLAAlertUser))
                return SLAAlertUser;
            try
            {
                var oldData = await UOW.SLAAlertUserRepository.Get(SLAAlertUser.Id);

                await UOW.Begin();
                await UOW.SLAAlertUserRepository.Update(SLAAlertUser);
                await UOW.Commit();

                SLAAlertUser = await UOW.SLAAlertUserRepository.Get(SLAAlertUser.Id);
                await Logging.CreateAuditLog(SLAAlertUser, oldData, nameof(SLAAlertUserService));
                return SLAAlertUser;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertUser> Delete(SLAAlertUser SLAAlertUser)
        {
            if (!await SLAAlertUserValidator.Delete(SLAAlertUser))
                return SLAAlertUser;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertUserRepository.Delete(SLAAlertUser);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertUser, nameof(SLAAlertUserService));
                return SLAAlertUser;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertUser>> BulkDelete(List<SLAAlertUser> SLAAlertUsers)
        {
            if (!await SLAAlertUserValidator.BulkDelete(SLAAlertUsers))
                return SLAAlertUsers;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertUserRepository.BulkDelete(SLAAlertUsers);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertUsers, nameof(SLAAlertUserService));
                return SLAAlertUsers;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAAlertUser>> Import(List<SLAAlertUser> SLAAlertUsers)
        {
            if (!await SLAAlertUserValidator.Import(SLAAlertUsers))
                return SLAAlertUsers;
            try
            {
                await UOW.Begin();
                await UOW.SLAAlertUserRepository.BulkMerge(SLAAlertUsers);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAAlertUsers, new { }, nameof(SLAAlertUserService));
                return SLAAlertUsers;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAAlertUserFilter ToFilter(SLAAlertUserFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAAlertUserFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAAlertUserFilter subFilter = new SLAAlertUserFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.SLAAlertId))
                        subFilter.SLAAlertId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
