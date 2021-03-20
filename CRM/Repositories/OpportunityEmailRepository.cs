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
    public interface IOpportunityEmailRepository
    {
        Task<int> Count(OpportunityEmailFilter OpportunityEmailFilter);
        Task<List<OpportunityEmail>> List(OpportunityEmailFilter OpportunityEmailFilter);
        Task<OpportunityEmail> Get(long Id);
        Task<bool> Create(OpportunityEmail OpportunityEmail);
        Task<bool> Update(OpportunityEmail OpportunityEmail);
        Task<bool> Delete(OpportunityEmail OpportunityEmail);
        Task<bool> BulkMerge(List<OpportunityEmail> OpportunityEmails);
        Task<bool> BulkDelete(List<OpportunityEmail> OpportunityEmails);
    }
    public class OpportunityEmailRepository : IOpportunityEmailRepository
    {
        private DataContext DataContext;
        public OpportunityEmailRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<OpportunityEmailDAO> DynamicFilter(IQueryable<OpportunityEmailDAO> query, OpportunityEmailFilter filter)
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
            if (filter.Content != null && filter.Content.HasValue)
                query = query.Where(q => q.Content, filter.Content);
            if (filter.Reciepient != null && filter.Reciepient.HasValue)
                query = query.Where(q => q.Reciepient, filter.Reciepient);
            if (filter.OpportunityId != null && filter.OpportunityId.HasValue)
                query = query.Where(q => q.OpportunityId, filter.OpportunityId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.EmailStatusId != null && filter.EmailStatusId.HasValue)
                query = query.Where(q => q.EmailStatusId, filter.EmailStatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<OpportunityEmailDAO> OrFilter(IQueryable<OpportunityEmailDAO> query, OpportunityEmailFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<OpportunityEmailDAO> initQuery = query.Where(q => false);
            foreach (OpportunityEmailFilter OpportunityEmailFilter in filter.OrFilter)
            {
                IQueryable<OpportunityEmailDAO> queryable = query;
                if (OpportunityEmailFilter.Id != null && OpportunityEmailFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, OpportunityEmailFilter.Id);
                if (OpportunityEmailFilter.Title != null && OpportunityEmailFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, OpportunityEmailFilter.Title);
                if (OpportunityEmailFilter.Content != null && OpportunityEmailFilter.Content.HasValue)
                    queryable = queryable.Where(q => q.Content, OpportunityEmailFilter.Content);
                if (OpportunityEmailFilter.Reciepient != null && OpportunityEmailFilter.Reciepient.HasValue)
                    queryable = queryable.Where(q => q.Reciepient, OpportunityEmailFilter.Reciepient);
                if (OpportunityEmailFilter.OpportunityId != null && OpportunityEmailFilter.OpportunityId.HasValue)
                    queryable = queryable.Where(q => q.OpportunityId, OpportunityEmailFilter.OpportunityId);
                if (OpportunityEmailFilter.CreatorId != null && OpportunityEmailFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, OpportunityEmailFilter.CreatorId);
                if (OpportunityEmailFilter.EmailStatusId != null && OpportunityEmailFilter.EmailStatusId.HasValue)
                    queryable = queryable.Where(q => q.EmailStatusId, OpportunityEmailFilter.EmailStatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<OpportunityEmailDAO> DynamicOrder(IQueryable<OpportunityEmailDAO> query, OpportunityEmailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case OpportunityEmailOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case OpportunityEmailOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case OpportunityEmailOrder.Content:
                            query = query.OrderBy(q => q.Content);
                            break;
                        case OpportunityEmailOrder.Reciepient:
                            query = query.OrderBy(q => q.Reciepient);
                            break;
                        case OpportunityEmailOrder.Opportunity:
                            query = query.OrderBy(q => q.OpportunityId);
                            break;
                        case OpportunityEmailOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case OpportunityEmailOrder.EmailStatus:
                            query = query.OrderBy(q => q.EmailStatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case OpportunityEmailOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case OpportunityEmailOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case OpportunityEmailOrder.Content:
                            query = query.OrderByDescending(q => q.Content);
                            break;
                        case OpportunityEmailOrder.Reciepient:
                            query = query.OrderByDescending(q => q.Reciepient);
                            break;
                        case OpportunityEmailOrder.Opportunity:
                            query = query.OrderByDescending(q => q.OpportunityId);
                            break;
                        case OpportunityEmailOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case OpportunityEmailOrder.EmailStatus:
                            query = query.OrderByDescending(q => q.EmailStatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<OpportunityEmail>> DynamicSelect(IQueryable<OpportunityEmailDAO> query, OpportunityEmailFilter filter)
        {
            List<OpportunityEmail> OpportunityEmails = await query.Select(q => new OpportunityEmail()
            {
                Id = filter.Selects.Contains(OpportunityEmailSelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(OpportunityEmailSelect.Title) ? q.Title : default(string),
                Content = filter.Selects.Contains(OpportunityEmailSelect.Content) ? q.Content : default(string),
                Reciepient = filter.Selects.Contains(OpportunityEmailSelect.Reciepient) ? q.Reciepient : default(string),
                OpportunityId = filter.Selects.Contains(OpportunityEmailSelect.Opportunity) ? q.OpportunityId : default(long),
                CreatorId = filter.Selects.Contains(OpportunityEmailSelect.Creator) ? q.CreatorId : default(long),
                EmailStatusId = filter.Selects.Contains(OpportunityEmailSelect.EmailStatus) ? q.EmailStatusId : default(long),
                Creator = filter.Selects.Contains(OpportunityEmailSelect.Creator) && q.Creator != null ? new AppUser
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
                EmailStatus = filter.Selects.Contains(OpportunityEmailSelect.EmailStatus) && q.EmailStatus != null ? new EmailStatus
                {
                    Id = q.EmailStatus.Id,
                    Code = q.EmailStatus.Code,
                    Name = q.EmailStatus.Name,
                } : null,
                Opportunity = filter.Selects.Contains(OpportunityEmailSelect.Opportunity) && q.Opportunity != null ? new Opportunity
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
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();

            var Ids = OpportunityEmails.Select(x => x.Id).ToList();
            var OpportunityEmailCCMappings = await DataContext.OpportunityEmailCCMapping.Where(x => Ids.Contains(x.OpportunityEmailId)).Select(x => new OpportunityEmailCCMapping
            {
                AppUserId = x.AppUserId,
                OpportunityEmailId = x.OpportunityEmailId,
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Email = x.AppUser.Email,
                }
            }).ToListAsync();

            foreach (var OpportunityEmail in OpportunityEmails)
            {
                OpportunityEmail.OpportunityEmailCCMappings = OpportunityEmailCCMappings.Where(x => x.OpportunityEmailId == OpportunityEmail.Id).ToList();
            }
            return OpportunityEmails;
        }

        public async Task<int> Count(OpportunityEmailFilter filter)
        {
            IQueryable<OpportunityEmailDAO> OpportunityEmails = DataContext.OpportunityEmail.AsNoTracking();
            OpportunityEmails = DynamicFilter(OpportunityEmails, filter);
            return await OpportunityEmails.CountAsync();
        }

        public async Task<List<OpportunityEmail>> List(OpportunityEmailFilter filter)
        {
            if (filter == null) return new List<OpportunityEmail>();
            IQueryable<OpportunityEmailDAO> OpportunityEmailDAOs = DataContext.OpportunityEmail.AsNoTracking();
            OpportunityEmailDAOs = DynamicFilter(OpportunityEmailDAOs, filter);
            OpportunityEmailDAOs = DynamicOrder(OpportunityEmailDAOs, filter);
            List<OpportunityEmail> OpportunityEmails = await DynamicSelect(OpportunityEmailDAOs, filter);
            return OpportunityEmails;
        }

        public async Task<OpportunityEmail> Get(long Id)
        {
            OpportunityEmail OpportunityEmail = await DataContext.OpportunityEmail.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new OpportunityEmail()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                Reciepient = x.Reciepient,
                OpportunityId = x.OpportunityId,
                CreatorId = x.CreatorId,
                EmailStatusId = x.EmailStatusId,
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
                EmailStatus = x.EmailStatus == null ? null : new EmailStatus
                {
                    Id = x.EmailStatus.Id,
                    Code = x.EmailStatus.Code,
                    Name = x.EmailStatus.Name,
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

            if (OpportunityEmail == null)
                return null;

            OpportunityEmail.OpportunityEmailCCMappings = await DataContext.OpportunityEmailCCMapping
              .Where(x => x.OpportunityEmailId == Id)
              .Select(x => new OpportunityEmailCCMapping
              {
                  OpportunityEmailId = x.OpportunityEmailId,
                  AppUserId = x.AppUserId,
                  AppUser = x.AppUser == null ? null : new AppUser
                  {
                      Id = x.AppUser.Id,
                      Username = x.AppUser.Username,
                      DisplayName = x.AppUser.DisplayName,
                      Email = x.AppUser.Email,
                  }
              }).ToListAsync();
            return OpportunityEmail;
        }
        public async Task<bool> Create(OpportunityEmail OpportunityEmail)
        {
            OpportunityEmailDAO OpportunityEmailDAO = new OpportunityEmailDAO();
            OpportunityEmailDAO.Id = OpportunityEmail.Id;
            OpportunityEmailDAO.Title = OpportunityEmail.Title;
            OpportunityEmailDAO.Content = OpportunityEmail.Content;
            OpportunityEmailDAO.Reciepient = OpportunityEmail.Reciepient;
            OpportunityEmailDAO.OpportunityId = OpportunityEmail.OpportunityId;
            OpportunityEmailDAO.CreatorId = OpportunityEmail.CreatorId;
            OpportunityEmailDAO.EmailStatusId = OpportunityEmail.EmailStatusId;
            OpportunityEmailDAO.CreatedAt = StaticParams.DateTimeNow;
            OpportunityEmailDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.OpportunityEmail.Add(OpportunityEmailDAO);
            await DataContext.SaveChangesAsync();
            OpportunityEmail.Id = OpportunityEmailDAO.Id;
            await SaveReference(OpportunityEmail);
            return true;
        }

        public async Task<bool> Update(OpportunityEmail OpportunityEmail)
        {
            OpportunityEmailDAO OpportunityEmailDAO = DataContext.OpportunityEmail.Where(x => x.Id == OpportunityEmail.Id).FirstOrDefault();
            if (OpportunityEmailDAO == null)
                return false;
            OpportunityEmailDAO.Id = OpportunityEmail.Id;
            OpportunityEmailDAO.Title = OpportunityEmail.Title;
            OpportunityEmailDAO.Content = OpportunityEmail.Content;
            OpportunityEmailDAO.Reciepient = OpportunityEmail.Reciepient;
            OpportunityEmailDAO.OpportunityId = OpportunityEmail.OpportunityId;
            OpportunityEmailDAO.CreatorId = OpportunityEmail.CreatorId;
            OpportunityEmailDAO.EmailStatusId = OpportunityEmail.EmailStatusId;
            OpportunityEmailDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(OpportunityEmail);
            return true;
        }

        public async Task<bool> Delete(OpportunityEmail OpportunityEmail)
        {
            await DataContext.OpportunityEmail.Where(x => x.Id == OpportunityEmail.Id).UpdateFromQueryAsync(x => new OpportunityEmailDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<OpportunityEmail> OpportunityEmails)
        {
            List<OpportunityEmailDAO> OpportunityEmailDAOs = new List<OpportunityEmailDAO>();
            foreach (OpportunityEmail OpportunityEmail in OpportunityEmails)
            {
                OpportunityEmailDAO OpportunityEmailDAO = new OpportunityEmailDAO();
                OpportunityEmailDAO.Id = OpportunityEmail.Id;
                OpportunityEmailDAO.Title = OpportunityEmail.Title;
                OpportunityEmailDAO.Content = OpportunityEmail.Content;
                OpportunityEmailDAO.Reciepient = OpportunityEmail.Reciepient;
                OpportunityEmailDAO.OpportunityId = OpportunityEmail.OpportunityId;
                OpportunityEmailDAO.CreatorId = OpportunityEmail.CreatorId;
                OpportunityEmailDAO.EmailStatusId = OpportunityEmail.EmailStatusId;
                OpportunityEmailDAO.CreatedAt = StaticParams.DateTimeNow;
                OpportunityEmailDAO.UpdatedAt = StaticParams.DateTimeNow;
                OpportunityEmailDAOs.Add(OpportunityEmailDAO);
            }
            await DataContext.BulkMergeAsync(OpportunityEmailDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<OpportunityEmail> OpportunityEmails)
        {
            List<long> Ids = OpportunityEmails.Select(x => x.Id).ToList();
            await DataContext.OpportunityEmail
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new OpportunityEmailDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(OpportunityEmail OpportunityEmail)
        {
            await DataContext.OpportunityEmailCCMapping.Where(x => x.OpportunityEmailId == OpportunityEmail.Id).DeleteFromQueryAsync();
            if (OpportunityEmail.OpportunityEmailCCMappings != null)
            {
                List<OpportunityEmailCCMappingDAO> OpportunityEmailCCMappingDAOs = new List<OpportunityEmailCCMappingDAO>();
                foreach (var OpportunityEmailCCMapping in OpportunityEmail.OpportunityEmailCCMappings)
                {
                    OpportunityEmailCCMappingDAO OpportunityEmailCCMappingDAO = new OpportunityEmailCCMappingDAO
                    {
                        AppUserId = OpportunityEmailCCMapping.AppUserId,
                        OpportunityEmailId = OpportunityEmail.Id,
                    };
                    OpportunityEmailCCMappingDAOs.Add(OpportunityEmailCCMappingDAO);
                }
                await DataContext.BulkMergeAsync(OpportunityEmailCCMappingDAOs);
            }
        }
        
    }
}
