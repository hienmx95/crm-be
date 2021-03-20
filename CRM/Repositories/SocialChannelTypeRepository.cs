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
    public interface ISocialChannelTypeRepository
    {
        Task<int> Count(SocialChannelTypeFilter SocialChannelTypeFilter);
        Task<List<SocialChannelType>> List(SocialChannelTypeFilter SocialChannelTypeFilter);
        Task<List<SocialChannelType>> List(List<long> Ids);
        Task<SocialChannelType> Get(long Id);
    }
    public class SocialChannelTypeRepository : ISocialChannelTypeRepository
    {
        private DataContext DataContext;
        public SocialChannelTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SocialChannelTypeDAO> DynamicFilter(IQueryable<SocialChannelTypeDAO> query, SocialChannelTypeFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null && filter.Code.HasValue)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<SocialChannelTypeDAO> OrFilter(IQueryable<SocialChannelTypeDAO> query, SocialChannelTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SocialChannelTypeDAO> initQuery = query.Where(q => false);
            foreach (SocialChannelTypeFilter SocialChannelTypeFilter in filter.OrFilter)
            {
                IQueryable<SocialChannelTypeDAO> queryable = query;
                if (SocialChannelTypeFilter.Id != null && SocialChannelTypeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, SocialChannelTypeFilter.Id);
                if (SocialChannelTypeFilter.Code != null && SocialChannelTypeFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, SocialChannelTypeFilter.Code);
                if (SocialChannelTypeFilter.Name != null && SocialChannelTypeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, SocialChannelTypeFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SocialChannelTypeDAO> DynamicOrder(IQueryable<SocialChannelTypeDAO> query, SocialChannelTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SocialChannelTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SocialChannelTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case SocialChannelTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SocialChannelTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SocialChannelTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case SocialChannelTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SocialChannelType>> DynamicSelect(IQueryable<SocialChannelTypeDAO> query, SocialChannelTypeFilter filter)
        {
            List<SocialChannelType> SocialChannelTypes = await query.Select(q => new SocialChannelType()
            {
                Id = filter.Selects.Contains(SocialChannelTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(SocialChannelTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(SocialChannelTypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return SocialChannelTypes;
        }

        public async Task<int> Count(SocialChannelTypeFilter filter)
        {
            IQueryable<SocialChannelTypeDAO> SocialChannelTypes = DataContext.SocialChannelType.AsNoTracking();
            SocialChannelTypes = DynamicFilter(SocialChannelTypes, filter);
            return await SocialChannelTypes.CountAsync();
        }

        public async Task<List<SocialChannelType>> List(SocialChannelTypeFilter filter)
        {
            if (filter == null) return new List<SocialChannelType>();
            IQueryable<SocialChannelTypeDAO> SocialChannelTypeDAOs = DataContext.SocialChannelType.AsNoTracking();
            SocialChannelTypeDAOs = DynamicFilter(SocialChannelTypeDAOs, filter);
            SocialChannelTypeDAOs = DynamicOrder(SocialChannelTypeDAOs, filter);
            List<SocialChannelType> SocialChannelTypes = await DynamicSelect(SocialChannelTypeDAOs, filter);
            return SocialChannelTypes;
        }

        public async Task<List<SocialChannelType>> List(List<long> Ids)
        {
            List<SocialChannelType> SocialChannelTypes = await DataContext.SocialChannelType.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new SocialChannelType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return SocialChannelTypes;
        }

        public async Task<SocialChannelType> Get(long Id)
        {
            SocialChannelType SocialChannelType = await DataContext.SocialChannelType.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new SocialChannelType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (SocialChannelType == null)
                return null;

            return SocialChannelType;
        }
    }
}
