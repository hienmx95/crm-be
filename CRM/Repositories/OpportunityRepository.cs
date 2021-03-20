using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using System.ComponentModel;
using CRM.Enums;
using System.Reflection;

namespace CRM.Repositories
{
    public interface IOpportunityRepository
    {
        Task<int> Count(OpportunityFilter OpportunityFilter);
        Task<List<Opportunity>> List(OpportunityFilter OpportunityFilter);
        Task<Opportunity> Get(long Id);
        Task<bool> Create(Opportunity Opportunity);
        Task<bool> Update(Opportunity Opportunity);
        Task<bool> Delete(Opportunity Opportunity);
        Task<bool> BulkMerge(List<Opportunity> Opportunities);
        Task<bool> BulkDelete(List<Opportunity> Opportunities);
        Task<bool> ChangeStatus(long Id, long statusId);
    }
    public class OpportunityRepository : IOpportunityRepository
    {
        private DataContext DataContext;
        public OpportunityRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<OpportunityDAO> DynamicFilter(IQueryable<OpportunityDAO> query, OpportunityFilter filter)
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
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.CompanyId != null && filter.CompanyId.HasValue)
                query = query.Where(q => q.CompanyId.HasValue).Where(q => q.CompanyId, filter.CompanyId);
            if (filter.CustomerLeadId != null && filter.CustomerLeadId.HasValue)
                query = query.Where(q => q.CustomerLeadId.HasValue).Where(q => q.CustomerLeadId, filter.CustomerLeadId);
            if (filter.ClosingDate != null && filter.ClosingDate.HasValue)
                query = query.Where(q => q.ClosingDate, filter.ClosingDate);
            if (filter.SaleStageId != null && filter.SaleStageId.HasValue)
                query = query.Where(q => q.SaleStageId.HasValue).Where(q => q.SaleStageId, filter.SaleStageId);
            if (filter.ProbabilityId != null && filter.ProbabilityId.HasValue)
                query = query.Where(q => q.ProbabilityId, filter.ProbabilityId);
            if (filter.PotentialResultId != null && filter.PotentialResultId.HasValue)
                query = query.Where(q => q.PotentialResultId.HasValue).Where(q => q.PotentialResultId, filter.PotentialResultId);
            if (filter.LeadSourceId != null && filter.LeadSourceId.HasValue)
                query = query.Where(q => q.LeadSourceId.HasValue).Where(q => q.LeadSourceId, filter.LeadSourceId);
            if (filter.AppUserId != null && filter.AppUserId.HasValue)
                query = query.Where(q => q.AppUserId, filter.AppUserId);
            if (filter.CurrencyId != null && filter.CurrencyId.HasValue)
                query = query.Where(q => q.CurrencyId.HasValue).Where(q => q.CurrencyId, filter.CurrencyId);
            if (filter.Amount != null && filter.Amount.HasValue)
                query = query.Where(q => q.Amount.HasValue).Where(q => q.Amount, filter.Amount);
            if (filter.ForecastAmount != null && filter.ForecastAmount.HasValue)
                query = query.Where(q => q.ForecastAmount.HasValue).Where(q => q.ForecastAmount, filter.ForecastAmount);
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.OpportunityResultTypeId != null && filter.OpportunityResultTypeId.HasValue)
                query = query.Where(q => q.OpportunityResultTypeId.HasValue).Where(q => q.OpportunityResultTypeId, filter.OpportunityResultTypeId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
        if (filter.ContactId != null && filter.ContactId.HasValue)
            {
                query = from q in query
                        join oc in DataContext.OpportunityContactMapping on q.Id equals oc.OpportunityId
                        where oc.ContactId == filter.ContactId.Equal.Value
                        select q;
            }
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<OpportunityDAO> OrFilter(IQueryable<OpportunityDAO> query, OpportunityFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<OpportunityDAO> initQuery = query.Where(q => false);
            foreach (OpportunityFilter OpportunityFilter in filter.OrFilter)
            {
                IQueryable<OpportunityDAO> queryable = query;
                if (OpportunityFilter.Id != null && OpportunityFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, OpportunityFilter.Id);
                if (OpportunityFilter.Name != null && OpportunityFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, OpportunityFilter.Name);
                if (OpportunityFilter.CompanyId != null && OpportunityFilter.CompanyId.HasValue)
                    queryable = queryable.Where(q => q.CompanyId.HasValue).Where(q => q.CompanyId, OpportunityFilter.CompanyId);
                if (OpportunityFilter.CustomerLeadId != null && OpportunityFilter.CustomerLeadId.HasValue)
                    queryable = queryable.Where(q => q.CustomerLeadId.HasValue).Where(q => q.CustomerLeadId, OpportunityFilter.CustomerLeadId);
                if (OpportunityFilter.ClosingDate != null && OpportunityFilter.ClosingDate.HasValue)
                    queryable = queryable.Where(q => q.ClosingDate, OpportunityFilter.ClosingDate);
                if (OpportunityFilter.SaleStageId != null && OpportunityFilter.SaleStageId.HasValue)
                    queryable = queryable.Where(q => q.SaleStageId.HasValue).Where(q => q.SaleStageId, OpportunityFilter.SaleStageId);
                if (OpportunityFilter.ProbabilityId != null && OpportunityFilter.ProbabilityId.HasValue)
                    queryable = queryable.Where(q => q.ProbabilityId, OpportunityFilter.ProbabilityId);
                if (OpportunityFilter.PotentialResultId != null && OpportunityFilter.PotentialResultId.HasValue)
                    queryable = queryable.Where(q => q.PotentialResultId.HasValue).Where(q => q.PotentialResultId, OpportunityFilter.PotentialResultId);
                if (OpportunityFilter.LeadSourceId != null && OpportunityFilter.LeadSourceId.HasValue)
                    queryable = queryable.Where(q => q.LeadSourceId.HasValue).Where(q => q.LeadSourceId, OpportunityFilter.LeadSourceId);
                if (OpportunityFilter.AppUserId != null && OpportunityFilter.AppUserId.HasValue)
                    queryable = queryable.Where(q => q.AppUserId, OpportunityFilter.AppUserId);
                if (OpportunityFilter.CurrencyId != null && OpportunityFilter.CurrencyId.HasValue)
                    queryable = queryable.Where(q => q.CurrencyId.HasValue).Where(q => q.CurrencyId, OpportunityFilter.CurrencyId);
                if (OpportunityFilter.Amount != null && OpportunityFilter.Amount.HasValue)
                    queryable = queryable.Where(q => q.Amount.HasValue).Where(q => q.Amount, OpportunityFilter.Amount);
                if (OpportunityFilter.ForecastAmount != null && OpportunityFilter.ForecastAmount.HasValue)
                    queryable = queryable.Where(q => q.ForecastAmount.HasValue).Where(q => q.ForecastAmount, OpportunityFilter.ForecastAmount);
                if (OpportunityFilter.Description != null && OpportunityFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, OpportunityFilter.Description);
                if (OpportunityFilter.OpportunityResultTypeId != null && OpportunityFilter.OpportunityResultTypeId.HasValue)
                    queryable = queryable.Where(q => q.OpportunityResultTypeId.HasValue).Where(q => q.OpportunityResultTypeId, OpportunityFilter.OpportunityResultTypeId);
                if (OpportunityFilter.CreatorId != null && OpportunityFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, OpportunityFilter.CreatorId);
               
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<OpportunityDAO> DynamicOrder(IQueryable<OpportunityDAO> query, OpportunityFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case OpportunityOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case OpportunityOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case OpportunityOrder.Company:
                            query = query.OrderBy(q => q.CompanyId);
                            break;
                        case OpportunityOrder.CustomerLead:
                            query = query.OrderBy(q => q.CustomerLeadId);
                            break;
                        case OpportunityOrder.ClosingDate:
                            query = query.OrderBy(q => q.ClosingDate);
                            break;
                        case OpportunityOrder.SaleStage:
                            query = query.OrderBy(q => q.SaleStageId);
                            break;
                        case OpportunityOrder.Probability:
                            query = query.OrderBy(q => q.ProbabilityId);
                            break;
                        case OpportunityOrder.PotentialResult:
                            query = query.OrderBy(q => q.PotentialResultId);
                            break;
                        case OpportunityOrder.LeadSource:
                            query = query.OrderBy(q => q.LeadSourceId);
                            break;
                        case OpportunityOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case OpportunityOrder.Currency:
                            query = query.OrderBy(q => q.CurrencyId);
                            break;
                        case OpportunityOrder.Amount:
                            query = query.OrderBy(q => q.Amount);
                            break;
                        case OpportunityOrder.ForecastAmount:
                            query = query.OrderBy(q => q.ForecastAmount);
                            break;
                        case OpportunityOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case OpportunityOrder.RefuseReciveSMS:
                            query = query.OrderBy(q => q.RefuseReciveSMS);
                            break;
                        case OpportunityOrder.RefuseReciveEmail:
                            query = query.OrderBy(q => q.RefuseReciveEmail);
                            break;
                        case OpportunityOrder.OpportunityResultType:
                            query = query.OrderBy(q => q.OpportunityResultTypeId);
                            break;
                        case OpportunityOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case OpportunityOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case OpportunityOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case OpportunityOrder.Company:
                            query = query.OrderByDescending(q => q.CompanyId);
                            break;
                        case OpportunityOrder.CustomerLead:
                            query = query.OrderByDescending(q => q.CustomerLeadId);
                            break;
                        case OpportunityOrder.ClosingDate:
                            query = query.OrderByDescending(q => q.ClosingDate);
                            break;
                        case OpportunityOrder.SaleStage:
                            query = query.OrderByDescending(q => q.SaleStageId);
                            break;
                        case OpportunityOrder.Probability:
                            query = query.OrderByDescending(q => q.ProbabilityId);
                            break;
                        case OpportunityOrder.PotentialResult:
                            query = query.OrderByDescending(q => q.PotentialResultId);
                            break;
                        case OpportunityOrder.LeadSource:
                            query = query.OrderByDescending(q => q.LeadSourceId);
                            break;
                        case OpportunityOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case OpportunityOrder.Currency:
                            query = query.OrderByDescending(q => q.CurrencyId);
                            break;
                        case OpportunityOrder.Amount:
                            query = query.OrderByDescending(q => q.Amount);
                            break;
                        case OpportunityOrder.ForecastAmount:
                            query = query.OrderByDescending(q => q.ForecastAmount);
                            break;
                        case OpportunityOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case OpportunityOrder.RefuseReciveSMS:
                            query = query.OrderByDescending(q => q.RefuseReciveSMS);
                            break;
                        case OpportunityOrder.RefuseReciveEmail:
                            query = query.OrderByDescending(q => q.RefuseReciveEmail);
                            break;
                        case OpportunityOrder.OpportunityResultType:
                            query = query.OrderByDescending(q => q.OpportunityResultTypeId);
                            break;
                        case OpportunityOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Opportunity>> DynamicSelect(IQueryable<OpportunityDAO> query, OpportunityFilter filter)
        {
            List<Opportunity> Opportunities = await query.Select(q => new Opportunity()
            {
                Id = filter.Selects.Contains(OpportunitySelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(OpportunitySelect.Name) ? q.Name : default(string),
                CompanyId = filter.Selects.Contains(OpportunitySelect.Company) ? q.CompanyId : default(long?),
                CustomerLeadId = filter.Selects.Contains(OpportunitySelect.CustomerLead) ? q.CustomerLeadId : default(long?),
                ClosingDate = filter.Selects.Contains(OpportunitySelect.ClosingDate) ? q.ClosingDate : default(DateTime),
                SaleStageId = filter.Selects.Contains(OpportunitySelect.SaleStage) ? q.SaleStageId : default(long?),
                ProbabilityId = filter.Selects.Contains(OpportunitySelect.Probability) ? q.ProbabilityId : default(long),
                PotentialResultId = filter.Selects.Contains(OpportunitySelect.PotentialResult) ? q.PotentialResultId : default(long?),
                LeadSourceId = filter.Selects.Contains(OpportunitySelect.LeadSource) ? q.LeadSourceId : default(long?),
                AppUserId = filter.Selects.Contains(OpportunitySelect.AppUser) ? q.AppUserId : default(long),
                CurrencyId = filter.Selects.Contains(OpportunitySelect.Currency) ? q.CurrencyId : default(long?),
                Amount = filter.Selects.Contains(OpportunitySelect.Amount) ? q.Amount : default(decimal?),
                ForecastAmount = filter.Selects.Contains(OpportunitySelect.ForecastAmount) ? q.ForecastAmount : default(decimal?),
                Description = filter.Selects.Contains(OpportunitySelect.Description) ? q.Description : default(string),
                RefuseReciveSMS = filter.Selects.Contains(OpportunitySelect.RefuseReciveSMS) ? q.RefuseReciveSMS : default(bool?),
                RefuseReciveEmail = filter.Selects.Contains(OpportunitySelect.RefuseReciveEmail) ? q.RefuseReciveEmail : default(bool?),
                OpportunityResultTypeId = filter.Selects.Contains(OpportunitySelect.OpportunityResultType) ? q.OpportunityResultTypeId : default(long?),
                CreatorId = filter.Selects.Contains(OpportunitySelect.Creator) ? q.CreatorId : default(long),
                //OrganizationId = filter.Selects.Contains(OpportunitySelect.Organization) ? q.OrganizationId : default(long),
                AppUser = filter.Selects.Contains(OpportunitySelect.AppUser) && q.AppUser != null ? new AppUser
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
                Company = filter.Selects.Contains(OpportunitySelect.Company) && q.Company != null ? new Company
                {
                    Id = q.Company.Id,
                    Name = q.Company.Name,
                    Phone = q.Company.Phone,
                    FAX = q.Company.FAX,
                    PhoneOther = q.Company.PhoneOther,
                    Email = q.Company.Email,
                    EmailOther = q.Company.EmailOther,
                    ZIPCode = q.Company.ZIPCode,
                    Revenue = q.Company.Revenue,
                    Website = q.Company.Website,
                    Address = q.Company.Address,
                    NationId = q.Company.NationId,
                    ProvinceId = q.Company.ProvinceId,
                    DistrictId = q.Company.DistrictId,
                    NumberOfEmployee = q.Company.NumberOfEmployee,
                    RefuseReciveEmail = q.Company.RefuseReciveEmail,
                    RefuseReciveSMS = q.Company.RefuseReciveSMS,
                    CustomerLeadId = q.Company.CustomerLeadId,
                    ParentId = q.Company.ParentId,
                    Path = q.Company.Path,
                    Level = q.Company.Level,
                    ProfessionId = q.Company.ProfessionId,
                    AppUserId = q.Company.AppUserId,
                    CreatorId = q.Company.CreatorId,
                    CurrencyId = q.Company.CurrencyId,
                    CompanyStatusId = q.Company.CompanyStatusId,
                    Description = q.Company.Description,
                    RowId = q.Company.RowId,
                } : null,
                Currency = filter.Selects.Contains(OpportunitySelect.Currency) && q.Currency != null ? new Currency
                {
                    Id = q.Currency.Id,
                    Code = q.Currency.Code,
                    Name = q.Currency.Name,
                } : null,
                CustomerLead = filter.Selects.Contains(OpportunitySelect.CustomerLead) && q.CustomerLead != null ? new CustomerLead
                {
                    Id = q.CustomerLead.Id,
                    Name = q.CustomerLead.Name,
                    CompanyName = q.CustomerLead.CompanyName,
                    TelePhone = q.CustomerLead.TelePhone,
                    Phone = q.CustomerLead.Phone,
                    Fax = q.CustomerLead.Fax,
                    Email = q.CustomerLead.Email,
                    SecondEmail = q.CustomerLead.SecondEmail,
                    Website = q.CustomerLead.Website,
                    CustomerLeadSourceId = q.CustomerLead.CustomerLeadSourceId,
                    CustomerLeadLevelId = q.CustomerLead.CustomerLeadLevelId,
                    CompanyId = q.CustomerLead.CompanyId,
                    CampaignId = q.CustomerLead.CampaignId,
                    ProfessionId = q.CustomerLead.ProfessionId,
                    Revenue = q.CustomerLead.Revenue,
                    EmployeeQuantity = q.CustomerLead.EmployeeQuantity,
                    Address = q.CustomerLead.Address,
                    NationId = q.CustomerLead.NationId,
                    ProvinceId = q.CustomerLead.ProvinceId,
                    DistrictId = q.CustomerLead.DistrictId,
                    CustomerLeadStatusId = q.CustomerLead.CustomerLeadStatusId,
                    BusinessRegistrationCode = q.CustomerLead.BusinessRegistrationCode,
                    SexId = q.CustomerLead.SexId,
                    RefuseReciveSMS = q.CustomerLead.RefuseReciveSMS,
                    RefuseReciveEmail = q.CustomerLead.RefuseReciveEmail,
                    Description = q.CustomerLead.Description,
                    AppUserId = q.CustomerLead.AppUserId,
                    CreatorId = q.CustomerLead.CreatorId,
                    ZipCode = q.CustomerLead.ZipCode,
                    CurrencyId = q.CustomerLead.CurrencyId,
                    RowId = q.CustomerLead.RowId,
                } : null,
                LeadSource = filter.Selects.Contains(OpportunitySelect.LeadSource) && q.LeadSource != null ? new CustomerLeadSource
                {
                    Id = q.LeadSource.Id,
                    Code = q.LeadSource.Code,
                    Name = q.LeadSource.Name,
                } : null,
                OpportunityResultType = filter.Selects.Contains(OpportunitySelect.OpportunityResultType) && q.OpportunityResultType != null ? new OpportunityResultType
                {
                    Id = q.OpportunityResultType.Id,
                    Code = q.OpportunityResultType.Code,
                    Name = q.OpportunityResultType.Name,
                } : null,
                //Organization = filter.Selects.Contains(OpportunitySelect.Organization) && q.Organization != null ? new Organization
                //{
                //    Id = q.Organization.Id,
                //    Code = q.Organization.Code,
                //    Name = q.Organization.Name,
                //    Address = q.Organization.Address,
                //    Phone = q.Organization.Phone,
                //    Path = q.Organization.Path,
                //    ParentId = q.Organization.ParentId,
                //    Email = q.Organization.Email,
                //    StatusId = q.Organization.StatusId,
                //    Level = q.Organization.Level
                //} : null,
                PotentialResult = filter.Selects.Contains(OpportunitySelect.PotentialResult) && q.PotentialResult != null ? new PotentialResult
                {
                    Id = q.PotentialResult.Id,
                    Code = q.PotentialResult.Code,
                    Name = q.PotentialResult.Name,
                } : null,
                Probability = filter.Selects.Contains(OpportunitySelect.Probability) && q.Probability != null ? new Probability
                {
                    Id = q.Probability.Id,
                    Code = q.Probability.Code,
                    Name = q.Probability.Name,
                } : null,
                SaleStage = filter.Selects.Contains(OpportunitySelect.SaleStage) && q.SaleStage != null ? new SaleStage
                {
                    Id = q.SaleStage.Id,
                    Code = q.SaleStage.Code,
                    Name = q.SaleStage.Name,
                } : null,
            }).ToListAsync();
            return Opportunities;
        }

        public async Task<int> Count(OpportunityFilter filter)
        {
            IQueryable<OpportunityDAO> Opportunities = DataContext.Opportunity.AsNoTracking();
            Opportunities = DynamicFilter(Opportunities, filter);
            return await Opportunities.CountAsync();
        }

        public async Task<List<Opportunity>> List(OpportunityFilter filter)
        {
            if (filter == null) return new List<Opportunity>();
            IQueryable<OpportunityDAO> OpportunityDAOs = DataContext.Opportunity.AsNoTracking();
            OpportunityDAOs = DynamicFilter(OpportunityDAOs, filter);
            OpportunityDAOs = DynamicOrder(OpportunityDAOs, filter);
            List<Opportunity> Opportunities = await DynamicSelect(OpportunityDAOs, filter);
            return Opportunities;
        }

        public async Task<Opportunity> Get(long Id)
        {
            Opportunity Opportunity = await DataContext.Opportunity.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new Opportunity()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Name = x.Name,
                CompanyId = x.CompanyId,
                CustomerLeadId = x.CustomerLeadId,
                ClosingDate = x.ClosingDate,
                SaleStageId = x.SaleStageId,
                ProbabilityId = x.ProbabilityId,
                PotentialResultId = x.PotentialResultId,
                LeadSourceId = x.LeadSourceId,
                AppUserId = x.AppUserId,
                CurrencyId = x.CurrencyId,
                Amount = x.Amount,
                ForecastAmount = x.ForecastAmount,
                Description = x.Description,
                RefuseReciveSMS = x.RefuseReciveSMS,
                RefuseReciveEmail = x.RefuseReciveEmail,
                OpportunityResultTypeId = x.OpportunityResultTypeId,
                CreatorId = x.CreatorId,
                //OrganizationId = x.OrganizationId,
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
                Company = x.Company == null ? null : new Company
                {
                    Id = x.Company.Id,
                    Name = x.Company.Name,
                    Phone = x.Company.Phone,
                    FAX = x.Company.FAX,
                    PhoneOther = x.Company.PhoneOther,
                    Email = x.Company.Email,
                    EmailOther = x.Company.EmailOther,
                    ZIPCode = x.Company.ZIPCode,
                    Revenue = x.Company.Revenue,
                    Website = x.Company.Website,
                    Address = x.Company.Address,
                    NationId = x.Company.NationId,
                    ProvinceId = x.Company.ProvinceId,
                    DistrictId = x.Company.DistrictId,
                    NumberOfEmployee = x.Company.NumberOfEmployee,
                    RefuseReciveEmail = x.Company.RefuseReciveEmail,
                    RefuseReciveSMS = x.Company.RefuseReciveSMS,
                    CustomerLeadId = x.Company.CustomerLeadId,
                    ParentId = x.Company.ParentId,
                    Path = x.Company.Path,
                    Level = x.Company.Level,
                    ProfessionId = x.Company.ProfessionId,
                    AppUserId = x.Company.AppUserId,
                    CreatorId = x.Company.CreatorId,
                    CurrencyId = x.Company.CurrencyId,
                    CompanyStatusId = x.Company.CompanyStatusId,
                    Description = x.Company.Description,
                    RowId = x.Company.RowId,
                },
                Currency = x.Currency == null ? null : new Currency
                {
                    Id = x.Currency.Id,
                    Code = x.Currency.Code,
                    Name = x.Currency.Name,
                },
                CustomerLead = x.CustomerLead == null ? null : new CustomerLead
                {
                    Id = x.CustomerLead.Id,
                    Name = x.CustomerLead.Name,
                    CompanyName = x.CustomerLead.CompanyName,
                    TelePhone = x.CustomerLead.TelePhone,
                    Phone = x.CustomerLead.Phone,
                    Fax = x.CustomerLead.Fax,
                    Email = x.CustomerLead.Email,
                    SecondEmail = x.CustomerLead.SecondEmail,
                    Website = x.CustomerLead.Website,
                    CustomerLeadSourceId = x.CustomerLead.CustomerLeadSourceId,
                    CustomerLeadLevelId = x.CustomerLead.CustomerLeadLevelId,
                    CompanyId = x.CustomerLead.CompanyId,
                    CampaignId = x.CustomerLead.CampaignId,
                    ProfessionId = x.CustomerLead.ProfessionId,
                    Revenue = x.CustomerLead.Revenue,
                    EmployeeQuantity = x.CustomerLead.EmployeeQuantity,
                    Address = x.CustomerLead.Address,
                    NationId = x.CustomerLead.NationId,
                    ProvinceId = x.CustomerLead.ProvinceId,
                    DistrictId = x.CustomerLead.DistrictId,
                    CustomerLeadStatusId = x.CustomerLead.CustomerLeadStatusId,
                    BusinessRegistrationCode = x.CustomerLead.BusinessRegistrationCode,
                    SexId = x.CustomerLead.SexId,
                    RefuseReciveSMS = x.CustomerLead.RefuseReciveSMS,
                    RefuseReciveEmail = x.CustomerLead.RefuseReciveEmail,
                    Description = x.CustomerLead.Description,
                    AppUserId = x.CustomerLead.AppUserId,
                    CreatorId = x.CustomerLead.CreatorId,
                    ZipCode = x.CustomerLead.ZipCode,
                    CurrencyId = x.CustomerLead.CurrencyId,
                    RowId = x.CustomerLead.RowId,
                },
                LeadSource = x.LeadSource == null ? null : new CustomerLeadSource
                {
                    Id = x.LeadSource.Id,
                    Code = x.LeadSource.Code,
                    Name = x.LeadSource.Name,
                },
                OpportunityResultType = x.OpportunityResultType == null ? null : new OpportunityResultType
                {
                    Id = x.OpportunityResultType.Id,
                    Code = x.OpportunityResultType.Code,
                    Name = x.OpportunityResultType.Name,
                },
                //Organization = x.Organization == null ? null : new Organization
                //{
                //    Id = x.Organization.Id,
                //    Code = x.Organization.Code,
                //    Name = x.Organization.Name,
                //    Address = x.Organization.Address,
                //    Phone = x.Organization.Phone,
                //    Path = x.Organization.Path,
                //    ParentId = x.Organization.ParentId,
                //    Email = x.Organization.Email,
                //    StatusId = x.Organization.StatusId,
                //    Level = x.Organization.Level
                //},
                PotentialResult = x.PotentialResult == null ? null : new PotentialResult
                {
                    Id = x.PotentialResult.Id,
                    Code = x.PotentialResult.Code,
                    Name = x.PotentialResult.Name,
                },
                Probability = x.Probability == null ? null : new Probability
                {
                    Id = x.Probability.Id,
                    Code = x.Probability.Code,
                    Name = x.Probability.Name,
                },
                SaleStage = x.SaleStage == null ? null : new SaleStage
                {
                    Id = x.SaleStage.Id,
                    Code = x.SaleStage.Code,
                    Name = x.SaleStage.Name,
                },
            }).FirstOrDefaultAsync();
            if (Opportunity == null)
                return null;
            Opportunity.OpportunityItemMappings = await DataContext.OpportunityItemMapping.AsNoTracking()
                .Where(x => x.OpportunityId == Opportunity.Id)
                .Where(x => x.Item.DeletedAt == null)
                .Select(x => new OpportunityItemMapping
                {
                    OpportunityId = x.OpportunityId,
                    ItemId = x.ItemId,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    Quantity = x.Quantity,
                    RequestQuantity = x.RequestQuantity,
                    PrimaryPrice = x.PrimaryPrice,
                    SalePrice = x.SalePrice,
                    DiscountPercentage = x.DiscountPercentage,
                    Discount = x.Discount,
                    VAT = x.VAT,
                    VATOther = x.VATOther,
                    Amount = x.Amount,
                    Factor = x.Factor,
                    Item = new Item
                    {
                        Id = x.Item.Id,
                        ProductId = x.Item.ProductId,
                        Code = x.Item.Code,
                        Name = x.Item.Name,
                        ScanCode = x.Item.ScanCode,
                        SalePrice = x.Item.SalePrice,
                        RetailPrice = x.Item.RetailPrice,
                        StatusId = x.Item.StatusId,
                        Used = x.Item.Used,
                        RowId = x.Item.RowId,
                    },
                    UnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.UnitOfMeasure.Id,
                        Code = x.UnitOfMeasure.Code,
                        Name = x.UnitOfMeasure.Name,
                        Description = x.UnitOfMeasure.Description,
                        StatusId = x.UnitOfMeasure.StatusId,
                        Used = x.UnitOfMeasure.Used,
                        RowId = x.UnitOfMeasure.RowId,
                    },
                }).ToListAsync();

            Opportunity.OpportunityContactMappings = await DataContext.OpportunityContactMapping.AsNoTracking()
                 .Where(x => x.OpportunityId == Opportunity.Id)
                 .Where(x => x.Contact.DeletedAt == null)
                 .Select(x => new OpportunityContactMapping
                 {
                     ContactId = x.ContactId,
                     OpportunityId = x.OpportunityId,
                     Contact = new Contact
                     {
                         Id = x.Contact.Id,
                         Name = x.Contact.Name,
                         ProfessionId = x.Contact.ProfessionId,
                         CompanyId = x.Contact.CompanyId,
                         ContactStatusId = x.Contact.ContactStatusId,
                         Address = x.Contact.Address,
                         NationId = x.Contact.NationId,
                         ProvinceId = x.Contact.ProvinceId,
                         DistrictId = x.Contact.DistrictId,
                         CustomerLeadId = x.Contact.CustomerLeadId,
                         ImageId = x.Contact.ImageId,
                         Description = x.Contact.Description,
                         EmailOther = x.Contact.EmailOther,
                         DateOfBirth = x.Contact.DateOfBirth,
                         Phone = x.Contact.Phone,
                         PhoneHome = x.Contact.PhoneHome,
                         FAX = x.Contact.FAX,
                         Email = x.Contact.Email,
                         Department = x.Contact.Department,
                         ZIPCode = x.Contact.ZIPCode,
                         SexId = x.Contact.SexId,
                         AppUserId = x.Contact.AppUserId,
                         RefuseReciveEmail = x.Contact.RefuseReciveEmail,
                         RefuseReciveSMS = x.Contact.RefuseReciveSMS,
                         PositionId = x.Contact.PositionId,
                         CreatorId = x.Contact.CreatorId,
                     },
                 }).ToListAsync();

            Opportunity.OpportunityFileGroupings = await DataContext.OpportunityFileGrouping
                .Where(x => x.OpportunityId == Id)
                .Where(x => x.DeletedAt == null)
                .Select(x => new OpportunityFileGrouping
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreatorId = x.CreatorId,
                    FileTypeId = x.FileTypeId,
                    OpportunityId = x.OpportunityId,
                    RowId = x.RowId,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Creator = x.Creator == null ? null : new AppUser
                    {
                        Id = x.Creator.Id,
                        Username = x.Creator.Username,
                        DisplayName = x.Creator.DisplayName,
                    },
                    FileType = x.FileType == null ? null : new FileType
                    {
                        Id = x.FileType.Id,
                        Code = x.FileType.Code,
                        Name = x.FileType.Name,
                    }
                }).ToListAsync();
            var OpportunityFileGroupingIds = Opportunity.OpportunityFileGroupings.Select(x => x.Id).ToList();

            var OpportunityFileMappings = await DataContext.OpportunityFileMapping.Where(x => OpportunityFileGroupingIds.Contains(x.OpportunityFileGroupingId))
                .Select(x => new OpportunityFileMapping
                {
                    OpportunityFileGroupingId = x.OpportunityFileGroupingId,
                    FileId = x.FileId,
                    File = x.File == null ? null : new File
                    {
                        Id = x.File.Id,
                        AppUserId = x.File.AppUserId,
                        CreatedAt = x.File.CreatedAt,
                        Name = x.File.Name,
                        Url = x.File.Url,
                        UpdatedAt = x.File.UpdatedAt,
                        RowId = x.File.RowId,
                        AppUser = x.File.AppUser == null ? null : new AppUser
                        {
                            Id = x.File.AppUser.Id,
                            Username = x.File.AppUser.Username,
                            DisplayName = x.File.AppUser.DisplayName,
                        },
                    },
                }).ToListAsync();

            foreach (OpportunityFileGrouping OpportunityFileGrouping in Opportunity.OpportunityFileGroupings)
            {
                OpportunityFileGrouping.OpportunityFileMappings = OpportunityFileMappings.Where(x => x.OpportunityFileGroupingId == OpportunityFileGrouping.Id).ToList();
            }
            return Opportunity;
        }
        public async Task<bool> Create(Opportunity Opportunity)
        {
            OpportunityDAO OpportunityDAO = new OpportunityDAO();
            OpportunityDAO.Id = Opportunity.Id;
            OpportunityDAO.Name = Opportunity.Name;
            OpportunityDAO.CompanyId = Opportunity.CompanyId;
            OpportunityDAO.CustomerLeadId = Opportunity.CustomerLeadId;
            OpportunityDAO.ClosingDate = Opportunity.ClosingDate;
            OpportunityDAO.SaleStageId = Opportunity.SaleStageId;
            OpportunityDAO.ProbabilityId = Opportunity.ProbabilityId;
            OpportunityDAO.PotentialResultId = Opportunity.PotentialResultId;
            OpportunityDAO.LeadSourceId = Opportunity.LeadSourceId;
            OpportunityDAO.AppUserId = Opportunity.AppUserId;
            OpportunityDAO.CurrencyId = Opportunity.CurrencyId;
            OpportunityDAO.Amount = Opportunity.Amount;
            OpportunityDAO.ForecastAmount = Opportunity.ForecastAmount;
            OpportunityDAO.Description = Opportunity.Description;
            OpportunityDAO.RefuseReciveSMS = Opportunity.RefuseReciveSMS;
            OpportunityDAO.RefuseReciveEmail = Opportunity.RefuseReciveEmail;
            OpportunityDAO.OpportunityResultTypeId = Opportunity.OpportunityResultTypeId;
            OpportunityDAO.CreatorId = Opportunity.CreatorId;
            //OpportunityDAO.OrganizationId = Opportunity.OrganizationId;
            OpportunityDAO.CreatedAt = StaticParams.DateTimeNow;
            OpportunityDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.Opportunity.Add(OpportunityDAO);
            await DataContext.SaveChangesAsync();
            Opportunity.Id = OpportunityDAO.Id;
            await SaveReference(Opportunity);
            return true;
        }

        public async Task<bool> Update(Opportunity Opportunity)
        {
            OpportunityDAO OpportunityDAO = DataContext.Opportunity.Where(x => x.Id == Opportunity.Id).FirstOrDefault();
            if (OpportunityDAO == null)
                return false;
            OpportunityDAO.Id = Opportunity.Id;
            OpportunityDAO.Name = Opportunity.Name;
            OpportunityDAO.CompanyId = Opportunity.CompanyId;
            OpportunityDAO.CustomerLeadId = Opportunity.CustomerLeadId;
            OpportunityDAO.ClosingDate = Opportunity.ClosingDate;
            OpportunityDAO.SaleStageId = Opportunity.SaleStageId;
            OpportunityDAO.ProbabilityId = Opportunity.ProbabilityId;
            OpportunityDAO.PotentialResultId = Opportunity.PotentialResultId;
            OpportunityDAO.LeadSourceId = Opportunity.LeadSourceId;
            OpportunityDAO.AppUserId = Opportunity.AppUserId;
            OpportunityDAO.CurrencyId = Opportunity.CurrencyId;
            OpportunityDAO.Amount = Opportunity.Amount;
            OpportunityDAO.ForecastAmount = Opportunity.ForecastAmount;
            OpportunityDAO.Description = Opportunity.Description;
            OpportunityDAO.RefuseReciveSMS = Opportunity.RefuseReciveSMS;
            OpportunityDAO.RefuseReciveEmail = Opportunity.RefuseReciveEmail;
            OpportunityDAO.OpportunityResultTypeId = Opportunity.OpportunityResultTypeId;
            OpportunityDAO.CreatorId = Opportunity.CreatorId;
            OpportunityDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(Opportunity);
            return true;
        }

        public async Task<bool> Delete(Opportunity Opportunity)
        {
            await DataContext.Opportunity.Where(x => x.Id == Opportunity.Id).UpdateFromQueryAsync(x => new OpportunityDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<Opportunity> Opportunities)
        {
            List<OpportunityDAO> OpportunityDAOs = new List<OpportunityDAO>();
            foreach (Opportunity Opportunity in Opportunities)
            {
                OpportunityDAO OpportunityDAO = new OpportunityDAO();
                OpportunityDAO.Id = Opportunity.Id;
                OpportunityDAO.Name = Opportunity.Name;
                OpportunityDAO.CompanyId = Opportunity.CompanyId;
                OpportunityDAO.CustomerLeadId = Opportunity.CustomerLeadId;
                OpportunityDAO.ClosingDate = Opportunity.ClosingDate;
                OpportunityDAO.SaleStageId = Opportunity.SaleStageId;
                OpportunityDAO.ProbabilityId = Opportunity.ProbabilityId;
                OpportunityDAO.PotentialResultId = Opportunity.PotentialResultId;
                OpportunityDAO.LeadSourceId = Opportunity.LeadSourceId;
                OpportunityDAO.AppUserId = Opportunity.AppUserId;
                OpportunityDAO.CurrencyId = Opportunity.CurrencyId;
                OpportunityDAO.Amount = Opportunity.Amount;
                OpportunityDAO.ForecastAmount = Opportunity.ForecastAmount;
                OpportunityDAO.Description = Opportunity.Description;
                OpportunityDAO.RefuseReciveSMS = Opportunity.RefuseReciveSMS;
                OpportunityDAO.RefuseReciveEmail = Opportunity.RefuseReciveEmail;
                OpportunityDAO.OpportunityResultTypeId = Opportunity.OpportunityResultTypeId;
                OpportunityDAO.CreatorId = Opportunity.CreatorId;
                //OpportunityDAO.OrganizationId = Opportunity.OrganizationId;
                OpportunityDAO.CreatedAt = StaticParams.DateTimeNow;
                OpportunityDAO.UpdatedAt = StaticParams.DateTimeNow;
                OpportunityDAOs.Add(OpportunityDAO);
            }
            await DataContext.BulkMergeAsync(OpportunityDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<Opportunity> Opportunities)
        {
            List<long> Ids = Opportunities.Select(x => x.Id).ToList();
            await DataContext.Opportunity
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new OpportunityDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        public async Task<bool> ChangeStatus(long Id, long StatusId)
        {
            OpportunityDAO OpportunityDAO = DataContext.Opportunity.Where(x => x.Id == Id).FirstOrDefault();
            if (OpportunityDAO == null)
                return false;
            OpportunityDAO.SaleStageId = StatusId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        private async Task SaveReference(Opportunity Opportunity)
        {
            await DataContext.OpportunityItemMapping
                .Where(x => x.OpportunityId == Opportunity.Id)
                .DeleteFromQueryAsync();
            List<OpportunityItemMappingDAO> OpportunityItemMappingDAOs = new List<OpportunityItemMappingDAO>();
            if (Opportunity.OpportunityItemMappings != null)
            {
                foreach (OpportunityItemMapping OpportunityItemMapping in Opportunity.OpportunityItemMappings)
                {
                    OpportunityItemMappingDAO OpportunityItemMappingDAO = new OpportunityItemMappingDAO();
                    OpportunityItemMappingDAO.OpportunityId = Opportunity.Id;
                    OpportunityItemMappingDAO.ItemId = OpportunityItemMapping.ItemId;
                    OpportunityItemMappingDAO.UnitOfMeasureId = OpportunityItemMapping.UnitOfMeasureId;
                    OpportunityItemMappingDAO.Quantity = OpportunityItemMapping.Quantity;
                    OpportunityItemMappingDAO.RequestQuantity = OpportunityItemMapping.RequestQuantity;
                    OpportunityItemMappingDAO.PrimaryPrice = OpportunityItemMapping.PrimaryPrice;
                    OpportunityItemMappingDAO.SalePrice = OpportunityItemMapping.SalePrice;
                    OpportunityItemMappingDAO.DiscountPercentage = OpportunityItemMapping.DiscountPercentage;
                    OpportunityItemMappingDAO.Discount = OpportunityItemMapping.Discount;
                    OpportunityItemMappingDAO.VAT = OpportunityItemMapping.VAT;
                    OpportunityItemMappingDAO.VATOther = OpportunityItemMapping.VATOther;
                    OpportunityItemMappingDAO.Amount = OpportunityItemMapping.Amount;
                    OpportunityItemMappingDAO.Factor = OpportunityItemMapping.Factor;
                    OpportunityItemMappingDAOs.Add(OpportunityItemMappingDAO);
                }
                await DataContext.OpportunityItemMapping.BulkMergeAsync(OpportunityItemMappingDAOs);
            }

            #region OpportunityContactMapping
            await DataContext.OpportunityContactMapping
                .Where(x => x.OpportunityId == Opportunity.Id)
                .DeleteFromQueryAsync();
            List<OpportunityContactMappingDAO> OpportunityContactMappingDAOs = new List<OpportunityContactMappingDAO>();
            if (Opportunity.OpportunityContactMappings != null)
            {
                foreach (OpportunityContactMapping OpportunityContactMapping in Opportunity.OpportunityContactMappings)
                {
                    OpportunityContactMappingDAO OpportunityContactMappingDAO = new OpportunityContactMappingDAO();
                    OpportunityContactMappingDAO.ContactId = OpportunityContactMapping.ContactId;
                    OpportunityContactMappingDAO.OpportunityId = Opportunity.Id;
                    OpportunityContactMappingDAOs.Add(OpportunityContactMappingDAO);
                }
                await DataContext.OpportunityContactMapping.BulkMergeAsync(OpportunityContactMappingDAOs);
            }
            #endregion

            List<OpportunityFileGroupingDAO> OpportunityFileGroupingDAOs = await DataContext.OpportunityFileGrouping.Where(x => x.OpportunityId == Opportunity.Id).ToListAsync();
            OpportunityFileGroupingDAOs.ForEach(x => x.DeletedAt = StaticParams.DateTimeNow);
            await DataContext.OpportunityFileMapping.Where(x => x.OpportunityFileGrouping.OpportunityId == Opportunity.Id).DeleteFromQueryAsync();
            List<OpportunityFileMappingDAO> OpportunityFileMappingDAOs = new List<OpportunityFileMappingDAO>();
            if (Opportunity.OpportunityFileGroupings != null)
            {
                foreach (var OpportunityFileGrouping in Opportunity.OpportunityFileGroupings)
                {
                    OpportunityFileGroupingDAO OpportunityFileGroupingDAO = OpportunityFileGroupingDAOs.Where(x => x.Id == OpportunityFileGrouping.Id && x.Id != 0).FirstOrDefault();
                    if (OpportunityFileGroupingDAO == null)
                    {
                        OpportunityFileGroupingDAO = new OpportunityFileGroupingDAO
                        {
                            CreatorId = OpportunityFileGrouping.CreatorId,
                            OpportunityId = Opportunity.Id,
                            Description = OpportunityFileGrouping.Description,
                            Title = OpportunityFileGrouping.Title,
                            FileTypeId = OpportunityFileGrouping.FileTypeId,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow,
                            RowId = Guid.NewGuid()
                        };
                        OpportunityFileGroupingDAOs.Add(OpportunityFileGroupingDAO);
                        OpportunityFileGrouping.RowId = OpportunityFileGroupingDAO.RowId;
                    }
                    else
                    {
                        OpportunityFileGroupingDAO.Description = OpportunityFileGrouping.Description;
                        OpportunityFileGroupingDAO.Title = OpportunityFileGrouping.Title;
                        OpportunityFileGroupingDAO.FileTypeId = OpportunityFileGrouping.FileTypeId;
                        OpportunityFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
                        OpportunityFileGroupingDAO.DeletedAt = null;
                        OpportunityFileGrouping.RowId = OpportunityFileGroupingDAO.RowId;
                    }
                }
            }
            await DataContext.BulkMergeAsync(OpportunityFileGroupingDAOs);

            if (Opportunity.OpportunityFileGroupings != null)
            {
                foreach (var OpportunityFileGrouping in Opportunity.OpportunityFileGroupings)
                {
                    OpportunityFileGrouping.Id = OpportunityFileGroupingDAOs.Where(x => x.RowId == OpportunityFileGrouping.RowId).Select(x => x.Id).FirstOrDefault();
                    if (OpportunityFileGrouping.OpportunityFileMappings != null)
                    {
                        foreach (var OpportunityFileMapping in OpportunityFileGrouping.OpportunityFileMappings)
                        {
                            OpportunityFileMappingDAO OpportunityFileMappingDAO = new OpportunityFileMappingDAO
                            {
                                FileId = OpportunityFileMapping.FileId,
                                OpportunityFileGroupingId = OpportunityFileGrouping.Id,
                            };
                            OpportunityFileMappingDAOs.Add(OpportunityFileMappingDAO);
                        }
                    }
                }
            }
            await DataContext.BulkMergeAsync(OpportunityFileMappingDAOs);
        }
    }
}
