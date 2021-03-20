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
    public interface IFileRepository
    {
        Task<int> Count(FileFilter FileFilter);
        Task<List<File>> List(FileFilter FileFilter);
        Task<File> Get(long Id);
        Task<bool> Create(File File);
        Task<bool> Update(File File);
        Task<bool> Delete(File File);
        Task<bool> BulkMerge(List<File> Files);
        Task<bool> BulkDelete(List<File> Files);
        Task<bool> SaveListFile(Entities.File File);
    }
    public class FileRepository : IFileRepository
    {
        private DataContext DataContext;
        public FileRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<FileDAO> DynamicFilter(IQueryable<FileDAO> query, FileFilter filter)
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
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Url != null)
                query = query.Where(q => q.Url, filter.Url);
            if (filter.AppUserId != null)
                query = query.Where(q => q.AppUserId, filter.AppUserId);
            //if (filter.GroupId != null)
            //    query = query.Where(q => q.GroupId, filter.GroupId); 
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<FileDAO> OrFilter(IQueryable<FileDAO> query, FileFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<FileDAO> initQuery = query.Where(q => false);
            foreach (FileFilter FileFilter in filter.OrFilter)
            {
                IQueryable<FileDAO> queryable = query;
                if (FileFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, FileFilter.Id);
                if (FileFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, FileFilter.Name);
                if (FileFilter.Url != null)
                    queryable = queryable.Where(q => q.Url, FileFilter.Url);
                if (FileFilter.AppUserId != null)
                    queryable = queryable.Where(q => q.AppUserId, FileFilter.AppUserId);
                //if (FileFilter.GroupId != null)
                //    queryable = queryable.Where(q => q.GroupId, FileFilter.GroupId);
 
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<FileDAO> DynamicOrder(IQueryable<FileDAO> query, FileFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case FileOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case FileOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case FileOrder.Url:
                            query = query.OrderBy(q => q.Url);
                            break;
                        case FileOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        //case FileOrder.Group:
                        //    query = query.OrderBy(q => q.GroupId);
                        //    break; 
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case FileOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case FileOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case FileOrder.Url:
                            query = query.OrderByDescending(q => q.Url);
                            break;
                        case FileOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        //case FileOrder.Group:
                        //    query = query.OrderByDescending(q => q.GroupId);
                        //    break; 
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<File>> DynamicSelect(IQueryable<FileDAO> query, FileFilter filter)
        {
            List<File> Files = await query.Select(q => new File()
            {
                Id = filter.Selects.Contains(FileSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(FileSelect.Name) ? q.Name : default(string),
                Url = filter.Selects.Contains(FileSelect.Url) ? q.Url : default(string),
                AppUserId = filter.Selects.Contains(FileSelect.AppUser) ? q.AppUserId : default(long?),
                //GroupId = filter.Selects.Contains(FileSelect.Group) ? q.GroupId : default(long?),
                AppUser = filter.Selects.Contains(FileSelect.AppUser) && q.AppUser != null ? new AppUser
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
                } : null,
                //Group = filter.Selects.Contains(FileSelect.Group) && q.Group != null ? new FileGroup
                //{
                //    Id = q.Group.Id,
                //    Title = q.Group.Name,
                //    Description = q.Group.Description,
                //    CreatorId = q.Group.AppUserId,
                //} : null,
                
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return Files;
        }

        public async Task<int> Count(FileFilter filter)
        {
            IQueryable<FileDAO> Files = DataContext.File.AsNoTracking();
            Files = DynamicFilter(Files, filter);
            return await Files.CountAsync();
        }

        public async Task<List<File>> List(FileFilter filter)
        {
            if (filter == null) return new List<File>();
            IQueryable<FileDAO> FileDAOs = DataContext.File.AsNoTracking();
            FileDAOs = DynamicFilter(FileDAOs, filter);
            FileDAOs = DynamicOrder(FileDAOs, filter);
            List<File> Files = await DynamicSelect(FileDAOs, filter);
            return Files;
        }

        public async Task<File> Get(long Id)
        {
            File File = await DataContext.File.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new File()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Name = x.Name,
                Url = x.Url,
                AppUserId = x.AppUserId,
                //GroupId = x.GroupId,
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
                },
                //Group = x.Group == null ? null : new FileGroup
                //{
                //    Id = x.Group.Id,
                //    Title = x.Group.Name,
                //    Description = x.Group.Description,
                //    CreatorId = x.Group.AppUserId,
                //},
                
            }).FirstOrDefaultAsync();

            if (File == null)
                return null;

            return File;
        }
        public async Task<bool> Create(File File)
        {
            try
            {
                FileDAO FileDAO = new FileDAO();
                FileDAO.Id = File.Id;
                FileDAO.Name = File.Name;
                FileDAO.Url = File.Url;
                FileDAO.AppUserId = File.AppUserId;
                //FileDAO.GroupId = File.GroupId;
                FileDAO.CreatedAt = StaticParams.DateTimeNow;
                FileDAO.UpdatedAt = StaticParams.DateTimeNow;
                DataContext.File.Add(FileDAO);
                await DataContext.SaveChangesAsync();
                File.Id = FileDAO.Id;
                await SaveReference(File);
                return true;
            }
           catch(Exception e)
            {
                return true;
            }
        }

        public async Task<bool> Update(File File)
        {
            FileDAO FileDAO = DataContext.File.Where(x => x.Id == File.Id).FirstOrDefault();
            if (FileDAO == null)
                return false;
            FileDAO.Id = File.Id;
            FileDAO.Name = File.Name;
            FileDAO.Url = File.Url;
            FileDAO.AppUserId = File.AppUserId;
            //FileDAO.GroupId = File.GroupId;
            FileDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(File);
            return true;
        }

        public async Task<bool> Delete(File File)
        {
            await DataContext.File.Where(x => x.Id == File.Id).UpdateFromQueryAsync(x => new FileDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<File> Files)
        {
            List<FileDAO> FileDAOs = new List<FileDAO>();
            foreach (File File in Files)
            {
                FileDAO FileDAO = new FileDAO();
                FileDAO.Id = File.Id;
                FileDAO.Name = File.Name;
                FileDAO.Url = File.Url;
                FileDAO.AppUserId = File.AppUserId;
                //FileDAO.GroupId = File.GroupId;
                FileDAO.CreatedAt = StaticParams.DateTimeNow;
                FileDAO.UpdatedAt = StaticParams.DateTimeNow;
                FileDAOs.Add(FileDAO);
            }
            await DataContext.BulkMergeAsync(FileDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<File> Files)
        {
            List<long> Ids = Files.Select(x => x.Id).ToList();
            await DataContext.File
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new FileDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        public async Task<bool> SaveListFile(Entities.File File)
        {
            try
            {
                await SaveReference(File);
                return true;
            }
            catch (Exception e)
            {
                throw new MessageException(e);
            }
        }

        private async Task SaveReference(File File)
        {
        }

    }
}
