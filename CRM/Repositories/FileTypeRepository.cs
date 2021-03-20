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
    public interface IFileTypeRepository
    {
        Task<int> Count(FileTypeFilter FileTypeFilter);
        Task<List<FileType>> List(FileTypeFilter FileTypeFilter);
        Task<List<FileType>> List(List<long> Ids);
        Task<FileType> Get(long Id);
        Task<bool> Create(FileType FileType);
        Task<bool> Update(FileType FileType);
        Task<bool> Delete(FileType FileType);
        Task<bool> BulkMerge(List<FileType> FileTypes);
        Task<bool> BulkDelete(List<FileType> FileTypes);
    }
    public class FileTypeRepository : IFileTypeRepository
    {
        private DataContext DataContext;
        public FileTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<FileTypeDAO> DynamicFilter(IQueryable<FileTypeDAO> query, FileTypeFilter filter)
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

        private IQueryable<FileTypeDAO> OrFilter(IQueryable<FileTypeDAO> query, FileTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<FileTypeDAO> initQuery = query.Where(q => false);
            foreach (FileTypeFilter FileTypeFilter in filter.OrFilter)
            {
                IQueryable<FileTypeDAO> queryable = query;
                if (FileTypeFilter.Id != null && FileTypeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, FileTypeFilter.Id);
                if (FileTypeFilter.Code != null && FileTypeFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, FileTypeFilter.Code);
                if (FileTypeFilter.Name != null && FileTypeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, FileTypeFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<FileTypeDAO> DynamicOrder(IQueryable<FileTypeDAO> query, FileTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case FileTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case FileTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case FileTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case FileTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case FileTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case FileTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<FileType>> DynamicSelect(IQueryable<FileTypeDAO> query, FileTypeFilter filter)
        {
            List<FileType> FileTypes = await query.Select(q => new FileType()
            {
                Id = filter.Selects.Contains(FileTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(FileTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(FileTypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return FileTypes;
        }

        public async Task<int> Count(FileTypeFilter filter)
        {
            IQueryable<FileTypeDAO> FileTypes = DataContext.FileType.AsNoTracking();
            FileTypes = DynamicFilter(FileTypes, filter);
            return await FileTypes.CountAsync();
        }

        public async Task<List<FileType>> List(FileTypeFilter filter)
        {
            if (filter == null) return new List<FileType>();
            IQueryable<FileTypeDAO> FileTypeDAOs = DataContext.FileType.AsNoTracking();
            FileTypeDAOs = DynamicFilter(FileTypeDAOs, filter);
            FileTypeDAOs = DynamicOrder(FileTypeDAOs, filter);
            List<FileType> FileTypes = await DynamicSelect(FileTypeDAOs, filter);
            return FileTypes;
        }

        public async Task<List<FileType>> List(List<long> Ids)
        {
            List<FileType> FileTypes = await DataContext.FileType.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new FileType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return FileTypes;
        }

        public async Task<FileType> Get(long Id)
        {
            FileType FileType = await DataContext.FileType.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new FileType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (FileType == null)
                return null;

            return FileType;
        }
        public async Task<bool> Create(FileType FileType)
        {
            FileTypeDAO FileTypeDAO = new FileTypeDAO();
            FileTypeDAO.Id = FileType.Id;
            FileTypeDAO.Code = FileType.Code;
            FileTypeDAO.Name = FileType.Name;
            DataContext.FileType.Add(FileTypeDAO);
            await DataContext.SaveChangesAsync();
            FileType.Id = FileTypeDAO.Id;
            await SaveReference(FileType);
            return true;
        }

        public async Task<bool> Update(FileType FileType)
        {
            FileTypeDAO FileTypeDAO = DataContext.FileType.Where(x => x.Id == FileType.Id).FirstOrDefault();
            if (FileTypeDAO == null)
                return false;
            FileTypeDAO.Id = FileType.Id;
            FileTypeDAO.Code = FileType.Code;
            FileTypeDAO.Name = FileType.Name;
            await DataContext.SaveChangesAsync();
            await SaveReference(FileType);
            return true;
        }

        public async Task<bool> Delete(FileType FileType)
        {
            await DataContext.FileType.Where(x => x.Id == FileType.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<FileType> FileTypes)
        {
            List<FileTypeDAO> FileTypeDAOs = new List<FileTypeDAO>();
            foreach (FileType FileType in FileTypes)
            {
                FileTypeDAO FileTypeDAO = new FileTypeDAO();
                FileTypeDAO.Id = FileType.Id;
                FileTypeDAO.Code = FileType.Code;
                FileTypeDAO.Name = FileType.Name;
                FileTypeDAOs.Add(FileTypeDAO);
            }
            await DataContext.BulkMergeAsync(FileTypeDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<FileType> FileTypes)
        {
            List<long> Ids = FileTypes.Select(x => x.Id).ToList();
            await DataContext.FileType
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(FileType FileType)
        {
        }
        
    }
}
