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
    public interface ICustomerLeadActivityRepository
    {
        Task<int> Count(CustomerLeadActivityFilter CustomerLeadActivityFilter);
        Task<List<CustomerLeadActivity>> List(CustomerLeadActivityFilter CustomerLeadActivityFilter);
        Task<List<CustomerLeadActivity>> List(List<long> Ids);
        Task<CustomerLeadActivity> Get(long Id);
        Task<bool> Create(CustomerLeadActivity CustomerLeadActivity);
        Task<bool> Update(CustomerLeadActivity CustomerLeadActivity);
        Task<bool> Delete(CustomerLeadActivity CustomerLeadActivity);
        Task<bool> BulkMerge(List<CustomerLeadActivity> CustomerLeadActivities);
        Task<bool> BulkDelete(List<CustomerLeadActivity> CustomerLeadActivities);
    }
    public class CustomerLeadActivityRepository : ICustomerLeadActivityRepository
    {
        private DataContext DataContext;
        public CustomerLeadActivityRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerLeadActivityDAO> DynamicFilter(IQueryable<CustomerLeadActivityDAO> query, CustomerLeadActivityFilter filter)
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
            if (filter.CustomerLeadId != null && filter.CustomerLeadId.HasValue)
                query = query.Where(q => q.CustomerLeadId, filter.CustomerLeadId);
            if (filter.AppUserId != null && filter.AppUserId.HasValue)
                query = query.Where(q => q.AppUserId, filter.AppUserId);
            if (filter.ActivityStatusId != null && filter.ActivityStatusId.HasValue)
                query = query.Where(q => q.ActivityStatusId, filter.ActivityStatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerLeadActivityDAO> OrFilter(IQueryable<CustomerLeadActivityDAO> query, CustomerLeadActivityFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerLeadActivityDAO> initQuery = query.Where(q => false);
            foreach (CustomerLeadActivityFilter CustomerLeadActivityFilter in filter.OrFilter)
            {
                IQueryable<CustomerLeadActivityDAO> queryable = query;
                if (CustomerLeadActivityFilter.Id != null && CustomerLeadActivityFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerLeadActivityFilter.Id);
                if (CustomerLeadActivityFilter.Title != null && CustomerLeadActivityFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, CustomerLeadActivityFilter.Title);
                if (CustomerLeadActivityFilter.FromDate != null && CustomerLeadActivityFilter.FromDate.HasValue)
                    queryable = queryable.Where(q => q.FromDate, CustomerLeadActivityFilter.FromDate);
                if (CustomerLeadActivityFilter.ToDate != null && CustomerLeadActivityFilter.ToDate.HasValue)
                    queryable = queryable.Where(q => q.ToDate.HasValue).Where(q => q.ToDate, CustomerLeadActivityFilter.ToDate);
                if (CustomerLeadActivityFilter.ActivityTypeId != null && CustomerLeadActivityFilter.ActivityTypeId.HasValue)
                    queryable = queryable.Where(q => q.ActivityTypeId, CustomerLeadActivityFilter.ActivityTypeId);
                if (CustomerLeadActivityFilter.ActivityPriorityId != null && CustomerLeadActivityFilter.ActivityPriorityId.HasValue)
                    queryable = queryable.Where(q => q.ActivityPriorityId.HasValue).Where(q => q.ActivityPriorityId, CustomerLeadActivityFilter.ActivityPriorityId);
                if (CustomerLeadActivityFilter.Description != null && CustomerLeadActivityFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, CustomerLeadActivityFilter.Description);
                if (CustomerLeadActivityFilter.Address != null && CustomerLeadActivityFilter.Address.HasValue)
                    queryable = queryable.Where(q => q.Address, CustomerLeadActivityFilter.Address);
                if (CustomerLeadActivityFilter.CustomerLeadId != null && CustomerLeadActivityFilter.CustomerLeadId.HasValue)
                    queryable = queryable.Where(q => q.CustomerLeadId, CustomerLeadActivityFilter.CustomerLeadId);
                if (CustomerLeadActivityFilter.AppUserId != null && CustomerLeadActivityFilter.AppUserId.HasValue)
                    queryable = queryable.Where(q => q.AppUserId, CustomerLeadActivityFilter.AppUserId);
                if (CustomerLeadActivityFilter.ActivityStatusId != null && CustomerLeadActivityFilter.ActivityStatusId.HasValue)
                    queryable = queryable.Where(q => q.ActivityStatusId, CustomerLeadActivityFilter.ActivityStatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerLeadActivityDAO> DynamicOrder(IQueryable<CustomerLeadActivityDAO> query, CustomerLeadActivityFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadActivityOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerLeadActivityOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case CustomerLeadActivityOrder.FromDate:
                            query = query.OrderBy(q => q.FromDate);
                            break;
                        case CustomerLeadActivityOrder.ToDate:
                            query = query.OrderBy(q => q.ToDate);
                            break;
                        case CustomerLeadActivityOrder.ActivityType:
                            query = query.OrderBy(q => q.ActivityTypeId);
                            break;
                        case CustomerLeadActivityOrder.ActivityPriority:
                            query = query.OrderBy(q => q.ActivityPriorityId);
                            break;
                        case CustomerLeadActivityOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case CustomerLeadActivityOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case CustomerLeadActivityOrder.CustomerLead:
                            query = query.OrderBy(q => q.CustomerLeadId);
                            break;
                        case CustomerLeadActivityOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case CustomerLeadActivityOrder.ActivityStatus:
                            query = query.OrderBy(q => q.ActivityStatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadActivityOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerLeadActivityOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case CustomerLeadActivityOrder.FromDate:
                            query = query.OrderByDescending(q => q.FromDate);
                            break;
                        case CustomerLeadActivityOrder.ToDate:
                            query = query.OrderByDescending(q => q.ToDate);
                            break;
                        case CustomerLeadActivityOrder.ActivityType:
                            query = query.OrderByDescending(q => q.ActivityTypeId);
                            break;
                        case CustomerLeadActivityOrder.ActivityPriority:
                            query = query.OrderByDescending(q => q.ActivityPriorityId);
                            break;
                        case CustomerLeadActivityOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case CustomerLeadActivityOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case CustomerLeadActivityOrder.CustomerLead:
                            query = query.OrderByDescending(q => q.CustomerLeadId);
                            break;
                        case CustomerLeadActivityOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case CustomerLeadActivityOrder.ActivityStatus:
                            query = query.OrderByDescending(q => q.ActivityStatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerLeadActivity>> DynamicSelect(IQueryable<CustomerLeadActivityDAO> query, CustomerLeadActivityFilter filter)
        {
            List<CustomerLeadActivity> CustomerLeadActivities = await query.Select(q => new CustomerLeadActivity()
            {
                Id = filter.Selects.Contains(CustomerLeadActivitySelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(CustomerLeadActivitySelect.Title) ? q.Title : default(string),
                FromDate = filter.Selects.Contains(CustomerLeadActivitySelect.FromDate) ? q.FromDate : default(DateTime),
                ToDate = filter.Selects.Contains(CustomerLeadActivitySelect.ToDate) ? q.ToDate : default(DateTime?),
                ActivityTypeId = filter.Selects.Contains(CustomerLeadActivitySelect.ActivityType) ? q.ActivityTypeId : default(long),
                ActivityPriorityId = filter.Selects.Contains(CustomerLeadActivitySelect.ActivityPriority) ? q.ActivityPriorityId : default(long?),
                Description = filter.Selects.Contains(CustomerLeadActivitySelect.Description) ? q.Description : default(string),
                Address = filter.Selects.Contains(CustomerLeadActivitySelect.Address) ? q.Address : default(string),
                CustomerLeadId = filter.Selects.Contains(CustomerLeadActivitySelect.CustomerLead) ? q.CustomerLeadId : default(long),
                AppUserId = filter.Selects.Contains(CustomerLeadActivitySelect.AppUser) ? q.AppUserId : default(long),
                ActivityStatusId = filter.Selects.Contains(CustomerLeadActivitySelect.ActivityStatus) ? q.ActivityStatusId : default(long),
                ActivityPriority = filter.Selects.Contains(CustomerLeadActivitySelect.ActivityPriority) && q.ActivityPriority != null ? new ActivityPriority
                {
                    Id = q.ActivityPriority.Id,
                    Code = q.ActivityPriority.Code,
                    Name = q.ActivityPriority.Name,
                } : null,
                ActivityStatus = filter.Selects.Contains(CustomerLeadActivitySelect.ActivityStatus) && q.ActivityStatus != null ? new ActivityStatus
                {
                    Id = q.ActivityStatus.Id,
                    Code = q.ActivityStatus.Code,
                    Name = q.ActivityStatus.Name,
                } : null,
                ActivityType = filter.Selects.Contains(CustomerLeadActivitySelect.ActivityType) && q.ActivityType != null ? new ActivityType
                {
                    Id = q.ActivityType.Id,
                    Code = q.ActivityType.Code,
                    Name = q.ActivityType.Name,
                } : null,
                AppUser = filter.Selects.Contains(CustomerLeadActivitySelect.AppUser) && q.AppUser != null ? new AppUser
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
                CustomerLead = filter.Selects.Contains(CustomerLeadActivitySelect.CustomerLead) && q.CustomerLead != null ? new CustomerLead
                {
                    Id = q.CustomerLead.Id,
                    Name = q.CustomerLead.Name,
                    TelePhone = q.CustomerLead.TelePhone,
                    Phone = q.CustomerLead.Phone,
                    CompanyName = q.CustomerLead.CompanyName,
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
                    ProvinceId = q.CustomerLead.ProvinceId,
                    DistrictId = q.CustomerLead.DistrictId,
                    AppUserId = q.CustomerLead.AppUserId,
                    CustomerLeadStatusId = q.CustomerLead.CustomerLeadStatusId,
                    BusinessRegistrationCode = q.CustomerLead.BusinessRegistrationCode,
                    SexId = q.CustomerLead.SexId,
                    RefuseReciveSMS = q.CustomerLead.RefuseReciveSMS,
                    NationId = q.CustomerLead.NationId,
                    RefuseReciveEmail = q.CustomerLead.RefuseReciveEmail,
                    Description = q.CustomerLead.Description,
                    CreatorId = q.CustomerLead.CreatorId,
                    ZipCode = q.CustomerLead.ZipCode,
                    CurrencyId = q.CustomerLead.CurrencyId,
                    RowId = q.CustomerLead.RowId,
                } : null,
            }).ToListAsync();
            return CustomerLeadActivities;
        }

        public async Task<int> Count(CustomerLeadActivityFilter filter)
        {
            IQueryable<CustomerLeadActivityDAO> CustomerLeadActivities = DataContext.CustomerLeadActivity.AsNoTracking();
            CustomerLeadActivities = DynamicFilter(CustomerLeadActivities, filter);
            return await CustomerLeadActivities.CountAsync();
        }

        public async Task<List<CustomerLeadActivity>> List(CustomerLeadActivityFilter filter)
        {
            if (filter == null) return new List<CustomerLeadActivity>();
            IQueryable<CustomerLeadActivityDAO> CustomerLeadActivityDAOs = DataContext.CustomerLeadActivity.AsNoTracking();
            CustomerLeadActivityDAOs = DynamicFilter(CustomerLeadActivityDAOs, filter);
            CustomerLeadActivityDAOs = DynamicOrder(CustomerLeadActivityDAOs, filter);
            List<CustomerLeadActivity> CustomerLeadActivities = await DynamicSelect(CustomerLeadActivityDAOs, filter);
            return CustomerLeadActivities;
        }

        public async Task<List<CustomerLeadActivity>> List(List<long> Ids)
        {
            List<CustomerLeadActivity> CustomerLeadActivities = await DataContext.CustomerLeadActivity.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerLeadActivity()
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
                CustomerLeadId = x.CustomerLeadId,
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
                CustomerLead = x.CustomerLead == null ? null : new CustomerLead
                {
                    Id = x.CustomerLead.Id,
                    Name = x.CustomerLead.Name,
                    TelePhone = x.CustomerLead.TelePhone,
                    Phone = x.CustomerLead.Phone,
                    CompanyName = x.CustomerLead.CompanyName,
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
                    ProvinceId = x.CustomerLead.ProvinceId,
                    DistrictId = x.CustomerLead.DistrictId,
                    AppUserId = x.CustomerLead.AppUserId,
                    CustomerLeadStatusId = x.CustomerLead.CustomerLeadStatusId,
                    BusinessRegistrationCode = x.CustomerLead.BusinessRegistrationCode,
                    SexId = x.CustomerLead.SexId,
                    RefuseReciveSMS = x.CustomerLead.RefuseReciveSMS,
                    NationId = x.CustomerLead.NationId,
                    RefuseReciveEmail = x.CustomerLead.RefuseReciveEmail,
                    Description = x.CustomerLead.Description,
                    CreatorId = x.CustomerLead.CreatorId,
                    ZipCode = x.CustomerLead.ZipCode,
                    CurrencyId = x.CustomerLead.CurrencyId,
                    RowId = x.CustomerLead.RowId,
                },
            }).ToListAsync();
            

            return CustomerLeadActivities;
        }

        public async Task<CustomerLeadActivity> Get(long Id)
        {
            CustomerLeadActivity CustomerLeadActivity = await DataContext.CustomerLeadActivity.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CustomerLeadActivity()
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
                CustomerLeadId = x.CustomerLeadId,
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
                CustomerLead = x.CustomerLead == null ? null : new CustomerLead
                {
                    Id = x.CustomerLead.Id,
                    Name = x.CustomerLead.Name,
                    TelePhone = x.CustomerLead.TelePhone,
                    Phone = x.CustomerLead.Phone,
                    CompanyName = x.CustomerLead.CompanyName,
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
                    ProvinceId = x.CustomerLead.ProvinceId,
                    DistrictId = x.CustomerLead.DistrictId,
                    AppUserId = x.CustomerLead.AppUserId,
                    CustomerLeadStatusId = x.CustomerLead.CustomerLeadStatusId,
                    BusinessRegistrationCode = x.CustomerLead.BusinessRegistrationCode,
                    SexId = x.CustomerLead.SexId,
                    RefuseReciveSMS = x.CustomerLead.RefuseReciveSMS,
                    NationId = x.CustomerLead.NationId,
                    RefuseReciveEmail = x.CustomerLead.RefuseReciveEmail,
                    Description = x.CustomerLead.Description,
                    CreatorId = x.CustomerLead.CreatorId,
                    ZipCode = x.CustomerLead.ZipCode,
                    CurrencyId = x.CustomerLead.CurrencyId,
                    RowId = x.CustomerLead.RowId,
                },
            }).FirstOrDefaultAsync();

            if (CustomerLeadActivity == null)
                return null;

            return CustomerLeadActivity;
        }
        public async Task<bool> Create(CustomerLeadActivity CustomerLeadActivity)
        {
            CustomerLeadActivityDAO CustomerLeadActivityDAO = new CustomerLeadActivityDAO();
            CustomerLeadActivityDAO.Id = CustomerLeadActivity.Id;
            CustomerLeadActivityDAO.Title = CustomerLeadActivity.Title;
            CustomerLeadActivityDAO.FromDate = CustomerLeadActivity.FromDate;
            CustomerLeadActivityDAO.ToDate = CustomerLeadActivity.ToDate;
            CustomerLeadActivityDAO.ActivityTypeId = CustomerLeadActivity.ActivityTypeId;
            CustomerLeadActivityDAO.ActivityPriorityId = CustomerLeadActivity.ActivityPriorityId;
            CustomerLeadActivityDAO.Description = CustomerLeadActivity.Description;
            CustomerLeadActivityDAO.Address = CustomerLeadActivity.Address;
            CustomerLeadActivityDAO.CustomerLeadId = CustomerLeadActivity.CustomerLeadId;
            CustomerLeadActivityDAO.AppUserId = CustomerLeadActivity.AppUserId;
            CustomerLeadActivityDAO.ActivityStatusId = CustomerLeadActivity.ActivityStatusId;
            CustomerLeadActivityDAO.CreatedAt = StaticParams.DateTimeNow;
            CustomerLeadActivityDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CustomerLeadActivity.Add(CustomerLeadActivityDAO);
            await DataContext.SaveChangesAsync();
            CustomerLeadActivity.Id = CustomerLeadActivityDAO.Id;
            await SaveReference(CustomerLeadActivity);
            return true;
        }

        public async Task<bool> Update(CustomerLeadActivity CustomerLeadActivity)
        {
            CustomerLeadActivityDAO CustomerLeadActivityDAO = DataContext.CustomerLeadActivity.Where(x => x.Id == CustomerLeadActivity.Id).FirstOrDefault();
            if (CustomerLeadActivityDAO == null)
                return false;
            CustomerLeadActivityDAO.Id = CustomerLeadActivity.Id;
            CustomerLeadActivityDAO.Title = CustomerLeadActivity.Title;
            CustomerLeadActivityDAO.FromDate = CustomerLeadActivity.FromDate;
            CustomerLeadActivityDAO.ToDate = CustomerLeadActivity.ToDate;
            CustomerLeadActivityDAO.ActivityTypeId = CustomerLeadActivity.ActivityTypeId;
            CustomerLeadActivityDAO.ActivityPriorityId = CustomerLeadActivity.ActivityPriorityId;
            CustomerLeadActivityDAO.Description = CustomerLeadActivity.Description;
            CustomerLeadActivityDAO.Address = CustomerLeadActivity.Address;
            CustomerLeadActivityDAO.CustomerLeadId = CustomerLeadActivity.CustomerLeadId;
            CustomerLeadActivityDAO.AppUserId = CustomerLeadActivity.AppUserId;
            CustomerLeadActivityDAO.ActivityStatusId = CustomerLeadActivity.ActivityStatusId;
            CustomerLeadActivityDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerLeadActivity);
            return true;
        }

        public async Task<bool> Delete(CustomerLeadActivity CustomerLeadActivity)
        {
            await DataContext.CustomerLeadActivity.Where(x => x.Id == CustomerLeadActivity.Id).UpdateFromQueryAsync(x => new CustomerLeadActivityDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerLeadActivity> CustomerLeadActivities)
        {
            List<CustomerLeadActivityDAO> CustomerLeadActivityDAOs = new List<CustomerLeadActivityDAO>();
            foreach (CustomerLeadActivity CustomerLeadActivity in CustomerLeadActivities)
            {
                CustomerLeadActivityDAO CustomerLeadActivityDAO = new CustomerLeadActivityDAO();
                CustomerLeadActivityDAO.Id = CustomerLeadActivity.Id;
                CustomerLeadActivityDAO.Title = CustomerLeadActivity.Title;
                CustomerLeadActivityDAO.FromDate = CustomerLeadActivity.FromDate;
                CustomerLeadActivityDAO.ToDate = CustomerLeadActivity.ToDate;
                CustomerLeadActivityDAO.ActivityTypeId = CustomerLeadActivity.ActivityTypeId;
                CustomerLeadActivityDAO.ActivityPriorityId = CustomerLeadActivity.ActivityPriorityId;
                CustomerLeadActivityDAO.Description = CustomerLeadActivity.Description;
                CustomerLeadActivityDAO.Address = CustomerLeadActivity.Address;
                CustomerLeadActivityDAO.CustomerLeadId = CustomerLeadActivity.CustomerLeadId;
                CustomerLeadActivityDAO.AppUserId = CustomerLeadActivity.AppUserId;
                CustomerLeadActivityDAO.ActivityStatusId = CustomerLeadActivity.ActivityStatusId;
                CustomerLeadActivityDAO.CreatedAt = StaticParams.DateTimeNow;
                CustomerLeadActivityDAO.UpdatedAt = StaticParams.DateTimeNow;
                CustomerLeadActivityDAOs.Add(CustomerLeadActivityDAO);
            }
            await DataContext.BulkMergeAsync(CustomerLeadActivityDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerLeadActivity> CustomerLeadActivities)
        {
            List<long> Ids = CustomerLeadActivities.Select(x => x.Id).ToList();
            await DataContext.CustomerLeadActivity
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CustomerLeadActivityDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CustomerLeadActivity CustomerLeadActivity)
        {
        }
        
    }
}
