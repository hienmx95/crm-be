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
    public interface IOpportunityFileGroupingRepository
    {
        Task<int> Count(OpportunityFileGroupingFilter OpportunityFileGroupingFilter);
        Task<List<OpportunityFileGrouping>> List(OpportunityFileGroupingFilter OpportunityFileGroupingFilter);
        Task<List<OpportunityFileGrouping>> List(List<long> Ids);
        Task<OpportunityFileGrouping> Get(long Id);
        Task<bool> Create(OpportunityFileGrouping OpportunityFileGrouping);
        Task<bool> Update(OpportunityFileGrouping OpportunityFileGrouping);
        Task<bool> Delete(OpportunityFileGrouping OpportunityFileGrouping);
        Task<bool> BulkMerge(List<OpportunityFileGrouping> OpportunityFileGroupings);
        Task<bool> BulkDelete(List<OpportunityFileGrouping> OpportunityFileGroupings);
    }
    public class OpportunityFileGroupingRepository : IOpportunityFileGroupingRepository
    {
        private DataContext DataContext;
        public OpportunityFileGroupingRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<OpportunityFileGroupingDAO> DynamicFilter(IQueryable<OpportunityFileGroupingDAO> query, OpportunityFileGroupingFilter filter)
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
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.OpportunityId != null && filter.OpportunityId.HasValue)
                query = query.Where(q => q.OpportunityId, filter.OpportunityId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.FileTypeId != null && filter.FileTypeId.HasValue)
                query = query.Where(q => q.FileTypeId, filter.FileTypeId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<OpportunityFileGroupingDAO> OrFilter(IQueryable<OpportunityFileGroupingDAO> query, OpportunityFileGroupingFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<OpportunityFileGroupingDAO> initQuery = query.Where(q => false);
            foreach (OpportunityFileGroupingFilter OpportunityFileGroupingFilter in filter.OrFilter)
            {
                IQueryable<OpportunityFileGroupingDAO> queryable = query;
                if (OpportunityFileGroupingFilter.Id != null && OpportunityFileGroupingFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, OpportunityFileGroupingFilter.Id);
                if (OpportunityFileGroupingFilter.Title != null && OpportunityFileGroupingFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, OpportunityFileGroupingFilter.Title);
                if (OpportunityFileGroupingFilter.Description != null && OpportunityFileGroupingFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, OpportunityFileGroupingFilter.Description);
                if (OpportunityFileGroupingFilter.OpportunityId != null && OpportunityFileGroupingFilter.OpportunityId.HasValue)
                    queryable = queryable.Where(q => q.OpportunityId, OpportunityFileGroupingFilter.OpportunityId);
                if (OpportunityFileGroupingFilter.CreatorId != null && OpportunityFileGroupingFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, OpportunityFileGroupingFilter.CreatorId);
                if (OpportunityFileGroupingFilter.FileTypeId != null && OpportunityFileGroupingFilter.FileTypeId.HasValue)
                    queryable = queryable.Where(q => q.FileTypeId, OpportunityFileGroupingFilter.FileTypeId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<OpportunityFileGroupingDAO> DynamicOrder(IQueryable<OpportunityFileGroupingDAO> query, OpportunityFileGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case OpportunityFileGroupingOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case OpportunityFileGroupingOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case OpportunityFileGroupingOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case OpportunityFileGroupingOrder.Opportunity:
                            query = query.OrderBy(q => q.OpportunityId);
                            break;
                        case OpportunityFileGroupingOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case OpportunityFileGroupingOrder.FileType:
                            query = query.OrderBy(q => q.FileTypeId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case OpportunityFileGroupingOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case OpportunityFileGroupingOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case OpportunityFileGroupingOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case OpportunityFileGroupingOrder.Opportunity:
                            query = query.OrderByDescending(q => q.OpportunityId);
                            break;
                        case OpportunityFileGroupingOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case OpportunityFileGroupingOrder.FileType:
                            query = query.OrderByDescending(q => q.FileTypeId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<OpportunityFileGrouping>> DynamicSelect(IQueryable<OpportunityFileGroupingDAO> query, OpportunityFileGroupingFilter filter)
        {
            List<OpportunityFileGrouping> OpportunityFileGroupings = await query.Select(q => new OpportunityFileGrouping()
            {
                Id = filter.Selects.Contains(OpportunityFileGroupingSelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(OpportunityFileGroupingSelect.Title) ? q.Title : default(string),
                Description = filter.Selects.Contains(OpportunityFileGroupingSelect.Description) ? q.Description : default(string),
                OpportunityId = filter.Selects.Contains(OpportunityFileGroupingSelect.Opportunity) ? q.OpportunityId : default(long),
                CreatorId = filter.Selects.Contains(OpportunityFileGroupingSelect.Creator) ? q.CreatorId : default(long),
                FileTypeId = filter.Selects.Contains(OpportunityFileGroupingSelect.FileType) ? q.FileTypeId : default(long),
                Creator = filter.Selects.Contains(OpportunityFileGroupingSelect.Creator) && q.Creator != null ? new AppUser
                {
                    Id = q.Creator.Id,
                    Username = q.Creator.Username,
                    DisplayName = q.Creator.DisplayName,
                    Address = q.Creator.Address,
                    Email = q.Creator.Email,
                    Phone = q.Creator.Phone,
                    SexId = q.Creator.SexId,
                    Birthday = q.Creator.Birthday,
                    Avatar = q.Creator.Avatar,
                    Department = q.Creator.Department,
                    OrganizationId = q.Creator.OrganizationId,
                    Longitude = q.Creator.Longitude,
                    Latitude = q.Creator.Latitude,
                    StatusId = q.Creator.StatusId,
                    RowId = q.Creator.RowId,
                    Used = q.Creator.Used,
                } : null,
                FileType = filter.Selects.Contains(OpportunityFileGroupingSelect.FileType) && q.FileType != null ? new FileType
                {
                    Id = q.FileType.Id,
                    Code = q.FileType.Code,
                    Name = q.FileType.Name,
                } : null,
                Opportunity = filter.Selects.Contains(OpportunityFileGroupingSelect.Opportunity) && q.Opportunity != null ? new Opportunity
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
                    RefuseReciveSMS = q.Opportunity.RefuseReciveSMS,
                    RefuseReciveEmail = q.Opportunity.RefuseReciveEmail,
                    OpportunityResultTypeId = q.Opportunity.OpportunityResultTypeId,
                    CreatorId = q.Opportunity.CreatorId,
                } : null,
            }).ToListAsync();
            return OpportunityFileGroupings;
        }

        public async Task<int> Count(OpportunityFileGroupingFilter filter)
        {
            IQueryable<OpportunityFileGroupingDAO> OpportunityFileGroupings = DataContext.OpportunityFileGrouping.AsNoTracking();
            OpportunityFileGroupings = DynamicFilter(OpportunityFileGroupings, filter);
            return await OpportunityFileGroupings.CountAsync();
        }

        public async Task<List<OpportunityFileGrouping>> List(OpportunityFileGroupingFilter filter)
        {
            if (filter == null) return new List<OpportunityFileGrouping>();
            IQueryable<OpportunityFileGroupingDAO> OpportunityFileGroupingDAOs = DataContext.OpportunityFileGrouping.AsNoTracking();
            OpportunityFileGroupingDAOs = DynamicFilter(OpportunityFileGroupingDAOs, filter);
            OpportunityFileGroupingDAOs = DynamicOrder(OpportunityFileGroupingDAOs, filter);
            List<OpportunityFileGrouping> OpportunityFileGroupings = await DynamicSelect(OpportunityFileGroupingDAOs, filter);
            return OpportunityFileGroupings;
        }

        public async Task<List<OpportunityFileGrouping>> List(List<long> Ids)
        {
            List<OpportunityFileGrouping> OpportunityFileGroupings = await DataContext.OpportunityFileGrouping.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new OpportunityFileGrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                OpportunityId = x.OpportunityId,
                CreatorId = x.CreatorId,
                FileTypeId = x.FileTypeId,
                RowId = x.RowId,
                Creator = x.Creator == null ? null : new AppUser
                {
                    Id = x.Creator.Id,
                    Username = x.Creator.Username,
                    DisplayName = x.Creator.DisplayName,
                    Address = x.Creator.Address,
                    Email = x.Creator.Email,
                    Phone = x.Creator.Phone,
                    SexId = x.Creator.SexId,
                    Birthday = x.Creator.Birthday,
                    Avatar = x.Creator.Avatar,
                    Department = x.Creator.Department,
                    OrganizationId = x.Creator.OrganizationId,
                    Longitude = x.Creator.Longitude,
                    Latitude = x.Creator.Latitude,
                    StatusId = x.Creator.StatusId,
                    RowId = x.Creator.RowId,
                    Used = x.Creator.Used,
                },
                FileType = x.FileType == null ? null : new FileType
                {
                    Id = x.FileType.Id,
                    Code = x.FileType.Code,
                    Name = x.FileType.Name,
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
                    RefuseReciveSMS = x.Opportunity.RefuseReciveSMS,
                    RefuseReciveEmail = x.Opportunity.RefuseReciveEmail,
                    OpportunityResultTypeId = x.Opportunity.OpportunityResultTypeId,
                    CreatorId = x.Opportunity.CreatorId,
                },
            }).ToListAsync();
            

            return OpportunityFileGroupings;
        }

        public async Task<OpportunityFileGrouping> Get(long Id)
        {
            OpportunityFileGrouping OpportunityFileGrouping = await DataContext.OpportunityFileGrouping.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new OpportunityFileGrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                OpportunityId = x.OpportunityId,
                CreatorId = x.CreatorId,
                FileTypeId = x.FileTypeId,
                RowId = x.RowId,
                Creator = x.Creator == null ? null : new AppUser
                {
                    Id = x.Creator.Id,
                    Username = x.Creator.Username,
                    DisplayName = x.Creator.DisplayName,
                    Address = x.Creator.Address,
                    Email = x.Creator.Email,
                    Phone = x.Creator.Phone,
                    SexId = x.Creator.SexId,
                    Birthday = x.Creator.Birthday,
                    Avatar = x.Creator.Avatar,
                    Department = x.Creator.Department,
                    OrganizationId = x.Creator.OrganizationId,
                    Longitude = x.Creator.Longitude,
                    Latitude = x.Creator.Latitude,
                    StatusId = x.Creator.StatusId,
                    RowId = x.Creator.RowId,
                    Used = x.Creator.Used,
                },
                FileType = x.FileType == null ? null : new FileType
                {
                    Id = x.FileType.Id,
                    Code = x.FileType.Code,
                    Name = x.FileType.Name,
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
                    RefuseReciveSMS = x.Opportunity.RefuseReciveSMS,
                    RefuseReciveEmail = x.Opportunity.RefuseReciveEmail,
                    OpportunityResultTypeId = x.Opportunity.OpportunityResultTypeId,
                    CreatorId = x.Opportunity.CreatorId,
                },
            }).FirstOrDefaultAsync();

            if (OpportunityFileGrouping == null)
                return null;

            return OpportunityFileGrouping;
        }
        public async Task<bool> Create(OpportunityFileGrouping OpportunityFileGrouping)
        {
            OpportunityFileGroupingDAO OpportunityFileGroupingDAO = new OpportunityFileGroupingDAO();
            OpportunityFileGroupingDAO.Id = OpportunityFileGrouping.Id;
            OpportunityFileGroupingDAO.Title = OpportunityFileGrouping.Title;
            OpportunityFileGroupingDAO.Description = OpportunityFileGrouping.Description;
            OpportunityFileGroupingDAO.OpportunityId = OpportunityFileGrouping.OpportunityId;
            OpportunityFileGroupingDAO.CreatorId = OpportunityFileGrouping.CreatorId;
            OpportunityFileGroupingDAO.FileTypeId = OpportunityFileGrouping.FileTypeId;
            OpportunityFileGroupingDAO.RowId = OpportunityFileGrouping.RowId;
            OpportunityFileGroupingDAO.CreatedAt = StaticParams.DateTimeNow;
            OpportunityFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.OpportunityFileGrouping.Add(OpportunityFileGroupingDAO);
            await DataContext.SaveChangesAsync();
            OpportunityFileGrouping.Id = OpportunityFileGroupingDAO.Id;
            await SaveReference(OpportunityFileGrouping);
            return true;
        }

        public async Task<bool> Update(OpportunityFileGrouping OpportunityFileGrouping)
        {
            OpportunityFileGroupingDAO OpportunityFileGroupingDAO = DataContext.OpportunityFileGrouping.Where(x => x.Id == OpportunityFileGrouping.Id).FirstOrDefault();
            if (OpportunityFileGroupingDAO == null)
                return false;
            OpportunityFileGroupingDAO.Id = OpportunityFileGrouping.Id;
            OpportunityFileGroupingDAO.Title = OpportunityFileGrouping.Title;
            OpportunityFileGroupingDAO.Description = OpportunityFileGrouping.Description;
            OpportunityFileGroupingDAO.OpportunityId = OpportunityFileGrouping.OpportunityId;
            OpportunityFileGroupingDAO.CreatorId = OpportunityFileGrouping.CreatorId;
            OpportunityFileGroupingDAO.FileTypeId = OpportunityFileGrouping.FileTypeId;
            OpportunityFileGroupingDAO.RowId = OpportunityFileGrouping.RowId;
            OpportunityFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(OpportunityFileGrouping);
            return true;
        }

        public async Task<bool> Delete(OpportunityFileGrouping OpportunityFileGrouping)
        {
            await DataContext.OpportunityFileGrouping.Where(x => x.Id == OpportunityFileGrouping.Id).UpdateFromQueryAsync(x => new OpportunityFileGroupingDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<OpportunityFileGrouping> OpportunityFileGroupings)
        {
            List<OpportunityFileGroupingDAO> OpportunityFileGroupingDAOs = new List<OpportunityFileGroupingDAO>();
            foreach (OpportunityFileGrouping OpportunityFileGrouping in OpportunityFileGroupings)
            {
                OpportunityFileGroupingDAO OpportunityFileGroupingDAO = new OpportunityFileGroupingDAO();
                OpportunityFileGroupingDAO.Id = OpportunityFileGrouping.Id;
                OpportunityFileGroupingDAO.Title = OpportunityFileGrouping.Title;
                OpportunityFileGroupingDAO.Description = OpportunityFileGrouping.Description;
                OpportunityFileGroupingDAO.OpportunityId = OpportunityFileGrouping.OpportunityId;
                OpportunityFileGroupingDAO.CreatorId = OpportunityFileGrouping.CreatorId;
                OpportunityFileGroupingDAO.FileTypeId = OpportunityFileGrouping.FileTypeId;
                OpportunityFileGroupingDAO.RowId = OpportunityFileGrouping.RowId;
                OpportunityFileGroupingDAO.CreatedAt = StaticParams.DateTimeNow;
                OpportunityFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
                OpportunityFileGroupingDAOs.Add(OpportunityFileGroupingDAO);
            }
            await DataContext.BulkMergeAsync(OpportunityFileGroupingDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<OpportunityFileGrouping> OpportunityFileGroupings)
        {
            List<long> Ids = OpportunityFileGroupings.Select(x => x.Id).ToList();
            await DataContext.OpportunityFileGrouping
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new OpportunityFileGroupingDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(OpportunityFileGrouping OpportunityFileGrouping)
        {
        }
        
    }
}
