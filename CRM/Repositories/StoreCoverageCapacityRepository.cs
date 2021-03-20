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
    public interface IStoreCoverageCapacityRepository
    {
        Task<int> Count(StoreCoverageCapacityFilter StoreCoverageCapacityFilter);
        Task<List<StoreCoverageCapacity>> List(StoreCoverageCapacityFilter StoreCoverageCapacityFilter);
        Task<List<StoreCoverageCapacity>> List(List<long> Ids);
        Task<StoreCoverageCapacity> Get(long Id);
        Task<bool> Create(StoreCoverageCapacity StoreCoverageCapacity);
        Task<bool> Update(StoreCoverageCapacity StoreCoverageCapacity);
        Task<bool> Delete(StoreCoverageCapacity StoreCoverageCapacity);
        Task<bool> BulkMerge(List<StoreCoverageCapacity> StoreCoverageCapacities);
        Task<bool> BulkDelete(List<StoreCoverageCapacity> StoreCoverageCapacities);
    }
    public class StoreCoverageCapacityRepository : IStoreCoverageCapacityRepository
    {
        private DataContext DataContext;
        public StoreCoverageCapacityRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<StoreCoverageCapacityDAO> DynamicFilter(IQueryable<StoreCoverageCapacityDAO> query, StoreCoverageCapacityFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Detail != null && filter.Detail.HasValue)
                query = query.Where(q => q.Detail, filter.Detail);
            if (filter.StoreId != null && filter.StoreId.HasValue)
                query = query.Where(q => q.StoreId.HasValue).Where(q => q.StoreId, filter.StoreId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<StoreCoverageCapacityDAO> OrFilter(IQueryable<StoreCoverageCapacityDAO> query, StoreCoverageCapacityFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<StoreCoverageCapacityDAO> initQuery = query.Where(q => false);
            foreach (StoreCoverageCapacityFilter StoreCoverageCapacityFilter in filter.OrFilter)
            {
                IQueryable<StoreCoverageCapacityDAO> queryable = query;
                if (StoreCoverageCapacityFilter.Id != null && StoreCoverageCapacityFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, StoreCoverageCapacityFilter.Id);
                if (StoreCoverageCapacityFilter.Name != null && StoreCoverageCapacityFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, StoreCoverageCapacityFilter.Name);
                if (StoreCoverageCapacityFilter.Detail != null && StoreCoverageCapacityFilter.Detail.HasValue)
                    queryable = queryable.Where(q => q.Detail, StoreCoverageCapacityFilter.Detail);
                if (StoreCoverageCapacityFilter.StoreId != null && StoreCoverageCapacityFilter.StoreId.HasValue)
                    queryable = queryable.Where(q => q.StoreId.HasValue).Where(q => q.StoreId, StoreCoverageCapacityFilter.StoreId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<StoreCoverageCapacityDAO> DynamicOrder(IQueryable<StoreCoverageCapacityDAO> query, StoreCoverageCapacityFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case StoreCoverageCapacityOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case StoreCoverageCapacityOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case StoreCoverageCapacityOrder.Detail:
                            query = query.OrderBy(q => q.Detail);
                            break;
                        case StoreCoverageCapacityOrder.Store:
                            query = query.OrderBy(q => q.StoreId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case StoreCoverageCapacityOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case StoreCoverageCapacityOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case StoreCoverageCapacityOrder.Detail:
                            query = query.OrderByDescending(q => q.Detail);
                            break;
                        case StoreCoverageCapacityOrder.Store:
                            query = query.OrderByDescending(q => q.StoreId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<StoreCoverageCapacity>> DynamicSelect(IQueryable<StoreCoverageCapacityDAO> query, StoreCoverageCapacityFilter filter)
        {
            List<StoreCoverageCapacity> StoreCoverageCapacities = await query.Select(q => new StoreCoverageCapacity()
            {
                Id = filter.Selects.Contains(StoreCoverageCapacitySelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(StoreCoverageCapacitySelect.Name) ? q.Name : default(string),
                Detail = filter.Selects.Contains(StoreCoverageCapacitySelect.Detail) ? q.Detail : default(string),
                StoreId = filter.Selects.Contains(StoreCoverageCapacitySelect.Store) ? q.StoreId : default(long?),
                Store = filter.Selects.Contains(StoreCoverageCapacitySelect.Store) && q.Store != null ? new Store
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
            return StoreCoverageCapacities;
        }

        public async Task<int> Count(StoreCoverageCapacityFilter filter)
        {
            IQueryable<StoreCoverageCapacityDAO> StoreCoverageCapacities = DataContext.StoreCoverageCapacity.AsNoTracking();
            StoreCoverageCapacities = DynamicFilter(StoreCoverageCapacities, filter);
            return await StoreCoverageCapacities.CountAsync();
        }

        public async Task<List<StoreCoverageCapacity>> List(StoreCoverageCapacityFilter filter)
        {
            if (filter == null) return new List<StoreCoverageCapacity>();
            IQueryable<StoreCoverageCapacityDAO> StoreCoverageCapacityDAOs = DataContext.StoreCoverageCapacity.AsNoTracking();
            StoreCoverageCapacityDAOs = DynamicFilter(StoreCoverageCapacityDAOs, filter);
            StoreCoverageCapacityDAOs = DynamicOrder(StoreCoverageCapacityDAOs, filter);
            List<StoreCoverageCapacity> StoreCoverageCapacities = await DynamicSelect(StoreCoverageCapacityDAOs, filter);
            return StoreCoverageCapacities;
        }

        public async Task<List<StoreCoverageCapacity>> List(List<long> Ids)
        {
            List<StoreCoverageCapacity> StoreCoverageCapacities = await DataContext.StoreCoverageCapacity.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new StoreCoverageCapacity()
            {
                Id = x.Id,
                Name = x.Name,
                Detail = x.Detail,
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
            

            return StoreCoverageCapacities;
        }

        public async Task<StoreCoverageCapacity> Get(long Id)
        {
            StoreCoverageCapacity StoreCoverageCapacity = await DataContext.StoreCoverageCapacity.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new StoreCoverageCapacity()
            {
                Id = x.Id,
                Name = x.Name,
                Detail = x.Detail,
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

            if (StoreCoverageCapacity == null)
                return null;

            return StoreCoverageCapacity;
        }
        public async Task<bool> Create(StoreCoverageCapacity StoreCoverageCapacity)
        {
            StoreCoverageCapacityDAO StoreCoverageCapacityDAO = new StoreCoverageCapacityDAO();
            StoreCoverageCapacityDAO.Id = StoreCoverageCapacity.Id;
            StoreCoverageCapacityDAO.Name = StoreCoverageCapacity.Name;
            StoreCoverageCapacityDAO.Detail = StoreCoverageCapacity.Detail;
            StoreCoverageCapacityDAO.StoreId = StoreCoverageCapacity.StoreId;
            DataContext.StoreCoverageCapacity.Add(StoreCoverageCapacityDAO);
            await DataContext.SaveChangesAsync();
            StoreCoverageCapacity.Id = StoreCoverageCapacityDAO.Id;
            await SaveReference(StoreCoverageCapacity);
            return true;
        }

        public async Task<bool> Update(StoreCoverageCapacity StoreCoverageCapacity)
        {
            StoreCoverageCapacityDAO StoreCoverageCapacityDAO = DataContext.StoreCoverageCapacity.Where(x => x.Id == StoreCoverageCapacity.Id).FirstOrDefault();
            if (StoreCoverageCapacityDAO == null)
                return false;
            StoreCoverageCapacityDAO.Id = StoreCoverageCapacity.Id;
            StoreCoverageCapacityDAO.Name = StoreCoverageCapacity.Name;
            StoreCoverageCapacityDAO.Detail = StoreCoverageCapacity.Detail;
            StoreCoverageCapacityDAO.StoreId = StoreCoverageCapacity.StoreId;
            await DataContext.SaveChangesAsync();
            await SaveReference(StoreCoverageCapacity);
            return true;
        }

        public async Task<bool> Delete(StoreCoverageCapacity StoreCoverageCapacity)
        {
            await DataContext.StoreCoverageCapacity.Where(x => x.Id == StoreCoverageCapacity.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<StoreCoverageCapacity> StoreCoverageCapacities)
        {
            List<StoreCoverageCapacityDAO> StoreCoverageCapacityDAOs = new List<StoreCoverageCapacityDAO>();
            foreach (StoreCoverageCapacity StoreCoverageCapacity in StoreCoverageCapacities)
            {
                StoreCoverageCapacityDAO StoreCoverageCapacityDAO = new StoreCoverageCapacityDAO();
                StoreCoverageCapacityDAO.Id = StoreCoverageCapacity.Id;
                StoreCoverageCapacityDAO.Name = StoreCoverageCapacity.Name;
                StoreCoverageCapacityDAO.Detail = StoreCoverageCapacity.Detail;
                StoreCoverageCapacityDAO.StoreId = StoreCoverageCapacity.StoreId;
                StoreCoverageCapacityDAOs.Add(StoreCoverageCapacityDAO);
            }
            await DataContext.BulkMergeAsync(StoreCoverageCapacityDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<StoreCoverageCapacity> StoreCoverageCapacities)
        {
            List<long> Ids = StoreCoverageCapacities.Select(x => x.Id).ToList();
            await DataContext.StoreCoverageCapacity
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(StoreCoverageCapacity StoreCoverageCapacity)
        {
        }
        
    }
}
