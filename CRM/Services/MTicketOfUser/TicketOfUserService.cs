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

namespace CRM.Services.MTicketOfUser
{
    public interface ITicketOfUserService :  IServiceScoped
    {
        Task<int> Count(TicketOfUserFilter TicketOfUserFilter);
        Task<List<TicketOfUser>> List(TicketOfUserFilter TicketOfUserFilter);
        Task<TicketOfUser> Get(long Id);
        Task<TicketOfUser> GetByTicketId(long Id);
        Task<TicketOfUser> Create(TicketOfUser TicketOfUser);
        Task<TicketOfUser> Update(TicketOfUser TicketOfUser);
        Task<TicketOfUser> Delete(TicketOfUser TicketOfUser);
        Task<List<TicketOfUser>> BulkDelete(List<TicketOfUser> TicketOfUsers);
        Task<List<TicketOfUser>> Import(List<TicketOfUser> TicketOfUsers);
        TicketOfUserFilter ToFilter(TicketOfUserFilter TicketOfUserFilter);
    }

    public class TicketOfUserService : BaseService, ITicketOfUserService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ITicketOfUserValidator TicketOfUserValidator;

        public TicketOfUserService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ITicketOfUserValidator TicketOfUserValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.TicketOfUserValidator = TicketOfUserValidator;
        }
        public async Task<int> Count(TicketOfUserFilter TicketOfUserFilter)
        {
            try
            {
                int result = await UOW.TicketOfUserRepository.Count(TicketOfUserFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketOfUser>> List(TicketOfUserFilter TicketOfUserFilter)
        {
            try
            {
                List<TicketOfUser> TicketOfUsers = await UOW.TicketOfUserRepository.List(TicketOfUserFilter);
                return TicketOfUsers;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<TicketOfUser> Get(long Id)
        {
            TicketOfUser TicketOfUser = await UOW.TicketOfUserRepository.Get(Id);
            if (TicketOfUser == null)
                return null;
            return TicketOfUser;
        }
        public async Task<TicketOfUser> GetByTicketId(long Id)
        {
            TicketOfUser TicketOfUser = await UOW.TicketOfUserRepository.GetByTicketId(Id);
            if (TicketOfUser == null)
                return null;
            return TicketOfUser;
        }

        public async Task<TicketOfUser> Create(TicketOfUser TicketOfUser)
        {
            if (!await TicketOfUserValidator.Create(TicketOfUser))
                return TicketOfUser;

            try
            {
                await UOW.Begin();
                await UOW.TicketOfUserRepository.Create(TicketOfUser);
                await UOW.Commit();
                TicketOfUser = await UOW.TicketOfUserRepository.Get(TicketOfUser.Id);
                await Logging.CreateAuditLog(TicketOfUser, new { }, nameof(TicketOfUserService));
                return TicketOfUser;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketOfUser> Update(TicketOfUser TicketOfUser)
        {
            if (!await TicketOfUserValidator.Update(TicketOfUser))
                return TicketOfUser;
            try
            {
                var oldData = await UOW.TicketOfUserRepository.Get(TicketOfUser.Id);

                await UOW.Begin();
                await UOW.TicketOfUserRepository.Update(TicketOfUser);
                await UOW.Commit();

                TicketOfUser = await UOW.TicketOfUserRepository.Get(TicketOfUser.Id);
                await Logging.CreateAuditLog(TicketOfUser, oldData, nameof(TicketOfUserService));
                return TicketOfUser;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketOfUser> Delete(TicketOfUser TicketOfUser)
        {
            if (!await TicketOfUserValidator.Delete(TicketOfUser))
                return TicketOfUser;

            try
            {
                await UOW.Begin();
                await UOW.TicketOfUserRepository.Delete(TicketOfUser);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketOfUser, nameof(TicketOfUserService));
                return TicketOfUser;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketOfUser>> BulkDelete(List<TicketOfUser> TicketOfUsers)
        {
            if (!await TicketOfUserValidator.BulkDelete(TicketOfUsers))
                return TicketOfUsers;

            try
            {
                await UOW.Begin();
                await UOW.TicketOfUserRepository.BulkDelete(TicketOfUsers);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketOfUsers, nameof(TicketOfUserService));
                return TicketOfUsers;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<TicketOfUser>> Import(List<TicketOfUser> TicketOfUsers)
        {
            if (!await TicketOfUserValidator.Import(TicketOfUsers))
                return TicketOfUsers;
            try
            {
                await UOW.Begin();
                await UOW.TicketOfUserRepository.BulkMerge(TicketOfUsers);
                await UOW.Commit();

                await Logging.CreateAuditLog(TicketOfUsers, new { }, nameof(TicketOfUserService));
                return TicketOfUsers;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketOfUserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketOfUserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public TicketOfUserFilter ToFilter(TicketOfUserFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<TicketOfUserFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                TicketOfUserFilter subFilter = new TicketOfUserFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Notes))
                        
                        
                        
                        
                        
                        
                        subFilter.Notes = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.UserId))
                        subFilter.UserId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.TicketId))
                        subFilter.TicketId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.TicketStatusId))
                        subFilter.TicketStatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
