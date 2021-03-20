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
    public interface IConsultingServiceRepository
    {
        Task<int> Count(ConsultingServiceFilter ConsultingServiceFilter);
        Task<List<ConsultingService>> List(ConsultingServiceFilter ConsultingServiceFilter);
        Task<List<ConsultingService>> List(List<long> Ids);
        Task<ConsultingService> Get(long Id);
    }
    public class ConsultingServiceRepository : IConsultingServiceRepository
    {
        private DataContext DataContext;
        public ConsultingServiceRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ConsultingServiceDAO> DynamicFilter(IQueryable<ConsultingServiceDAO> query, ConsultingServiceFilter filter)
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

        private IQueryable<ConsultingServiceDAO> OrFilter(IQueryable<ConsultingServiceDAO> query, ConsultingServiceFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ConsultingServiceDAO> initQuery = query.Where(q => false);
            foreach (ConsultingServiceFilter ConsultingServiceFilter in filter.OrFilter)
            {
                IQueryable<ConsultingServiceDAO> queryable = query;
                if (ConsultingServiceFilter.Id != null && ConsultingServiceFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ConsultingServiceFilter.Id);
                if (ConsultingServiceFilter.Code != null && ConsultingServiceFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, ConsultingServiceFilter.Code);
                if (ConsultingServiceFilter.Name != null && ConsultingServiceFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, ConsultingServiceFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ConsultingServiceDAO> DynamicOrder(IQueryable<ConsultingServiceDAO> query, ConsultingServiceFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ConsultingServiceOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ConsultingServiceOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ConsultingServiceOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ConsultingServiceOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ConsultingServiceOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ConsultingServiceOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ConsultingService>> DynamicSelect(IQueryable<ConsultingServiceDAO> query, ConsultingServiceFilter filter)
        {
            List<ConsultingService> ConsultingServices = await query.Select(q => new ConsultingService()
            {
                Id = filter.Selects.Contains(ConsultingServiceSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ConsultingServiceSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ConsultingServiceSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return ConsultingServices;
        }

        public async Task<int> Count(ConsultingServiceFilter filter)
        {
            IQueryable<ConsultingServiceDAO> ConsultingServices = DataContext.ConsultingService.AsNoTracking();
            ConsultingServices = DynamicFilter(ConsultingServices, filter);
            return await ConsultingServices.CountAsync();
        }

        public async Task<List<ConsultingService>> List(ConsultingServiceFilter filter)
        {
            if (filter == null) return new List<ConsultingService>();
            IQueryable<ConsultingServiceDAO> ConsultingServiceDAOs = DataContext.ConsultingService.AsNoTracking();
            ConsultingServiceDAOs = DynamicFilter(ConsultingServiceDAOs, filter);
            ConsultingServiceDAOs = DynamicOrder(ConsultingServiceDAOs, filter);
            List<ConsultingService> ConsultingServices = await DynamicSelect(ConsultingServiceDAOs, filter);
            return ConsultingServices;
        }

        public async Task<List<ConsultingService>> List(List<long> Ids)
        {
            List<ConsultingService> ConsultingServices = await DataContext.ConsultingService.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new ConsultingService()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return ConsultingServices;
        }

        public async Task<ConsultingService> Get(long Id)
        {
            ConsultingService ConsultingService = await DataContext.ConsultingService.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new ConsultingService()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (ConsultingService == null)
                return null;

            return ConsultingService;
        }
    }
}
