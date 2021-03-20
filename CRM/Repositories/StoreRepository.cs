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
    public interface IStoreRepository
    {
        Task<int> Count(StoreFilter StoreFilter);
        Task<List<Store>> List(StoreFilter StoreFilter);
        Task<Store> Get(long Id);
        Task<bool> BulkMerge(List<Store> Stores);
    }
    public class StoreRepository : IStoreRepository
    {
        private DataContext DataContext;
        public StoreRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<StoreDAO> DynamicFilter(IQueryable<StoreDAO> query, StoreFilter filter)
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
            if (filter.CodeDraft != null)
                query = query.Where(q => q.CodeDraft, filter.CodeDraft);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.UnsignName != null)
                query = query.Where(q => q.UnsignName, filter.UnsignName);
            if (filter.ParentStoreId != null)
                query = query.Where(q => q.ParentStoreId, filter.ParentStoreId);
            if (filter.OrganizationId != null)
                query = query.Where(q => q.OrganizationId, filter.OrganizationId);
            if (filter.StoreTypeId != null)
                query = query.Where(q => q.StoreTypeId, filter.StoreTypeId);
            if (filter.StoreGroupingId != null)
                query = query.Where(q => q.StoreGroupingId, filter.StoreGroupingId);
            if (filter.Telephone != null)
                query = query.Where(q => q.Telephone, filter.Telephone);
            if (filter.ProvinceId != null)
                query = query.Where(q => q.ProvinceId, filter.ProvinceId);
            if (filter.DistrictId != null)
                query = query.Where(q => q.DistrictId, filter.DistrictId);
            if (filter.WardId != null)
                query = query.Where(q => q.WardId, filter.WardId);
            if (filter.Address != null)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.UnsignAddress != null)
                query = query.Where(q => q.UnsignAddress, filter.UnsignAddress);
            if (filter.DeliveryAddress != null)
                query = query.Where(q => q.DeliveryAddress, filter.DeliveryAddress);
            if (filter.Latitude != null)
                query = query.Where(q => q.Latitude, filter.Latitude);
            if (filter.Longitude != null)
                query = query.Where(q => q.Longitude, filter.Longitude);
            if (filter.DeliveryLatitude != null)
                query = query.Where(q => q.DeliveryLatitude, filter.DeliveryLatitude);
            if (filter.DeliveryLongitude != null)
                query = query.Where(q => q.DeliveryLongitude, filter.DeliveryLongitude);
            if (filter.OwnerName != null)
                query = query.Where(q => q.OwnerName, filter.OwnerName);
            if (filter.OwnerPhone != null)
                query = query.Where(q => q.OwnerPhone, filter.OwnerPhone);
            if (filter.OwnerEmail != null)
                query = query.Where(q => q.OwnerEmail, filter.OwnerEmail);
            if (filter.TaxCode != null)
                query = query.Where(q => q.TaxCode, filter.TaxCode);
            if (filter.LegalEntity != null)
                query = query.Where(q => q.LegalEntity, filter.LegalEntity);
            if (filter.AppUserId != null)
                query = query.Where(q => q.AppUserId, filter.AppUserId);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.StoreStatusId != null)
                query = query.Where(q => q.StoreStatusId, filter.StoreStatusId);
            if (filter.CustomerId != null && filter.CustomerId.HasValue)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if(filter.isSelected != null && filter.isSelected.Value == false)
                query = query.Where(q => q.CustomerId.HasValue == false);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<StoreDAO> OrFilter(IQueryable<StoreDAO> query, StoreFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<StoreDAO> initQuery = query.Where(q => false);
            foreach (StoreFilter StoreFilter in filter.OrFilter)
            {
                IQueryable<StoreDAO> queryable = query;
                if (StoreFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, StoreFilter.Id);
                if (StoreFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, StoreFilter.Code);
                if (StoreFilter.CodeDraft != null)
                    queryable = queryable.Where(q => q.CodeDraft, StoreFilter.CodeDraft);
                if (StoreFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, StoreFilter.Name);
                if (StoreFilter.UnsignName != null)
                    queryable = queryable.Where(q => q.UnsignName, StoreFilter.UnsignName);
                if (StoreFilter.ParentStoreId != null)
                    queryable = queryable.Where(q => q.ParentStoreId, StoreFilter.ParentStoreId);
                if (StoreFilter.OrganizationId != null)
                    queryable = queryable.Where(q => q.OrganizationId, StoreFilter.OrganizationId);
                if (StoreFilter.StoreTypeId != null)
                    queryable = queryable.Where(q => q.StoreTypeId, StoreFilter.StoreTypeId);
                if (StoreFilter.StoreGroupingId != null)
                    queryable = queryable.Where(q => q.StoreGroupingId, StoreFilter.StoreGroupingId);
                if (StoreFilter.Telephone != null)
                    queryable = queryable.Where(q => q.Telephone, StoreFilter.Telephone);
                if (StoreFilter.ProvinceId != null)
                    queryable = queryable.Where(q => q.ProvinceId, StoreFilter.ProvinceId);
                if (StoreFilter.DistrictId != null)
                    queryable = queryable.Where(q => q.DistrictId, StoreFilter.DistrictId);
                if (StoreFilter.WardId != null)
                    queryable = queryable.Where(q => q.WardId, StoreFilter.WardId);
                if (StoreFilter.Address != null)
                    queryable = queryable.Where(q => q.Address, StoreFilter.Address);
                if (StoreFilter.UnsignAddress != null)
                    queryable = queryable.Where(q => q.UnsignAddress, StoreFilter.UnsignAddress);
                if (StoreFilter.DeliveryAddress != null)
                    queryable = queryable.Where(q => q.DeliveryAddress, StoreFilter.DeliveryAddress);
                if (StoreFilter.Latitude != null)
                    queryable = queryable.Where(q => q.Latitude, StoreFilter.Latitude);
                if (StoreFilter.Longitude != null)
                    queryable = queryable.Where(q => q.Longitude, StoreFilter.Longitude);
                if (StoreFilter.DeliveryLatitude != null)
                    queryable = queryable.Where(q => q.DeliveryLatitude, StoreFilter.DeliveryLatitude);
                if (StoreFilter.DeliveryLongitude != null)
                    queryable = queryable.Where(q => q.DeliveryLongitude, StoreFilter.DeliveryLongitude);
                if (StoreFilter.OwnerName != null)
                    queryable = queryable.Where(q => q.OwnerName, StoreFilter.OwnerName);
                if (StoreFilter.OwnerPhone != null)
                    queryable = queryable.Where(q => q.OwnerPhone, StoreFilter.OwnerPhone);
                if (StoreFilter.OwnerEmail != null)
                    queryable = queryable.Where(q => q.OwnerEmail, StoreFilter.OwnerEmail);
                if (StoreFilter.TaxCode != null)
                    queryable = queryable.Where(q => q.TaxCode, StoreFilter.TaxCode);
                if (StoreFilter.LegalEntity != null)
                    queryable = queryable.Where(q => q.LegalEntity, StoreFilter.LegalEntity);
                if (StoreFilter.AppUserId != null)
                    queryable = queryable.Where(q => q.AppUserId, StoreFilter.AppUserId);
                if (StoreFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, StoreFilter.StatusId);
                if (StoreFilter.StoreStatusId != null)
                    queryable = queryable.Where(q => q.StoreStatusId, StoreFilter.StoreStatusId);
                if (StoreFilter.CustomerId != null)
                    queryable = queryable.Where(q => q.CustomerId, StoreFilter.CustomerId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<StoreDAO> DynamicOrder(IQueryable<StoreDAO> query, StoreFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case StoreOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case StoreOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case StoreOrder.CodeDraft:
                            query = query.OrderBy(q => q.CodeDraft);
                            break;
                        case StoreOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case StoreOrder.UnsignName:
                            query = query.OrderBy(q => q.UnsignName);
                            break;
                        case StoreOrder.ParentStore:
                            query = query.OrderBy(q => q.ParentStoreId);
                            break;
                        case StoreOrder.Organization:
                            query = query.OrderBy(q => q.OrganizationId);
                            break;
                        case StoreOrder.StoreType:
                            query = query.OrderBy(q => q.StoreTypeId);
                            break;
                        case StoreOrder.StoreGrouping:
                            query = query.OrderBy(q => q.StoreGroupingId);
                            break;
                        case StoreOrder.Telephone:
                            query = query.OrderBy(q => q.Telephone);
                            break;
                        case StoreOrder.Province:
                            query = query.OrderBy(q => q.ProvinceId);
                            break;
                        case StoreOrder.District:
                            query = query.OrderBy(q => q.DistrictId);
                            break;
                        case StoreOrder.Ward:
                            query = query.OrderBy(q => q.WardId);
                            break;
                        case StoreOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case StoreOrder.UnsignAddress:
                            query = query.OrderBy(q => q.UnsignAddress);
                            break;
                        case StoreOrder.DeliveryAddress:
                            query = query.OrderBy(q => q.DeliveryAddress);
                            break;
                        case StoreOrder.Latitude:
                            query = query.OrderBy(q => q.Latitude);
                            break;
                        case StoreOrder.Longitude:
                            query = query.OrderBy(q => q.Longitude);
                            break;
                        case StoreOrder.DeliveryLatitude:
                            query = query.OrderBy(q => q.DeliveryLatitude);
                            break;
                        case StoreOrder.DeliveryLongitude:
                            query = query.OrderBy(q => q.DeliveryLongitude);
                            break;
                        case StoreOrder.OwnerName:
                            query = query.OrderBy(q => q.OwnerName);
                            break;
                        case StoreOrder.OwnerPhone:
                            query = query.OrderBy(q => q.OwnerPhone);
                            break;
                        case StoreOrder.OwnerEmail:
                            query = query.OrderBy(q => q.OwnerEmail);
                            break;
                        case StoreOrder.TaxCode:
                            query = query.OrderBy(q => q.TaxCode);
                            break;
                        case StoreOrder.LegalEntity:
                            query = query.OrderBy(q => q.LegalEntity);
                            break;
                        case StoreOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case StoreOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case StoreOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                        case StoreOrder.StoreStatus:
                            query = query.OrderBy(q => q.StoreStatusId);
                            break;
                        case StoreOrder.Customer:
                            query = query.OrderBy(q => q.CustomerId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case StoreOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case StoreOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case StoreOrder.CodeDraft:
                            query = query.OrderByDescending(q => q.CodeDraft);
                            break;
                        case StoreOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case StoreOrder.UnsignName:
                            query = query.OrderByDescending(q => q.UnsignName);
                            break;
                        case StoreOrder.ParentStore:
                            query = query.OrderByDescending(q => q.ParentStoreId);
                            break;
                        case StoreOrder.Organization:
                            query = query.OrderByDescending(q => q.OrganizationId);
                            break;
                        case StoreOrder.StoreType:
                            query = query.OrderByDescending(q => q.StoreTypeId);
                            break;
                        case StoreOrder.StoreGrouping:
                            query = query.OrderByDescending(q => q.StoreGroupingId);
                            break;
                        case StoreOrder.Telephone:
                            query = query.OrderByDescending(q => q.Telephone);
                            break;
                        case StoreOrder.Province:
                            query = query.OrderByDescending(q => q.ProvinceId);
                            break;
                        case StoreOrder.District:
                            query = query.OrderByDescending(q => q.DistrictId);
                            break;
                        case StoreOrder.Ward:
                            query = query.OrderByDescending(q => q.WardId);
                            break;
                        case StoreOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case StoreOrder.UnsignAddress:
                            query = query.OrderByDescending(q => q.UnsignAddress);
                            break;
                        case StoreOrder.DeliveryAddress:
                            query = query.OrderByDescending(q => q.DeliveryAddress);
                            break;
                        case StoreOrder.Latitude:
                            query = query.OrderByDescending(q => q.Latitude);
                            break;
                        case StoreOrder.Longitude:
                            query = query.OrderByDescending(q => q.Longitude);
                            break;
                        case StoreOrder.DeliveryLatitude:
                            query = query.OrderByDescending(q => q.DeliveryLatitude);
                            break;
                        case StoreOrder.DeliveryLongitude:
                            query = query.OrderByDescending(q => q.DeliveryLongitude);
                            break;
                        case StoreOrder.OwnerName:
                            query = query.OrderByDescending(q => q.OwnerName);
                            break;
                        case StoreOrder.OwnerPhone:
                            query = query.OrderByDescending(q => q.OwnerPhone);
                            break;
                        case StoreOrder.OwnerEmail:
                            query = query.OrderByDescending(q => q.OwnerEmail);
                            break;
                        case StoreOrder.TaxCode:
                            query = query.OrderByDescending(q => q.TaxCode);
                            break;
                        case StoreOrder.LegalEntity:
                            query = query.OrderByDescending(q => q.LegalEntity);
                            break;
                        case StoreOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case StoreOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case StoreOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                        case StoreOrder.StoreStatus:
                            query = query.OrderByDescending(q => q.StoreStatusId);
                            break;
                        case StoreOrder.Customer:
                            query = query.OrderByDescending(q => q.CustomerId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Store>> DynamicSelect(IQueryable<StoreDAO> query, StoreFilter filter)
        {
            List<Store> Stores = await query.Select(q => new Store()
            {
                Id = filter.Selects.Contains(StoreSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(StoreSelect.Code) ? q.Code : default(string),
                CodeDraft = filter.Selects.Contains(StoreSelect.CodeDraft) ? q.CodeDraft : default(string),
                Name = filter.Selects.Contains(StoreSelect.Name) ? q.Name : default(string),
                UnsignName = filter.Selects.Contains(StoreSelect.UnsignName) ? q.UnsignName : default(string),
                ParentStoreId = filter.Selects.Contains(StoreSelect.ParentStore) ? q.ParentStoreId : default(long?),
                OrganizationId = filter.Selects.Contains(StoreSelect.Organization) ? q.OrganizationId : default(long),
                StoreTypeId = filter.Selects.Contains(StoreSelect.StoreType) ? q.StoreTypeId : default(long),
                StoreGroupingId = filter.Selects.Contains(StoreSelect.StoreGrouping) ? q.StoreGroupingId : default(long?),
                Telephone = filter.Selects.Contains(StoreSelect.Telephone) ? q.Telephone : default(string),
                ProvinceId = filter.Selects.Contains(StoreSelect.Province) ? q.ProvinceId : default(long?),
                DistrictId = filter.Selects.Contains(StoreSelect.District) ? q.DistrictId : default(long?),
                WardId = filter.Selects.Contains(StoreSelect.Ward) ? q.WardId : default(long?),
                Address = filter.Selects.Contains(StoreSelect.Address) ? q.Address : default(string),
                UnsignAddress = filter.Selects.Contains(StoreSelect.UnsignAddress) ? q.UnsignAddress : default(string),
                DeliveryAddress = filter.Selects.Contains(StoreSelect.DeliveryAddress) ? q.DeliveryAddress : default(string),
                Latitude = filter.Selects.Contains(StoreSelect.Latitude) ? q.Latitude : default(decimal),
                Longitude = filter.Selects.Contains(StoreSelect.Longitude) ? q.Longitude : default(decimal),
                DeliveryLatitude = filter.Selects.Contains(StoreSelect.DeliveryLatitude) ? q.DeliveryLatitude : default(decimal?),
                DeliveryLongitude = filter.Selects.Contains(StoreSelect.DeliveryLongitude) ? q.DeliveryLongitude : default(decimal?),
                OwnerName = filter.Selects.Contains(StoreSelect.OwnerName) ? q.OwnerName : default(string),
                OwnerPhone = filter.Selects.Contains(StoreSelect.OwnerPhone) ? q.OwnerPhone : default(string),
                OwnerEmail = filter.Selects.Contains(StoreSelect.OwnerEmail) ? q.OwnerEmail : default(string),
                TaxCode = filter.Selects.Contains(StoreSelect.TaxCode) ? q.TaxCode : default(string),
                LegalEntity = filter.Selects.Contains(StoreSelect.LegalEntity) ? q.LegalEntity : default(string),
                AppUserId = filter.Selects.Contains(StoreSelect.AppUser) ? q.AppUserId : default(long?),
                StatusId = filter.Selects.Contains(StoreSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(StoreSelect.Used) ? q.Used : default(bool),
                StoreStatusId = filter.Selects.Contains(StoreSelect.StoreStatus) ? q.StoreStatusId : default(long),
                CustomerId = filter.Selects.Contains(StoreSelect.Customer) ? q.CustomerId : default(long),
                AppUser = filter.Selects.Contains(StoreSelect.AppUser) && q.AppUser != null ? new AppUser
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
                } : null,
                Customer = filter.Selects.Contains(StoreSelect.Customer) && q.Customer != null ? new Customer
                {
                    Id = q.Customer.Id,
                    Code = q.Customer.Code,
                    Name = q.Customer.Name,
                    Phone = q.Customer.Phone,
                    Address = q.Customer.Address,
                    NationId = q.Customer.NationId,
                    ProvinceId = q.Customer.ProvinceId,
                    DistrictId = q.Customer.DistrictId,
                    WardId = q.Customer.WardId,
                    CustomerTypeId = q.Customer.CustomerTypeId,
                    Birthday = q.Customer.Birthday,
                    Email = q.Customer.Email,
                    ProfessionId = q.Customer.ProfessionId,
                    CustomerResourceId = q.Customer.CustomerResourceId,
                    SexId = q.Customer.SexId,
                    StatusId = q.Customer.StatusId,
                    CompanyId = q.Customer.CompanyId,
                    ParentCompanyId = q.Customer.ParentCompanyId,
                    TaxCode = q.Customer.TaxCode,
                    Fax = q.Customer.Fax,
                    Website = q.Customer.Website,
                    NumberOfEmployee = q.Customer.NumberOfEmployee,
                    BusinessTypeId = q.Customer.BusinessTypeId,
                    Investment = q.Customer.Investment,
                    RevenueAnnual = q.Customer.RevenueAnnual,
                    IsSupplier = q.Customer.IsSupplier,
                    Descreption = q.Customer.Descreption,
                    Used = q.Customer.Used,
                    RowId = q.Customer.RowId,
                    Company = q.Customer.Company == null ? null : new Company
                    {
                        Id = q.Customer.Company.Id,
                        Name = q.Customer.Company.Name,
                    },
                } : null,
                District = filter.Selects.Contains(StoreSelect.District) && q.District != null ? new District
                {
                    Id = q.District.Id,
                    Code = q.District.Code,
                    Name = q.District.Name,
                    Priority = q.District.Priority,
                    ProvinceId = q.District.ProvinceId,
                    StatusId = q.District.StatusId,
                } : null,
                Organization = filter.Selects.Contains(StoreSelect.Organization) && q.Organization != null ? new Organization
                {
                    Id = q.Organization.Id,
                    Code = q.Organization.Code,
                    Name = q.Organization.Name,
                    ParentId = q.Organization.ParentId,
                    Path = q.Organization.Path,
                    Level = q.Organization.Level,
                    StatusId = q.Organization.StatusId,
                    Phone = q.Organization.Phone,
                    Email = q.Organization.Email,
                    Address = q.Organization.Address,
                } : null,
                ParentStore = filter.Selects.Contains(StoreSelect.ParentStore) && q.ParentStore != null ? new Store
                {
                    Id = q.ParentStore.Id,
                    Code = q.ParentStore.Code,
                    CodeDraft = q.ParentStore.CodeDraft,
                    Name = q.ParentStore.Name,
                    UnsignName = q.ParentStore.UnsignName,
                    ParentStoreId = q.ParentStore.ParentStoreId,
                    OrganizationId = q.ParentStore.OrganizationId,
                    StoreTypeId = q.ParentStore.StoreTypeId,
                    StoreGroupingId = q.ParentStore.StoreGroupingId,
                    Telephone = q.ParentStore.Telephone,
                    ProvinceId = q.ParentStore.ProvinceId,
                    DistrictId = q.ParentStore.DistrictId,
                    WardId = q.ParentStore.WardId,
                    Address = q.ParentStore.Address,
                    UnsignAddress = q.ParentStore.UnsignAddress,
                    DeliveryAddress = q.ParentStore.DeliveryAddress,
                    Latitude = q.ParentStore.Latitude,
                    Longitude = q.ParentStore.Longitude,
                    DeliveryLatitude = q.ParentStore.DeliveryLatitude,
                    DeliveryLongitude = q.ParentStore.DeliveryLongitude,
                    OwnerName = q.ParentStore.OwnerName,
                    OwnerPhone = q.ParentStore.OwnerPhone,
                    OwnerEmail = q.ParentStore.OwnerEmail,
                    TaxCode = q.ParentStore.TaxCode,
                    LegalEntity = q.ParentStore.LegalEntity,
                    AppUserId = q.ParentStore.AppUserId,
                    StatusId = q.ParentStore.StatusId,
                    Used = q.ParentStore.Used,
                    StoreStatusId = q.ParentStore.StoreStatusId,
                } : null,
                Province = filter.Selects.Contains(StoreSelect.Province) && q.Province != null ? new Province
                {
                    Id = q.Province.Id,
                    Code = q.Province.Code,
                    Name = q.Province.Name,
                    Priority = q.Province.Priority,
                    StatusId = q.Province.StatusId,
                } : null,
                Status = filter.Selects.Contains(StoreSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                StoreGrouping = filter.Selects.Contains(StoreSelect.StoreGrouping) && q.StoreGrouping != null ? new StoreGrouping
                {
                    Id = q.StoreGrouping.Id,
                    Code = q.StoreGrouping.Code,
                    Name = q.StoreGrouping.Name,
                    ParentId = q.StoreGrouping.ParentId,
                    Path = q.StoreGrouping.Path,
                    Level = q.StoreGrouping.Level,
                    StatusId = q.StoreGrouping.StatusId,
                } : null,
                StoreStatus = filter.Selects.Contains(StoreSelect.StoreStatus) && q.StoreStatus != null ? new StoreStatus
                {
                    Id = q.StoreStatus.Id,
                    Code = q.StoreStatus.Code,
                    Name = q.StoreStatus.Name,
                } : null,
                StoreType = filter.Selects.Contains(StoreSelect.StoreType) && q.StoreType != null ? new StoreType
                {
                    Id = q.StoreType.Id,
                    Code = q.StoreType.Code,
                    Name = q.StoreType.Name,
                    ColorId = q.StoreType.ColorId,
                    StatusId = q.StoreType.StatusId,
                    Used = q.StoreType.Used,
                } : null,
                Ward = filter.Selects.Contains(StoreSelect.Ward) && q.Ward != null ? new Ward
                {
                    Id = q.Ward.Id,
                    Code = q.Ward.Code,
                    Name = q.Ward.Name,
                    Priority = q.Ward.Priority,
                    DistrictId = q.Ward.DistrictId,
                    StatusId = q.Ward.StatusId,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                RowId = q.RowId,
            }).ToListAsync();
            return Stores;
        }

        public async Task<int> Count(StoreFilter filter)
        {
            IQueryable<StoreDAO> Stores = DataContext.Store.AsNoTracking();
            Stores = DynamicFilter(Stores, filter);
            return await Stores.CountAsync();
        }

        public async Task<List<Store>> List(StoreFilter filter)
        {
            if (filter == null) return new List<Store>();
            IQueryable<StoreDAO> StoreDAOs = DataContext.Store.AsNoTracking();
            StoreDAOs = DynamicFilter(StoreDAOs, filter);
            StoreDAOs = DynamicOrder(StoreDAOs, filter);
            List<Store> Stores = await DynamicSelect(StoreDAOs, filter);
            return Stores;
        }

        public async Task<Store> Get(long Id)
        {
            Store Store = await DataContext.Store.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new Store()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
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
                CustomerId = x.CustomerId,
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
                },
                Customer = x.Customer == null ? null : new Customer
                {
                    Id = x.Customer.Id,
                    Code = x.Customer.Code,
                    Name = x.Customer.Name,
                    Phone = x.Customer.Phone,
                    Address = x.Customer.Address,
                    NationId = x.Customer.NationId,
                    ProvinceId = x.Customer.ProvinceId,
                    DistrictId = x.Customer.DistrictId,
                    WardId = x.Customer.WardId,
                    CustomerTypeId = x.Customer.CustomerTypeId,
                    Birthday = x.Customer.Birthday,
                    Email = x.Customer.Email,
                    ProfessionId = x.Customer.ProfessionId,
                    CustomerResourceId = x.Customer.CustomerResourceId,
                    SexId = x.Customer.SexId,
                    StatusId = x.Customer.StatusId,
                    CompanyId = x.Customer.CompanyId,
                    ParentCompanyId = x.Customer.ParentCompanyId,
                    TaxCode = x.Customer.TaxCode,
                    Fax = x.Customer.Fax,
                    Website = x.Customer.Website,
                    NumberOfEmployee = x.Customer.NumberOfEmployee,
                    BusinessTypeId = x.Customer.BusinessTypeId,
                    Investment = x.Customer.Investment,
                    RevenueAnnual = x.Customer.RevenueAnnual,
                    IsSupplier = x.Customer.IsSupplier,
                    Descreption = x.Customer.Descreption,
                    Used = x.Customer.Used,
                    RowId = x.Customer.RowId,
                    Company = x.Customer.Company == null ? null : new Company
                    {
                        Id = x.Customer.Company.Id,
                        Name = x.Customer.Company.Name,
                    }
                },
                District = x.District == null ? null : new District
                {
                    Id = x.District.Id,
                    Code = x.District.Code,
                    Name = x.District.Name,
                    Priority = x.District.Priority,
                    ProvinceId = x.District.ProvinceId,
                    StatusId = x.District.StatusId,
                },
                Organization = x.Organization == null ? null : new Organization
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
                ParentStore = x.ParentStore == null ? null : new Store
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
                Province = x.Province == null ? null : new Province
                {
                    Id = x.Province.Id,
                    Code = x.Province.Code,
                    Name = x.Province.Name,
                    Priority = x.Province.Priority,
                    StatusId = x.Province.StatusId,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
                StoreGrouping = x.StoreGrouping == null ? null : new StoreGrouping
                {
                    Id = x.StoreGrouping.Id,
                    Code = x.StoreGrouping.Code,
                    Name = x.StoreGrouping.Name,
                    ParentId = x.StoreGrouping.ParentId,
                    Path = x.StoreGrouping.Path,
                    Level = x.StoreGrouping.Level,
                    StatusId = x.StoreGrouping.StatusId,
                },
                StoreStatus = x.StoreStatus == null ? null : new StoreStatus
                {
                    Id = x.StoreStatus.Id,
                    Code = x.StoreStatus.Code,
                    Name = x.StoreStatus.Name,
                },
                StoreType = x.StoreType == null ? null : new StoreType
                {
                    Id = x.StoreType.Id,
                    Code = x.StoreType.Code,
                    Name = x.StoreType.Name,
                    ColorId = x.StoreType.ColorId,
                    StatusId = x.StoreType.StatusId,
                    Used = x.StoreType.Used,
                },
                Ward = x.Ward == null ? null : new Ward
                {
                    Id = x.Ward.Id,
                    Code = x.Ward.Code,
                    Name = x.Ward.Name,
                    Priority = x.Ward.Priority,
                    DistrictId = x.Ward.DistrictId,
                    StatusId = x.Ward.StatusId,
                },
                //StoreExtend = x.StoreExtend == null ? null : new StoreExtend
                //{
                //    StoreId = x.StoreExtend.StoreId,
                //    PhoneOther = x.StoreExtend.PhoneOther,
                //    Fax = x.StoreExtend.Fax,
                //    BusinessCapital = x.StoreExtend.BusinessCapital,
                //    BusinessTypeId = x.StoreExtend.BusinessTypeId,
                //    BankAccountNumber = x.StoreExtend.BankAccountNumber,
                //    BusinessLicense = x.StoreExtend.BusinessLicense,
                //    BankName = x.StoreExtend.BankName,
                //    DateOfBusinessLicense = x.StoreExtend.DateOfBusinessLicense,
                //    AgentContractNumber = x.StoreExtend.AgentContractNumber,
                //    DateOfAgentContractNumber = x.StoreExtend.DateOfAgentContractNumber,
                //    DistributionArea = x.StoreExtend.DistributionArea,
                //    RegionalPopulation = x.StoreExtend.RegionalPopulation,
                //    DistributionAcreage = x.StoreExtend.DistributionAcreage,
                //    UrbanizationLevel = x.StoreExtend.UrbanizationLevel,
                //    NumberOfPointsOfSale = x.StoreExtend.NumberOfPointsOfSale,
                //    NumberOfKeyCustomer = x.StoreExtend.NumberOfKeyCustomer,
                //    MarketCharacteristics = x.StoreExtend.MarketCharacteristics,
                //    StoreAcreage = x.StoreExtend.StoreAcreage,
                //    AbilityToPay = x.StoreExtend.AbilityToPay,
                //    RewardInYear = x.StoreExtend.RewardInYear,
                //    AbilityRaisingCapital = x.StoreExtend.AbilityRaisingCapital,
                //    AbilityLimitedCapital = x.StoreExtend.AbilityLimitedCapital,
                //    DivideEachPart = x.StoreExtend.DivideEachPart,
                //    DivideHuman = x.StoreExtend.DivideHuman,
                //    AnotherStrongPoint = x.StoreExtend.AnotherStrongPoint,
                //    ReadyCoordinate = x.StoreExtend.ReadyCoordinate,
                //    Invest = x.StoreExtend.Invest,
                //    WareHouseAcreage = x.StoreExtend.WareHouseAcreage,
                //},
                //StoreRepresents = x.StoreRepresents == null ? null : x.StoreRepresents.Select(p => new StoreRepresent
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    DateOfBirth = p.DateOfBirth,
                //    Phone = p.Phone,
                //    Email = p.Email,
                //    PositionId = p.PositionId,
                //    StoreId = p.StoreId,
                //    Position = p.Position == null ? null : new Position
                //    {
                //        Id = p.Position.Id,
                //        Code = p.Position.Code,
                //        Name = p.Position.Name,
                //        StatusId = p.Position.StatusId,
                //    }
                //}).ToList(),
                //StorePersonnels = x.StorePersonnels == null ? null : x.StorePersonnels.Select(p => new StorePersonnel
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    Quantity = p.Quantity,
                //    StoreId = p.StoreId,
                //}).ToList(),
                //StoreMeansOfDeliveries = x.StoreMeansOfDeliveries == null ? null : x.StoreMeansOfDeliveries.Select(p => new StoreMeansOfDelivery
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    Quantity = p.Quantity,
                //    Owned = p.Owned,
                //    Rent = p.Rent,
                //    StoreId = p.StoreId,
                //}).ToList(),
                //StoreAssetses = x.StoreAssets == null ? null : x.StoreAssets.Select(p => new StoreAssets
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    Quantity = p.Quantity,
                //    Owned = p.Owned,
                //    Rent = p.Rent,
                //    StoreId = p.StoreId,
                //}).ToList(),
                //StoreDeliveryTimeMappings = x.StoreDeliveryTimeMappings == null ? null : x.StoreDeliveryTimeMappings.Select(p => new StoreDeliveryTimeMapping
                //{
                //    StoreId = p.StoreId,
                //    StoreDeliveryTimeId = p.StoreDeliveryTimeId,
                //    StoreDeliveryTime = p.StoreDeliveryTime == null ? null : new StoreDeliveryTime
                //    {
                //        Id = p.StoreDeliveryTime.Id,
                //        Code = p.StoreDeliveryTime.Code,
                //        Name = p.StoreDeliveryTime.Name,
                //    },
                //}).ToList(),
                //StoreRelationshipCustomerMappings = x.StoreRelationshipCustomerMappings == null ? null : x.StoreRelationshipCustomerMappings.Select(p => new StoreRelationshipCustomerMapping
                //{
                //    StoreId = p.StoreId,
                //    RelationshipCustomerTypeId = p.RelationshipCustomerTypeId,
                //    RelationshipCustomerType = p.RelationshipCustomerType == null ? null : new RelationshipCustomerType
                //    {
                //        Id = p.RelationshipCustomerType.Id,
                //        Name = p.RelationshipCustomerType.Name,
                //    },
                //}).ToList(),
                //StoreInfulenceLevelMarketMappings = x.StoreInfulenceLevelMarketMappings == null ? null : x.StoreInfulenceLevelMarketMappings.Select(p => new StoreInfulenceLevelMarketMapping
                //{
                //    StoreId = p.StoreId,
                //    InfulenceLevelMarketId = p.InfulenceLevelMarketId,
                //    InfulenceLevelMarket = p.InfulenceLevelMarket == null ? null : new InfulenceLevelMarket
                //    {
                //        Id = p.InfulenceLevelMarket.Id,
                //        Name = p.InfulenceLevelMarket.Name,
                //    },
                //}).ToList(),
                //StoreMarketPriceMappings = x.StoreMarketPriceMappings == null ? null : x.StoreMarketPriceMappings.Select(p => new StoreMarketPriceMapping
                //{
                //    StoreId = p.StoreId,
                //    MarketPriceId = p.MarketPriceId,
                //    MarketPrice = p.MarketPrice == null ? null : new MarketPrice
                //    {
                //        Id = p.MarketPrice.Id,
                //        Name = p.MarketPrice.Name,
                //    },
                //}).ToList(),
                //StoreWarrantyServices = x.StoreWarrantyServices == null ? null : x.StoreWarrantyServices.Select(p => new StoreWarrantyService
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    Detail = p.Detail,
                //    StoreId = p.StoreId,
                //}).ToList(),
                //StoreCoverageCapacities = x.StoreCoverageCapacities == null ? null : x.StoreCoverageCapacities.Select(p => new StoreCoverageCapacity
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    Detail = p.Detail,
                //    StoreId = p.StoreId,
                //}).ToList(),
                //StoreConsultingServiceMappings = x.StoreConsultingServiceMappings == null ? null : x.StoreConsultingServiceMappings.Select(p => new StoreConsultingServiceMapping
                //{
                //    StoreId = p.StoreId,
                //    ConsultingServiceId = p.ConsultingServiceId,
                //    ConsultingService = p.ConsultingService == null ? null : new ConsultingService
                //    {
                //        Id = p.ConsultingService.Id,
                //        Name = p.ConsultingService.Name,
                //    }
                //}).ToList(),
                //StoreCooperativeAttitudeMappings = x.StoreCooperativeAttitudeMappings == null ? null : x.StoreCooperativeAttitudeMappings.Select(p => new StoreCooperativeAttitudeMapping
                //{
                //    StoreId = p.StoreId,
                //    CooperativeAttitudeId = p.CooperativeAttitudeId,
                //    CooperativeAttitude = p.CooperativeAttitude == null ? null : new CooperativeAttitude
                //    {
                //        Id = p.CooperativeAttitude.Id,
                //        Name = p.CooperativeAttitude.Name,
                //    }
                //}).ToList(),
                //BusinessConcentrationLevels = x.BusinessConcentrationLevels == null ? null : x.BusinessConcentrationLevels.Select(p => new BusinessConcentrationLevel
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    Manufacturer = p.Manufacturer,
                //    Branch = p.Branch,
                //    RevenueInYear = p.RevenueInYear,
                //    MarketingStaff = p.MarketingStaff,
                //    StoreId = p.StoreId,
                //}).ToList(),
                //ImproveQualityServings = x.ImproveQualityServings == null ? null : x.ImproveQualityServings.Select(p => new ImproveQualityServing
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    Detail = p.Detail,
                //}).ToList(),
            }).FirstOrDefaultAsync();

            if (Store == null)
                return null;

            return Store;
        }
      
        public async Task<bool> BulkMerge(List<Store> Stores)
        {
            var Ids = Stores.Select(x => x.Id).ToList();
            var oldData = await DataContext.Store.Where(x => Ids.Contains(x.Id)).ToListAsync();
            Dictionary<long, Store> dict = Stores.ToDictionary(x => x.Id, y => y);
            foreach (var StoreDAO in oldData)
            {
                var Store = dict[StoreDAO.Id];
                if(Store != null)
                {
                    StoreDAO.CustomerId = Store.CustomerId;
                    StoreDAO.UpdatedAt = StaticParams.DateTimeNow;
                }
            }
            await DataContext.SaveChangesAsync();
            return true;
        }

        private async Task SaveReference(Store Store)
        {
            //#region StoreExtend
            //if (Store.StoreExtend != null)
            //{
            //    await DataContext.StoreExtend.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    StoreExtendDAO StoreExtendDAO = new StoreExtendDAO();
            //    var StoreExtend = Store.StoreExtend;
            //    StoreExtendDAO.StoreId = Store.Id;
            //    StoreExtendDAO.PhoneOther = StoreExtend.PhoneOther;
            //    StoreExtendDAO.Fax = StoreExtend.Fax;
            //    StoreExtendDAO.BusinessCapital = StoreExtend.BusinessCapital;
            //    StoreExtendDAO.BusinessTypeId = StoreExtend.BusinessTypeId;
            //    StoreExtendDAO.BankAccountNumber = StoreExtend.BankAccountNumber;
            //    StoreExtendDAO.BusinessLicense = StoreExtend.BusinessLicense;
            //    StoreExtendDAO.BankName = StoreExtend.BankName;
            //    StoreExtendDAO.DateOfBusinessLicense = StoreExtend.DateOfBusinessLicense;
            //    StoreExtendDAO.AgentContractNumber = StoreExtend.AgentContractNumber;
            //    StoreExtendDAO.DateOfAgentContractNumber = StoreExtend.DateOfAgentContractNumber;
            //    StoreExtendDAO.DistributionArea = StoreExtend.DistributionArea;
            //    StoreExtendDAO.RegionalPopulation = StoreExtend.RegionalPopulation;
            //    StoreExtendDAO.DistributionAcreage = StoreExtend.DistributionAcreage;
            //    StoreExtendDAO.WareHouseAcreage = StoreExtend.WareHouseAcreage;
            //    StoreExtendDAO.UrbanizationLevel = StoreExtend.UrbanizationLevel;
            //    StoreExtendDAO.NumberOfPointsOfSale = StoreExtend.NumberOfPointsOfSale;
            //    StoreExtendDAO.NumberOfKeyCustomer = StoreExtend.NumberOfKeyCustomer;
            //    StoreExtendDAO.MarketCharacteristics = StoreExtend.MarketCharacteristics;
            //    StoreExtendDAO.StoreAcreage = StoreExtend.StoreAcreage;
            //    StoreExtendDAO.AbilityToPay = StoreExtend.AbilityToPay;
            //    StoreExtendDAO.RewardInYear = StoreExtend.RewardInYear;
            //    StoreExtendDAO.AbilityRaisingCapital = StoreExtend.AbilityRaisingCapital;
            //    StoreExtendDAO.AbilityLimitedCapital = StoreExtend.AbilityLimitedCapital;
            //    StoreExtendDAO.DivideEachPart = StoreExtend.DivideEachPart;
            //    StoreExtendDAO.DivideHuman = StoreExtend.DivideHuman;
            //    StoreExtendDAO.AnotherStrongPoint = StoreExtend.AnotherStrongPoint;
            //    StoreExtendDAO.ReadyCoordinate = StoreExtend.ReadyCoordinate;
            //    StoreExtendDAO.Invest = StoreExtend.Invest;
            //    StoreExtendDAO.CurrencyId = StoreExtend.CurrencyId;
            //    DataContext.StoreExtend.Add(StoreExtendDAO);
            //    await DataContext.SaveChangesAsync();
            //}
            //#endregion

            //#region StoreRepresent - Đại diện doanh nghiệp
            //if (Store.StoreRepresents.Any())
            //{
            //    await DataContext.StoreRepresent.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<StoreRepresentDAO> StoreRepresentDAOs = Store.StoreRepresents.Select(p => new StoreRepresentDAO
            //    {
            //        Name = p.Name,
            //        DateOfBirth = p.DateOfBirth,
            //        Phone = p.Phone,
            //        Email = p.Email,
            //        PositionId = p.PositionId,
            //        StoreId = Store.Id,
            //    }).ToList();
            //    await DataContext.StoreRepresent.BulkMergeAsync(StoreRepresentDAOs);
            //}
            //#endregion

            //#region StorePersonel - Nhân sự
            //if (Store.StorePersonnels.Any())
            //{
            //    await DataContext.StorePersonnel.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<StorePersonnelDAO> StorePersonnelDAOs = Store.StorePersonnels.Select(p => new StorePersonnelDAO
            //    {
            //        Name = p.Name,
            //        Quantity = p.Quantity,
            //        StoreId = Store.Id,
            //    }).ToList();
            //    await DataContext.StorePersonnel.BulkMergeAsync(StorePersonnelDAOs);
            //}

            //#endregion

            //#region StoreMeansOfDelivery - Phương tiện giao hàng
            //if (Store.StoreMeansOfDeliveries.Any())
            //{
            //    await DataContext.StoreMeansOfDelivery.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<StoreMeansOfDeliveryDAO> StoreMeansOfDeliveryDAOs = Store.StoreMeansOfDeliveries.Select(p => new StoreMeansOfDeliveryDAO
            //    {
            //        Name = p.Name,
            //        Quantity = p.Quantity,
            //        Owned = p.Owned,
            //        Rent = p.Rent,
            //        StoreId = Store.Id,
            //    }).ToList();
            //    await DataContext.StoreMeansOfDelivery.BulkMergeAsync(StoreMeansOfDeliveryDAOs);
            //}

            //#endregion

            //#region StoreAssets - Tài sản
            //if (Store.StoreAssetses.Any())
            //{
            //    await DataContext.StoreAssets.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<StoreAssetsDAO> StoreAssetsDAOs = Store.StoreAssetses.Select(p => new StoreAssetsDAO
            //    {
            //        Name = p.Name,
            //        Quantity = p.Quantity,
            //        Owned = p.Owned,
            //        Rent = p.Rent,
            //        StoreId = Store.Id,
            //    }).ToList();
            //    await DataContext.StoreAssets.BulkMergeAsync(StoreAssetsDAOs);
            //}

            //#endregion

            //#region StoreDeliveryTimeMapping - Thời gian giao hàng
            //if (Store.StoreDeliveryTimeMappings.Any())
            //{
            //    await DataContext.StoreDeliveryTimeMapping.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<StoreDeliveryTimeMappingDAO> StoreDeliveryTimeMappingDAOs = Store.StoreDeliveryTimeMappings.Select(p => new StoreDeliveryTimeMappingDAO
            //    {
            //        StoreId = Store.Id,
            //        StoreDeliveryTimeId = p.StoreDeliveryTimeId,
            //    }).ToList();
            //    await DataContext.StoreDeliveryTimeMapping.BulkMergeAsync(StoreDeliveryTimeMappingDAOs);
            //}

            //#endregion

            //#region StoreRelationshipCustomerMapping - Mối quan hệ khách hàng
            //if (Store.StoreRelationshipCustomerMappings.Any())
            //{
            //    await DataContext.StoreRelationshipCustomerMapping.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<StoreRelationshipCustomerMappingDAO> StoreRelationshipCustomerMappingDAOs = Store.StoreRelationshipCustomerMappings.Select(p => new StoreRelationshipCustomerMappingDAO
            //    {
            //        StoreId = Store.Id,
            //        RelationshipCustomerTypeId = p.RelationshipCustomerTypeId,
            //    }).ToList();
            //    await DataContext.StoreRelationshipCustomerMapping.BulkMergeAsync(StoreRelationshipCustomerMappingDAOs);
            //}
            //#endregion

            //#region StoreInfulenceLevelMarketMapping - Mức độ ảnh hưởng trên thị trường
            //if (Store.StoreInfulenceLevelMarketMappings.Any())
            //{
            //    await DataContext.StoreInfulenceLevelMarketMapping.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<StoreInfulenceLevelMarketMappingDAO> StoreInfulenceLevelMarketMappingDAOs = Store.StoreInfulenceLevelMarketMappings.Select(p => new StoreInfulenceLevelMarketMappingDAO
            //    {
            //        StoreId = Store.Id,
            //        InfulenceLevelMarketId = p.InfulenceLevelMarketId,
            //    }).ToList();
            //    await DataContext.StoreInfulenceLevelMarketMapping.BulkMergeAsync(StoreInfulenceLevelMarketMappingDAOs);
            //}
            //#endregion

            //#region StoreMarketPriceMapping - Giá bán thị trường
            //if (Store.StoreMarketPriceMappings.Any())
            //{
            //    await DataContext.StoreMarketPriceMapping.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<StoreMarketPriceMappingDAO> StoreMarketPriceMappingDAOs = Store.StoreMarketPriceMappings.Select(p => new StoreMarketPriceMappingDAO
            //    {
            //        StoreId = Store.Id,
            //        MarketPriceId = p.MarketPriceId,
            //    }).ToList();
            //    await DataContext.StoreMarketPriceMapping.BulkMergeAsync(StoreMarketPriceMappingDAOs);
            //}
            //#endregion

            //#region StoreWarrantyServiceMapping - Dịch vụ bảo hành
            //if (Store.StoreMarketPriceMappings.Any())
            //{
            //    await DataContext.StoreWarrantyService.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<StoreWarrantyServiceDAO> StoreWarrantyServiceDAOs = Store.StoreWarrantyServices.Select(p => new StoreWarrantyServiceDAO
            //    {
            //        StoreId = Store.Id,
            //        Id = p.Id,
            //        Name = p.Name,
            //        Detail = p.Detail,
            //    }).ToList();
            //    await DataContext.StoreWarrantyService.BulkMergeAsync(StoreWarrantyServiceDAOs);
            //}
            //#endregion

            //#region StoreCoverageCapacityMapping - Khả năng bao phủ thị trường
            //if (Store.StoreCoverageCapacities.Any())
            //{
            //    await DataContext.StoreCoverageCapacity.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<StoreCoverageCapacityDAO> StoreCoverageCapacityDAOs = Store.StoreCoverageCapacities.Select(p => new StoreCoverageCapacityDAO
            //    {
            //        StoreId = Store.Id,
            //        Id = p.Id,
            //        Name = p.Name,
            //        Detail = p.Detail,
            //    }).ToList();
            //    await DataContext.StoreCoverageCapacity.BulkMergeAsync(StoreCoverageCapacityDAOs);
            //}
            //#endregion

            //#region StoreConsultingServiceMapping - Khả năng tư vấn dịch vụ
            //if (Store.StoreConsultingServiceMappings.Any())
            //{
            //    await DataContext.StoreConsultingServiceMapping.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<StoreConsultingServiceMappingDAO> StoreConsultingServiceMappingDAOs = Store.StoreConsultingServiceMappings.Select(p => new StoreConsultingServiceMappingDAO
            //    {
            //        StoreId = Store.Id,
            //        ConsultingServiceId = p.ConsultingServiceId,
            //    }).ToList();
            //    await DataContext.StoreConsultingServiceMapping.BulkMergeAsync(StoreConsultingServiceMappingDAOs);
            //}
            //#endregion

            //#region StoreCooperativeAttitudeMapping - Thái độ hợp tác
            //if (Store.StoreCooperativeAttitudeMappings.Any())
            //{
            //    await DataContext.StoreCooperativeAttitudeMapping.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<StoreCooperativeAttitudeMappingDAO> StoreCooperativeAttitudeMappingDAOs = Store.StoreCooperativeAttitudeMappings.Select(p => new StoreCooperativeAttitudeMappingDAO
            //    {
            //        StoreId = Store.Id,
            //        CooperativeAttitudeId = p.CooperativeAttitudeId,
            //    }).ToList();
            //    await DataContext.StoreCooperativeAttitudeMapping.BulkMergeAsync(StoreCooperativeAttitudeMappingDAOs);
            //}
            //#endregion

            //#region StoreBusinessConcentrationLevelMapping - Mức độ chuyên tâm kinh doanh
            //if (Store.BusinessConcentrationLevels.Any())
            //{
            //    await DataContext.BusinessConcentrationLevel.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<BusinessConcentrationLevelDAO> BusinessConcentrationLevelDAOs = Store.BusinessConcentrationLevels.Select(p => new BusinessConcentrationLevelDAO
            //    {
            //        StoreId = Store.Id,
            //        Id = p.Id,
            //        Name = p.Name,
            //        Manufacturer = p.Manufacturer,
            //        Branch = p.Branch,
            //        RevenueInYear = p.RevenueInYear,
            //        MarketingStaff = p.MarketingStaff,
            //    }).ToList();
            //    await DataContext.BusinessConcentrationLevel.BulkMergeAsync(BusinessConcentrationLevelDAOs);
            //}
            //#endregion

            //#region StoreImproveQualityServingMapping - Nâng cao chất lượng dịch vụ
            //if (Store.ImproveQualityServings.Any())
            //{
            //    await DataContext.ImproveQualityServing.Where(p => p.StoreId == Store.Id).DeleteFromQueryAsync();
            //    List<ImproveQualityServingDAO> ImproveQualityServingDAOs = Store.ImproveQualityServings.Select(p => new ImproveQualityServingDAO
            //    {
            //        StoreId = Store.Id,
            //        Id = p.Id,
            //        Name = p.Name,
            //        Detail = p.Detail,
            //    }).ToList();
            //    await DataContext.ImproveQualityServing.BulkMergeAsync(ImproveQualityServingDAOs);
            //}
            //#endregion

        }

    }
}
