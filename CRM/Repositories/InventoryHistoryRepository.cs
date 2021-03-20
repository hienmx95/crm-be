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
    public interface IInventoryHistoryRepository
    {
        Task<int> Count(InventoryHistoryFilter InventoryHistoryFilter);
        Task<List<InventoryHistory>> List(InventoryHistoryFilter InventoryHistoryFilter);
        Task<List<InventoryHistory>> List(List<long> Ids);
        Task<InventoryHistory> Get(long Id);
        Task<bool> Create(InventoryHistory InventoryHistory);
        Task<bool> Update(InventoryHistory InventoryHistory);
        Task<bool> Delete(InventoryHistory InventoryHistory);
        Task<bool> BulkMerge(List<InventoryHistory> InventoryHistories);
        Task<bool> BulkDelete(List<InventoryHistory> InventoryHistories);
    }
    public class InventoryHistoryRepository : IInventoryHistoryRepository
    {
        private DataContext DataContext;
        public InventoryHistoryRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<InventoryHistoryDAO> DynamicFilter(IQueryable<InventoryHistoryDAO> query, InventoryHistoryFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.InventoryId != null && filter.InventoryId.HasValue)
                query = query.Where(q => q.InventoryId, filter.InventoryId);
            if (filter.OldSaleStock != null && filter.OldSaleStock.HasValue)
                query = query.Where(q => q.OldSaleStock, filter.OldSaleStock);
            if (filter.SaleStock != null && filter.SaleStock.HasValue)
                query = query.Where(q => q.SaleStock, filter.SaleStock);
            if (filter.OldAccountingStock != null && filter.OldAccountingStock.HasValue)
                query = query.Where(q => q.OldAccountingStock, filter.OldAccountingStock);
            if (filter.AccountingStock != null && filter.AccountingStock.HasValue)
                query = query.Where(q => q.AccountingStock, filter.AccountingStock);
            if (filter.AppUserId != null && filter.AppUserId.HasValue)
                query = query.Where(q => q.AppUserId, filter.AppUserId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<InventoryHistoryDAO> OrFilter(IQueryable<InventoryHistoryDAO> query, InventoryHistoryFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<InventoryHistoryDAO> initQuery = query.Where(q => false);
            foreach (InventoryHistoryFilter InventoryHistoryFilter in filter.OrFilter)
            {
                IQueryable<InventoryHistoryDAO> queryable = query;
                if (InventoryHistoryFilter.Id != null && InventoryHistoryFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, InventoryHistoryFilter.Id);
                if (InventoryHistoryFilter.InventoryId != null && InventoryHistoryFilter.InventoryId.HasValue)
                    queryable = queryable.Where(q => q.InventoryId, InventoryHistoryFilter.InventoryId);
                if (InventoryHistoryFilter.OldSaleStock != null && InventoryHistoryFilter.OldSaleStock.HasValue)
                    queryable = queryable.Where(q => q.OldSaleStock, InventoryHistoryFilter.OldSaleStock);
                if (InventoryHistoryFilter.SaleStock != null && InventoryHistoryFilter.SaleStock.HasValue)
                    queryable = queryable.Where(q => q.SaleStock, InventoryHistoryFilter.SaleStock);
                if (InventoryHistoryFilter.OldAccountingStock != null && InventoryHistoryFilter.OldAccountingStock.HasValue)
                    queryable = queryable.Where(q => q.OldAccountingStock, InventoryHistoryFilter.OldAccountingStock);
                if (InventoryHistoryFilter.AccountingStock != null && InventoryHistoryFilter.AccountingStock.HasValue)
                    queryable = queryable.Where(q => q.AccountingStock, InventoryHistoryFilter.AccountingStock);
                if (InventoryHistoryFilter.AppUserId != null && InventoryHistoryFilter.AppUserId.HasValue)
                    queryable = queryable.Where(q => q.AppUserId, InventoryHistoryFilter.AppUserId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<InventoryHistoryDAO> DynamicOrder(IQueryable<InventoryHistoryDAO> query, InventoryHistoryFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case InventoryHistoryOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case InventoryHistoryOrder.Inventory:
                            query = query.OrderBy(q => q.InventoryId);
                            break;
                        case InventoryHistoryOrder.OldSaleStock:
                            query = query.OrderBy(q => q.OldSaleStock);
                            break;
                        case InventoryHistoryOrder.SaleStock:
                            query = query.OrderBy(q => q.SaleStock);
                            break;
                        case InventoryHistoryOrder.OldAccountingStock:
                            query = query.OrderBy(q => q.OldAccountingStock);
                            break;
                        case InventoryHistoryOrder.AccountingStock:
                            query = query.OrderBy(q => q.AccountingStock);
                            break;
                        case InventoryHistoryOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case InventoryHistoryOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case InventoryHistoryOrder.Inventory:
                            query = query.OrderByDescending(q => q.InventoryId);
                            break;
                        case InventoryHistoryOrder.OldSaleStock:
                            query = query.OrderByDescending(q => q.OldSaleStock);
                            break;
                        case InventoryHistoryOrder.SaleStock:
                            query = query.OrderByDescending(q => q.SaleStock);
                            break;
                        case InventoryHistoryOrder.OldAccountingStock:
                            query = query.OrderByDescending(q => q.OldAccountingStock);
                            break;
                        case InventoryHistoryOrder.AccountingStock:
                            query = query.OrderByDescending(q => q.AccountingStock);
                            break;
                        case InventoryHistoryOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<InventoryHistory>> DynamicSelect(IQueryable<InventoryHistoryDAO> query, InventoryHistoryFilter filter)
        {
            List<InventoryHistory> InventoryHistories = await query.Select(q => new InventoryHistory()
            {
                Id = filter.Selects.Contains(InventoryHistorySelect.Id) ? q.Id : default(long),
                InventoryId = filter.Selects.Contains(InventoryHistorySelect.Inventory) ? q.InventoryId : default(long),
                OldSaleStock = filter.Selects.Contains(InventoryHistorySelect.OldSaleStock) ? q.OldSaleStock : default(long),
                SaleStock = filter.Selects.Contains(InventoryHistorySelect.SaleStock) ? q.SaleStock : default(long),
                OldAccountingStock = filter.Selects.Contains(InventoryHistorySelect.OldAccountingStock) ? q.OldAccountingStock : default(long),
                AccountingStock = filter.Selects.Contains(InventoryHistorySelect.AccountingStock) ? q.AccountingStock : default(long),
                AppUserId = filter.Selects.Contains(InventoryHistorySelect.AppUser) ? q.AppUserId : default(long),
                AppUser = filter.Selects.Contains(InventoryHistorySelect.AppUser) && q.AppUser != null ? new AppUser
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
                    RowId = q.AppUser.RowId,
                    Used = q.AppUser.Used,
                } : null,
                Inventory = filter.Selects.Contains(InventoryHistorySelect.Inventory) && q.Inventory != null ? new Inventory
                {
                    Id = q.Inventory.Id,
                    WarehouseId = q.Inventory.WarehouseId,
                    ItemId = q.Inventory.ItemId,
                    SaleStock = q.Inventory.SaleStock,
                    AccountingStock = q.Inventory.AccountingStock,
                } : null,
            }).ToListAsync();
            return InventoryHistories;
        }

        public async Task<int> Count(InventoryHistoryFilter filter)
        {
            IQueryable<InventoryHistoryDAO> InventoryHistories = DataContext.InventoryHistory.AsNoTracking();
            InventoryHistories = DynamicFilter(InventoryHistories, filter);
            return await InventoryHistories.CountAsync();
        }

        public async Task<List<InventoryHistory>> List(InventoryHistoryFilter filter)
        {
            if (filter == null) return new List<InventoryHistory>();
            IQueryable<InventoryHistoryDAO> InventoryHistoryDAOs = DataContext.InventoryHistory.AsNoTracking();
            InventoryHistoryDAOs = DynamicFilter(InventoryHistoryDAOs, filter);
            InventoryHistoryDAOs = DynamicOrder(InventoryHistoryDAOs, filter);
            List<InventoryHistory> InventoryHistories = await DynamicSelect(InventoryHistoryDAOs, filter);
            return InventoryHistories;
        }

        public async Task<List<InventoryHistory>> List(List<long> Ids)
        {
            List<InventoryHistory> InventoryHistories = await DataContext.InventoryHistory.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new InventoryHistory()
            {
                Id = x.Id,
                InventoryId = x.InventoryId,
                OldSaleStock = x.OldSaleStock,
                SaleStock = x.SaleStock,
                OldAccountingStock = x.OldAccountingStock,
                AccountingStock = x.AccountingStock,
                AppUserId = x.AppUserId,
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
                    RowId = x.AppUser.RowId,
                    Used = x.AppUser.Used,
                },
                Inventory = x.Inventory == null ? null : new Inventory
                {
                    Id = x.Inventory.Id,
                    WarehouseId = x.Inventory.WarehouseId,
                    ItemId = x.Inventory.ItemId,
                    SaleStock = x.Inventory.SaleStock,
                    AccountingStock = x.Inventory.AccountingStock,
                },
            }).ToListAsync();
            

            return InventoryHistories;
        }

        public async Task<InventoryHistory> Get(long Id)
        {
            InventoryHistory InventoryHistory = await DataContext.InventoryHistory.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new InventoryHistory()
            {
                Id = x.Id,
                InventoryId = x.InventoryId,
                OldSaleStock = x.OldSaleStock,
                SaleStock = x.SaleStock,
                OldAccountingStock = x.OldAccountingStock,
                AccountingStock = x.AccountingStock,
                AppUserId = x.AppUserId,
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
                    RowId = x.AppUser.RowId,
                    Used = x.AppUser.Used,
                },
                Inventory = x.Inventory == null ? null : new Inventory
                {
                    Id = x.Inventory.Id,
                    WarehouseId = x.Inventory.WarehouseId,
                    ItemId = x.Inventory.ItemId,
                    SaleStock = x.Inventory.SaleStock,
                    AccountingStock = x.Inventory.AccountingStock,
                },
            }).FirstOrDefaultAsync();

            if (InventoryHistory == null)
                return null;

            return InventoryHistory;
        }
        public async Task<bool> Create(InventoryHistory InventoryHistory)
        {
            InventoryHistoryDAO InventoryHistoryDAO = new InventoryHistoryDAO();
            InventoryHistoryDAO.Id = InventoryHistory.Id;
            InventoryHistoryDAO.InventoryId = InventoryHistory.InventoryId;
            InventoryHistoryDAO.OldSaleStock = InventoryHistory.OldSaleStock;
            InventoryHistoryDAO.SaleStock = InventoryHistory.SaleStock;
            InventoryHistoryDAO.OldAccountingStock = InventoryHistory.OldAccountingStock;
            InventoryHistoryDAO.AccountingStock = InventoryHistory.AccountingStock;
            InventoryHistoryDAO.AppUserId = InventoryHistory.AppUserId;
            InventoryHistoryDAO.CreatedAt = StaticParams.DateTimeNow;
            InventoryHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.InventoryHistory.Add(InventoryHistoryDAO);
            await DataContext.SaveChangesAsync();
            InventoryHistory.Id = InventoryHistoryDAO.Id;
            await SaveReference(InventoryHistory);
            return true;
        }

        public async Task<bool> Update(InventoryHistory InventoryHistory)
        {
            InventoryHistoryDAO InventoryHistoryDAO = DataContext.InventoryHistory.Where(x => x.Id == InventoryHistory.Id).FirstOrDefault();
            if (InventoryHistoryDAO == null)
                return false;
            InventoryHistoryDAO.Id = InventoryHistory.Id;
            InventoryHistoryDAO.InventoryId = InventoryHistory.InventoryId;
            InventoryHistoryDAO.OldSaleStock = InventoryHistory.OldSaleStock;
            InventoryHistoryDAO.SaleStock = InventoryHistory.SaleStock;
            InventoryHistoryDAO.OldAccountingStock = InventoryHistory.OldAccountingStock;
            InventoryHistoryDAO.AccountingStock = InventoryHistory.AccountingStock;
            InventoryHistoryDAO.AppUserId = InventoryHistory.AppUserId;
            InventoryHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(InventoryHistory);
            return true;
        }

        public async Task<bool> Delete(InventoryHistory InventoryHistory)
        {
            await DataContext.InventoryHistory.Where(x => x.Id == InventoryHistory.Id).UpdateFromQueryAsync(x => new InventoryHistoryDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<InventoryHistory> InventoryHistories)
        {
            List<InventoryHistoryDAO> InventoryHistoryDAOs = new List<InventoryHistoryDAO>();
            foreach (InventoryHistory InventoryHistory in InventoryHistories)
            {
                InventoryHistoryDAO InventoryHistoryDAO = new InventoryHistoryDAO();
                InventoryHistoryDAO.Id = InventoryHistory.Id;
                InventoryHistoryDAO.InventoryId = InventoryHistory.InventoryId;
                InventoryHistoryDAO.OldSaleStock = InventoryHistory.OldSaleStock;
                InventoryHistoryDAO.SaleStock = InventoryHistory.SaleStock;
                InventoryHistoryDAO.OldAccountingStock = InventoryHistory.OldAccountingStock;
                InventoryHistoryDAO.AccountingStock = InventoryHistory.AccountingStock;
                InventoryHistoryDAO.AppUserId = InventoryHistory.AppUserId;
                InventoryHistoryDAO.CreatedAt = StaticParams.DateTimeNow;
                InventoryHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
                InventoryHistoryDAOs.Add(InventoryHistoryDAO);
            }
            await DataContext.BulkMergeAsync(InventoryHistoryDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<InventoryHistory> InventoryHistories)
        {
            List<long> Ids = InventoryHistories.Select(x => x.Id).ToList();
            await DataContext.InventoryHistory
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new InventoryHistoryDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(InventoryHistory InventoryHistory)
        {
        }
        
    }
}
