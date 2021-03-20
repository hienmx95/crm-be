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

namespace CRM.Services.MSLAEscalationUser
{
    public interface ISLAEscalationUserService :  IServiceScoped
    {
        Task<int> Count(SLAEscalationUserFilter SLAEscalationUserFilter);
        Task<List<SLAEscalationUser>> List(SLAEscalationUserFilter SLAEscalationUserFilter);
        Task<SLAEscalationUser> Get(long Id);
        Task<SLAEscalationUser> Create(SLAEscalationUser SLAEscalationUser);
        Task<SLAEscalationUser> Update(SLAEscalationUser SLAEscalationUser);
        Task<SLAEscalationUser> Delete(SLAEscalationUser SLAEscalationUser);
        Task<List<SLAEscalationUser>> BulkDelete(List<SLAEscalationUser> SLAEscalationUsers);
        Task<List<SLAEscalationUser>> Import(List<SLAEscalationUser> SLAEscalationUsers);
        SLAEscalationUserFilter ToFilter(SLAEscalationUserFilter SLAEscalationUserFilter);
    }

    public class SLAEscalationUserService : BaseService, ISLAEscalationUserService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAEscalationUserValidator SLAEscalationUserValidator;

        public SLAEscalationUserService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAEscalationUserValidator SLAEscalationUserValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAEscalationUserValidator = SLAEscalationUserValidator;
        }
        public async Task<int> Count(SLAEscalationUserFilter SLAEscalationUserFilter)
        {
            try
            {
                int result = await UOW.SLAEscalationUserRepository.Count(SLAEscalationUserFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationUser>> List(SLAEscalationUserFilter SLAEscalationUserFilter)
        {
            try
            {
                List<SLAEscalationUser> SLAEscalationUsers = await UOW.SLAEscalationUserRepository.List(SLAEscalationUserFilter);
                return SLAEscalationUsers;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAEscalationUser> Get(long Id)
        {
            SLAEscalationUser SLAEscalationUser = await UOW.SLAEscalationUserRepository.Get(Id);
            if (SLAEscalationUser == null)
                return null;
            return SLAEscalationUser;
        }
       
        public async Task<SLAEscalationUser> Create(SLAEscalationUser SLAEscalationUser)
        {
            if (!await SLAEscalationUserValidator.Create(SLAEscalationUser))
                return SLAEscalationUser;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationUserRepository.Create(SLAEscalationUser);
                await UOW.Commit();
                SLAEscalationUser = await UOW.SLAEscalationUserRepository.Get(SLAEscalationUser.Id);
                await Logging.CreateAuditLog(SLAEscalationUser, new { }, nameof(SLAEscalationUserService));
                return SLAEscalationUser;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationUser> Update(SLAEscalationUser SLAEscalationUser)
        {
            if (!await SLAEscalationUserValidator.Update(SLAEscalationUser))
                return SLAEscalationUser;
            try
            {
                var oldData = await UOW.SLAEscalationUserRepository.Get(SLAEscalationUser.Id);

                await UOW.Begin();
                await UOW.SLAEscalationUserRepository.Update(SLAEscalationUser);
                await UOW.Commit();

                SLAEscalationUser = await UOW.SLAEscalationUserRepository.Get(SLAEscalationUser.Id);
                await Logging.CreateAuditLog(SLAEscalationUser, oldData, nameof(SLAEscalationUserService));
                return SLAEscalationUser;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationUser> Delete(SLAEscalationUser SLAEscalationUser)
        {
            if (!await SLAEscalationUserValidator.Delete(SLAEscalationUser))
                return SLAEscalationUser;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationUserRepository.Delete(SLAEscalationUser);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationUser, nameof(SLAEscalationUserService));
                return SLAEscalationUser;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationUser>> BulkDelete(List<SLAEscalationUser> SLAEscalationUsers)
        {
            if (!await SLAEscalationUserValidator.BulkDelete(SLAEscalationUsers))
                return SLAEscalationUsers;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationUserRepository.BulkDelete(SLAEscalationUsers);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationUsers, nameof(SLAEscalationUserService));
                return SLAEscalationUsers;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAEscalationUser>> Import(List<SLAEscalationUser> SLAEscalationUsers)
        {
            if (!await SLAEscalationUserValidator.Import(SLAEscalationUsers))
                return SLAEscalationUsers;
            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationUserRepository.BulkMerge(SLAEscalationUsers);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAEscalationUsers, new { }, nameof(SLAEscalationUserService));
                return SLAEscalationUsers;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAEscalationUserFilter ToFilter(SLAEscalationUserFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAEscalationUserFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAEscalationUserFilter subFilter = new SLAEscalationUserFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.SLAEscalationId))
                        subFilter.SLAEscalationId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
