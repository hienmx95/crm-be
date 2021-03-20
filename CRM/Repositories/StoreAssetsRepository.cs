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
    public interface IStoreAssetsRepository
    {
        Task<int> Count(StoreAssetsFilter StoreAssetsFilter);
        Task<List<StoreAssets>> List(StoreAssetsFilter StoreAssetsFilter);
        Task<List<StoreAssets>> List(List<long> Ids);
        Task<StoreAssets> Get(long Id);
        Task<bool> Create(StoreAssets StoreAssets);
        Task<bool> Update(StoreAssets StoreAssets);
        Task<bool> Delete(StoreAssets StoreAssets);
        Task<bool> BulkMerge(List<StoreAssets> StoreAssets);
        Task<bool> BulkDelete(List<StoreAssets> StoreAssets);
    }
    public class StoreAssetsRepository : IStoreAssetsRepository
    {
        private DataContext DataContext;
        public StoreAssetsRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<StoreAssetsDAO> DynamicFilter(IQueryable<StoreAssetsDAO> query, StoreAssetsFilter filter)
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

        private IQueryable<StoreAssetsDAO> OrFilter(IQueryable<StoreAssetsDAO> query, StoreAssetsFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<StoreAssetsDAO> initQuery = query.Where(q => false);
            foreach (StoreAssetsFilter StoreAssetsFilter in filter.OrFilter)
            {
                IQueryable<StoreAssetsDAO> queryable = query;
                if (StoreAssetsFilter.Id != null && StoreAssetsFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, StoreAssetsFilter.Id);
                if (StoreAssetsFilter.Name != null && StoreAssetsFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, StoreAssetsFilter.Name);
                if (StoreAssetsFilter.Quantity != null && StoreAssetsFilter.Quantity.HasValue)
                    queryable = queryable.Where(q => q.Quantity.HasValue).Where(q => q.Quantity, StoreAssetsFilter.Quantity);
                if (StoreAssetsFilter.Owned != null && StoreAssetsFilter.Owned.HasValue)
                    queryable = queryable.Where(q => q.Owned.HasValue).Where(q => q.Owned, StoreAssetsFilter.Owned);
                if (StoreAssetsFilter.Rent != null && StoreAssetsFilter.Rent.HasValue)
                    queryable = queryable.Where(q => q.Rent.HasValue).Where(q => q.Rent, StoreAssetsFilter.Rent);
                if (StoreAssetsFilter.StoreId != null && StoreAssetsFilter.StoreId.HasValue)
                    queryable = queryable.Where(q => q.StoreId.HasValue).Where(q => q.StoreId, StoreAssetsFilter.StoreId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<StoreAssetsDAO> DynamicOrder(IQueryable<StoreAssetsDAO> query, StoreAssetsFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case StoreAssetsOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case StoreAssetsOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case StoreAssetsOrder.Quantity:
                            query = query.OrderBy(q => q.Quantity);
                            break;
                        case StoreAssetsOrder.Owned:
                            query = query.OrderBy(q => q.Owned);
                            break;
                        case StoreAssetsOrder.Rent:
                            query = query.OrderBy(q => q.Rent);
                            break;
                        case StoreAssetsOrder.Store:
                            query = query.OrderBy(q => q.StoreId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case StoreAssetsOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case StoreAssetsOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case StoreAssetsOrder.Quantity:
                            query = query.OrderByDescending(q => q.Quantity);
                            break;
                        case StoreAssetsOrder.Owned:
                            query = query.OrderByDescending(q => q.Owned);
                            break;
                        case StoreAssetsOrder.Rent:
                            query = query.OrderByDescending(q => q.Rent);
                            break;
                        case StoreAssetsOrder.Store:
                            query = query.OrderByDescending(q => q.StoreId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<StoreAssets>> DynamicSelect(IQueryable<StoreAssetsDAO> query, StoreAssetsFilter filter)
        {
            List<StoreAssets> StoreAssets = await query.Select(q => new StoreAssets()
            {
                Id = filter.Selects.Contains(StoreAssetsSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(StoreAssetsSelect.Name) ? q.Name : default(string),
                Quantity = filter.Selects.Contains(StoreAssetsSelect.Quantity) ? q.Quantity : default(long?),
                Owned = filter.Selects.Contains(StoreAssetsSelect.Owned) ? q.Owned : default(long?),
                Rent = filter.Selects.Contains(StoreAssetsSelect.Rent) ? q.Rent : default(long?),
                StoreId = filter.Selects.Contains(StoreAssetsSelect.Store) ? q.StoreId : default(long?),
                Store = filter.Selects.Contains(StoreAssetsSelect.Store) && q.Store != null ? new Store
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
            return StoreAssets;
        }

        public async Task<int> Count(StoreAssetsFilter filter)
        {
            IQueryable<StoreAssetsDAO> StoreAssets = DataContext.StoreAssets.AsNoTracking();
            StoreAssets = DynamicFilter(StoreAssets, filter);
            return await StoreAssets.CountAsync();
        }

        public async Task<List<StoreAssets>> List(StoreAssetsFilter filter)
        {
            if (filter == null) return new List<StoreAssets>();
            IQueryable<StoreAssetsDAO> StoreAssetsDAOs = DataContext.StoreAssets.AsNoTracking();
            StoreAssetsDAOs = DynamicFilter(StoreAssetsDAOs, filter);
            StoreAssetsDAOs = DynamicOrder(StoreAssetsDAOs, filter);
            List<StoreAssets> StoreAssets = await DynamicSelect(StoreAssetsDAOs, filter);
            return StoreAssets;
        }

        public async Task<List<StoreAssets>> List(List<long> Ids)
        {
            List<StoreAssets> StoreAssets = await DataContext.StoreAssets.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new StoreAssets()
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
            

            return StoreAssets;
        }

        public async Task<StoreAssets> Get(long Id)
        {
            StoreAssets StoreAssets = await DataContext.StoreAssets.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new StoreAssets()
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

            if (StoreAssets == null)
                return null;

            return StoreAssets;
        }
        public async Task<bool> Create(StoreAssets StoreAssets)
        {
            StoreAssetsDAO StoreAssetsDAO = new StoreAssetsDAO();
            StoreAssetsDAO.Id = StoreAssets.Id;
            StoreAssetsDAO.Name = StoreAssets.Name;
            StoreAssetsDAO.Quantity = StoreAssets.Quantity;
            StoreAssetsDAO.Owned = StoreAssets.Owned;
            StoreAssetsDAO.Rent = StoreAssets.Rent;
            StoreAssetsDAO.StoreId = StoreAssets.StoreId;
            DataContext.StoreAssets.Add(StoreAssetsDAO);
            await DataContext.SaveChangesAsync();
            StoreAssets.Id = StoreAssetsDAO.Id;
            await SaveReference(StoreAssets);
            return true;
        }

        public async Task<bool> Update(StoreAssets StoreAssets)
        {
            StoreAssetsDAO StoreAssetsDAO = DataContext.StoreAssets.Where(x => x.Id == StoreAssets.Id).FirstOrDefault();
            if (StoreAssetsDAO == null)
                return false;
            StoreAssetsDAO.Id = StoreAssets.Id;
            StoreAssetsDAO.Name = StoreAssets.Name;
            StoreAssetsDAO.Quantity = StoreAssets.Quantity;
            StoreAssetsDAO.Owned = StoreAssets.Owned;
            StoreAssetsDAO.Rent = StoreAssets.Rent;
            StoreAssetsDAO.StoreId = StoreAssets.StoreId;
            await DataContext.SaveChangesAsync();
            await SaveReference(StoreAssets);
            return true;
        }

        public async Task<bool> Delete(StoreAssets StoreAssets)
        {
            await DataContext.StoreAssets.Where(x => x.Id == StoreAssets.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<StoreAssets> StoreAssetses)
        {
            List<StoreAssetsDAO> StoreAssetsDAOs = new List<StoreAssetsDAO>();
            foreach (StoreAssets StoreAssets in StoreAssetses)
            {
                StoreAssetsDAO StoreAssetsDAO = new StoreAssetsDAO();
                StoreAssetsDAO.Id = StoreAssets.Id;
                StoreAssetsDAO.Name = StoreAssets.Name;
                StoreAssetsDAO.Quantity = StoreAssets.Quantity;
                StoreAssetsDAO.Owned = StoreAssets.Owned;
                StoreAssetsDAO.Rent = StoreAssets.Rent;
                StoreAssetsDAO.StoreId = StoreAssets.StoreId;
                StoreAssetsDAOs.Add(StoreAssetsDAO);
            }
            await DataContext.BulkMergeAsync(StoreAssetsDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<StoreAssets> StoreAssets)
        {
            List<long> Ids = StoreAssets.Select(x => x.Id).ToList();
            await DataContext.StoreAssets
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(StoreAssets StoreAssets)
        {
        }
        
    }
}
