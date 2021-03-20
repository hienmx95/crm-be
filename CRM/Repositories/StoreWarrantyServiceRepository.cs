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
    public interface IStoreWarrantyServiceRepository
    {
        Task<int> Count(StoreWarrantyServiceFilter StoreWarrantyServiceFilter);
        Task<List<StoreWarrantyService>> List(StoreWarrantyServiceFilter StoreWarrantyServiceFilter);
        Task<List<StoreWarrantyService>> List(List<long> Ids);
        Task<StoreWarrantyService> Get(long Id);
        Task<bool> Create(StoreWarrantyService StoreWarrantyService);
        Task<bool> Update(StoreWarrantyService StoreWarrantyService);
        Task<bool> Delete(StoreWarrantyService StoreWarrantyService);
        Task<bool> BulkMerge(List<StoreWarrantyService> StoreWarrantyServices);
        Task<bool> BulkDelete(List<StoreWarrantyService> StoreWarrantyServices);
    }
    public class StoreWarrantyServiceRepository : IStoreWarrantyServiceRepository
    {
        private DataContext DataContext;
        public StoreWarrantyServiceRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<StoreWarrantyServiceDAO> DynamicFilter(IQueryable<StoreWarrantyServiceDAO> query, StoreWarrantyServiceFilter filter)
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
                query = query.Where(q => q.StoreId, filter.StoreId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<StoreWarrantyServiceDAO> OrFilter(IQueryable<StoreWarrantyServiceDAO> query, StoreWarrantyServiceFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<StoreWarrantyServiceDAO> initQuery = query.Where(q => false);
            foreach (StoreWarrantyServiceFilter StoreWarrantyServiceFilter in filter.OrFilter)
            {
                IQueryable<StoreWarrantyServiceDAO> queryable = query;
                if (StoreWarrantyServiceFilter.Id != null && StoreWarrantyServiceFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, StoreWarrantyServiceFilter.Id);
                if (StoreWarrantyServiceFilter.Name != null && StoreWarrantyServiceFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, StoreWarrantyServiceFilter.Name);
                if (StoreWarrantyServiceFilter.Detail != null && StoreWarrantyServiceFilter.Detail.HasValue)
                    queryable = queryable.Where(q => q.Detail, StoreWarrantyServiceFilter.Detail);
                if (StoreWarrantyServiceFilter.StoreId != null && StoreWarrantyServiceFilter.StoreId.HasValue)
                    queryable = queryable.Where(q => q.StoreId, StoreWarrantyServiceFilter.StoreId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<StoreWarrantyServiceDAO> DynamicOrder(IQueryable<StoreWarrantyServiceDAO> query, StoreWarrantyServiceFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case StoreWarrantyServiceOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case StoreWarrantyServiceOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case StoreWarrantyServiceOrder.Detail:
                            query = query.OrderBy(q => q.Detail);
                            break;
                        case StoreWarrantyServiceOrder.Store:
                            query = query.OrderBy(q => q.StoreId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case StoreWarrantyServiceOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case StoreWarrantyServiceOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case StoreWarrantyServiceOrder.Detail:
                            query = query.OrderByDescending(q => q.Detail);
                            break;
                        case StoreWarrantyServiceOrder.Store:
                            query = query.OrderByDescending(q => q.StoreId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<StoreWarrantyService>> DynamicSelect(IQueryable<StoreWarrantyServiceDAO> query, StoreWarrantyServiceFilter filter)
        {
            List<StoreWarrantyService> StoreWarrantyServices = await query.Select(q => new StoreWarrantyService()
            {
                Id = filter.Selects.Contains(StoreWarrantyServiceSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(StoreWarrantyServiceSelect.Name) ? q.Name : default(string),
                Detail = filter.Selects.Contains(StoreWarrantyServiceSelect.Detail) ? q.Detail : default(string),
                StoreId = filter.Selects.Contains(StoreWarrantyServiceSelect.Store) ? q.StoreId : default(long),
                Store = filter.Selects.Contains(StoreWarrantyServiceSelect.Store) && q.Store != null ? new Store
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
            return StoreWarrantyServices;
        }

        public async Task<int> Count(StoreWarrantyServiceFilter filter)
        {
            IQueryable<StoreWarrantyServiceDAO> StoreWarrantyServices = DataContext.StoreWarrantyService.AsNoTracking();
            StoreWarrantyServices = DynamicFilter(StoreWarrantyServices, filter);
            return await StoreWarrantyServices.CountAsync();
        }

        public async Task<List<StoreWarrantyService>> List(StoreWarrantyServiceFilter filter)
        {
            if (filter == null) return new List<StoreWarrantyService>();
            IQueryable<StoreWarrantyServiceDAO> StoreWarrantyServiceDAOs = DataContext.StoreWarrantyService.AsNoTracking();
            StoreWarrantyServiceDAOs = DynamicFilter(StoreWarrantyServiceDAOs, filter);
            StoreWarrantyServiceDAOs = DynamicOrder(StoreWarrantyServiceDAOs, filter);
            List<StoreWarrantyService> StoreWarrantyServices = await DynamicSelect(StoreWarrantyServiceDAOs, filter);
            return StoreWarrantyServices;
        }

        public async Task<List<StoreWarrantyService>> List(List<long> Ids)
        {
            List<StoreWarrantyService> StoreWarrantyServices = await DataContext.StoreWarrantyService.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new StoreWarrantyService()
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
            

            return StoreWarrantyServices;
        }

        public async Task<StoreWarrantyService> Get(long Id)
        {
            StoreWarrantyService StoreWarrantyService = await DataContext.StoreWarrantyService.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new StoreWarrantyService()
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

            if (StoreWarrantyService == null)
                return null;

            return StoreWarrantyService;
        }
        public async Task<bool> Create(StoreWarrantyService StoreWarrantyService)
        {
            StoreWarrantyServiceDAO StoreWarrantyServiceDAO = new StoreWarrantyServiceDAO();
            StoreWarrantyServiceDAO.Id = StoreWarrantyService.Id;
            StoreWarrantyServiceDAO.Name = StoreWarrantyService.Name;
            StoreWarrantyServiceDAO.Detail = StoreWarrantyService.Detail;
            StoreWarrantyServiceDAO.StoreId = StoreWarrantyService.StoreId;
            DataContext.StoreWarrantyService.Add(StoreWarrantyServiceDAO);
            await DataContext.SaveChangesAsync();
            StoreWarrantyService.Id = StoreWarrantyServiceDAO.Id;
            await SaveReference(StoreWarrantyService);
            return true;
        }

        public async Task<bool> Update(StoreWarrantyService StoreWarrantyService)
        {
            StoreWarrantyServiceDAO StoreWarrantyServiceDAO = DataContext.StoreWarrantyService.Where(x => x.Id == StoreWarrantyService.Id).FirstOrDefault();
            if (StoreWarrantyServiceDAO == null)
                return false;
            StoreWarrantyServiceDAO.Id = StoreWarrantyService.Id;
            StoreWarrantyServiceDAO.Name = StoreWarrantyService.Name;
            StoreWarrantyServiceDAO.Detail = StoreWarrantyService.Detail;
            StoreWarrantyServiceDAO.StoreId = StoreWarrantyService.StoreId;
            await DataContext.SaveChangesAsync();
            await SaveReference(StoreWarrantyService);
            return true;
        }

        public async Task<bool> Delete(StoreWarrantyService StoreWarrantyService)
        {
            await DataContext.StoreWarrantyService.Where(x => x.Id == StoreWarrantyService.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<StoreWarrantyService> StoreWarrantyServices)
        {
            List<StoreWarrantyServiceDAO> StoreWarrantyServiceDAOs = new List<StoreWarrantyServiceDAO>();
            foreach (StoreWarrantyService StoreWarrantyService in StoreWarrantyServices)
            {
                StoreWarrantyServiceDAO StoreWarrantyServiceDAO = new StoreWarrantyServiceDAO();
                StoreWarrantyServiceDAO.Id = StoreWarrantyService.Id;
                StoreWarrantyServiceDAO.Name = StoreWarrantyService.Name;
                StoreWarrantyServiceDAO.Detail = StoreWarrantyService.Detail;
                StoreWarrantyServiceDAO.StoreId = StoreWarrantyService.StoreId;
                StoreWarrantyServiceDAOs.Add(StoreWarrantyServiceDAO);
            }
            await DataContext.BulkMergeAsync(StoreWarrantyServiceDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<StoreWarrantyService> StoreWarrantyServices)
        {
            List<long> Ids = StoreWarrantyServices.Select(x => x.Id).ToList();
            await DataContext.StoreWarrantyService
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(StoreWarrantyService StoreWarrantyService)
        {
        }
        
    }
}
