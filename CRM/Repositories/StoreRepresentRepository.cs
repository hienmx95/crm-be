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
    public interface IStoreRepresentRepository
    {
        Task<int> Count(StoreRepresentFilter StoreRepresentFilter);
        Task<List<StoreRepresent>> List(StoreRepresentFilter StoreRepresentFilter);
        Task<List<StoreRepresent>> List(List<long> Ids);
        Task<StoreRepresent> Get(long Id);
        Task<bool> Create(StoreRepresent StoreRepresent);
        Task<bool> Update(StoreRepresent StoreRepresent);
        Task<bool> Delete(StoreRepresent StoreRepresent);
        Task<bool> BulkMerge(List<StoreRepresent> StoreRepresents);
        Task<bool> BulkDelete(List<StoreRepresent> StoreRepresents);
    }
    public class StoreRepresentRepository : IStoreRepresentRepository
    {
        private DataContext DataContext;
        public StoreRepresentRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<StoreRepresentDAO> DynamicFilter(IQueryable<StoreRepresentDAO> query, StoreRepresentFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.DateOfBirth != null && filter.DateOfBirth.HasValue)
                query = query.Where(q => q.DateOfBirth == null).Union(query.Where(q => q.DateOfBirth.HasValue).Where(q => q.DateOfBirth, filter.DateOfBirth));
            if (filter.Phone != null && filter.Phone.HasValue)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.PositionId != null && filter.PositionId.HasValue)
                query = query.Where(q => q.PositionId.HasValue).Where(q => q.PositionId, filter.PositionId);
            if (filter.StoreId != null && filter.StoreId.HasValue)
                query = query.Where(q => q.StoreId, filter.StoreId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<StoreRepresentDAO> OrFilter(IQueryable<StoreRepresentDAO> query, StoreRepresentFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<StoreRepresentDAO> initQuery = query.Where(q => false);
            foreach (StoreRepresentFilter StoreRepresentFilter in filter.OrFilter)
            {
                IQueryable<StoreRepresentDAO> queryable = query;
                if (StoreRepresentFilter.Id != null && StoreRepresentFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, StoreRepresentFilter.Id);
                if (StoreRepresentFilter.Name != null && StoreRepresentFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, StoreRepresentFilter.Name);
                if (StoreRepresentFilter.DateOfBirth != null && StoreRepresentFilter.DateOfBirth.HasValue)
                    queryable = queryable.Where(q => q.DateOfBirth.HasValue).Where(q => q.DateOfBirth, StoreRepresentFilter.DateOfBirth);
                if (StoreRepresentFilter.Phone != null && StoreRepresentFilter.Phone.HasValue)
                    queryable = queryable.Where(q => q.Phone, StoreRepresentFilter.Phone);
                if (StoreRepresentFilter.PositionId != null && StoreRepresentFilter.PositionId.HasValue)
                    queryable = queryable.Where(q => q.PositionId.HasValue).Where(q => q.PositionId, StoreRepresentFilter.PositionId);
                if (StoreRepresentFilter.StoreId != null && StoreRepresentFilter.StoreId.HasValue)
                    queryable = queryable.Where(q => q.StoreId, StoreRepresentFilter.StoreId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<StoreRepresentDAO> DynamicOrder(IQueryable<StoreRepresentDAO> query, StoreRepresentFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case StoreRepresentOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case StoreRepresentOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case StoreRepresentOrder.DateOfBirth:
                            query = query.OrderBy(q => q.DateOfBirth);
                            break;
                        case StoreRepresentOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case StoreRepresentOrder.Position:
                            query = query.OrderBy(q => q.PositionId);
                            break;
                        case StoreRepresentOrder.Store:
                            query = query.OrderBy(q => q.StoreId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case StoreRepresentOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case StoreRepresentOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case StoreRepresentOrder.DateOfBirth:
                            query = query.OrderByDescending(q => q.DateOfBirth);
                            break;
                        case StoreRepresentOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case StoreRepresentOrder.Position:
                            query = query.OrderByDescending(q => q.PositionId);
                            break;
                        case StoreRepresentOrder.Store:
                            query = query.OrderByDescending(q => q.StoreId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<StoreRepresent>> DynamicSelect(IQueryable<StoreRepresentDAO> query, StoreRepresentFilter filter)
        {
            List<StoreRepresent> StoreRepresents = await query.Select(q => new StoreRepresent()
            {
                Id = filter.Selects.Contains(StoreRepresentSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(StoreRepresentSelect.Name) ? q.Name : default(string),
                DateOfBirth = filter.Selects.Contains(StoreRepresentSelect.DateOfBirth) ? q.DateOfBirth : default(DateTime?),
                Phone = filter.Selects.Contains(StoreRepresentSelect.Phone) ? q.Phone : default(string),
                PositionId = filter.Selects.Contains(StoreRepresentSelect.Position) ? q.PositionId : default(long?),
                StoreId = filter.Selects.Contains(StoreRepresentSelect.Store) ? q.StoreId : default(long),
                Position = filter.Selects.Contains(StoreRepresentSelect.Position) && q.Position != null ? new Position
                {
                    Id = q.Position.Id,
                    Code = q.Position.Code,
                    Name = q.Position.Name,
                    StatusId = q.Position.StatusId,
                    RowId = q.Position.RowId,
                    Used = q.Position.Used,
                } : null,
                Store = filter.Selects.Contains(StoreRepresentSelect.Store) && q.Store != null ? new Store
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
            return StoreRepresents;
        }

        public async Task<int> Count(StoreRepresentFilter filter)
        {
            IQueryable<StoreRepresentDAO> StoreRepresents = DataContext.StoreRepresent.AsNoTracking();
            StoreRepresents = DynamicFilter(StoreRepresents, filter);
            return await StoreRepresents.CountAsync();
        }

        public async Task<List<StoreRepresent>> List(StoreRepresentFilter filter)
        {
            if (filter == null) return new List<StoreRepresent>();
            IQueryable<StoreRepresentDAO> StoreRepresentDAOs = DataContext.StoreRepresent.AsNoTracking();
            StoreRepresentDAOs = DynamicFilter(StoreRepresentDAOs, filter);
            StoreRepresentDAOs = DynamicOrder(StoreRepresentDAOs, filter);
            List<StoreRepresent> StoreRepresents = await DynamicSelect(StoreRepresentDAOs, filter);
            return StoreRepresents;
        }

        public async Task<List<StoreRepresent>> List(List<long> Ids)
        {
            List<StoreRepresent> StoreRepresents = await DataContext.StoreRepresent.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new StoreRepresent()
            {
                Id = x.Id,
                Name = x.Name,
                DateOfBirth = x.DateOfBirth,
                Phone = x.Phone,
                Email = x.Email,
                PositionId = x.PositionId,
                StoreId = x.StoreId,
                Position = x.Position == null ? null : new Position
                {
                    Id = x.Position.Id,
                    Code = x.Position.Code,
                    Name = x.Position.Name,
                    StatusId = x.Position.StatusId,
                    RowId = x.Position.RowId,
                    Used = x.Position.Used,
                },
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
            

            return StoreRepresents;
        }

        public async Task<StoreRepresent> Get(long Id)
        {
            StoreRepresent StoreRepresent = await DataContext.StoreRepresent.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new StoreRepresent()
            {
                Id = x.Id,
                Name = x.Name,
                DateOfBirth = x.DateOfBirth,
                Phone = x.Phone,
                Email = x.Email,
                PositionId = x.PositionId,
                StoreId = x.StoreId,
                Position = x.Position == null ? null : new Position
                {
                    Id = x.Position.Id,
                    Code = x.Position.Code,
                    Name = x.Position.Name,
                    StatusId = x.Position.StatusId,
                    RowId = x.Position.RowId,
                    Used = x.Position.Used,
                },
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

            if (StoreRepresent == null)
                return null;

            return StoreRepresent;
        }
        public async Task<bool> Create(StoreRepresent StoreRepresent)
        {
            StoreRepresentDAO StoreRepresentDAO = new StoreRepresentDAO();
            StoreRepresentDAO.Id = StoreRepresent.Id;
            StoreRepresentDAO.Name = StoreRepresent.Name;
            StoreRepresentDAO.DateOfBirth = StoreRepresent.DateOfBirth;
            StoreRepresentDAO.Phone = StoreRepresent.Phone;
            StoreRepresentDAO.Email = StoreRepresent.Email;
            StoreRepresentDAO.PositionId = StoreRepresent.PositionId;
            StoreRepresentDAO.StoreId = StoreRepresent.StoreId;
            DataContext.StoreRepresent.Add(StoreRepresentDAO);
            await DataContext.SaveChangesAsync();
            StoreRepresent.Id = StoreRepresentDAO.Id;
            await SaveReference(StoreRepresent);
            return true;
        }

        public async Task<bool> Update(StoreRepresent StoreRepresent)
        {
            StoreRepresentDAO StoreRepresentDAO = DataContext.StoreRepresent.Where(x => x.Id == StoreRepresent.Id).FirstOrDefault();
            if (StoreRepresentDAO == null)
                return false;
            StoreRepresentDAO.Id = StoreRepresent.Id;
            StoreRepresentDAO.Name = StoreRepresent.Name;
            StoreRepresentDAO.DateOfBirth = StoreRepresent.DateOfBirth;
            StoreRepresentDAO.Phone = StoreRepresent.Phone;
            StoreRepresentDAO.Email = StoreRepresent.Email;
            StoreRepresentDAO.PositionId = StoreRepresent.PositionId;
            StoreRepresentDAO.StoreId = StoreRepresent.StoreId;
            await DataContext.SaveChangesAsync();
            await SaveReference(StoreRepresent);
            return true;
        }

        public async Task<bool> Delete(StoreRepresent StoreRepresent)
        {
            await DataContext.StoreRepresent.Where(x => x.Id == StoreRepresent.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<StoreRepresent> StoreRepresents)
        {
            List<StoreRepresentDAO> StoreRepresentDAOs = new List<StoreRepresentDAO>();
            foreach (StoreRepresent StoreRepresent in StoreRepresents)
            {
                StoreRepresentDAO StoreRepresentDAO = new StoreRepresentDAO();
                StoreRepresentDAO.Id = StoreRepresent.Id;
                StoreRepresentDAO.Name = StoreRepresent.Name;
                StoreRepresentDAO.DateOfBirth = StoreRepresent.DateOfBirth;
                StoreRepresentDAO.Phone = StoreRepresent.Phone;
                StoreRepresentDAO.Email = StoreRepresent.Email;
                StoreRepresentDAO.PositionId = StoreRepresent.PositionId;
                StoreRepresentDAO.StoreId = StoreRepresent.StoreId;
                StoreRepresentDAOs.Add(StoreRepresentDAO);
            }
            await DataContext.BulkMergeAsync(StoreRepresentDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<StoreRepresent> StoreRepresents)
        {
            List<long> Ids = StoreRepresents.Select(x => x.Id).ToList();
            await DataContext.StoreRepresent
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(StoreRepresent StoreRepresent)
        {
        }
        
    }
}
