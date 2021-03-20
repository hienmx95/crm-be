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
    public interface IStoreMeansOfDeliveryRepository
    {
        Task<int> Count(StoreMeansOfDeliveryFilter StoreMeansOfDeliveryFilter);
        Task<List<StoreMeansOfDelivery>> List(StoreMeansOfDeliveryFilter StoreMeansOfDeliveryFilter);
        Task<List<StoreMeansOfDelivery>> List(List<long> Ids);
        Task<StoreMeansOfDelivery> Get(long Id);
        Task<bool> Create(StoreMeansOfDelivery StoreMeansOfDelivery);
        Task<bool> Update(StoreMeansOfDelivery StoreMeansOfDelivery);
        Task<bool> Delete(StoreMeansOfDelivery StoreMeansOfDelivery);
        Task<bool> BulkMerge(List<StoreMeansOfDelivery> StoreMeansOfDeliveries);
        Task<bool> BulkDelete(List<StoreMeansOfDelivery> StoreMeansOfDeliveries);
    }
    public class StoreMeansOfDeliveryRepository : IStoreMeansOfDeliveryRepository
    {
        private DataContext DataContext;
        public StoreMeansOfDeliveryRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<StoreMeansOfDeliveryDAO> DynamicFilter(IQueryable<StoreMeansOfDeliveryDAO> query, StoreMeansOfDeliveryFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Quantity != null && filter.Quantity.HasValue)
                query = query.Where(q => q.Quantity.HasValue).Where(q => q.Quantity, filter.Quantity);
            if (filter.Owned != null && filter.Owned.HasValue)
                query = query.Where(q => q.Owned.HasValue).Where(q => q.Owned, filter.Owned);
            if (filter.Rent != null && filter.Rent.HasValue)
                query = query.Where(q => q.Rent.HasValue).Where(q => q.Rent, filter.Rent);
            if (filter.StoreId != null && filter.StoreId.HasValue)
                query = query.Where(q => q.StoreId.HasValue).Where(q => q.StoreId, filter.StoreId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<StoreMeansOfDeliveryDAO> OrFilter(IQueryable<StoreMeansOfDeliveryDAO> query, StoreMeansOfDeliveryFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<StoreMeansOfDeliveryDAO> initQuery = query.Where(q => false);
            foreach (StoreMeansOfDeliveryFilter StoreMeansOfDeliveryFilter in filter.OrFilter)
            {
                IQueryable<StoreMeansOfDeliveryDAO> queryable = query;
                if (StoreMeansOfDeliveryFilter.Id != null && StoreMeansOfDeliveryFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, StoreMeansOfDeliveryFilter.Id);
                if (StoreMeansOfDeliveryFilter.Name != null && StoreMeansOfDeliveryFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, StoreMeansOfDeliveryFilter.Name);
                if (StoreMeansOfDeliveryFilter.Quantity != null && StoreMeansOfDeliveryFilter.Quantity.HasValue)
                    queryable = queryable.Where(q => q.Quantity.HasValue).Where(q => q.Quantity, StoreMeansOfDeliveryFilter.Quantity);
                if (StoreMeansOfDeliveryFilter.Owned != null && StoreMeansOfDeliveryFilter.Owned.HasValue)
                    queryable = queryable.Where(q => q.Owned.HasValue).Where(q => q.Owned, StoreMeansOfDeliveryFilter.Owned);
                if (StoreMeansOfDeliveryFilter.Rent != null && StoreMeansOfDeliveryFilter.Rent.HasValue)
                    queryable = queryable.Where(q => q.Rent.HasValue).Where(q => q.Rent, StoreMeansOfDeliveryFilter.Rent);
                if (StoreMeansOfDeliveryFilter.StoreId != null && StoreMeansOfDeliveryFilter.StoreId.HasValue)
                    queryable = queryable.Where(q => q.StoreId.HasValue).Where(q => q.StoreId, StoreMeansOfDeliveryFilter.StoreId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<StoreMeansOfDeliveryDAO> DynamicOrder(IQueryable<StoreMeansOfDeliveryDAO> query, StoreMeansOfDeliveryFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case StoreMeansOfDeliveryOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case StoreMeansOfDeliveryOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case StoreMeansOfDeliveryOrder.Quantity:
                            query = query.OrderBy(q => q.Quantity);
                            break;
                        case StoreMeansOfDeliveryOrder.Owned:
                            query = query.OrderBy(q => q.Owned);
                            break;
                        case StoreMeansOfDeliveryOrder.Rent:
                            query = query.OrderBy(q => q.Rent);
                            break;
                        case StoreMeansOfDeliveryOrder.Store:
                            query = query.OrderBy(q => q.StoreId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case StoreMeansOfDeliveryOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case StoreMeansOfDeliveryOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case StoreMeansOfDeliveryOrder.Quantity:
                            query = query.OrderByDescending(q => q.Quantity);
                            break;
                        case StoreMeansOfDeliveryOrder.Owned:
                            query = query.OrderByDescending(q => q.Owned);
                            break;
                        case StoreMeansOfDeliveryOrder.Rent:
                            query = query.OrderByDescending(q => q.Rent);
                            break;
                        case StoreMeansOfDeliveryOrder.Store:
                            query = query.OrderByDescending(q => q.StoreId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<StoreMeansOfDelivery>> DynamicSelect(IQueryable<StoreMeansOfDeliveryDAO> query, StoreMeansOfDeliveryFilter filter)
        {
            List<StoreMeansOfDelivery> StoreMeansOfDeliveries = await query.Select(q => new StoreMeansOfDelivery()
            {
                Id = filter.Selects.Contains(StoreMeansOfDeliverySelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(StoreMeansOfDeliverySelect.Name) ? q.Name : default(string),
                Quantity = filter.Selects.Contains(StoreMeansOfDeliverySelect.Quantity) ? q.Quantity : default(long?),
                Owned = filter.Selects.Contains(StoreMeansOfDeliverySelect.Owned) ? q.Owned : default(long?),
                Rent = filter.Selects.Contains(StoreMeansOfDeliverySelect.Rent) ? q.Rent : default(long?),
                StoreId = filter.Selects.Contains(StoreMeansOfDeliverySelect.Store) ? q.StoreId : default(long?),
                Store = filter.Selects.Contains(StoreMeansOfDeliverySelect.Store) && q.Store != null ? new Store
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
            return StoreMeansOfDeliveries;
        }

        public async Task<int> Count(StoreMeansOfDeliveryFilter filter)
        {
            IQueryable<StoreMeansOfDeliveryDAO> StoreMeansOfDeliveries = DataContext.StoreMeansOfDelivery.AsNoTracking();
            StoreMeansOfDeliveries = DynamicFilter(StoreMeansOfDeliveries, filter);
            return await StoreMeansOfDeliveries.CountAsync();
        }

        public async Task<List<StoreMeansOfDelivery>> List(StoreMeansOfDeliveryFilter filter)
        {
            if (filter == null) return new List<StoreMeansOfDelivery>();
            IQueryable<StoreMeansOfDeliveryDAO> StoreMeansOfDeliveryDAOs = DataContext.StoreMeansOfDelivery.AsNoTracking();
            StoreMeansOfDeliveryDAOs = DynamicFilter(StoreMeansOfDeliveryDAOs, filter);
            StoreMeansOfDeliveryDAOs = DynamicOrder(StoreMeansOfDeliveryDAOs, filter);
            List<StoreMeansOfDelivery> StoreMeansOfDeliveries = await DynamicSelect(StoreMeansOfDeliveryDAOs, filter);
            return StoreMeansOfDeliveries;
        }

        public async Task<List<StoreMeansOfDelivery>> List(List<long> Ids)
        {
            List<StoreMeansOfDelivery> StoreMeansOfDeliveries = await DataContext.StoreMeansOfDelivery.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new StoreMeansOfDelivery()
            {
                Id = x.Id,
                Name = x.Name,
                Quantity = x.Quantity,
                Owned = x.Owned,
                Rent = x.Rent,
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
            

            return StoreMeansOfDeliveries;
        }

        public async Task<StoreMeansOfDelivery> Get(long Id)
        {
            StoreMeansOfDelivery StoreMeansOfDelivery = await DataContext.StoreMeansOfDelivery.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new StoreMeansOfDelivery()
            {
                Id = x.Id,
                Name = x.Name,
                Quantity = x.Quantity,
                Owned = x.Owned,
                Rent = x.Rent,
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

            if (StoreMeansOfDelivery == null)
                return null;

            return StoreMeansOfDelivery;
        }
        public async Task<bool> Create(StoreMeansOfDelivery StoreMeansOfDelivery)
        {
            StoreMeansOfDeliveryDAO StoreMeansOfDeliveryDAO = new StoreMeansOfDeliveryDAO();
            StoreMeansOfDeliveryDAO.Id = StoreMeansOfDelivery.Id;
            StoreMeansOfDeliveryDAO.Name = StoreMeansOfDelivery.Name;
            StoreMeansOfDeliveryDAO.Quantity = StoreMeansOfDelivery.Quantity;
            StoreMeansOfDeliveryDAO.Owned = StoreMeansOfDelivery.Owned;
            StoreMeansOfDeliveryDAO.Rent = StoreMeansOfDelivery.Rent;
            StoreMeansOfDeliveryDAO.StoreId = StoreMeansOfDelivery.StoreId;
            DataContext.StoreMeansOfDelivery.Add(StoreMeansOfDeliveryDAO);
            await DataContext.SaveChangesAsync();
            StoreMeansOfDelivery.Id = StoreMeansOfDeliveryDAO.Id;
            await SaveReference(StoreMeansOfDelivery);
            return true;
        }

        public async Task<bool> Update(StoreMeansOfDelivery StoreMeansOfDelivery)
        {
            StoreMeansOfDeliveryDAO StoreMeansOfDeliveryDAO = DataContext.StoreMeansOfDelivery.Where(x => x.Id == StoreMeansOfDelivery.Id).FirstOrDefault();
            if (StoreMeansOfDeliveryDAO == null)
                return false;
            StoreMeansOfDeliveryDAO.Id = StoreMeansOfDelivery.Id;
            StoreMeansOfDeliveryDAO.Name = StoreMeansOfDelivery.Name;
            StoreMeansOfDeliveryDAO.Quantity = StoreMeansOfDelivery.Quantity;
            StoreMeansOfDeliveryDAO.Owned = StoreMeansOfDelivery.Owned;
            StoreMeansOfDeliveryDAO.Rent = StoreMeansOfDelivery.Rent;
            StoreMeansOfDeliveryDAO.StoreId = StoreMeansOfDelivery.StoreId;
            await DataContext.SaveChangesAsync();
            await SaveReference(StoreMeansOfDelivery);
            return true;
        }

        public async Task<bool> Delete(StoreMeansOfDelivery StoreMeansOfDelivery)
        {
            await DataContext.StoreMeansOfDelivery.Where(x => x.Id == StoreMeansOfDelivery.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<StoreMeansOfDelivery> StoreMeansOfDeliveries)
        {
            List<StoreMeansOfDeliveryDAO> StoreMeansOfDeliveryDAOs = new List<StoreMeansOfDeliveryDAO>();
            foreach (StoreMeansOfDelivery StoreMeansOfDelivery in StoreMeansOfDeliveries)
            {
                StoreMeansOfDeliveryDAO StoreMeansOfDeliveryDAO = new StoreMeansOfDeliveryDAO();
                StoreMeansOfDeliveryDAO.Id = StoreMeansOfDelivery.Id;
                StoreMeansOfDeliveryDAO.Name = StoreMeansOfDelivery.Name;
                StoreMeansOfDeliveryDAO.Quantity = StoreMeansOfDelivery.Quantity;
                StoreMeansOfDeliveryDAO.Owned = StoreMeansOfDelivery.Owned;
                StoreMeansOfDeliveryDAO.Rent = StoreMeansOfDelivery.Rent;
                StoreMeansOfDeliveryDAO.StoreId = StoreMeansOfDelivery.StoreId;
                StoreMeansOfDeliveryDAOs.Add(StoreMeansOfDeliveryDAO);
            }
            await DataContext.BulkMergeAsync(StoreMeansOfDeliveryDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<StoreMeansOfDelivery> StoreMeansOfDeliveries)
        {
            List<long> Ids = StoreMeansOfDeliveries.Select(x => x.Id).ToList();
            await DataContext.StoreMeansOfDelivery
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(StoreMeansOfDelivery StoreMeansOfDelivery)
        {
        }
        
    }
}
