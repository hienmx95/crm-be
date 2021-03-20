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
    public interface IPositionRepository
    {
        Task<int> Count(PositionFilter PositionFilter);
        Task<List<Position>> List(PositionFilter PositionFilter);
        Task<List<Position>> List(List<long> Ids);
        Task<Position> Get(long Id);
        Task<bool> Create(Position Position);
        Task<bool> Update(Position Position);
        Task<bool> Delete(Position Position);
        Task<bool> BulkMerge(List<Position> Positions);
        Task<bool> BulkDelete(List<Position> Positions);
    }
    public class PositionRepository : IPositionRepository
    {
        private DataContext DataContext;
        public PositionRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<PositionDAO> DynamicFilter(IQueryable<PositionDAO> query, PositionFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null && filter.CreatedAt.HasValue)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null && filter.UpdatedAt.HasValue)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null && filter.Code.HasValue)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.StatusId != null && filter.StatusId.HasValue)
                query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<PositionDAO> OrFilter(IQueryable<PositionDAO> query, PositionFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<PositionDAO> initQuery = query.Where(q => false);
            foreach (PositionFilter PositionFilter in filter.OrFilter)
            {
                IQueryable<PositionDAO> queryable = query;
                if (PositionFilter.Id != null && PositionFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, PositionFilter.Id);
                if (PositionFilter.Code != null && PositionFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, PositionFilter.Code);
                if (PositionFilter.Name != null && PositionFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, PositionFilter.Name);
                if (PositionFilter.StatusId != null && PositionFilter.StatusId.HasValue)
                    queryable = queryable.Where(q => q.StatusId, PositionFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<PositionDAO> DynamicOrder(IQueryable<PositionDAO> query, PositionFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case PositionOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case PositionOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case PositionOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case PositionOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case PositionOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case PositionOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case PositionOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case PositionOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Position>> DynamicSelect(IQueryable<PositionDAO> query, PositionFilter filter)
        {
            List<Position> Positions = await query.Select(q => new Position()
            {
                Id = filter.Selects.Contains(PositionSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(PositionSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(PositionSelect.Name) ? q.Name : default(string),
                StatusId = filter.Selects.Contains(PositionSelect.Status) ? q.StatusId : default(long),
                Status = filter.Selects.Contains(PositionSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                RowId = q.RowId,
                Used = q.Used,
            }).ToListAsync();
            return Positions;
        }

        public async Task<int> Count(PositionFilter filter)
        {
            IQueryable<PositionDAO> Positions = DataContext.Position.AsNoTracking();
            Positions = DynamicFilter(Positions, filter);
            return await Positions.CountAsync();
        }

        public async Task<List<Position>> List(PositionFilter filter)
        {
            if (filter == null) return new List<Position>();
            IQueryable<PositionDAO> PositionDAOs = DataContext.Position.AsNoTracking();
            PositionDAOs = DynamicFilter(PositionDAOs, filter);
            PositionDAOs = DynamicOrder(PositionDAOs, filter);
            List<Position> Positions = await DynamicSelect(PositionDAOs, filter);
            return Positions;
        }

        public async Task<List<Position>> List(List<long> Ids)
        {
            List<Position> Positions = await DataContext.Position.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new Position()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                StatusId = x.StatusId,
                RowId = x.RowId,
                Used = x.Used,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).ToListAsync();
            

            return Positions;
        }

        public async Task<Position> Get(long Id)
        {
            Position Position = await DataContext.Position.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new Position()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                StatusId = x.StatusId,
                RowId = x.RowId,
                Used = x.Used,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (Position == null)
                return null;

            return Position;
        }
        public async Task<bool> Create(Position Position)
        {
            PositionDAO PositionDAO = new PositionDAO();
            PositionDAO.Id = Position.Id;
            PositionDAO.Code = Position.Code;
            PositionDAO.Name = Position.Name;
            PositionDAO.StatusId = Position.StatusId;
            PositionDAO.RowId = Position.RowId;
            PositionDAO.Used = Position.Used;
            PositionDAO.CreatedAt = StaticParams.DateTimeNow;
            PositionDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.Position.Add(PositionDAO);
            await DataContext.SaveChangesAsync();
            Position.Id = PositionDAO.Id;
            await SaveReference(Position);
            return true;
        }

        public async Task<bool> Update(Position Position)
        {
            PositionDAO PositionDAO = DataContext.Position.Where(x => x.Id == Position.Id).FirstOrDefault();
            if (PositionDAO == null)
                return false;
            PositionDAO.Id = Position.Id;
            PositionDAO.Code = Position.Code;
            PositionDAO.Name = Position.Name;
            PositionDAO.StatusId = Position.StatusId;
            PositionDAO.RowId = Position.RowId;
            PositionDAO.Used = Position.Used;
            PositionDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(Position);
            return true;
        }

        public async Task<bool> Delete(Position Position)
        {
            await DataContext.Position.Where(x => x.Id == Position.Id).UpdateFromQueryAsync(x => new PositionDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<Position> Positions)
        {
            List<PositionDAO> PositionDAOs = new List<PositionDAO>();
            foreach (Position Position in Positions)
            {
                PositionDAO PositionDAO = new PositionDAO();
                PositionDAO.Id = Position.Id;
                PositionDAO.Code = Position.Code;
                PositionDAO.Name = Position.Name;
                PositionDAO.StatusId = Position.StatusId;
                PositionDAO.RowId = Position.RowId;
                PositionDAO.Used = Position.Used;
                PositionDAO.CreatedAt = StaticParams.DateTimeNow;
                PositionDAO.UpdatedAt = StaticParams.DateTimeNow;
                PositionDAOs.Add(PositionDAO);
            }
            await DataContext.BulkMergeAsync(PositionDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<Position> Positions)
        {
            List<long> Ids = Positions.Select(x => x.Id).ToList();
            await DataContext.Position
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new PositionDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(Position Position)
        {
        }
        
    }
}
