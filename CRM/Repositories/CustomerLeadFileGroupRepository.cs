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
    public interface ICustomerLeadFileGroupRepository
    {
        Task<int> Count(CustomerLeadFileGroupFilter CustomerLeadFileGroupFilter);
        Task<List<CustomerLeadFileGroup>> List(CustomerLeadFileGroupFilter CustomerLeadFileGroupFilter);
        Task<List<CustomerLeadFileGroup>> List(List<long> Ids);
        Task<CustomerLeadFileGroup> Get(long Id);
        Task<bool> Create(CustomerLeadFileGroup CustomerLeadFileGroup);
        Task<bool> Update(CustomerLeadFileGroup CustomerLeadFileGroup);
        Task<bool> Delete(CustomerLeadFileGroup CustomerLeadFileGroup);
        Task<bool> BulkMerge(List<CustomerLeadFileGroup> CustomerLeadFileGroups);
        Task<bool> BulkDelete(List<CustomerLeadFileGroup> CustomerLeadFileGroups);
    }
    public class CustomerLeadFileGroupRepository : ICustomerLeadFileGroupRepository
    {
        private DataContext DataContext;
        public CustomerLeadFileGroupRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerLeadFileGroupDAO> DynamicFilter(IQueryable<CustomerLeadFileGroupDAO> query, CustomerLeadFileGroupFilter filter)
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
            if (filter.CustomerLeadId != null && filter.CustomerLeadId.HasValue)
                query = query.Where(q => q.CustomerLeadId, filter.CustomerLeadId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.FileTypeId != null && filter.FileTypeId.HasValue)
                query = query.Where(q => q.FileTypeId, filter.FileTypeId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerLeadFileGroupDAO> OrFilter(IQueryable<CustomerLeadFileGroupDAO> query, CustomerLeadFileGroupFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerLeadFileGroupDAO> initQuery = query.Where(q => false);
            foreach (CustomerLeadFileGroupFilter CustomerLeadFileGroupFilter in filter.OrFilter)
            {
                IQueryable<CustomerLeadFileGroupDAO> queryable = query;
                if (CustomerLeadFileGroupFilter.Id != null && CustomerLeadFileGroupFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerLeadFileGroupFilter.Id);
                if (CustomerLeadFileGroupFilter.Title != null && CustomerLeadFileGroupFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, CustomerLeadFileGroupFilter.Title);
                if (CustomerLeadFileGroupFilter.Description != null && CustomerLeadFileGroupFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, CustomerLeadFileGroupFilter.Description);
                if (CustomerLeadFileGroupFilter.CustomerLeadId != null && CustomerLeadFileGroupFilter.CustomerLeadId.HasValue)
                    queryable = queryable.Where(q => q.CustomerLeadId, CustomerLeadFileGroupFilter.CustomerLeadId);
                if (CustomerLeadFileGroupFilter.CreatorId != null && CustomerLeadFileGroupFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, CustomerLeadFileGroupFilter.CreatorId);
                if (CustomerLeadFileGroupFilter.FileTypeId != null && CustomerLeadFileGroupFilter.FileTypeId.HasValue)
                    queryable = queryable.Where(q => q.FileTypeId, CustomerLeadFileGroupFilter.FileTypeId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerLeadFileGroupDAO> DynamicOrder(IQueryable<CustomerLeadFileGroupDAO> query, CustomerLeadFileGroupFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadFileGroupOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerLeadFileGroupOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case CustomerLeadFileGroupOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case CustomerLeadFileGroupOrder.CustomerLead:
                            query = query.OrderBy(q => q.CustomerLeadId);
                            break;
                        case CustomerLeadFileGroupOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case CustomerLeadFileGroupOrder.FileType:
                            query = query.OrderBy(q => q.FileTypeId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadFileGroupOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerLeadFileGroupOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case CustomerLeadFileGroupOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case CustomerLeadFileGroupOrder.CustomerLead:
                            query = query.OrderByDescending(q => q.CustomerLeadId);
                            break;
                        case CustomerLeadFileGroupOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case CustomerLeadFileGroupOrder.FileType:
                            query = query.OrderByDescending(q => q.FileTypeId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerLeadFileGroup>> DynamicSelect(IQueryable<CustomerLeadFileGroupDAO> query, CustomerLeadFileGroupFilter filter)
        {
            List<CustomerLeadFileGroup> CustomerLeadFileGroups = await query.Select(q => new CustomerLeadFileGroup()
            {
                Id = filter.Selects.Contains(CustomerLeadFileGroupSelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(CustomerLeadFileGroupSelect.Title) ? q.Title : default(string),
                Description = filter.Selects.Contains(CustomerLeadFileGroupSelect.Description) ? q.Description : default(string),
                CustomerLeadId = filter.Selects.Contains(CustomerLeadFileGroupSelect.CustomerLead) ? q.CustomerLeadId : default(long),
                CreatorId = filter.Selects.Contains(CustomerLeadFileGroupSelect.Creator) ? q.CreatorId : default(long),
                FileTypeId = filter.Selects.Contains(CustomerLeadFileGroupSelect.FileType) ? q.FileTypeId : default(long),
                Creator = filter.Selects.Contains(CustomerLeadFileGroupSelect.Creator) && q.Creator != null ? new AppUser
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
                CustomerLead = filter.Selects.Contains(CustomerLeadFileGroupSelect.CustomerLead) && q.CustomerLead != null ? new CustomerLead
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
                FileType = filter.Selects.Contains(CustomerLeadFileGroupSelect.FileType) && q.FileType != null ? new FileType
                {
                    Id = q.FileType.Id,
                    Code = q.FileType.Code,
                    Name = q.FileType.Name,
                } : null,
            }).ToListAsync();
            return CustomerLeadFileGroups;
        }

        public async Task<int> Count(CustomerLeadFileGroupFilter filter)
        {
            IQueryable<CustomerLeadFileGroupDAO> CustomerLeadFileGroups = DataContext.CustomerLeadFileGroup.AsNoTracking();
            CustomerLeadFileGroups = DynamicFilter(CustomerLeadFileGroups, filter);
            return await CustomerLeadFileGroups.CountAsync();
        }

        public async Task<List<CustomerLeadFileGroup>> List(CustomerLeadFileGroupFilter filter)
        {
            if (filter == null) return new List<CustomerLeadFileGroup>();
            IQueryable<CustomerLeadFileGroupDAO> CustomerLeadFileGroupDAOs = DataContext.CustomerLeadFileGroup.AsNoTracking();
            CustomerLeadFileGroupDAOs = DynamicFilter(CustomerLeadFileGroupDAOs, filter);
            CustomerLeadFileGroupDAOs = DynamicOrder(CustomerLeadFileGroupDAOs, filter);
            List<CustomerLeadFileGroup> CustomerLeadFileGroups = await DynamicSelect(CustomerLeadFileGroupDAOs, filter);
            return CustomerLeadFileGroups;
        }

        public async Task<List<CustomerLeadFileGroup>> List(List<long> Ids)
        {
            List<CustomerLeadFileGroup> CustomerLeadFileGroups = await DataContext.CustomerLeadFileGroup.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerLeadFileGroup()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CustomerLeadId = x.CustomerLeadId,
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
                FileType = x.FileType == null ? null : new FileType
                {
                    Id = x.FileType.Id,
                    Code = x.FileType.Code,
                    Name = x.FileType.Name,
                },
            }).ToListAsync();
            

            return CustomerLeadFileGroups;
        }

        public async Task<CustomerLeadFileGroup> Get(long Id)
        {
            CustomerLeadFileGroup CustomerLeadFileGroup = await DataContext.CustomerLeadFileGroup.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CustomerLeadFileGroup()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CustomerLeadId = x.CustomerLeadId,
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
                FileType = x.FileType == null ? null : new FileType
                {
                    Id = x.FileType.Id,
                    Code = x.FileType.Code,
                    Name = x.FileType.Name,
                },
            }).FirstOrDefaultAsync();

            if (CustomerLeadFileGroup == null)
                return null;

            return CustomerLeadFileGroup;
        }
        public async Task<bool> Create(CustomerLeadFileGroup CustomerLeadFileGroup)
        {
            CustomerLeadFileGroupDAO CustomerLeadFileGroupDAO = new CustomerLeadFileGroupDAO();
            CustomerLeadFileGroupDAO.Id = CustomerLeadFileGroup.Id;
            CustomerLeadFileGroupDAO.Title = CustomerLeadFileGroup.Title;
            CustomerLeadFileGroupDAO.Description = CustomerLeadFileGroup.Description;
            CustomerLeadFileGroupDAO.CustomerLeadId = CustomerLeadFileGroup.CustomerLeadId;
            CustomerLeadFileGroupDAO.CreatorId = CustomerLeadFileGroup.CreatorId;
            CustomerLeadFileGroupDAO.FileTypeId = CustomerLeadFileGroup.FileTypeId;
            CustomerLeadFileGroupDAO.RowId = CustomerLeadFileGroup.RowId;
            CustomerLeadFileGroupDAO.CreatedAt = StaticParams.DateTimeNow;
            CustomerLeadFileGroupDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CustomerLeadFileGroup.Add(CustomerLeadFileGroupDAO);
            await DataContext.SaveChangesAsync();
            CustomerLeadFileGroup.Id = CustomerLeadFileGroupDAO.Id;
            await SaveReference(CustomerLeadFileGroup);
            return true;
        }

        public async Task<bool> Update(CustomerLeadFileGroup CustomerLeadFileGroup)
        {
            CustomerLeadFileGroupDAO CustomerLeadFileGroupDAO = DataContext.CustomerLeadFileGroup.Where(x => x.Id == CustomerLeadFileGroup.Id).FirstOrDefault();
            if (CustomerLeadFileGroupDAO == null)
                return false;
            CustomerLeadFileGroupDAO.Id = CustomerLeadFileGroup.Id;
            CustomerLeadFileGroupDAO.Title = CustomerLeadFileGroup.Title;
            CustomerLeadFileGroupDAO.Description = CustomerLeadFileGroup.Description;
            CustomerLeadFileGroupDAO.CustomerLeadId = CustomerLeadFileGroup.CustomerLeadId;
            CustomerLeadFileGroupDAO.CreatorId = CustomerLeadFileGroup.CreatorId;
            CustomerLeadFileGroupDAO.FileTypeId = CustomerLeadFileGroup.FileTypeId;
            CustomerLeadFileGroupDAO.RowId = CustomerLeadFileGroup.RowId;
            CustomerLeadFileGroupDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerLeadFileGroup);
            return true;
        }

        public async Task<bool> Delete(CustomerLeadFileGroup CustomerLeadFileGroup)
        {
            await DataContext.CustomerLeadFileGroup.Where(x => x.Id == CustomerLeadFileGroup.Id).UpdateFromQueryAsync(x => new CustomerLeadFileGroupDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerLeadFileGroup> CustomerLeadFileGroups)
        {
            List<CustomerLeadFileGroupDAO> CustomerLeadFileGroupDAOs = new List<CustomerLeadFileGroupDAO>();
            foreach (CustomerLeadFileGroup CustomerLeadFileGroup in CustomerLeadFileGroups)
            {
                CustomerLeadFileGroupDAO CustomerLeadFileGroupDAO = new CustomerLeadFileGroupDAO();
                CustomerLeadFileGroupDAO.Id = CustomerLeadFileGroup.Id;
                CustomerLeadFileGroupDAO.Title = CustomerLeadFileGroup.Title;
                CustomerLeadFileGroupDAO.Description = CustomerLeadFileGroup.Description;
                CustomerLeadFileGroupDAO.CustomerLeadId = CustomerLeadFileGroup.CustomerLeadId;
                CustomerLeadFileGroupDAO.CreatorId = CustomerLeadFileGroup.CreatorId;
                CustomerLeadFileGroupDAO.FileTypeId = CustomerLeadFileGroup.FileTypeId;
                CustomerLeadFileGroupDAO.RowId = CustomerLeadFileGroup.RowId;
                CustomerLeadFileGroupDAO.CreatedAt = StaticParams.DateTimeNow;
                CustomerLeadFileGroupDAO.UpdatedAt = StaticParams.DateTimeNow;
                CustomerLeadFileGroupDAOs.Add(CustomerLeadFileGroupDAO);
            }
            await DataContext.BulkMergeAsync(CustomerLeadFileGroupDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerLeadFileGroup> CustomerLeadFileGroups)
        {
            List<long> Ids = CustomerLeadFileGroups.Select(x => x.Id).ToList();
            await DataContext.CustomerLeadFileGroup
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CustomerLeadFileGroupDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CustomerLeadFileGroup CustomerLeadFileGroup)
        {
        }
        
    }
}
