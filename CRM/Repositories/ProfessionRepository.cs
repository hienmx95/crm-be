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
    public interface IProfessionRepository
    {
        Task<int> Count(ProfessionFilter ProfessionFilter);
        Task<List<Profession>> List(ProfessionFilter ProfessionFilter);
        Task<List<Profession>> List(List<long> Ids);
        Task<Profession> Get(long Id);
        Task<bool> Create(Profession Profession);
        Task<bool> Update(Profession Profession);
        Task<bool> Delete(Profession Profession);
        Task<bool> BulkMerge(List<Profession> Professions);
        Task<bool> BulkDelete(List<Profession> Professions);
    }
    public class ProfessionRepository : IProfessionRepository
    {
        private DataContext DataContext;
        public ProfessionRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ProfessionDAO> DynamicFilter(IQueryable<ProfessionDAO> query, ProfessionFilter filter)
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
            if (filter.RowId != null && filter.RowId.HasValue)
                query = query.Where(q => q.RowId, filter.RowId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<ProfessionDAO> OrFilter(IQueryable<ProfessionDAO> query, ProfessionFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ProfessionDAO> initQuery = query.Where(q => false);
            foreach (ProfessionFilter ProfessionFilter in filter.OrFilter)
            {
                IQueryable<ProfessionDAO> queryable = query;
                if (ProfessionFilter.Id != null && ProfessionFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ProfessionFilter.Id);
                if (ProfessionFilter.Code != null && ProfessionFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, ProfessionFilter.Code);
                if (ProfessionFilter.Name != null && ProfessionFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, ProfessionFilter.Name);
                if (ProfessionFilter.StatusId != null && ProfessionFilter.StatusId.HasValue)
                    queryable = queryable.Where(q => q.StatusId, ProfessionFilter.StatusId);
                if (ProfessionFilter.RowId != null && ProfessionFilter.RowId.HasValue)
                    queryable = queryable.Where(q => q.RowId, ProfessionFilter.RowId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ProfessionDAO> DynamicOrder(IQueryable<ProfessionDAO> query, ProfessionFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ProfessionOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ProfessionOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ProfessionOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ProfessionOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case ProfessionOrder.Row:
                            query = query.OrderBy(q => q.RowId);
                            break;
                        case ProfessionOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ProfessionOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ProfessionOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ProfessionOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ProfessionOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case ProfessionOrder.Row:
                            query = query.OrderByDescending(q => q.RowId);
                            break;
                        case ProfessionOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Profession>> DynamicSelect(IQueryable<ProfessionDAO> query, ProfessionFilter filter)
        {
            List<Profession> Professions = await query.Select(q => new Profession()
            {
                Id = filter.Selects.Contains(ProfessionSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ProfessionSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ProfessionSelect.Name) ? q.Name : default(string),
                StatusId = filter.Selects.Contains(ProfessionSelect.Status) ? q.StatusId : default(long),
                RowId = filter.Selects.Contains(ProfessionSelect.Row) ? q.RowId : default(Guid),
                Used = filter.Selects.Contains(ProfessionSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(ProfessionSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
            }).ToListAsync();
            return Professions;
        }

        public async Task<int> Count(ProfessionFilter filter)
        {
            IQueryable<ProfessionDAO> Professions = DataContext.Profession.AsNoTracking();
            Professions = DynamicFilter(Professions, filter);
            return await Professions.CountAsync();
        }

        public async Task<List<Profession>> List(ProfessionFilter filter)
        {
            if (filter == null) return new List<Profession>();
            IQueryable<ProfessionDAO> ProfessionDAOs = DataContext.Profession.AsNoTracking();
            ProfessionDAOs = DynamicFilter(ProfessionDAOs, filter);
            ProfessionDAOs = DynamicOrder(ProfessionDAOs, filter);
            List<Profession> Professions = await DynamicSelect(ProfessionDAOs, filter);
            return Professions;
        }

        public async Task<List<Profession>> List(List<long> Ids)
        {
            List<Profession> Professions = await DataContext.Profession.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new Profession()
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
            

            return Professions;
        }

        public async Task<Profession> Get(long Id)
        {
            Profession Profession = await DataContext.Profession.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new Profession()
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

            if (Profession == null)
                return null;

            return Profession;
        }
        public async Task<bool> Create(Profession Profession)
        {
            ProfessionDAO ProfessionDAO = new ProfessionDAO();
            ProfessionDAO.Id = Profession.Id;
            ProfessionDAO.Code = Profession.Code;
            ProfessionDAO.Name = Profession.Name;
            ProfessionDAO.StatusId = Profession.StatusId;
            ProfessionDAO.RowId = Profession.RowId;
            ProfessionDAO.Used = Profession.Used;
            ProfessionDAO.CreatedAt = StaticParams.DateTimeNow;
            ProfessionDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.Profession.Add(ProfessionDAO);
            await DataContext.SaveChangesAsync();
            Profession.Id = ProfessionDAO.Id;
            await SaveReference(Profession);
            return true;
        }

        public async Task<bool> Update(Profession Profession)
        {
            ProfessionDAO ProfessionDAO = DataContext.Profession.Where(x => x.Id == Profession.Id).FirstOrDefault();
            if (ProfessionDAO == null)
                return false;
            ProfessionDAO.Id = Profession.Id;
            ProfessionDAO.Code = Profession.Code;
            ProfessionDAO.Name = Profession.Name;
            ProfessionDAO.StatusId = Profession.StatusId;
            ProfessionDAO.RowId = Profession.RowId;
            ProfessionDAO.Used = Profession.Used;
            ProfessionDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(Profession);
            return true;
        }

        public async Task<bool> Delete(Profession Profession)
        {
            await DataContext.Profession.Where(x => x.Id == Profession.Id).UpdateFromQueryAsync(x => new ProfessionDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<Profession> Professions)
        {
            List<ProfessionDAO> ProfessionDAOs = new List<ProfessionDAO>();
            foreach (Profession Profession in Professions)
            {
                ProfessionDAO ProfessionDAO = new ProfessionDAO();
                ProfessionDAO.Id = Profession.Id;
                ProfessionDAO.Code = Profession.Code;
                ProfessionDAO.Name = Profession.Name;
                ProfessionDAO.StatusId = Profession.StatusId;
                ProfessionDAO.RowId = Profession.RowId;
                ProfessionDAO.Used = Profession.Used;
                ProfessionDAO.CreatedAt = StaticParams.DateTimeNow;
                ProfessionDAO.UpdatedAt = StaticParams.DateTimeNow;
                ProfessionDAOs.Add(ProfessionDAO);
            }
            await DataContext.BulkMergeAsync(ProfessionDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<Profession> Professions)
        {
            List<long> Ids = Professions.Select(x => x.Id).ToList();
            await DataContext.Profession
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new ProfessionDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(Profession Profession)
        {
        }
        
    }
}
