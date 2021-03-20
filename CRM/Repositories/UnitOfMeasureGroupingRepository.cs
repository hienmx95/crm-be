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
    public interface IUnitOfMeasureGroupingRepository
    {
        Task<int> Count(UnitOfMeasureGroupingFilter UnitOfMeasureGroupingFilter);
        Task<List<UnitOfMeasureGrouping>> List(UnitOfMeasureGroupingFilter UnitOfMeasureGroupingFilter);
        Task<UnitOfMeasureGrouping> Get(long Id);
    }
    public class UnitOfMeasureGroupingRepository : IUnitOfMeasureGroupingRepository
    {
        private DataContext DataContext;
        public UnitOfMeasureGroupingRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<UnitOfMeasureGroupingDAO> DynamicFilter(IQueryable<UnitOfMeasureGroupingDAO> query, UnitOfMeasureGroupingFilter filter)
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
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.UnitOfMeasureId != null)
                query = query.Where(q => q.UnitOfMeasureId, filter.UnitOfMeasureId);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<UnitOfMeasureGroupingDAO> OrFilter(IQueryable<UnitOfMeasureGroupingDAO> query, UnitOfMeasureGroupingFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<UnitOfMeasureGroupingDAO> initQuery = query.Where(q => false);
            foreach (UnitOfMeasureGroupingFilter UnitOfMeasureGroupingFilter in filter.OrFilter)
            {
                IQueryable<UnitOfMeasureGroupingDAO> queryable = query;
                if (UnitOfMeasureGroupingFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, UnitOfMeasureGroupingFilter.Id);
                if (UnitOfMeasureGroupingFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, UnitOfMeasureGroupingFilter.Code);
                if (UnitOfMeasureGroupingFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, UnitOfMeasureGroupingFilter.Name);
                if (UnitOfMeasureGroupingFilter.Description != null)
                    queryable = queryable.Where(q => q.Description, UnitOfMeasureGroupingFilter.Description);
                if (UnitOfMeasureGroupingFilter.UnitOfMeasureId != null)
                    queryable = queryable.Where(q => q.UnitOfMeasureId, UnitOfMeasureGroupingFilter.UnitOfMeasureId);
                if (UnitOfMeasureGroupingFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, UnitOfMeasureGroupingFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<UnitOfMeasureGroupingDAO> DynamicOrder(IQueryable<UnitOfMeasureGroupingDAO> query, UnitOfMeasureGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case UnitOfMeasureGroupingOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case UnitOfMeasureGroupingOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case UnitOfMeasureGroupingOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case UnitOfMeasureGroupingOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case UnitOfMeasureGroupingOrder.UnitOfMeasure:
                            query = query.OrderBy(q => q.UnitOfMeasureId);
                            break;
                        case UnitOfMeasureGroupingOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case UnitOfMeasureGroupingOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case UnitOfMeasureGroupingOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case UnitOfMeasureGroupingOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case UnitOfMeasureGroupingOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case UnitOfMeasureGroupingOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case UnitOfMeasureGroupingOrder.UnitOfMeasure:
                            query = query.OrderByDescending(q => q.UnitOfMeasureId);
                            break;
                        case UnitOfMeasureGroupingOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case UnitOfMeasureGroupingOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<UnitOfMeasureGrouping>> DynamicSelect(IQueryable<UnitOfMeasureGroupingDAO> query, UnitOfMeasureGroupingFilter filter)
        {
            List<UnitOfMeasureGrouping> UnitOfMeasureGroupings = await query.Select(q => new UnitOfMeasureGrouping()
            {
                Id = filter.Selects.Contains(UnitOfMeasureGroupingSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(UnitOfMeasureGroupingSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(UnitOfMeasureGroupingSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(UnitOfMeasureGroupingSelect.Description) ? q.Description : default(string),
                UnitOfMeasureId = filter.Selects.Contains(UnitOfMeasureGroupingSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(long),
                StatusId = filter.Selects.Contains(UnitOfMeasureGroupingSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(UnitOfMeasureGroupingSelect.Used) ? q.Used : default(bool),
                UnitOfMeasure = filter.Selects.Contains(UnitOfMeasureGroupingSelect.UnitOfMeasure) && q.UnitOfMeasure != null ? new UnitOfMeasure
                {
                    Id = q.UnitOfMeasure.Id,
                    Code = q.UnitOfMeasure.Code,
                    Name = q.UnitOfMeasure.Name,
                    Description = q.UnitOfMeasure.Description,
                    StatusId = q.UnitOfMeasure.StatusId,
                    Used = q.UnitOfMeasure.Used,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                UnitOfMeasureGroupingContents = filter.Selects.Contains(UnitOfMeasureGroupingSelect.UnitOfMeasureGroupingContents) && q.UnitOfMeasureGroupingContents == null ? null :
                q.UnitOfMeasureGroupingContents.Select(x => new UnitOfMeasureGroupingContent
                {
                    Id = x.Id,
                    Factor = x.Factor,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    UnitOfMeasureGroupingId = x.UnitOfMeasureGroupingId,
                    UnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.UnitOfMeasure.Id,
                        Name = x.UnitOfMeasure.Name,
                        Code = x.UnitOfMeasure.Code,
                    }
                }).ToList(),
            }).ToListAsync();
            return UnitOfMeasureGroupings;
        }

        public async Task<int> Count(UnitOfMeasureGroupingFilter filter)
        {
            IQueryable<UnitOfMeasureGroupingDAO> UnitOfMeasureGroupings = DataContext.UnitOfMeasureGrouping.AsNoTracking();
            UnitOfMeasureGroupings = DynamicFilter(UnitOfMeasureGroupings, filter);
            return await UnitOfMeasureGroupings.CountAsync();
        }

        public async Task<List<UnitOfMeasureGrouping>> List(UnitOfMeasureGroupingFilter filter)
        {
            if (filter == null) return new List<UnitOfMeasureGrouping>();
            IQueryable<UnitOfMeasureGroupingDAO> UnitOfMeasureGroupingDAOs = DataContext.UnitOfMeasureGrouping.AsNoTracking();
            UnitOfMeasureGroupingDAOs = DynamicFilter(UnitOfMeasureGroupingDAOs, filter);
            UnitOfMeasureGroupingDAOs = DynamicOrder(UnitOfMeasureGroupingDAOs, filter);
            List<UnitOfMeasureGrouping> UnitOfMeasureGroupings = await DynamicSelect(UnitOfMeasureGroupingDAOs, filter);
            return UnitOfMeasureGroupings;
        }

        public async Task<UnitOfMeasureGrouping> Get(long Id)
        {
            UnitOfMeasureGrouping UnitOfMeasureGrouping = await DataContext.UnitOfMeasureGrouping.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new UnitOfMeasureGrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Description = x.Description,
                UnitOfMeasureId = x.UnitOfMeasureId,
                StatusId = x.StatusId,
                Used = x.Used,
                UnitOfMeasure = x.UnitOfMeasure == null ? null : new UnitOfMeasure
                {
                    Id = x.UnitOfMeasure.Id,
                    Code = x.UnitOfMeasure.Code,
                    Name = x.UnitOfMeasure.Name,
                    Description = x.UnitOfMeasure.Description,
                    StatusId = x.UnitOfMeasure.StatusId,
                    Used = x.UnitOfMeasure.Used,
                },
            }).FirstOrDefaultAsync();

            if (UnitOfMeasureGrouping == null)
                return null;

            return UnitOfMeasureGrouping;
        }
    }
}
