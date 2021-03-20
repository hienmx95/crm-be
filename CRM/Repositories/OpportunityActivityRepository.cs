using CRM.Common;
using CRM.Helpers;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface IOpportunityActivityRepository
    {
        Task<int> Count(OpportunityActivityFilter OpportunityActivityFilter);
        Task<List<OpportunityActivity>> List(OpportunityActivityFilter OpportunityActivityFilter);
        Task<List<OpportunityActivity>> List(List<long> Ids);
        Task<OpportunityActivity> Get(long Id);
        Task<bool> Create(OpportunityActivity OpportunityActivity);
        Task<bool> Update(OpportunityActivity OpportunityActivity);
        Task<bool> Delete(OpportunityActivity OpportunityActivity);
        Task<bool> BulkMerge(List<OpportunityActivity> OpportunityActivities);
        Task<bool> BulkDelete(List<OpportunityActivity> OpportunityActivities);
    }
    public class OpportunityActivityRepository : IOpportunityActivityRepository
    {
        private DataContext DataContext;
        public OpportunityActivityRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<OpportunityActivityDAO> DynamicFilter(IQueryable<OpportunityActivityDAO> query, OpportunityActivityFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null && filter.CreatedAt.HasValue)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null && filter.UpdatedAt.HasValue)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Title != null && filter.Title.HasValue)
                query = query.Where(q => q.Title, filter.Title);
            if (filter.FromDate != null && filter.FromDate.HasValue)
                query = query.Where(q => q.FromDate, filter.FromDate);
            if (filter.ToDate != null && filter.ToDate.HasValue)
                query = query.Where(q => q.ToDate == null).Union(query.Where(q => q.ToDate.HasValue).Where(q => q.ToDate, filter.ToDate));
            if (filter.ActivityTypeId != null && filter.ActivityTypeId.HasValue)
                query = query.Where(q => q.ActivityTypeId, filter.ActivityTypeId);
            if (filter.ActivityPriorityId != null && filter.ActivityPriorityId.HasValue)
                query = query.Where(q => q.ActivityPriorityId.HasValue).Where(q => q.ActivityPriorityId, filter.ActivityPriorityId);
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.Address != null && filter.Address.HasValue)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.OpportunityId != null && filter.OpportunityId.HasValue)
                query = query.Where(q => q.OpportunityId, filter.OpportunityId);
            if (filter.AppUserId != null && filter.AppUserId.HasValue)
                query = query.Where(q => q.AppUserId, filter.AppUserId);
            if (filter.ActivityStatusId != null && filter.ActivityStatusId.HasValue)
                query = query.Where(q => q.ActivityStatusId, filter.ActivityStatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<OpportunityActivityDAO> OrFilter(IQueryable<OpportunityActivityDAO> query, OpportunityActivityFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<OpportunityActivityDAO> initQuery = query.Where(q => false);
            foreach (OpportunityActivityFilter OpportunityActivityFilter in filter.OrFilter)
            {
                IQueryable<OpportunityActivityDAO> queryable = query;
                if (OpportunityActivityFilter.Id != null && OpportunityActivityFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, OpportunityActivityFilter.Id);
                if (OpportunityActivityFilter.Title != null && OpportunityActivityFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, OpportunityActivityFilter.Title);
                if (OpportunityActivityFilter.FromDate != null && OpportunityActivityFilter.FromDate.HasValue)
                    queryable = queryable.Where(q => q.FromDate, OpportunityActivityFilter.FromDate);
                if (OpportunityActivityFilter.ToDate != null && OpportunityActivityFilter.ToDate.HasValue)
                    queryable = queryable.Where(q => q.ToDate.HasValue).Where(q => q.ToDate, OpportunityActivityFilter.ToDate);
                if (OpportunityActivityFilter.ActivityTypeId != null && OpportunityActivityFilter.ActivityTypeId.HasValue)
                    queryable = queryable.Where(q => q.ActivityTypeId, OpportunityActivityFilter.ActivityTypeId);
                if (OpportunityActivityFilter.ActivityPriorityId != null && OpportunityActivityFilter.ActivityPriorityId.HasValue)
                    queryable = queryable.Where(q => q.ActivityPriorityId.HasValue).Where(q => q.ActivityPriorityId, OpportunityActivityFilter.ActivityPriorityId);
                if (OpportunityActivityFilter.Description != null && OpportunityActivityFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, OpportunityActivityFilter.Description);
                if (OpportunityActivityFilter.Address != null && OpportunityActivityFilter.Address.HasValue)
                    queryable = queryable.Where(q => q.Address, OpportunityActivityFilter.Address);
                if (OpportunityActivityFilter.OpportunityId != null && OpportunityActivityFilter.OpportunityId.HasValue)
                    queryable = queryable.Where(q => q.OpportunityId, OpportunityActivityFilter.OpportunityId);
                if (OpportunityActivityFilter.AppUserId != null && OpportunityActivityFilter.AppUserId.HasValue)
                    queryable = queryable.Where(q => q.AppUserId, OpportunityActivityFilter.AppUserId);
                if (OpportunityActivityFilter.ActivityStatusId != null && OpportunityActivityFilter.ActivityStatusId.HasValue)
                    queryable = queryable.Where(q => q.ActivityStatusId, OpportunityActivityFilter.ActivityStatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<OpportunityActivityDAO> DynamicOrder(IQueryable<OpportunityActivityDAO> query, OpportunityActivityFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case OpportunityActivityOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case OpportunityActivityOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case OpportunityActivityOrder.FromDate:
                            query = query.OrderBy(q => q.FromDate);
                            break;
                        case OpportunityActivityOrder.ToDate:
                            query = query.OrderBy(q => q.ToDate);
                            break;
                        case OpportunityActivityOrder.ActivityType:
                            query = query.OrderBy(q => q.ActivityTypeId);
                            break;
                        case OpportunityActivityOrder.ActivityPriority:
                            query = query.OrderBy(q => q.ActivityPriorityId);
                            break;
                        case OpportunityActivityOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case OpportunityActivityOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case OpportunityActivityOrder.Opportunity:
                            query = query.OrderBy(q => q.OpportunityId);
                            break;
                        case OpportunityActivityOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case OpportunityActivityOrder.ActivityStatus:
                            query = query.OrderBy(q => q.ActivityStatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case OpportunityActivityOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case OpportunityActivityOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case OpportunityActivityOrder.FromDate:
                            query = query.OrderByDescending(q => q.FromDate);
                            break;
                        case OpportunityActivityOrder.ToDate:
                            query = query.OrderByDescending(q => q.ToDate);
                            break;
                        case OpportunityActivityOrder.ActivityType:
                            query = query.OrderByDescending(q => q.ActivityTypeId);
                            break;
                        case OpportunityActivityOrder.ActivityPriority:
                            query = query.OrderByDescending(q => q.ActivityPriorityId);
                            break;
                        case OpportunityActivityOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case OpportunityActivityOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case OpportunityActivityOrder.Opportunity:
                            query = query.OrderByDescending(q => q.OpportunityId);
                            break;
                        case OpportunityActivityOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case OpportunityActivityOrder.ActivityStatus:
                            query = query.OrderByDescending(q => q.ActivityStatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<OpportunityActivity>> DynamicSelect(IQueryable<OpportunityActivityDAO> query, OpportunityActivityFilter filter)
        {
            List<OpportunityActivity> OpportunityActivities = await query.Select(q => new OpportunityActivity()
            {
                Id = filter.Selects.Contains(OpportunityActivitySelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(OpportunityActivitySelect.Title) ? q.Title : default(string),
                FromDate = filter.Selects.Contains(OpportunityActivitySelect.FromDate) ? q.FromDate : default(DateTime),
                ToDate = filter.Selects.Contains(OpportunityActivitySelect.ToDate) ? q.ToDate : default(DateTime?),
                ActivityTypeId = filter.Selects.Contains(OpportunityActivitySelect.ActivityType) ? q.ActivityTypeId : default(long),
                ActivityPriorityId = filter.Selects.Contains(OpportunityActivitySelect.ActivityPriority) ? q.ActivityPriorityId : default(long?),
                Description = filter.Selects.Contains(OpportunityActivitySelect.Description) ? q.Description : default(string),
                Address = filter.Selects.Contains(OpportunityActivitySelect.Address) ? q.Address : default(string),
                OpportunityId = filter.Selects.Contains(OpportunityActivitySelect.Opportunity) ? q.OpportunityId : default(long),
                AppUserId = filter.Selects.Contains(OpportunityActivitySelect.AppUser) ? q.AppUserId : default(long),
                ActivityStatusId = filter.Selects.Contains(OpportunityActivitySelect.ActivityStatus) ? q.ActivityStatusId : default(long),
                ActivityPriority = filter.Selects.Contains(OpportunityActivitySelect.ActivityPriority) && q.ActivityPriority != null ? new ActivityPriority
                {
                    Id = q.ActivityPriority.Id,
                    Code = q.ActivityPriority.Code,
                    Name = q.ActivityPriority.Name,
                } : null,
                ActivityStatus = filter.Selects.Contains(OpportunityActivitySelect.ActivityStatus) && q.ActivityStatus != null ? new ActivityStatus
                {
                    Id = q.ActivityStatus.Id,
                    Code = q.ActivityStatus.Code,
                    Name = q.ActivityStatus.Name,
                } : null,
                ActivityType = filter.Selects.Contains(OpportunityActivitySelect.ActivityType) && q.ActivityType != null ? new ActivityType
                {
                    Id = q.ActivityType.Id,
                    Code = q.ActivityType.Code,
                    Name = q.ActivityType.Name,
                } : null,
                AppUser = filter.Selects.Contains(OpportunityActivitySelect.AppUser) && q.AppUser != null ? new AppUser
                {
                    Id = q.AppUser.Id,
                    Username = q.AppUser.Username,
                    DisplayName = q.AppUser.DisplayName,
                    Address = q.AppUser.Address,
                    Email = q.AppUser.Email,
                    Phone = q.AppUser.Phone,
                    SexId = q.AppUser.SexId,
                    Birthday = q.AppUser.Birthday,
                    Avatar = q.AppUser.Avatar,
                    Department = q.AppUser.Department,
                    OrganizationId = q.AppUser.OrganizationId,
                    Longitude = q.AppUser.Longitude,
                    Latitude = q.AppUser.Latitude,
                    StatusId = q.AppUser.StatusId,
                    RowId = q.AppUser.RowId,
                    Used = q.AppUser.Used,
                } : null,
                Opportunity = filter.Selects.Contains(OpportunityActivitySelect.Opportunity) && q.Opportunity != null ? new Opportunity
                {
                    Id = q.Opportunity.Id,
                    Name = q.Opportunity.Name,
                    CompanyId = q.Opportunity.CompanyId,
                    CustomerLeadId = q.Opportunity.CustomerLeadId,
                    ClosingDate = q.Opportunity.ClosingDate,
                    SaleStageId = q.Opportunity.SaleStageId,
                    ProbabilityId = q.Opportunity.ProbabilityId,
                    PotentialResultId = q.Opportunity.PotentialResultId,
                    LeadSourceId = q.Opportunity.LeadSourceId,
                    AppUserId = q.Opportunity.AppUserId,
                    CurrencyId = q.Opportunity.CurrencyId,
                    Amount = q.Opportunity.Amount,
                    ForecastAmount = q.Opportunity.ForecastAmount,
                    Description = q.Opportunity.Description,
                    CreatorId = q.Opportunity.CreatorId,
                    RefuseReciveSMS = q.Opportunity.RefuseReciveSMS,
                    RefuseReciveEmail = q.Opportunity.RefuseReciveEmail,
                    OpportunityResultTypeId = q.Opportunity.OpportunityResultTypeId,
                } : null,
            }).ToListAsync();
            return OpportunityActivities;
        }

        public async Task<int> Count(OpportunityActivityFilter filter)
        {
            IQueryable<OpportunityActivityDAO> OpportunityActivities = DataContext.OpportunityActivity.AsNoTracking();
            OpportunityActivities = DynamicFilter(OpportunityActivities, filter);
            return await OpportunityActivities.CountAsync();
        }

        public async Task<List<OpportunityActivity>> List(OpportunityActivityFilter filter)
        {
            if (filter == null) return new List<OpportunityActivity>();
            IQueryable<OpportunityActivityDAO> OpportunityActivityDAOs = DataContext.OpportunityActivity.AsNoTracking();
            OpportunityActivityDAOs = DynamicFilter(OpportunityActivityDAOs, filter);
            OpportunityActivityDAOs = DynamicOrder(OpportunityActivityDAOs, filter);
            List<OpportunityActivity> OpportunityActivities = await DynamicSelect(OpportunityActivityDAOs, filter);
            return OpportunityActivities;
        }

        public async Task<List<OpportunityActivity>> List(List<long> Ids)
        {
            List<OpportunityActivity> OpportunityActivities = await DataContext.OpportunityActivity.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new OpportunityActivity()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Title = x.Title,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                ActivityTypeId = x.ActivityTypeId,
                ActivityPriorityId = x.ActivityPriorityId,
                Description = x.Description,
                Address = x.Address,
                OpportunityId = x.OpportunityId,
                AppUserId = x.AppUserId,
                ActivityStatusId = x.ActivityStatusId,
                ActivityPriority = x.ActivityPriority == null ? null : new ActivityPriority
                {
                    Id = x.ActivityPriority.Id,
                    Code = x.ActivityPriority.Code,
                    Name = x.ActivityPriority.Name,
                },
                ActivityStatus = x.ActivityStatus == null ? null : new ActivityStatus
                {
                    Id = x.ActivityStatus.Id,
                    Code = x.ActivityStatus.Code,
                    Name = x.ActivityStatus.Name,
                },
                ActivityType = x.ActivityType == null ? null : new ActivityType
                {
                    Id = x.ActivityType.Id,
                    Code = x.ActivityType.Code,
                    Name = x.ActivityType.Name,
                },
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Address = x.AppUser.Address,
                    Email = x.AppUser.Email,
                    Phone = x.AppUser.Phone,
                    SexId = x.AppUser.SexId,
                    Birthday = x.AppUser.Birthday,
                    Avatar = x.AppUser.Avatar,
                    Department = x.AppUser.Department,
                    OrganizationId = x.AppUser.OrganizationId,
                    Longitude = x.AppUser.Longitude,
                    Latitude = x.AppUser.Latitude,
                    StatusId = x.AppUser.StatusId,
                    RowId = x.AppUser.RowId,
                    Used = x.AppUser.Used,
                },
                Opportunity = x.Opportunity == null ? null : new Opportunity
                {
                    Id = x.Opportunity.Id,
                    Name = x.Opportunity.Name,
                    CompanyId = x.Opportunity.CompanyId,
                    CustomerLeadId = x.Opportunity.CustomerLeadId,
                    ClosingDate = x.Opportunity.ClosingDate,
                    SaleStageId = x.Opportunity.SaleStageId,
                    ProbabilityId = x.Opportunity.ProbabilityId,
                    PotentialResultId = x.Opportunity.PotentialResultId,
                    LeadSourceId = x.Opportunity.LeadSourceId,
                    AppUserId = x.Opportunity.AppUserId,
                    CurrencyId = x.Opportunity.CurrencyId,
                    Amount = x.Opportunity.Amount,
                    ForecastAmount = x.Opportunity.ForecastAmount,
                    Description = x.Opportunity.Description,
                    CreatorId = x.Opportunity.CreatorId,
                    RefuseReciveSMS = x.Opportunity.RefuseReciveSMS,
                    RefuseReciveEmail = x.Opportunity.RefuseReciveEmail,
                    OpportunityResultTypeId = x.Opportunity.OpportunityResultTypeId,
                },
            }).ToListAsync();
            

            return OpportunityActivities;
        }

        public async Task<OpportunityActivity> Get(long Id)
        {
            OpportunityActivity OpportunityActivity = await DataContext.OpportunityActivity.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new OpportunityActivity()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                ActivityTypeId = x.ActivityTypeId,
                ActivityPriorityId = x.ActivityPriorityId,
                Description = x.Description,
                Address = x.Address,
                OpportunityId = x.OpportunityId,
                AppUserId = x.AppUserId,
                ActivityStatusId = x.ActivityStatusId,
                ActivityPriority = x.ActivityPriority == null ? null : new ActivityPriority
                {
                    Id = x.ActivityPriority.Id,
                    Code = x.ActivityPriority.Code,
                    Name = x.ActivityPriority.Name,
                },
                ActivityStatus = x.ActivityStatus == null ? null : new ActivityStatus
                {
                    Id = x.ActivityStatus.Id,
                    Code = x.ActivityStatus.Code,
                    Name = x.ActivityStatus.Name,
                },
                ActivityType = x.ActivityType == null ? null : new ActivityType
                {
                    Id = x.ActivityType.Id,
                    Code = x.ActivityType.Code,
                    Name = x.ActivityType.Name,
                },
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Address = x.AppUser.Address,
                    Email = x.AppUser.Email,
                    Phone = x.AppUser.Phone,
                    SexId = x.AppUser.SexId,
                    Birthday = x.AppUser.Birthday,
                    Avatar = x.AppUser.Avatar,
                    Department = x.AppUser.Department,
                    OrganizationId = x.AppUser.OrganizationId,
                    Longitude = x.AppUser.Longitude,
                    Latitude = x.AppUser.Latitude,
                    StatusId = x.AppUser.StatusId,
                    RowId = x.AppUser.RowId,
                    Used = x.AppUser.Used,
                },
                Opportunity = x.Opportunity == null ? null : new Opportunity
                {
                    Id = x.Opportunity.Id,
                    Name = x.Opportunity.Name,
                    CompanyId = x.Opportunity.CompanyId,
                    CustomerLeadId = x.Opportunity.CustomerLeadId,
                    ClosingDate = x.Opportunity.ClosingDate,
                    SaleStageId = x.Opportunity.SaleStageId,
                    ProbabilityId = x.Opportunity.ProbabilityId,
                    PotentialResultId = x.Opportunity.PotentialResultId,
                    LeadSourceId = x.Opportunity.LeadSourceId,
                    AppUserId = x.Opportunity.AppUserId,
                    CurrencyId = x.Opportunity.CurrencyId,
                    Amount = x.Opportunity.Amount,
                    ForecastAmount = x.Opportunity.ForecastAmount,
                    Description = x.Opportunity.Description,
                    CreatorId = x.Opportunity.CreatorId,
                    RefuseReciveSMS = x.Opportunity.RefuseReciveSMS,
                    RefuseReciveEmail = x.Opportunity.RefuseReciveEmail,
                    OpportunityResultTypeId = x.Opportunity.OpportunityResultTypeId,
                },
            }).FirstOrDefaultAsync();

            if (OpportunityActivity == null)
                return null;

            return OpportunityActivity;
        }
        public async Task<bool> Create(OpportunityActivity OpportunityActivity)
        {
            OpportunityActivityDAO OpportunityActivityDAO = new OpportunityActivityDAO();
            OpportunityActivityDAO.Id = OpportunityActivity.Id;
            OpportunityActivityDAO.Title = OpportunityActivity.Title;
            OpportunityActivityDAO.FromDate = OpportunityActivity.FromDate;
            OpportunityActivityDAO.ToDate = OpportunityActivity.ToDate;
            OpportunityActivityDAO.ActivityTypeId = OpportunityActivity.ActivityTypeId;
            OpportunityActivityDAO.ActivityPriorityId = OpportunityActivity.ActivityPriorityId;
            OpportunityActivityDAO.Description = OpportunityActivity.Description;
            OpportunityActivityDAO.Address = OpportunityActivity.Address;
            OpportunityActivityDAO.OpportunityId = OpportunityActivity.OpportunityId;
            OpportunityActivityDAO.AppUserId = OpportunityActivity.AppUserId;
            OpportunityActivityDAO.ActivityStatusId = OpportunityActivity.ActivityStatusId;
            OpportunityActivityDAO.CreatedAt = StaticParams.DateTimeNow;
            OpportunityActivityDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.OpportunityActivity.Add(OpportunityActivityDAO);
            await DataContext.SaveChangesAsync();
            OpportunityActivity.Id = OpportunityActivityDAO.Id;
            await SaveReference(OpportunityActivity);
            return true;
        }

        public async Task<bool> Update(OpportunityActivity OpportunityActivity)
        {
            OpportunityActivityDAO OpportunityActivityDAO = DataContext.OpportunityActivity.Where(x => x.Id == OpportunityActivity.Id).FirstOrDefault();
            if (OpportunityActivityDAO == null)
                return false;
            OpportunityActivityDAO.Id = OpportunityActivity.Id;
            OpportunityActivityDAO.Title = OpportunityActivity.Title;
            OpportunityActivityDAO.FromDate = OpportunityActivity.FromDate;
            OpportunityActivityDAO.ToDate = OpportunityActivity.ToDate;
            OpportunityActivityDAO.ActivityTypeId = OpportunityActivity.ActivityTypeId;
            OpportunityActivityDAO.ActivityPriorityId = OpportunityActivity.ActivityPriorityId;
            OpportunityActivityDAO.Description = OpportunityActivity.Description;
            OpportunityActivityDAO.Address = OpportunityActivity.Address;
            OpportunityActivityDAO.OpportunityId = OpportunityActivity.OpportunityId;
            OpportunityActivityDAO.AppUserId = OpportunityActivity.AppUserId;
            OpportunityActivityDAO.ActivityStatusId = OpportunityActivity.ActivityStatusId;
            OpportunityActivityDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(OpportunityActivity);
            return true;
        }

        public async Task<bool> Delete(OpportunityActivity OpportunityActivity)
        {
            await DataContext.OpportunityActivity.Where(x => x.Id == OpportunityActivity.Id).UpdateFromQueryAsync(x => new OpportunityActivityDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<OpportunityActivity> OpportunityActivities)
        {
            List<OpportunityActivityDAO> OpportunityActivityDAOs = new List<OpportunityActivityDAO>();
            foreach (OpportunityActivity OpportunityActivity in OpportunityActivities)
            {
                OpportunityActivityDAO OpportunityActivityDAO = new OpportunityActivityDAO();
                OpportunityActivityDAO.Id = OpportunityActivity.Id;
                OpportunityActivityDAO.Title = OpportunityActivity.Title;
                OpportunityActivityDAO.FromDate = OpportunityActivity.FromDate;
                OpportunityActivityDAO.ToDate = OpportunityActivity.ToDate;
                OpportunityActivityDAO.ActivityTypeId = OpportunityActivity.ActivityTypeId;
                OpportunityActivityDAO.ActivityPriorityId = OpportunityActivity.ActivityPriorityId;
                OpportunityActivityDAO.Description = OpportunityActivity.Description;
                OpportunityActivityDAO.Address = OpportunityActivity.Address;
                OpportunityActivityDAO.OpportunityId = OpportunityActivity.OpportunityId;
                OpportunityActivityDAO.AppUserId = OpportunityActivity.AppUserId;
                OpportunityActivityDAO.ActivityStatusId = OpportunityActivity.ActivityStatusId;
                OpportunityActivityDAO.CreatedAt = StaticParams.DateTimeNow;
                OpportunityActivityDAO.UpdatedAt = StaticParams.DateTimeNow;
                OpportunityActivityDAOs.Add(OpportunityActivityDAO);
            }
            await DataContext.BulkMergeAsync(OpportunityActivityDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<OpportunityActivity> OpportunityActivities)
        {
            List<long> Ids = OpportunityActivities.Select(x => x.Id).ToList();
            await DataContext.OpportunityActivity
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new OpportunityActivityDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(OpportunityActivity OpportunityActivity)
        {
        }
        
    }
}
