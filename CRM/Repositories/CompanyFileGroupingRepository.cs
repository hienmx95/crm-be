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
    public interface ICompanyFileGroupingRepository
    {
        Task<int> Count(CompanyFileGroupingFilter CompanyFileGroupingFilter);
        Task<List<CompanyFileGrouping>> List(CompanyFileGroupingFilter CompanyFileGroupingFilter);
        Task<List<CompanyFileGrouping>> List(List<long> Ids);
        Task<CompanyFileGrouping> Get(long Id);
        Task<bool> Create(CompanyFileGrouping CompanyFileGrouping);
        Task<bool> Update(CompanyFileGrouping CompanyFileGrouping);
        Task<bool> Delete(CompanyFileGrouping CompanyFileGrouping);
        Task<bool> BulkMerge(List<CompanyFileGrouping> CompanyFileGroupings);
        Task<bool> BulkDelete(List<CompanyFileGrouping> CompanyFileGroupings);
    }
    public class CompanyFileGroupingRepository : ICompanyFileGroupingRepository
    {
        private DataContext DataContext;
        public CompanyFileGroupingRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CompanyFileGroupingDAO> DynamicFilter(IQueryable<CompanyFileGroupingDAO> query, CompanyFileGroupingFilter filter)
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
            if (filter.CompanyId != null && filter.CompanyId.HasValue)
                query = query.Where(q => q.CompanyId, filter.CompanyId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.FileTypeId != null && filter.FileTypeId.HasValue)
                query = query.Where(q => q.FileTypeId, filter.FileTypeId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CompanyFileGroupingDAO> OrFilter(IQueryable<CompanyFileGroupingDAO> query, CompanyFileGroupingFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CompanyFileGroupingDAO> initQuery = query.Where(q => false);
            foreach (CompanyFileGroupingFilter CompanyFileGroupingFilter in filter.OrFilter)
            {
                IQueryable<CompanyFileGroupingDAO> queryable = query;
                if (CompanyFileGroupingFilter.Id != null && CompanyFileGroupingFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CompanyFileGroupingFilter.Id);
                if (CompanyFileGroupingFilter.Title != null && CompanyFileGroupingFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, CompanyFileGroupingFilter.Title);
                if (CompanyFileGroupingFilter.Description != null && CompanyFileGroupingFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, CompanyFileGroupingFilter.Description);
                if (CompanyFileGroupingFilter.CompanyId != null && CompanyFileGroupingFilter.CompanyId.HasValue)
                    queryable = queryable.Where(q => q.CompanyId, CompanyFileGroupingFilter.CompanyId);
                if (CompanyFileGroupingFilter.CreatorId != null && CompanyFileGroupingFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, CompanyFileGroupingFilter.CreatorId);
                if (CompanyFileGroupingFilter.FileTypeId != null && CompanyFileGroupingFilter.FileTypeId.HasValue)
                    queryable = queryable.Where(q => q.FileTypeId, CompanyFileGroupingFilter.FileTypeId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CompanyFileGroupingDAO> DynamicOrder(IQueryable<CompanyFileGroupingDAO> query, CompanyFileGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CompanyFileGroupingOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CompanyFileGroupingOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case CompanyFileGroupingOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case CompanyFileGroupingOrder.Company:
                            query = query.OrderBy(q => q.CompanyId);
                            break;
                        case CompanyFileGroupingOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case CompanyFileGroupingOrder.FileType:
                            query = query.OrderBy(q => q.FileTypeId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CompanyFileGroupingOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CompanyFileGroupingOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case CompanyFileGroupingOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case CompanyFileGroupingOrder.Company:
                            query = query.OrderByDescending(q => q.CompanyId);
                            break;
                        case CompanyFileGroupingOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case CompanyFileGroupingOrder.FileType:
                            query = query.OrderByDescending(q => q.FileTypeId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CompanyFileGrouping>> DynamicSelect(IQueryable<CompanyFileGroupingDAO> query, CompanyFileGroupingFilter filter)
        {
            List<CompanyFileGrouping> CompanyFileGroupings = await query.Select(q => new CompanyFileGrouping()
            {
                Id = filter.Selects.Contains(CompanyFileGroupingSelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(CompanyFileGroupingSelect.Title) ? q.Title : default(string),
                Description = filter.Selects.Contains(CompanyFileGroupingSelect.Description) ? q.Description : default(string),
                CompanyId = filter.Selects.Contains(CompanyFileGroupingSelect.Company) ? q.CompanyId : default(long),
                CreatorId = filter.Selects.Contains(CompanyFileGroupingSelect.Creator) ? q.CreatorId : default(long),
                FileTypeId = filter.Selects.Contains(CompanyFileGroupingSelect.FileType) ? q.FileTypeId : default(long),
                Company = filter.Selects.Contains(CompanyFileGroupingSelect.Company) && q.Company != null ? new Company
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
                Creator = filter.Selects.Contains(CompanyFileGroupingSelect.Creator) && q.Creator != null ? new AppUser
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
                FileType = filter.Selects.Contains(CompanyFileGroupingSelect.FileType) && q.FileType != null ? new FileType
                {
                    Id = q.FileType.Id,
                    Code = q.FileType.Code,
                    Name = q.FileType.Name,
                } : null,
            }).ToListAsync();
            return CompanyFileGroupings;
        }

        public async Task<int> Count(CompanyFileGroupingFilter filter)
        {
            IQueryable<CompanyFileGroupingDAO> CompanyFileGroupings = DataContext.CompanyFileGrouping.AsNoTracking();
            CompanyFileGroupings = DynamicFilter(CompanyFileGroupings, filter);
            return await CompanyFileGroupings.CountAsync();
        }

        public async Task<List<CompanyFileGrouping>> List(CompanyFileGroupingFilter filter)
        {
            if (filter == null) return new List<CompanyFileGrouping>();
            IQueryable<CompanyFileGroupingDAO> CompanyFileGroupingDAOs = DataContext.CompanyFileGrouping.AsNoTracking();
            CompanyFileGroupingDAOs = DynamicFilter(CompanyFileGroupingDAOs, filter);
            CompanyFileGroupingDAOs = DynamicOrder(CompanyFileGroupingDAOs, filter);
            List<CompanyFileGrouping> CompanyFileGroupings = await DynamicSelect(CompanyFileGroupingDAOs, filter);
            return CompanyFileGroupings;
        }

        public async Task<List<CompanyFileGrouping>> List(List<long> Ids)
        {
            List<CompanyFileGrouping> CompanyFileGroupings = await DataContext.CompanyFileGrouping.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CompanyFileGrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CompanyId = x.CompanyId,
                CreatorId = x.CreatorId,
                FileTypeId = x.FileTypeId,
                RowId = x.RowId,
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
            }).ToListAsync();
            

            return CompanyFileGroupings;
        }

        public async Task<CompanyFileGrouping> Get(long Id)
        {
            CompanyFileGrouping CompanyFileGrouping = await DataContext.CompanyFileGrouping.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CompanyFileGrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CompanyId = x.CompanyId,
                CreatorId = x.CreatorId,
                FileTypeId = x.FileTypeId,
                RowId = x.RowId,
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
            }).FirstOrDefaultAsync();

            if (CompanyFileGrouping == null)
                return null;

            return CompanyFileGrouping;
        }
        public async Task<bool> Create(CompanyFileGrouping CompanyFileGrouping)
        {
            CompanyFileGroupingDAO CompanyFileGroupingDAO = new CompanyFileGroupingDAO();
            CompanyFileGroupingDAO.Id = CompanyFileGrouping.Id;
            CompanyFileGroupingDAO.Title = CompanyFileGrouping.Title;
            CompanyFileGroupingDAO.Description = CompanyFileGrouping.Description;
            CompanyFileGroupingDAO.CompanyId = CompanyFileGrouping.CompanyId;
            CompanyFileGroupingDAO.CreatorId = CompanyFileGrouping.CreatorId;
            CompanyFileGroupingDAO.FileTypeId = CompanyFileGrouping.FileTypeId;
            CompanyFileGroupingDAO.RowId = CompanyFileGrouping.RowId;
            CompanyFileGroupingDAO.CreatedAt = StaticParams.DateTimeNow;
            CompanyFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CompanyFileGrouping.Add(CompanyFileGroupingDAO);
            await DataContext.SaveChangesAsync();
            CompanyFileGrouping.Id = CompanyFileGroupingDAO.Id;
            await SaveReference(CompanyFileGrouping);
            return true;
        }

        public async Task<bool> Update(CompanyFileGrouping CompanyFileGrouping)
        {
            CompanyFileGroupingDAO CompanyFileGroupingDAO = DataContext.CompanyFileGrouping.Where(x => x.Id == CompanyFileGrouping.Id).FirstOrDefault();
            if (CompanyFileGroupingDAO == null)
                return false;
            CompanyFileGroupingDAO.Id = CompanyFileGrouping.Id;
            CompanyFileGroupingDAO.Title = CompanyFileGrouping.Title;
            CompanyFileGroupingDAO.Description = CompanyFileGrouping.Description;
            CompanyFileGroupingDAO.CompanyId = CompanyFileGrouping.CompanyId;
            CompanyFileGroupingDAO.CreatorId = CompanyFileGrouping.CreatorId;
            CompanyFileGroupingDAO.FileTypeId = CompanyFileGrouping.FileTypeId;
            CompanyFileGroupingDAO.RowId = CompanyFileGrouping.RowId;
            CompanyFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CompanyFileGrouping);
            return true;
        }

        public async Task<bool> Delete(CompanyFileGrouping CompanyFileGrouping)
        {
            await DataContext.CompanyFileGrouping.Where(x => x.Id == CompanyFileGrouping.Id).UpdateFromQueryAsync(x => new CompanyFileGroupingDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CompanyFileGrouping> CompanyFileGroupings)
        {
            List<CompanyFileGroupingDAO> CompanyFileGroupingDAOs = new List<CompanyFileGroupingDAO>();
            foreach (CompanyFileGrouping CompanyFileGrouping in CompanyFileGroupings)
            {
                CompanyFileGroupingDAO CompanyFileGroupingDAO = new CompanyFileGroupingDAO();
                CompanyFileGroupingDAO.Id = CompanyFileGrouping.Id;
                CompanyFileGroupingDAO.Title = CompanyFileGrouping.Title;
                CompanyFileGroupingDAO.Description = CompanyFileGrouping.Description;
                CompanyFileGroupingDAO.CompanyId = CompanyFileGrouping.CompanyId;
                CompanyFileGroupingDAO.CreatorId = CompanyFileGrouping.CreatorId;
                CompanyFileGroupingDAO.FileTypeId = CompanyFileGrouping.FileTypeId;
                CompanyFileGroupingDAO.RowId = CompanyFileGrouping.RowId;
                CompanyFileGroupingDAO.CreatedAt = StaticParams.DateTimeNow;
                CompanyFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
                CompanyFileGroupingDAOs.Add(CompanyFileGroupingDAO);
            }
            await DataContext.BulkMergeAsync(CompanyFileGroupingDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CompanyFileGrouping> CompanyFileGroupings)
        {
            List<long> Ids = CompanyFileGroupings.Select(x => x.Id).ToList();
            await DataContext.CompanyFileGrouping
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CompanyFileGroupingDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CompanyFileGrouping CompanyFileGrouping)
        {
        }
        
    }
}
