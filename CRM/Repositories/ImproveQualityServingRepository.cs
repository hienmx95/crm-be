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
    public interface IImproveQualityServingRepository
    {
        Task<int> Count(ImproveQualityServingFilter ImproveQualityServingFilter);
        Task<List<ImproveQualityServing>> List(ImproveQualityServingFilter ImproveQualityServingFilter);
        Task<List<ImproveQualityServing>> List(List<long> Ids);
        Task<ImproveQualityServing> Get(long Id);
        Task<bool> Create(ImproveQualityServing ImproveQualityServing);
        Task<bool> Update(ImproveQualityServing ImproveQualityServing);
        Task<bool> Delete(ImproveQualityServing ImproveQualityServing);
        Task<bool> BulkMerge(List<ImproveQualityServing> ImproveQualityServings);
        Task<bool> BulkDelete(List<ImproveQualityServing> ImproveQualityServings);
    }
    public class ImproveQualityServingRepository : IImproveQualityServingRepository
    {
        private DataContext DataContext;
        public ImproveQualityServingRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ImproveQualityServingDAO> DynamicFilter(IQueryable<ImproveQualityServingDAO> query, ImproveQualityServingFilter filter)
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

        private IQueryable<ImproveQualityServingDAO> OrFilter(IQueryable<ImproveQualityServingDAO> query, ImproveQualityServingFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ImproveQualityServingDAO> initQuery = query.Where(q => false);
            foreach (ImproveQualityServingFilter ImproveQualityServingFilter in filter.OrFilter)
            {
                IQueryable<ImproveQualityServingDAO> queryable = query;
                if (ImproveQualityServingFilter.Id != null && ImproveQualityServingFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ImproveQualityServingFilter.Id);
                if (ImproveQualityServingFilter.Name != null && ImproveQualityServingFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, ImproveQualityServingFilter.Name);
                if (ImproveQualityServingFilter.Detail != null && ImproveQualityServingFilter.Detail.HasValue)
                    queryable = queryable.Where(q => q.Detail, ImproveQualityServingFilter.Detail);
                if (ImproveQualityServingFilter.StoreId != null && ImproveQualityServingFilter.StoreId.HasValue)
                    queryable = queryable.Where(q => q.StoreId.HasValue).Where(q => q.StoreId, ImproveQualityServingFilter.StoreId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ImproveQualityServingDAO> DynamicOrder(IQueryable<ImproveQualityServingDAO> query, ImproveQualityServingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ImproveQualityServingOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ImproveQualityServingOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ImproveQualityServingOrder.Detail:
                            query = query.OrderBy(q => q.Detail);
                            break;
                        case ImproveQualityServingOrder.Store:
                            query = query.OrderBy(q => q.StoreId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ImproveQualityServingOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ImproveQualityServingOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ImproveQualityServingOrder.Detail:
                            query = query.OrderByDescending(q => q.Detail);
                            break;
                        case ImproveQualityServingOrder.Store:
                            query = query.OrderByDescending(q => q.StoreId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ImproveQualityServing>> DynamicSelect(IQueryable<ImproveQualityServingDAO> query, ImproveQualityServingFilter filter)
        {
            List<ImproveQualityServing> ImproveQualityServings = await query.Select(q => new ImproveQualityServing()
            {
                Id = filter.Selects.Contains(ImproveQualityServingSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(ImproveQualityServingSelect.Name) ? q.Name : default(string),
                Detail = filter.Selects.Contains(ImproveQualityServingSelect.Detail) ? q.Detail : default(string),
                StoreId = filter.Selects.Contains(ImproveQualityServingSelect.Store) ? q.StoreId : default(long?),
                Store = filter.Selects.Contains(ImproveQualityServingSelect.Store) && q.Store != null ? new Store
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
            return ImproveQualityServings;
        }

        public async Task<int> Count(ImproveQualityServingFilter filter)
        {
            IQueryable<ImproveQualityServingDAO> ImproveQualityServings = DataContext.ImproveQualityServing.AsNoTracking();
            ImproveQualityServings = DynamicFilter(ImproveQualityServings, filter);
            return await ImproveQualityServings.CountAsync();
        }

        public async Task<List<ImproveQualityServing>> List(ImproveQualityServingFilter filter)
        {
            if (filter == null) return new List<ImproveQualityServing>();
            IQueryable<ImproveQualityServingDAO> ImproveQualityServingDAOs = DataContext.ImproveQualityServing.AsNoTracking();
            ImproveQualityServingDAOs = DynamicFilter(ImproveQualityServingDAOs, filter);
            ImproveQualityServingDAOs = DynamicOrder(ImproveQualityServingDAOs, filter);
            List<ImproveQualityServing> ImproveQualityServings = await DynamicSelect(ImproveQualityServingDAOs, filter);
            return ImproveQualityServings;
        }

        public async Task<List<ImproveQualityServing>> List(List<long> Ids)
        {
            List<ImproveQualityServing> ImproveQualityServings = await DataContext.ImproveQualityServing.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new ImproveQualityServing()
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
            

            return ImproveQualityServings;
        }

        public async Task<ImproveQualityServing> Get(long Id)
        {
            ImproveQualityServing ImproveQualityServing = await DataContext.ImproveQualityServing.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new ImproveQualityServing()
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

            if (ImproveQualityServing == null)
                return null;

            return ImproveQualityServing;
        }
        public async Task<bool> Create(ImproveQualityServing ImproveQualityServing)
        {
            ImproveQualityServingDAO ImproveQualityServingDAO = new ImproveQualityServingDAO();
            ImproveQualityServingDAO.Id = ImproveQualityServing.Id;
            ImproveQualityServingDAO.Name = ImproveQualityServing.Name;
            ImproveQualityServingDAO.Detail = ImproveQualityServing.Detail;
            ImproveQualityServingDAO.StoreId = ImproveQualityServing.StoreId;
            DataContext.ImproveQualityServing.Add(ImproveQualityServingDAO);
            await DataContext.SaveChangesAsync();
            ImproveQualityServing.Id = ImproveQualityServingDAO.Id;
            await SaveReference(ImproveQualityServing);
            return true;
        }

        public async Task<bool> Update(ImproveQualityServing ImproveQualityServing)
        {
            ImproveQualityServingDAO ImproveQualityServingDAO = DataContext.ImproveQualityServing.Where(x => x.Id == ImproveQualityServing.Id).FirstOrDefault();
            if (ImproveQualityServingDAO == null)
                return false;
            ImproveQualityServingDAO.Id = ImproveQualityServing.Id;
            ImproveQualityServingDAO.Name = ImproveQualityServing.Name;
            ImproveQualityServingDAO.Detail = ImproveQualityServing.Detail;
            ImproveQualityServingDAO.StoreId = ImproveQualityServing.StoreId;
            await DataContext.SaveChangesAsync();
            await SaveReference(ImproveQualityServing);
            return true;
        }

        public async Task<bool> Delete(ImproveQualityServing ImproveQualityServing)
        {
            await DataContext.ImproveQualityServing.Where(x => x.Id == ImproveQualityServing.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<ImproveQualityServing> ImproveQualityServings)
        {
            List<ImproveQualityServingDAO> ImproveQualityServingDAOs = new List<ImproveQualityServingDAO>();
            foreach (ImproveQualityServing ImproveQualityServing in ImproveQualityServings)
            {
                ImproveQualityServingDAO ImproveQualityServingDAO = new ImproveQualityServingDAO();
                ImproveQualityServingDAO.Id = ImproveQualityServing.Id;
                ImproveQualityServingDAO.Name = ImproveQualityServing.Name;
                ImproveQualityServingDAO.Detail = ImproveQualityServing.Detail;
                ImproveQualityServingDAO.StoreId = ImproveQualityServing.StoreId;
                ImproveQualityServingDAOs.Add(ImproveQualityServingDAO);
            }
            await DataContext.BulkMergeAsync(ImproveQualityServingDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<ImproveQualityServing> ImproveQualityServings)
        {
            List<long> Ids = ImproveQualityServings.Select(x => x.Id).ToList();
            await DataContext.ImproveQualityServing
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(ImproveQualityServing ImproveQualityServing)
        {
        }
        
    }
}
