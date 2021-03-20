using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;

namespace CRM.Repositories
{
    public interface IStoreGroupingRepository
    {
        Task<int> Count(StoreGroupingFilter StoreGroupingFilter);
        Task<List<StoreGrouping>> List(StoreGroupingFilter StoreGroupingFilter);
        Task<StoreGrouping> Get(long Id);
    }
    public class StoreGroupingRepository : IStoreGroupingRepository
    {
        private DataContext DataContext;
        public StoreGroupingRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<StoreGroupingDAO> DynamicFilter(IQueryable<StoreGroupingDAO> query, StoreGroupingFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.ParentId != null)
                query = query.Where(q => q.ParentId, filter.ParentId);
            if (filter.Path != null)
                query = query.Where(q => q.Path, filter.Path);
            if (filter.Level != null)
                query = query.Where(q => q.Level, filter.Level);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<StoreGroupingDAO> OrFilter(IQueryable<StoreGroupingDAO> query, StoreGroupingFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<StoreGroupingDAO> initQuery = query.Where(q => false);
            foreach (StoreGroupingFilter StoreGroupingFilter in filter.OrFilter)
            {
                IQueryable<StoreGroupingDAO> queryable = query;
                if (StoreGroupingFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, StoreGroupingFilter.Id);
                if (StoreGroupingFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, StoreGroupingFilter.Code);
                if (StoreGroupingFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, StoreGroupingFilter.Name);
                if (StoreGroupingFilter.ParentId != null)
                    queryable = queryable.Where(q => q.ParentId, StoreGroupingFilter.ParentId);
                if (StoreGroupingFilter.Path != null)
                    queryable = queryable.Where(q => q.Path, StoreGroupingFilter.Path);
                if (StoreGroupingFilter.Level != null)
                    queryable = queryable.Where(q => q.Level, StoreGroupingFilter.Level);
                if (StoreGroupingFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, StoreGroupingFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<StoreGroupingDAO> DynamicOrder(IQueryable<StoreGroupingDAO> query, StoreGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case StoreGroupingOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case StoreGroupingOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case StoreGroupingOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case StoreGroupingOrder.Parent:
                            query = query.OrderBy(q => q.ParentId);
                            break;
                        case StoreGroupingOrder.Path:
                            query = query.OrderBy(q => q.Path);
                            break;
                        case StoreGroupingOrder.Level:
                            query = query.OrderBy(q => q.Level);
                            break;
                        case StoreGroupingOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case StoreGroupingOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case StoreGroupingOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case StoreGroupingOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case StoreGroupingOrder.Parent:
                            query = query.OrderByDescending(q => q.ParentId);
                            break;
                        case StoreGroupingOrder.Path:
                            query = query.OrderByDescending(q => q.Path);
                            break;
                        case StoreGroupingOrder.Level:
                            query = query.OrderByDescending(q => q.Level);
                            break;
                        case StoreGroupingOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<StoreGrouping>> DynamicSelect(IQueryable<StoreGroupingDAO> query, StoreGroupingFilter filter)
        {
            List<StoreGrouping> StoreGroupings = await query.Select(q => new StoreGrouping()
            {
                Id = filter.Selects.Contains(StoreGroupingSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(StoreGroupingSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(StoreGroupingSelect.Name) ? q.Name : default(string),
                ParentId = filter.Selects.Contains(StoreGroupingSelect.Parent) ? q.ParentId : default(long?),
                Path = filter.Selects.Contains(StoreGroupingSelect.Path) ? q.Path : default(string),
                Level = filter.Selects.Contains(StoreGroupingSelect.Level) ? q.Level : default(long),
                StatusId = filter.Selects.Contains(StoreGroupingSelect.Status) ? q.StatusId : default(long),
                Parent = filter.Selects.Contains(StoreGroupingSelect.Parent) && q.Parent != null ? new StoreGrouping
                {
                    Id = q.Parent.Id,
                    Code = q.Parent.Code,
                    Name = q.Parent.Name,
                    ParentId = q.Parent.ParentId,
                    Path = q.Parent.Path,
                    Level = q.Parent.Level,
                    StatusId = q.Parent.StatusId,
                } : null,
                Status = filter.Selects.Contains(StoreGroupingSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return StoreGroupings;
        }

        public async Task<int> Count(StoreGroupingFilter filter)
        {
            IQueryable<StoreGroupingDAO> StoreGroupings = DataContext.StoreGrouping.AsNoTracking();
            StoreGroupings = DynamicFilter(StoreGroupings, filter);
            return await StoreGroupings.CountAsync();
        }

        public async Task<List<StoreGrouping>> List(StoreGroupingFilter filter)
        {
            if (filter == null) return new List<StoreGrouping>();
            IQueryable<StoreGroupingDAO> StoreGroupingDAOs = DataContext.StoreGrouping.AsNoTracking();
            StoreGroupingDAOs = DynamicFilter(StoreGroupingDAOs, filter);
            StoreGroupingDAOs = DynamicOrder(StoreGroupingDAOs, filter);
            List<StoreGrouping> StoreGroupings = await DynamicSelect(StoreGroupingDAOs, filter);
            return StoreGroupings;
        }

        public async Task<StoreGrouping> Get(long Id)
        {
            StoreGrouping StoreGrouping = await DataContext.StoreGrouping.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new StoreGrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                ParentId = x.ParentId,
                Path = x.Path,
                Level = x.Level,
                StatusId = x.StatusId,
                Parent = x.Parent == null ? null : new StoreGrouping
                {
                    Id = x.Parent.Id,
                    Code = x.Parent.Code,
                    Name = x.Parent.Name,
                    ParentId = x.Parent.ParentId,
                    Path = x.Parent.Path,
                    Level = x.Parent.Level,
                    StatusId = x.Parent.StatusId,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (StoreGrouping == null)
                return null;
            StoreGrouping.Stores = await DataContext.Store.AsNoTracking()
                .Where(x => x.StoreGroupingId == StoreGrouping.Id)
                .Where(x => x.DeletedAt == null)
                .Select(x => new Store
                {
                    Id = x.Id,
                    Code = x.Code,
                    CodeDraft = x.CodeDraft,
                    Name = x.Name,
                    UnsignName = x.UnsignName,
                    ParentStoreId = x.ParentStoreId,
                    OrganizationId = x.OrganizationId,
                    StoreTypeId = x.StoreTypeId,
                    StoreGroupingId = x.StoreGroupingId,
                    Telephone = x.Telephone,
                    ProvinceId = x.ProvinceId,
                    DistrictId = x.DistrictId,
                    WardId = x.WardId,
                    Address = x.Address,
                    UnsignAddress = x.UnsignAddress,
                    DeliveryAddress = x.DeliveryAddress,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    DeliveryLatitude = x.DeliveryLatitude,
                    DeliveryLongitude = x.DeliveryLongitude,
                    OwnerName = x.OwnerName,
                    OwnerPhone = x.OwnerPhone,
                    OwnerEmail = x.OwnerEmail,
                    TaxCode = x.TaxCode,
                    LegalEntity = x.LegalEntity,
                    AppUserId = x.AppUserId,
                    StatusId = x.StatusId,
                    Used = x.Used,
                    StoreStatusId = x.StoreStatusId,
                    AppUser = new AppUser
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
                    },
                    District = new District
                    {
                        Id = x.District.Id,
                        Code = x.District.Code,
                        Name = x.District.Name,
                        Priority = x.District.Priority,
                        ProvinceId = x.District.ProvinceId,
                        StatusId = x.District.StatusId,
                    },
                    Organization = new Organization
                    {
                        Id = x.Organization.Id,
                        Code = x.Organization.Code,
                        Name = x.Organization.Name,
                        ParentId = x.Organization.ParentId,
                        Path = x.Organization.Path,
                        Level = x.Organization.Level,
                        StatusId = x.Organization.StatusId,
                        Phone = x.Organization.Phone,
                        Email = x.Organization.Email,
                        Address = x.Organization.Address,
                    },
                    ParentStore = new Store
                    {
                        Id = x.ParentStore.Id,
                        Code = x.ParentStore.Code,
                        CodeDraft = x.ParentStore.CodeDraft,
                        Name = x.ParentStore.Name,
                        UnsignName = x.ParentStore.UnsignName,
                        ParentStoreId = x.ParentStore.ParentStoreId,
                        OrganizationId = x.ParentStore.OrganizationId,
                        StoreTypeId = x.ParentStore.StoreTypeId,
                        StoreGroupingId = x.ParentStore.StoreGroupingId,
                        Telephone = x.ParentStore.Telephone,
                        ProvinceId = x.ParentStore.ProvinceId,
                        DistrictId = x.ParentStore.DistrictId,
                        WardId = x.ParentStore.WardId,
                        Address = x.ParentStore.Address,
                        UnsignAddress = x.ParentStore.UnsignAddress,
                        DeliveryAddress = x.ParentStore.DeliveryAddress,
                        Latitude = x.ParentStore.Latitude,
                        Longitude = x.ParentStore.Longitude,
                        DeliveryLatitude = x.ParentStore.DeliveryLatitude,
                        DeliveryLongitude = x.ParentStore.DeliveryLongitude,
                        OwnerName = x.ParentStore.OwnerName,
                        OwnerPhone = x.ParentStore.OwnerPhone,
                        OwnerEmail = x.ParentStore.OwnerEmail,
                        TaxCode = x.ParentStore.TaxCode,
                        LegalEntity = x.ParentStore.LegalEntity,
                        AppUserId = x.ParentStore.AppUserId,
                        StatusId = x.ParentStore.StatusId,
                        Used = x.ParentStore.Used,
                        StoreStatusId = x.ParentStore.StoreStatusId,
                    },
                    Province = new Province
                    {
                        Id = x.Province.Id,
                        Code = x.Province.Code,
                        Name = x.Province.Name,
                        Priority = x.Province.Priority,
                        StatusId = x.Province.StatusId,
                    },
                    Status = new Status
                    {
                        Id = x.Status.Id,
                        Code = x.Status.Code,
                        Name = x.Status.Name,
                    },
                    StoreStatus = new StoreStatus
                    {
                        Id = x.StoreStatus.Id,
                        Code = x.StoreStatus.Code,
                        Name = x.StoreStatus.Name,
                    },
                    StoreType = new StoreType
                    {
                        Id = x.StoreType.Id,
                        Code = x.StoreType.Code,
                        Name = x.StoreType.Name,
                        ColorId = x.StoreType.ColorId,
                        StatusId = x.StoreType.StatusId,
                        Used = x.StoreType.Used,
                    },
                    Ward = new Ward
                    {
                        Id = x.Ward.Id,
                        Code = x.Ward.Code,
                        Name = x.Ward.Name,
                        Priority = x.Ward.Priority,
                        DistrictId = x.Ward.DistrictId,
                        StatusId = x.Ward.StatusId,
                    },
                }).ToListAsync();

            return StoreGrouping;
        }
    }
}
