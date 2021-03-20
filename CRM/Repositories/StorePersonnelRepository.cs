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
    public interface IStorePersonnelRepository
    {
        Task<int> Count(StorePersonnelFilter StorePersonnelFilter);
        Task<List<StorePersonnel>> List(StorePersonnelFilter StorePersonnelFilter);
        Task<List<StorePersonnel>> List(List<long> Ids);
        Task<StorePersonnel> Get(long Id);
        Task<bool> Create(StorePersonnel StorePersonnel);
        Task<bool> Update(StorePersonnel StorePersonnel);
        Task<bool> Delete(StorePersonnel StorePersonnel);
        Task<bool> BulkMerge(List<StorePersonnel> StorePersonnels);
        Task<bool> BulkDelete(List<StorePersonnel> StorePersonnels);
    }
    public class StorePersonnelRepository : IStorePersonnelRepository
    {
        private DataContext DataContext;
        public StorePersonnelRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<StorePersonnelDAO> DynamicFilter(IQueryable<StorePersonnelDAO> query, StorePersonnelFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Quantity != null && filter.Quantity.HasValue)
                query = query.Where(q => q.Quantity.HasValue).Where(q => q.Quantity, filter.Quantity);
            if (filter.StoreId != null && filter.StoreId.HasValue)
                query = query.Where(q => q.StoreId.HasValue).Where(q => q.StoreId, filter.StoreId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<StorePersonnelDAO> OrFilter(IQueryable<StorePersonnelDAO> query, StorePersonnelFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<StorePersonnelDAO> initQuery = query.Where(q => false);
            foreach (StorePersonnelFilter StorePersonnelFilter in filter.OrFilter)
            {
                IQueryable<StorePersonnelDAO> queryable = query;
                if (StorePersonnelFilter.Id != null && StorePersonnelFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, StorePersonnelFilter.Id);
                if (StorePersonnelFilter.Name != null && StorePersonnelFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, StorePersonnelFilter.Name);
                if (StorePersonnelFilter.Quantity != null && StorePersonnelFilter.Quantity.HasValue)
                    queryable = queryable.Where(q => q.Quantity.HasValue).Where(q => q.Quantity, StorePersonnelFilter.Quantity);
                if (StorePersonnelFilter.StoreId != null && StorePersonnelFilter.StoreId.HasValue)
                    queryable = queryable.Where(q => q.StoreId.HasValue).Where(q => q.StoreId, StorePersonnelFilter.StoreId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<StorePersonnelDAO> DynamicOrder(IQueryable<StorePersonnelDAO> query, StorePersonnelFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case StorePersonnelOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case StorePersonnelOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case StorePersonnelOrder.Quantity:
                            query = query.OrderBy(q => q.Quantity);
                            break;
                        case StorePersonnelOrder.Store:
                            query = query.OrderBy(q => q.StoreId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case StorePersonnelOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case StorePersonnelOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case StorePersonnelOrder.Quantity:
                            query = query.OrderByDescending(q => q.Quantity);
                            break;
                        case StorePersonnelOrder.Store:
                            query = query.OrderByDescending(q => q.StoreId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<StorePersonnel>> DynamicSelect(IQueryable<StorePersonnelDAO> query, StorePersonnelFilter filter)
        {
            List<StorePersonnel> StorePersonnels = await query.Select(q => new StorePersonnel()
            {
                Id = filter.Selects.Contains(StorePersonnelSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(StorePersonnelSelect.Name) ? q.Name : default(string),
                Quantity = filter.Selects.Contains(StorePersonnelSelect.Quantity) ? q.Quantity : default(long?),
                StoreId = filter.Selects.Contains(StorePersonnelSelect.Store) ? q.StoreId : default(long?),
                Store = filter.Selects.Contains(StorePersonnelSelect.Store) && q.Store != null ? new Store
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
            return StorePersonnels;
        }

        public async Task<int> Count(StorePersonnelFilter filter)
        {
            IQueryable<StorePersonnelDAO> StorePersonnels = DataContext.StorePersonnel.AsNoTracking();
            StorePersonnels = DynamicFilter(StorePersonnels, filter);
            return await StorePersonnels.CountAsync();
        }

        public async Task<List<StorePersonnel>> List(StorePersonnelFilter filter)
        {
            if (filter == null) return new List<StorePersonnel>();
            IQueryable<StorePersonnelDAO> StorePersonnelDAOs = DataContext.StorePersonnel.AsNoTracking();
            StorePersonnelDAOs = DynamicFilter(StorePersonnelDAOs, filter);
            StorePersonnelDAOs = DynamicOrder(StorePersonnelDAOs, filter);
            List<StorePersonnel> StorePersonnels = await DynamicSelect(StorePersonnelDAOs, filter);
            return StorePersonnels;
        }

        public async Task<List<StorePersonnel>> List(List<long> Ids)
        {
            List<StorePersonnel> StorePersonnels = await DataContext.StorePersonnel.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new StorePersonnel()
            {
                Id = x.Id,
                Name = x.Name,
                Quantity = x.Quantity,
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
            

            return StorePersonnels;
        }

        public async Task<StorePersonnel> Get(long Id)
        {
            StorePersonnel StorePersonnel = await DataContext.StorePersonnel.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new StorePersonnel()
            {
                Id = x.Id,
                Name = x.Name,
                Quantity = x.Quantity,
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

            if (StorePersonnel == null)
                return null;

            return StorePersonnel;
        }
        public async Task<bool> Create(StorePersonnel StorePersonnel)
        {
            StorePersonnelDAO StorePersonnelDAO = new StorePersonnelDAO();
            StorePersonnelDAO.Id = StorePersonnel.Id;
            StorePersonnelDAO.Name = StorePersonnel.Name;
            StorePersonnelDAO.Quantity = StorePersonnel.Quantity;
            StorePersonnelDAO.StoreId = StorePersonnel.StoreId;
            DataContext.StorePersonnel.Add(StorePersonnelDAO);
            await DataContext.SaveChangesAsync();
            StorePersonnel.Id = StorePersonnelDAO.Id;
            await SaveReference(StorePersonnel);
            return true;
        }

        public async Task<bool> Update(StorePersonnel StorePersonnel)
        {
            StorePersonnelDAO StorePersonnelDAO = DataContext.StorePersonnel.Where(x => x.Id == StorePersonnel.Id).FirstOrDefault();
            if (StorePersonnelDAO == null)
                return false;
            StorePersonnelDAO.Id = StorePersonnel.Id;
            StorePersonnelDAO.Name = StorePersonnel.Name;
            StorePersonnelDAO.Quantity = StorePersonnel.Quantity;
            StorePersonnelDAO.StoreId = StorePersonnel.StoreId;
            await DataContext.SaveChangesAsync();
            await SaveReference(StorePersonnel);
            return true;
        }

        public async Task<bool> Delete(StorePersonnel StorePersonnel)
        {
            await DataContext.StorePersonnel.Where(x => x.Id == StorePersonnel.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<StorePersonnel> StorePersonnels)
        {
            List<StorePersonnelDAO> StorePersonnelDAOs = new List<StorePersonnelDAO>();
            foreach (StorePersonnel StorePersonnel in StorePersonnels)
            {
                StorePersonnelDAO StorePersonnelDAO = new StorePersonnelDAO();
                StorePersonnelDAO.Id = StorePersonnel.Id;
                StorePersonnelDAO.Name = StorePersonnel.Name;
                StorePersonnelDAO.Quantity = StorePersonnel.Quantity;
                StorePersonnelDAO.StoreId = StorePersonnel.StoreId;
                StorePersonnelDAOs.Add(StorePersonnelDAO);
            }
            await DataContext.BulkMergeAsync(StorePersonnelDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<StorePersonnel> StorePersonnels)
        {
            List<long> Ids = StorePersonnels.Select(x => x.Id).ToList();
            await DataContext.StorePersonnel
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(StorePersonnel StorePersonnel)
        {
        }
        
    }
}
