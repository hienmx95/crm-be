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
    public interface IBusinessConcentrationLevelRepository
    {
        Task<int> Count(BusinessConcentrationLevelFilter BusinessConcentrationLevelFilter);
        Task<List<BusinessConcentrationLevel>> List(BusinessConcentrationLevelFilter BusinessConcentrationLevelFilter);
        Task<List<BusinessConcentrationLevel>> List(List<long> Ids);
        Task<BusinessConcentrationLevel> Get(long Id);
        Task<bool> Create(BusinessConcentrationLevel BusinessConcentrationLevel);
        Task<bool> Update(BusinessConcentrationLevel BusinessConcentrationLevel);
        Task<bool> Delete(BusinessConcentrationLevel BusinessConcentrationLevel);
        Task<bool> BulkMerge(List<BusinessConcentrationLevel> BusinessConcentrationLevels);
        Task<bool> BulkDelete(List<BusinessConcentrationLevel> BusinessConcentrationLevels);
    }
    public class BusinessConcentrationLevelRepository : IBusinessConcentrationLevelRepository
    {
        private DataContext DataContext;
        public BusinessConcentrationLevelRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<BusinessConcentrationLevelDAO> DynamicFilter(IQueryable<BusinessConcentrationLevelDAO> query, BusinessConcentrationLevelFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Manufacturer != null && filter.Manufacturer.HasValue)
                query = query.Where(q => q.Manufacturer, filter.Manufacturer);
            if (filter.Branch != null && filter.Branch.HasValue)
                query = query.Where(q => q.Branch, filter.Branch);
            if (filter.RevenueInYear != null && filter.RevenueInYear.HasValue)
                query = query.Where(q => q.RevenueInYear.HasValue).Where(q => q.RevenueInYear, filter.RevenueInYear);
            if (filter.MarketingStaff != null && filter.MarketingStaff.HasValue)
                query = query.Where(q => q.MarketingStaff.HasValue).Where(q => q.MarketingStaff, filter.MarketingStaff);
            if (filter.StoreId != null && filter.StoreId.HasValue)
                query = query.Where(q => q.StoreId.HasValue).Where(q => q.StoreId, filter.StoreId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<BusinessConcentrationLevelDAO> OrFilter(IQueryable<BusinessConcentrationLevelDAO> query, BusinessConcentrationLevelFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<BusinessConcentrationLevelDAO> initQuery = query.Where(q => false);
            foreach (BusinessConcentrationLevelFilter BusinessConcentrationLevelFilter in filter.OrFilter)
            {
                IQueryable<BusinessConcentrationLevelDAO> queryable = query;
                if (BusinessConcentrationLevelFilter.Id != null && BusinessConcentrationLevelFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, BusinessConcentrationLevelFilter.Id);
                if (BusinessConcentrationLevelFilter.Name != null && BusinessConcentrationLevelFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, BusinessConcentrationLevelFilter.Name);
                if (BusinessConcentrationLevelFilter.Manufacturer != null && BusinessConcentrationLevelFilter.Manufacturer.HasValue)
                    queryable = queryable.Where(q => q.Manufacturer, BusinessConcentrationLevelFilter.Manufacturer);
                if (BusinessConcentrationLevelFilter.Branch != null && BusinessConcentrationLevelFilter.Branch.HasValue)
                    queryable = queryable.Where(q => q.Branch, BusinessConcentrationLevelFilter.Branch);
                if (BusinessConcentrationLevelFilter.RevenueInYear != null && BusinessConcentrationLevelFilter.RevenueInYear.HasValue)
                    queryable = queryable.Where(q => q.RevenueInYear.HasValue).Where(q => q.RevenueInYear, BusinessConcentrationLevelFilter.RevenueInYear);
                if (BusinessConcentrationLevelFilter.MarketingStaff != null && BusinessConcentrationLevelFilter.MarketingStaff.HasValue)
                    queryable = queryable.Where(q => q.MarketingStaff.HasValue).Where(q => q.MarketingStaff, BusinessConcentrationLevelFilter.MarketingStaff);
                if (BusinessConcentrationLevelFilter.StoreId != null && BusinessConcentrationLevelFilter.StoreId.HasValue)
                    queryable = queryable.Where(q => q.StoreId.HasValue).Where(q => q.StoreId, BusinessConcentrationLevelFilter.StoreId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<BusinessConcentrationLevelDAO> DynamicOrder(IQueryable<BusinessConcentrationLevelDAO> query, BusinessConcentrationLevelFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case BusinessConcentrationLevelOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case BusinessConcentrationLevelOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case BusinessConcentrationLevelOrder.Manufacturer:
                            query = query.OrderBy(q => q.Manufacturer);
                            break;
                        case BusinessConcentrationLevelOrder.Branch:
                            query = query.OrderBy(q => q.Branch);
                            break;
                        case BusinessConcentrationLevelOrder.RevenueInYear:
                            query = query.OrderBy(q => q.RevenueInYear);
                            break;
                        case BusinessConcentrationLevelOrder.MarketingStaff:
                            query = query.OrderBy(q => q.MarketingStaff);
                            break;
                        case BusinessConcentrationLevelOrder.Store:
                            query = query.OrderBy(q => q.StoreId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case BusinessConcentrationLevelOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case BusinessConcentrationLevelOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case BusinessConcentrationLevelOrder.Manufacturer:
                            query = query.OrderByDescending(q => q.Manufacturer);
                            break;
                        case BusinessConcentrationLevelOrder.Branch:
                            query = query.OrderByDescending(q => q.Branch);
                            break;
                        case BusinessConcentrationLevelOrder.RevenueInYear:
                            query = query.OrderByDescending(q => q.RevenueInYear);
                            break;
                        case BusinessConcentrationLevelOrder.MarketingStaff:
                            query = query.OrderByDescending(q => q.MarketingStaff);
                            break;
                        case BusinessConcentrationLevelOrder.Store:
                            query = query.OrderByDescending(q => q.StoreId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<BusinessConcentrationLevel>> DynamicSelect(IQueryable<BusinessConcentrationLevelDAO> query, BusinessConcentrationLevelFilter filter)
        {
            List<BusinessConcentrationLevel> BusinessConcentrationLevels = await query.Select(q => new BusinessConcentrationLevel()
            {
                Id = filter.Selects.Contains(BusinessConcentrationLevelSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(BusinessConcentrationLevelSelect.Name) ? q.Name : default(string),
                Manufacturer = filter.Selects.Contains(BusinessConcentrationLevelSelect.Manufacturer) ? q.Manufacturer : default(string),
                Branch = filter.Selects.Contains(BusinessConcentrationLevelSelect.Branch) ? q.Branch : default(string),
                RevenueInYear = filter.Selects.Contains(BusinessConcentrationLevelSelect.RevenueInYear) ? q.RevenueInYear : default(decimal?),
                MarketingStaff = filter.Selects.Contains(BusinessConcentrationLevelSelect.MarketingStaff) ? q.MarketingStaff : default(long?),
                StoreId = filter.Selects.Contains(BusinessConcentrationLevelSelect.Store) ? q.StoreId : default(long?),
                Store = filter.Selects.Contains(BusinessConcentrationLevelSelect.Store) && q.Store != null ? new Store
                {
                    Id = q.Store.Id,
                    Code = q.Store.Code,
                    CodeDraft = q.Store.CodeDraft,
                    Name = q.Store.Name,
                    UnsignName = q.Store.UnsignName,
                    ParentStoreId = q.Store.ParentStoreId,
                    OrganizationId = q.Store.OrganizationId,
                    StoreTypeId = q.Store.StoreTypeId,
                    StoreGroupingId = q.Store.StoreGroupingId,
                    Telephone = q.Store.Telephone,
                    ProvinceId = q.Store.ProvinceId,
                    DistrictId = q.Store.DistrictId,
                    WardId = q.Store.WardId,
                    Address = q.Store.Address,
                    UnsignAddress = q.Store.UnsignAddress,
                    DeliveryAddress = q.Store.DeliveryAddress,
                    Latitude = q.Store.Latitude,
                    Longitude = q.Store.Longitude,
                    DeliveryLatitude = q.Store.DeliveryLatitude,
                    DeliveryLongitude = q.Store.DeliveryLongitude,
                    OwnerName = q.Store.OwnerName,
                    OwnerPhone = q.Store.OwnerPhone,
                    OwnerEmail = q.Store.OwnerEmail,
                    TaxCode = q.Store.TaxCode,
                    LegalEntity = q.Store.LegalEntity,
                    AppUserId = q.Store.AppUserId,
                    StatusId = q.Store.StatusId,
                    RowId = q.Store.RowId,
                    Used = q.Store.Used,
                    StoreStatusId = q.Store.StoreStatusId,
                } : null,
            }).ToListAsync();
            return BusinessConcentrationLevels;
        }

        public async Task<int> Count(BusinessConcentrationLevelFilter filter)
        {
            IQueryable<BusinessConcentrationLevelDAO> BusinessConcentrationLevels = DataContext.BusinessConcentrationLevel.AsNoTracking();
            BusinessConcentrationLevels = DynamicFilter(BusinessConcentrationLevels, filter);
            return await BusinessConcentrationLevels.CountAsync();
        }

        public async Task<List<BusinessConcentrationLevel>> List(BusinessConcentrationLevelFilter filter)
        {
            if (filter == null) return new List<BusinessConcentrationLevel>();
            IQueryable<BusinessConcentrationLevelDAO> BusinessConcentrationLevelDAOs = DataContext.BusinessConcentrationLevel.AsNoTracking();
            BusinessConcentrationLevelDAOs = DynamicFilter(BusinessConcentrationLevelDAOs, filter);
            BusinessConcentrationLevelDAOs = DynamicOrder(BusinessConcentrationLevelDAOs, filter);
            List<BusinessConcentrationLevel> BusinessConcentrationLevels = await DynamicSelect(BusinessConcentrationLevelDAOs, filter);
            return BusinessConcentrationLevels;
        }

        public async Task<List<BusinessConcentrationLevel>> List(List<long> Ids)
        {
            List<BusinessConcentrationLevel> BusinessConcentrationLevels = await DataContext.BusinessConcentrationLevel.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new BusinessConcentrationLevel()
            {
                Id = x.Id,
                Name = x.Name,
                Manufacturer = x.Manufacturer,
                Branch = x.Branch,
                RevenueInYear = x.RevenueInYear,
                MarketingStaff = x.MarketingStaff,
                StoreId = x.StoreId,
                Store = x.Store == null ? null : new Store
                {
                    Id = x.Store.Id,
                    Code = x.Store.Code,
                    CodeDraft = x.Store.CodeDraft,
                    Name = x.Store.Name,
                    UnsignName = x.Store.UnsignName,
                    ParentStoreId = x.Store.ParentStoreId,
                    OrganizationId = x.Store.OrganizationId,
                    StoreTypeId = x.Store.StoreTypeId,
                    StoreGroupingId = x.Store.StoreGroupingId,
                    Telephone = x.Store.Telephone,
                    ProvinceId = x.Store.ProvinceId,
                    DistrictId = x.Store.DistrictId,
                    WardId = x.Store.WardId,
                    Address = x.Store.Address,
                    UnsignAddress = x.Store.UnsignAddress,
                    DeliveryAddress = x.Store.DeliveryAddress,
                    Latitude = x.Store.Latitude,
                    Longitude = x.Store.Longitude,
                    DeliveryLatitude = x.Store.DeliveryLatitude,
                    DeliveryLongitude = x.Store.DeliveryLongitude,
                    OwnerName = x.Store.OwnerName,
                    OwnerPhone = x.Store.OwnerPhone,
                    OwnerEmail = x.Store.OwnerEmail,
                    TaxCode = x.Store.TaxCode,
                    LegalEntity = x.Store.LegalEntity,
                    AppUserId = x.Store.AppUserId,
                    StatusId = x.Store.StatusId,
                    RowId = x.Store.RowId,
                    Used = x.Store.Used,
                    StoreStatusId = x.Store.StoreStatusId,
                },
            }).ToListAsync();
            

            return BusinessConcentrationLevels;
        }

        public async Task<BusinessConcentrationLevel> Get(long Id)
        {
            BusinessConcentrationLevel BusinessConcentrationLevel = await DataContext.BusinessConcentrationLevel.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new BusinessConcentrationLevel()
            {
                Id = x.Id,
                Name = x.Name,
                Manufacturer = x.Manufacturer,
                Branch = x.Branch,
                RevenueInYear = x.RevenueInYear,
                MarketingStaff = x.MarketingStaff,
                StoreId = x.StoreId,
                Store = x.Store == null ? null : new Store
                {
                    Id = x.Store.Id,
                    Code = x.Store.Code,
                    CodeDraft = x.Store.CodeDraft,
                    Name = x.Store.Name,
                    UnsignName = x.Store.UnsignName,
                    ParentStoreId = x.Store.ParentStoreId,
                    OrganizationId = x.Store.OrganizationId,
                    StoreTypeId = x.Store.StoreTypeId,
                    StoreGroupingId = x.Store.StoreGroupingId,
                    Telephone = x.Store.Telephone,
                    ProvinceId = x.Store.ProvinceId,
                    DistrictId = x.Store.DistrictId,
                    WardId = x.Store.WardId,
                    Address = x.Store.Address,
                    UnsignAddress = x.Store.UnsignAddress,
                    DeliveryAddress = x.Store.DeliveryAddress,
                    Latitude = x.Store.Latitude,
                    Longitude = x.Store.Longitude,
                    DeliveryLatitude = x.Store.DeliveryLatitude,
                    DeliveryLongitude = x.Store.DeliveryLongitude,
                    OwnerName = x.Store.OwnerName,
                    OwnerPhone = x.Store.OwnerPhone,
                    OwnerEmail = x.Store.OwnerEmail,
                    TaxCode = x.Store.TaxCode,
                    LegalEntity = x.Store.LegalEntity,
                    AppUserId = x.Store.AppUserId,
                    StatusId = x.Store.StatusId,
                    RowId = x.Store.RowId,
                    Used = x.Store.Used,
                    StoreStatusId = x.Store.StoreStatusId,
                },
            }).FirstOrDefaultAsync();

            if (BusinessConcentrationLevel == null)
                return null;

            return BusinessConcentrationLevel;
        }
        public async Task<bool> Create(BusinessConcentrationLevel BusinessConcentrationLevel)
        {
            BusinessConcentrationLevelDAO BusinessConcentrationLevelDAO = new BusinessConcentrationLevelDAO();
            BusinessConcentrationLevelDAO.Id = BusinessConcentrationLevel.Id;
            BusinessConcentrationLevelDAO.Name = BusinessConcentrationLevel.Name;
            BusinessConcentrationLevelDAO.Manufacturer = BusinessConcentrationLevel.Manufacturer;
            BusinessConcentrationLevelDAO.Branch = BusinessConcentrationLevel.Branch;
            BusinessConcentrationLevelDAO.RevenueInYear = BusinessConcentrationLevel.RevenueInYear;
            BusinessConcentrationLevelDAO.MarketingStaff = BusinessConcentrationLevel.MarketingStaff;
            BusinessConcentrationLevelDAO.StoreId = BusinessConcentrationLevel.StoreId;
            DataContext.BusinessConcentrationLevel.Add(BusinessConcentrationLevelDAO);
            await DataContext.SaveChangesAsync();
            BusinessConcentrationLevel.Id = BusinessConcentrationLevelDAO.Id;
            await SaveReference(BusinessConcentrationLevel);
            return true;
        }

        public async Task<bool> Update(BusinessConcentrationLevel BusinessConcentrationLevel)
        {
            BusinessConcentrationLevelDAO BusinessConcentrationLevelDAO = DataContext.BusinessConcentrationLevel.Where(x => x.Id == BusinessConcentrationLevel.Id).FirstOrDefault();
            if (BusinessConcentrationLevelDAO == null)
                return false;
            BusinessConcentrationLevelDAO.Id = BusinessConcentrationLevel.Id;
            BusinessConcentrationLevelDAO.Name = BusinessConcentrationLevel.Name;
            BusinessConcentrationLevelDAO.Manufacturer = BusinessConcentrationLevel.Manufacturer;
            BusinessConcentrationLevelDAO.Branch = BusinessConcentrationLevel.Branch;
            BusinessConcentrationLevelDAO.RevenueInYear = BusinessConcentrationLevel.RevenueInYear;
            BusinessConcentrationLevelDAO.MarketingStaff = BusinessConcentrationLevel.MarketingStaff;
            BusinessConcentrationLevelDAO.StoreId = BusinessConcentrationLevel.StoreId;
            await DataContext.SaveChangesAsync();
            await SaveReference(BusinessConcentrationLevel);
            return true;
        }

        public async Task<bool> Delete(BusinessConcentrationLevel BusinessConcentrationLevel)
        {
            await DataContext.BusinessConcentrationLevel.Where(x => x.Id == BusinessConcentrationLevel.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<BusinessConcentrationLevel> BusinessConcentrationLevels)
        {
            List<BusinessConcentrationLevelDAO> BusinessConcentrationLevelDAOs = new List<BusinessConcentrationLevelDAO>();
            foreach (BusinessConcentrationLevel BusinessConcentrationLevel in BusinessConcentrationLevels)
            {
                BusinessConcentrationLevelDAO BusinessConcentrationLevelDAO = new BusinessConcentrationLevelDAO();
                BusinessConcentrationLevelDAO.Id = BusinessConcentrationLevel.Id;
                BusinessConcentrationLevelDAO.Name = BusinessConcentrationLevel.Name;
                BusinessConcentrationLevelDAO.Manufacturer = BusinessConcentrationLevel.Manufacturer;
                BusinessConcentrationLevelDAO.Branch = BusinessConcentrationLevel.Branch;
                BusinessConcentrationLevelDAO.RevenueInYear = BusinessConcentrationLevel.RevenueInYear;
                BusinessConcentrationLevelDAO.MarketingStaff = BusinessConcentrationLevel.MarketingStaff;
                BusinessConcentrationLevelDAO.StoreId = BusinessConcentrationLevel.StoreId;
                BusinessConcentrationLevelDAOs.Add(BusinessConcentrationLevelDAO);
            }
            await DataContext.BulkMergeAsync(BusinessConcentrationLevelDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<BusinessConcentrationLevel> BusinessConcentrationLevels)
        {
            List<long> Ids = BusinessConcentrationLevels.Select(x => x.Id).ToList();
            await DataContext.BusinessConcentrationLevel
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(BusinessConcentrationLevel BusinessConcentrationLevel)
        {
        }
        
    }
}
