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
    public interface IWarehouseRepository
    {
        Task<int> Count(WarehouseFilter WarehouseFilter);
        Task<List<Warehouse>> List(WarehouseFilter WarehouseFilter);
        Task<List<Warehouse>> List(List<long> Ids);
        Task<Warehouse> Get(long Id);
        Task<bool> Create(Warehouse Warehouse);
        Task<bool> Update(Warehouse Warehouse);
        Task<bool> Delete(Warehouse Warehouse);
        Task<bool> BulkMerge(List<Warehouse> Warehouses);
        Task<bool> BulkDelete(List<Warehouse> Warehouses);
    }
    public class WarehouseRepository : IWarehouseRepository
    {
        private DataContext DataContext;
        public WarehouseRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<WarehouseDAO> DynamicFilter(IQueryable<WarehouseDAO> query, WarehouseFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null && filter.Code.HasValue)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Address != null && filter.Address.HasValue)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.OrganizationId != null && filter.OrganizationId.HasValue)
                query = query.Where(q => q.OrganizationId, filter.OrganizationId);
            if (filter.ProvinceId != null && filter.ProvinceId.HasValue)
                query = query.Where(q => q.ProvinceId.HasValue).Where(q => q.ProvinceId, filter.ProvinceId);
            if (filter.DistrictId != null && filter.DistrictId.HasValue)
                query = query.Where(q => q.DistrictId.HasValue).Where(q => q.DistrictId, filter.DistrictId);
            if (filter.WardId != null && filter.WardId.HasValue)
                query = query.Where(q => q.WardId.HasValue).Where(q => q.WardId, filter.WardId);
            if (filter.StatusId != null && filter.StatusId.HasValue)
                query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<WarehouseDAO> OrFilter(IQueryable<WarehouseDAO> query, WarehouseFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<WarehouseDAO> initQuery = query.Where(q => false);
            foreach (WarehouseFilter WarehouseFilter in filter.OrFilter)
            {
                IQueryable<WarehouseDAO> queryable = query;
                if (WarehouseFilter.Id != null && WarehouseFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, WarehouseFilter.Id);
                if (WarehouseFilter.Code != null && WarehouseFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, WarehouseFilter.Code);
                if (WarehouseFilter.Name != null && WarehouseFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, WarehouseFilter.Name);
                if (WarehouseFilter.Address != null && WarehouseFilter.Address.HasValue)
                    queryable = queryable.Where(q => q.Address, WarehouseFilter.Address);
                if (WarehouseFilter.OrganizationId != null && WarehouseFilter.OrganizationId.HasValue)
                    queryable = queryable.Where(q => q.OrganizationId, WarehouseFilter.OrganizationId);
                if (WarehouseFilter.ProvinceId != null && WarehouseFilter.ProvinceId.HasValue)
                    queryable = queryable.Where(q => q.ProvinceId.HasValue).Where(q => q.ProvinceId, WarehouseFilter.ProvinceId);
                if (WarehouseFilter.DistrictId != null && WarehouseFilter.DistrictId.HasValue)
                    queryable = queryable.Where(q => q.DistrictId.HasValue).Where(q => q.DistrictId, WarehouseFilter.DistrictId);
                if (WarehouseFilter.WardId != null && WarehouseFilter.WardId.HasValue)
                    queryable = queryable.Where(q => q.WardId.HasValue).Where(q => q.WardId, WarehouseFilter.WardId);
                if (WarehouseFilter.StatusId != null && WarehouseFilter.StatusId.HasValue)
                    queryable = queryable.Where(q => q.StatusId, WarehouseFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<WarehouseDAO> DynamicOrder(IQueryable<WarehouseDAO> query, WarehouseFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case WarehouseOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case WarehouseOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case WarehouseOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case WarehouseOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case WarehouseOrder.Organization:
                            query = query.OrderBy(q => q.OrganizationId);
                            break;
                        case WarehouseOrder.Province:
                            query = query.OrderBy(q => q.ProvinceId);
                            break;
                        case WarehouseOrder.District:
                            query = query.OrderBy(q => q.DistrictId);
                            break;
                        case WarehouseOrder.Ward:
                            query = query.OrderBy(q => q.WardId);
                            break;
                        case WarehouseOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case WarehouseOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case WarehouseOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case WarehouseOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case WarehouseOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case WarehouseOrder.Organization:
                            query = query.OrderByDescending(q => q.OrganizationId);
                            break;
                        case WarehouseOrder.Province:
                            query = query.OrderByDescending(q => q.ProvinceId);
                            break;
                        case WarehouseOrder.District:
                            query = query.OrderByDescending(q => q.DistrictId);
                            break;
                        case WarehouseOrder.Ward:
                            query = query.OrderByDescending(q => q.WardId);
                            break;
                        case WarehouseOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Warehouse>> DynamicSelect(IQueryable<WarehouseDAO> query, WarehouseFilter filter)
        {
            List<Warehouse> Warehouses = await query.Select(q => new Warehouse()
            {
                Id = filter.Selects.Contains(WarehouseSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(WarehouseSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(WarehouseSelect.Name) ? q.Name : default(string),
                Address = filter.Selects.Contains(WarehouseSelect.Address) ? q.Address : default(string),
                OrganizationId = filter.Selects.Contains(WarehouseSelect.Organization) ? q.OrganizationId : default(long),
                ProvinceId = filter.Selects.Contains(WarehouseSelect.Province) ? q.ProvinceId : default(long?),
                DistrictId = filter.Selects.Contains(WarehouseSelect.District) ? q.DistrictId : default(long?),
                WardId = filter.Selects.Contains(WarehouseSelect.Ward) ? q.WardId : default(long?),
                StatusId = filter.Selects.Contains(WarehouseSelect.Status) ? q.StatusId : default(long),
                District = filter.Selects.Contains(WarehouseSelect.District) && q.District != null ? new District
                {
                    Id = q.District.Id,
                    Code = q.District.Code,
                    Name = q.District.Name,
                    Priority = q.District.Priority,
                    ProvinceId = q.District.ProvinceId,
                    StatusId = q.District.StatusId,
                    RowId = q.District.RowId,
                    Used = q.District.Used,
                } : null,
                Organization = filter.Selects.Contains(WarehouseSelect.Organization) && q.Organization != null ? new Organization
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
                    RowId = q.Organization.RowId,
                    Used = q.Organization.Used,
                } : null,
                Province = filter.Selects.Contains(WarehouseSelect.Province) && q.Province != null ? new Province
                {
                    Id = q.Province.Id,
                    Code = q.Province.Code,
                    Name = q.Province.Name,
                    Priority = q.Province.Priority,
                    StatusId = q.Province.StatusId,
                    RowId = q.Province.RowId,
                    Used = q.Province.Used,
                } : null,
                Status = filter.Selects.Contains(WarehouseSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                Ward = filter.Selects.Contains(WarehouseSelect.Ward) && q.Ward != null ? new Ward
                {
                    Id = q.Ward.Id,
                    Code = q.Ward.Code,
                    Name = q.Ward.Name,
                    Priority = q.Ward.Priority,
                    DistrictId = q.Ward.DistrictId,
                    StatusId = q.Ward.StatusId,
                    RowId = q.Ward.RowId,
                    Used = q.Ward.Used,
                } : null,
            }).ToListAsync();
            return Warehouses;
        }

        public async Task<int> Count(WarehouseFilter filter)
        {
            IQueryable<WarehouseDAO> Warehouses = DataContext.Warehouse.AsNoTracking();
            Warehouses = DynamicFilter(Warehouses, filter);
            return await Warehouses.CountAsync();
        }

        public async Task<List<Warehouse>> List(WarehouseFilter filter)
        {
            if (filter == null) return new List<Warehouse>();
            IQueryable<WarehouseDAO> WarehouseDAOs = DataContext.Warehouse.AsNoTracking();
            WarehouseDAOs = DynamicFilter(WarehouseDAOs, filter);
            WarehouseDAOs = DynamicOrder(WarehouseDAOs, filter);
            List<Warehouse> Warehouses = await DynamicSelect(WarehouseDAOs, filter);
            return Warehouses;
        }

        public async Task<List<Warehouse>> List(List<long> Ids)
        {
            List<Warehouse> Warehouses = await DataContext.Warehouse.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new Warehouse()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Address = x.Address,
                OrganizationId = x.OrganizationId,
                ProvinceId = x.ProvinceId,
                DistrictId = x.DistrictId,
                WardId = x.WardId,
                StatusId = x.StatusId,
                RowId = x.RowId,
                District = x.District == null ? null : new District
                {
                    Id = x.District.Id,
                    Code = x.District.Code,
                    Name = x.District.Name,
                    Priority = x.District.Priority,
                    ProvinceId = x.District.ProvinceId,
                    StatusId = x.District.StatusId,
                    RowId = x.District.RowId,
                    Used = x.District.Used,
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
                    RowId = x.Organization.RowId,
                    Used = x.Organization.Used,
                },
                Province = x.Province == null ? null : new Province
                {
                    Id = x.Province.Id,
                    Code = x.Province.Code,
                    Name = x.Province.Name,
                    Priority = x.Province.Priority,
                    StatusId = x.Province.StatusId,
                    RowId = x.Province.RowId,
                    Used = x.Province.Used,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
                Ward = x.Ward == null ? null : new Ward
                {
                    Id = x.Ward.Id,
                    Code = x.Ward.Code,
                    Name = x.Ward.Name,
                    Priority = x.Ward.Priority,
                    DistrictId = x.Ward.DistrictId,
                    StatusId = x.Ward.StatusId,
                    RowId = x.Ward.RowId,
                    Used = x.Ward.Used,
                },
            }).ToListAsync();
            

            return Warehouses;
        }

        public async Task<Warehouse> Get(long Id)
        {
            Warehouse Warehouse = await DataContext.Warehouse.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new Warehouse()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Address = x.Address,
                OrganizationId = x.OrganizationId,
                ProvinceId = x.ProvinceId,
                DistrictId = x.DistrictId,
                WardId = x.WardId,
                StatusId = x.StatusId,
                RowId = x.RowId,
                District = x.District == null ? null : new District
                {
                    Id = x.District.Id,
                    Code = x.District.Code,
                    Name = x.District.Name,
                    Priority = x.District.Priority,
                    ProvinceId = x.District.ProvinceId,
                    StatusId = x.District.StatusId,
                    RowId = x.District.RowId,
                    Used = x.District.Used,
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
                    RowId = x.Organization.RowId,
                    Used = x.Organization.Used,
                },
                Province = x.Province == null ? null : new Province
                {
                    Id = x.Province.Id,
                    Code = x.Province.Code,
                    Name = x.Province.Name,
                    Priority = x.Province.Priority,
                    StatusId = x.Province.StatusId,
                    RowId = x.Province.RowId,
                    Used = x.Province.Used,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
                Ward = x.Ward == null ? null : new Ward
                {
                    Id = x.Ward.Id,
                    Code = x.Ward.Code,
                    Name = x.Ward.Name,
                    Priority = x.Ward.Priority,
                    DistrictId = x.Ward.DistrictId,
                    StatusId = x.Ward.StatusId,
                    RowId = x.Ward.RowId,
                    Used = x.Ward.Used,
                },
            }).FirstOrDefaultAsync();

            if (Warehouse == null)
                return null;

            return Warehouse;
        }
        public async Task<bool> Create(Warehouse Warehouse)
        {
            WarehouseDAO WarehouseDAO = new WarehouseDAO();
            WarehouseDAO.Id = Warehouse.Id;
            WarehouseDAO.Code = Warehouse.Code;
            WarehouseDAO.Name = Warehouse.Name;
            WarehouseDAO.Address = Warehouse.Address;
            WarehouseDAO.OrganizationId = Warehouse.OrganizationId;
            WarehouseDAO.ProvinceId = Warehouse.ProvinceId;
            WarehouseDAO.DistrictId = Warehouse.DistrictId;
            WarehouseDAO.WardId = Warehouse.WardId;
            WarehouseDAO.StatusId = Warehouse.StatusId;
            WarehouseDAO.RowId = Warehouse.RowId;
            WarehouseDAO.CreatedAt = StaticParams.DateTimeNow;
            WarehouseDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.Warehouse.Add(WarehouseDAO);
            await DataContext.SaveChangesAsync();
            Warehouse.Id = WarehouseDAO.Id;
            await SaveReference(Warehouse);
            return true;
        }

        public async Task<bool> Update(Warehouse Warehouse)
        {
            WarehouseDAO WarehouseDAO = DataContext.Warehouse.Where(x => x.Id == Warehouse.Id).FirstOrDefault();
            if (WarehouseDAO == null)
                return false;
            WarehouseDAO.Id = Warehouse.Id;
            WarehouseDAO.Code = Warehouse.Code;
            WarehouseDAO.Name = Warehouse.Name;
            WarehouseDAO.Address = Warehouse.Address;
            WarehouseDAO.OrganizationId = Warehouse.OrganizationId;
            WarehouseDAO.ProvinceId = Warehouse.ProvinceId;
            WarehouseDAO.DistrictId = Warehouse.DistrictId;
            WarehouseDAO.WardId = Warehouse.WardId;
            WarehouseDAO.StatusId = Warehouse.StatusId;
            WarehouseDAO.RowId = Warehouse.RowId;
            WarehouseDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(Warehouse);
            return true;
        }

        public async Task<bool> Delete(Warehouse Warehouse)
        {
            await DataContext.Warehouse.Where(x => x.Id == Warehouse.Id).UpdateFromQueryAsync(x => new WarehouseDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<Warehouse> Warehouses)
        {
            List<WarehouseDAO> WarehouseDAOs = new List<WarehouseDAO>();
            foreach (Warehouse Warehouse in Warehouses)
            {
                WarehouseDAO WarehouseDAO = new WarehouseDAO();
                WarehouseDAO.Id = Warehouse.Id;
                WarehouseDAO.Code = Warehouse.Code;
                WarehouseDAO.Name = Warehouse.Name;
                WarehouseDAO.Address = Warehouse.Address;
                WarehouseDAO.OrganizationId = Warehouse.OrganizationId;
                WarehouseDAO.ProvinceId = Warehouse.ProvinceId;
                WarehouseDAO.DistrictId = Warehouse.DistrictId;
                WarehouseDAO.WardId = Warehouse.WardId;
                WarehouseDAO.StatusId = Warehouse.StatusId;
                WarehouseDAO.RowId = Warehouse.RowId;
                WarehouseDAO.CreatedAt = StaticParams.DateTimeNow;
                WarehouseDAO.UpdatedAt = StaticParams.DateTimeNow;
                WarehouseDAOs.Add(WarehouseDAO);
            }
            await DataContext.BulkMergeAsync(WarehouseDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<Warehouse> Warehouses)
        {
            List<long> Ids = Warehouses.Select(x => x.Id).ToList();
            await DataContext.Warehouse
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new WarehouseDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(Warehouse Warehouse)
        {
        }
        
    }
}
