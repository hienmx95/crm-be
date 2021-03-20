using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;

namespace CRM.Repositories
{
    public interface IScheduleMasterRepository
    {
        Task<int> Count(ScheduleMasterFilter ScheduleMasterFilter);
        Task<List<ScheduleMaster>> List(ScheduleMasterFilter ScheduleMasterFilter);
        Task<ScheduleMaster> Get(long Id);
        Task<bool> Create(ScheduleMaster ScheduleMaster);
        Task<bool> Update(ScheduleMaster ScheduleMaster);
        Task<bool> Delete(ScheduleMaster ScheduleMaster);
        Task<bool> BulkMerge(List<ScheduleMaster> ScheduleMasters);
        Task<bool> BulkDelete(List<ScheduleMaster> ScheduleMasters);
    }
    public class ScheduleMasterRepository : IScheduleMasterRepository
    {
        private DataContext DataContext;
        public ScheduleMasterRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ScheduleMasterDAO> DynamicFilter(IQueryable<ScheduleMasterDAO> query, ScheduleMasterFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.ManagerId != null)
                query = query.Where(q => q.ManagerId, filter.ManagerId);
            if (filter.SalerId != null)
                query = query.Where(q => q.SalerId, filter.SalerId);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.RecurDays != null)
                query = query.Where(q => q.RecurDays, filter.RecurDays);
            if (filter.StartDate != null)
                query = query.Where(q => q.StartDate, filter.StartDate);
            if (filter.EndDate != null)
                query = query.Where(q => q.EndDate, filter.EndDate);
            if (filter.StartDayOfWeek != null)
                query = query.Where(q => q.StartDayOfWeek, filter.StartDayOfWeek);
            if (filter.DisplayOrder != null)
                query = query.Where(q => q.DisplayOrder, filter.DisplayOrder);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<ScheduleMasterDAO> OrFilter(IQueryable<ScheduleMasterDAO> query, ScheduleMasterFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ScheduleMasterDAO> initQuery = query.Where(q => false);
            foreach (ScheduleMasterFilter ScheduleMasterFilter in filter.OrFilter)
            {
                IQueryable<ScheduleMasterDAO> queryable = query;
                if (filter.Id != null)
                    queryable = queryable.Where(q => q.Id, filter.Id);
                if (filter.ManagerId != null)
                    queryable = queryable.Where(q => q.ManagerId, filter.ManagerId);
                if (filter.SalerId != null)
                    queryable = queryable.Where(q => q.SalerId, filter.SalerId);
                if (filter.Name != null)
                    queryable = queryable.Where(q => q.Name, filter.Name);
                if (filter.Code != null)
                    queryable = queryable.Where(q => q.Code, filter.Code);
                if (filter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, filter.StatusId);
                if (filter.RecurDays != null)
                    queryable = queryable.Where(q => q.RecurDays, filter.RecurDays);
                if (filter.StartDate != null)
                    queryable = queryable.Where(q => q.StartDate, filter.StartDate);
                if (filter.EndDate != null)
                    queryable = queryable.Where(q => q.EndDate, filter.EndDate);
                if (filter.StartDayOfWeek != null)
                    queryable = queryable.Where(q => q.StartDayOfWeek, filter.StartDayOfWeek);
                if (filter.DisplayOrder != null)
                    queryable = queryable.Where(q => q.DisplayOrder, filter.DisplayOrder);
                if (filter.Description != null)
                    queryable = queryable.Where(q => q.Description, filter.Description);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ScheduleMasterDAO> DynamicOrder(IQueryable<ScheduleMasterDAO> query, ScheduleMasterFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ScheduleMasterOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ScheduleMasterOrder.Manager:
                            query = query.OrderBy(q => q.ManagerId);
                            break;
                        case ScheduleMasterOrder.Saler:
                            query = query.OrderBy(q => q.SalerId);
                            break;
                        case ScheduleMasterOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ScheduleMasterOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ScheduleMasterOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case ScheduleMasterOrder.RecurDays:
                            query = query.OrderBy(q => q.RecurDays);
                            break;
                        case ScheduleMasterOrder.StartDate:
                            query = query.OrderBy(q => q.StartDate);
                            break;
                        case ScheduleMasterOrder.EndDate:
                            query = query.OrderBy(q => q.EndDate);
                            break;
                        case ScheduleMasterOrder.NoEndDate:
                            query = query.OrderBy(q => q.NoEndDate);
                            break;
                        case ScheduleMasterOrder.StartDayOfWeek:
                            query = query.OrderBy(q => q.StartDayOfWeek);
                            break;
                        case ScheduleMasterOrder.DisplayOrder:
                            query = query.OrderBy(q => q.DisplayOrder);
                            break;
                        case ScheduleMasterOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ScheduleMasterOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ScheduleMasterOrder.Manager:
                            query = query.OrderByDescending(q => q.ManagerId);
                            break;
                        case ScheduleMasterOrder.Saler:
                            query = query.OrderByDescending(q => q.SalerId);
                            break;
                        case ScheduleMasterOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ScheduleMasterOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ScheduleMasterOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case ScheduleMasterOrder.RecurDays:
                            query = query.OrderByDescending(q => q.RecurDays);
                            break;
                        case ScheduleMasterOrder.StartDate:
                            query = query.OrderByDescending(q => q.StartDate);
                            break;
                        case ScheduleMasterOrder.EndDate:
                            query = query.OrderByDescending(q => q.EndDate);
                            break;
                        case ScheduleMasterOrder.NoEndDate:
                            query = query.OrderByDescending(q => q.NoEndDate);
                            break;
                        case ScheduleMasterOrder.StartDayOfWeek:
                            query = query.OrderByDescending(q => q.StartDayOfWeek);
                            break;
                        case ScheduleMasterOrder.DisplayOrder:
                            query = query.OrderByDescending(q => q.DisplayOrder);
                            break;
                        case ScheduleMasterOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ScheduleMaster>> DynamicSelect(IQueryable<ScheduleMasterDAO> query, ScheduleMasterFilter filter)
        {
            List<ScheduleMaster> ScheduleMasters = await query.Select(q => new ScheduleMaster()
            {
                Id = filter.Selects.Contains(ScheduleMasterSelect.Id) ? q.Id : default(long),
                ManagerId = filter.Selects.Contains(ScheduleMasterSelect.Manager) ? q.ManagerId : default(long?),
                SalerId = filter.Selects.Contains(ScheduleMasterSelect.Saler) ? q.SalerId : default(long?),
                Name = filter.Selects.Contains(ScheduleMasterSelect.Name) ? q.Name : default(string),
                Code = filter.Selects.Contains(ScheduleMasterSelect.Code) ? q.Code : default(string),
                StatusId = filter.Selects.Contains(ScheduleMasterSelect.Status) ? q.StatusId : default(long?),
                RecurDays = filter.Selects.Contains(ScheduleMasterSelect.RecurDays) ? q.RecurDays : default(DateTime?),
                StartDate = filter.Selects.Contains(ScheduleMasterSelect.StartDate) ? q.StartDate : default(DateTime?),
                EndDate = filter.Selects.Contains(ScheduleMasterSelect.EndDate) ? q.EndDate : default(DateTime?),
                NoEndDate = filter.Selects.Contains(ScheduleMasterSelect.NoEndDate) ? q.NoEndDate : default(bool?),
                StartDayOfWeek = filter.Selects.Contains(ScheduleMasterSelect.StartDayOfWeek) ? q.StartDayOfWeek : default(DateTime?),
                DisplayOrder = filter.Selects.Contains(ScheduleMasterSelect.DisplayOrder) ? q.DisplayOrder : default(long?),
                Description = filter.Selects.Contains(ScheduleMasterSelect.Description) ? q.Description : default(string),
                Manager = filter.Selects.Contains(ScheduleMasterSelect.Manager) && q.Manager != null ? new AppUser
                {
                    Id = q.Manager.Id,
                    Username = q.Manager.Username,
                    DisplayName = q.Manager.DisplayName,
                    Address = q.Manager.Address,
                    Email = q.Manager.Email,
                    Phone = q.Manager.Phone,
                    Department = q.Manager.Department,
                    OrganizationId = q.Manager.OrganizationId,
                    StatusId = q.Manager.StatusId,
                    Avatar = q.Manager.Avatar,
                    SexId = q.Manager.SexId,
                    Birthday = q.Manager.Birthday,
                } : null,
                Saler = filter.Selects.Contains(ScheduleMasterSelect.Saler) && q.Saler != null ? new AppUser
                {
                    Id = q.Saler.Id,
                    Username = q.Saler.Username,
                    DisplayName = q.Saler.DisplayName,
                    Address = q.Saler.Address,
                    Email = q.Saler.Email,
                    Phone = q.Saler.Phone,
                    Department = q.Saler.Department,
                    OrganizationId = q.Saler.OrganizationId,
                    StatusId = q.Saler.StatusId,
                    Avatar = q.Saler.Avatar,
                    SexId = q.Saler.SexId,
                    Birthday = q.Saler.Birthday,
                } : null,
                Status = filter.Selects.Contains(ScheduleMasterSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return ScheduleMasters;
        }

        public async Task<int> Count(ScheduleMasterFilter filter)
        {
            IQueryable<ScheduleMasterDAO> ScheduleMasters = DataContext.ScheduleMaster.AsNoTracking();
            ScheduleMasters = DynamicFilter(ScheduleMasters, filter);
            return await ScheduleMasters.CountAsync();
        }

        public async Task<List<ScheduleMaster>> List(ScheduleMasterFilter filter)
        {
            if (filter == null) return new List<ScheduleMaster>();
            IQueryable<ScheduleMasterDAO> ScheduleMasterDAOs = DataContext.ScheduleMaster.AsNoTracking();
            ScheduleMasterDAOs = DynamicFilter(ScheduleMasterDAOs, filter);
            ScheduleMasterDAOs = DynamicOrder(ScheduleMasterDAOs, filter);
            List<ScheduleMaster> ScheduleMasters = await DynamicSelect(ScheduleMasterDAOs, filter);
            return ScheduleMasters;
        }

        public async Task<ScheduleMaster> Get(long Id)
        {
            ScheduleMaster ScheduleMaster = await DataContext.ScheduleMaster.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new ScheduleMaster()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                ManagerId = x.ManagerId,
                SalerId = x.SalerId,
                Name = x.Name,
                Code = x.Code,
                StatusId = x.StatusId,
                RecurDays = x.RecurDays,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                NoEndDate = x.NoEndDate,
                StartDayOfWeek = x.StartDayOfWeek,
                DisplayOrder = x.DisplayOrder,
                Description = x.Description,
                Manager = x.Manager == null ? null : new AppUser
                {
                    Id = x.Manager.Id,
                    Username = x.Manager.Username,
                    DisplayName = x.Manager.DisplayName,
                    Address = x.Manager.Address,
                    Email = x.Manager.Email,
                    Phone = x.Manager.Phone,
                    Department = x.Manager.Department,
                    OrganizationId = x.Manager.OrganizationId,
                    StatusId = x.Manager.StatusId,
                    Avatar = x.Manager.Avatar,
                    SexId = x.Manager.SexId,
                    Birthday = x.Manager.Birthday,
                },
                Saler = x.Saler == null ? null : new AppUser
                {
                    Id = x.Saler.Id,
                    Username = x.Saler.Username,
                    DisplayName = x.Saler.DisplayName,
                    Address = x.Saler.Address,
                    Email = x.Saler.Email,
                    Phone = x.Saler.Phone,
                    Department = x.Saler.Department,
                    OrganizationId = x.Saler.OrganizationId,
                    StatusId = x.Saler.StatusId,
                    Avatar = x.Saler.Avatar,
                    SexId = x.Saler.SexId,
                    Birthday = x.Saler.Birthday,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (ScheduleMaster == null)
                return null;

            return ScheduleMaster;
        }
        public async Task<bool> Create(ScheduleMaster ScheduleMaster)
        {
            ScheduleMasterDAO ScheduleMasterDAO = new ScheduleMasterDAO();
            ScheduleMasterDAO.Id = ScheduleMaster.Id;
            ScheduleMasterDAO.ManagerId = ScheduleMaster.ManagerId;
            ScheduleMasterDAO.SalerId = ScheduleMaster.SalerId;
            ScheduleMasterDAO.Name = ScheduleMaster.Name;
            ScheduleMasterDAO.Code = ScheduleMaster.Code;
            ScheduleMasterDAO.StatusId = ScheduleMaster.StatusId;
            ScheduleMasterDAO.RecurDays = ScheduleMaster.RecurDays;
            ScheduleMasterDAO.StartDate = ScheduleMaster.StartDate;
            ScheduleMasterDAO.EndDate = ScheduleMaster.EndDate;
            ScheduleMasterDAO.NoEndDate = ScheduleMaster.NoEndDate;
            ScheduleMasterDAO.StartDayOfWeek = ScheduleMaster.StartDayOfWeek;
            ScheduleMasterDAO.DisplayOrder = ScheduleMaster.DisplayOrder;
            ScheduleMasterDAO.Description = ScheduleMaster.Description;
            ScheduleMasterDAO.CreatedAt = StaticParams.DateTimeNow;
            ScheduleMasterDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.ScheduleMaster.Add(ScheduleMasterDAO);
            await DataContext.SaveChangesAsync();
            ScheduleMaster.Id = ScheduleMasterDAO.Id;
            await SaveReference(ScheduleMaster);
            return true;
        }

        public async Task<bool> Update(ScheduleMaster ScheduleMaster)
        {
            ScheduleMasterDAO ScheduleMasterDAO = DataContext.ScheduleMaster.Where(x => x.Id == ScheduleMaster.Id).FirstOrDefault();
            if (ScheduleMasterDAO == null)
                return false;
            ScheduleMasterDAO.Id = ScheduleMaster.Id;
            ScheduleMasterDAO.ManagerId = ScheduleMaster.ManagerId;
            ScheduleMasterDAO.SalerId = ScheduleMaster.SalerId;
            ScheduleMasterDAO.Name = ScheduleMaster.Name;
            ScheduleMasterDAO.Code = ScheduleMaster.Code;
            ScheduleMasterDAO.StatusId = ScheduleMaster.StatusId;
            ScheduleMasterDAO.RecurDays = ScheduleMaster.RecurDays;
            ScheduleMasterDAO.StartDate = ScheduleMaster.StartDate;
            ScheduleMasterDAO.EndDate = ScheduleMaster.EndDate;
            ScheduleMasterDAO.NoEndDate = ScheduleMaster.NoEndDate;
            ScheduleMasterDAO.StartDayOfWeek = ScheduleMaster.StartDayOfWeek;
            ScheduleMasterDAO.DisplayOrder = ScheduleMaster.DisplayOrder;
            ScheduleMasterDAO.Description = ScheduleMaster.Description;
            ScheduleMasterDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(ScheduleMaster);
            return true;
        }

        public async Task<bool> Delete(ScheduleMaster ScheduleMaster)
        {
            await DataContext.ScheduleMaster.Where(x => x.Id == ScheduleMaster.Id).UpdateFromQueryAsync(x => new ScheduleMasterDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<ScheduleMaster> ScheduleMasters)
        {
            List<ScheduleMasterDAO> ScheduleMasterDAOs = new List<ScheduleMasterDAO>();
            foreach (ScheduleMaster ScheduleMaster in ScheduleMasters)
            {
                ScheduleMasterDAO ScheduleMasterDAO = new ScheduleMasterDAO();
                ScheduleMasterDAO.Id = ScheduleMaster.Id;
                ScheduleMasterDAO.ManagerId = ScheduleMaster.ManagerId;
                ScheduleMasterDAO.SalerId = ScheduleMaster.SalerId;
                ScheduleMasterDAO.Name = ScheduleMaster.Name;
                ScheduleMasterDAO.Code = ScheduleMaster.Code;
                ScheduleMasterDAO.StatusId = ScheduleMaster.StatusId;
                ScheduleMasterDAO.RecurDays = ScheduleMaster.RecurDays;
                ScheduleMasterDAO.StartDate = ScheduleMaster.StartDate;
                ScheduleMasterDAO.EndDate = ScheduleMaster.EndDate;
                ScheduleMasterDAO.NoEndDate = ScheduleMaster.NoEndDate;
                ScheduleMasterDAO.StartDayOfWeek = ScheduleMaster.StartDayOfWeek;
                ScheduleMasterDAO.DisplayOrder = ScheduleMaster.DisplayOrder;
                ScheduleMasterDAO.Description = ScheduleMaster.Description;
                ScheduleMasterDAO.CreatedAt = StaticParams.DateTimeNow;
                ScheduleMasterDAO.UpdatedAt = StaticParams.DateTimeNow;
                ScheduleMasterDAOs.Add(ScheduleMasterDAO);
            }
            await DataContext.BulkMergeAsync(ScheduleMasterDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<ScheduleMaster> ScheduleMasters)
        {
            List<long> Ids = ScheduleMasters.Select(x => x.Id).ToList();
            await DataContext.ScheduleMaster
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new ScheduleMasterDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(ScheduleMaster ScheduleMaster)
        {
        }
        
    }
}
