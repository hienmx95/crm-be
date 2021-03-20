using CRM.Common;
using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using CRM.Repositories;
using CRM.Entities;
using CRM.Enums;

namespace CRM.Services.MFileType
{
    public interface IFileTypeService :  IServiceScoped
    {
        Task<int> Count(FileTypeFilter FileTypeFilter);
        Task<List<FileType>> List(FileTypeFilter FileTypeFilter);
        Task<FileType> Get(long Id);
        Task<FileType> Create(FileType FileType);
        Task<FileType> Update(FileType FileType);
        Task<FileType> Delete(FileType FileType);
        Task<List<FileType>> BulkDelete(List<FileType> FileTypes);
        Task<List<FileType>> Import(List<FileType> FileTypes);
        Task<FileTypeFilter> ToFilter(FileTypeFilter FileTypeFilter);
    }

    public class FileTypeService : BaseService, IFileTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IFileTypeValidator FileTypeValidator;

        public FileTypeService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            IFileTypeValidator FileTypeValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.FileTypeValidator = FileTypeValidator;
        }
        public async Task<int> Count(FileTypeFilter FileTypeFilter)
        {
            try
            {
                int result = await UOW.FileTypeRepository.Count(FileTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(FileTypeService));
            }
            return 0;
        }

        public async Task<List<FileType>> List(FileTypeFilter FileTypeFilter)
        {
            try
            {
                List<FileType> FileTypes = await UOW.FileTypeRepository.List(FileTypeFilter);
                return FileTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(FileTypeService));
            }
            return null;
        }
        
        public async Task<FileType> Get(long Id)
        {
            FileType FileType = await UOW.FileTypeRepository.Get(Id);
            if (FileType == null)
                return null;
            return FileType;
        }
        public async Task<FileType> Create(FileType FileType)
        {
            if (!await FileTypeValidator.Create(FileType))
                return FileType;

            try
            {
                await UOW.FileTypeRepository.Create(FileType);
                FileType = await UOW.FileTypeRepository.Get(FileType.Id);
                await Logging.CreateAuditLog(FileType, new { }, nameof(FileTypeService));
                return FileType;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(FileTypeService));
            }
            return null;
        }

        public async Task<FileType> Update(FileType FileType)
        {
            if (!await FileTypeValidator.Update(FileType))
                return FileType;
            try
            {
                var oldData = await UOW.FileTypeRepository.Get(FileType.Id);

                await UOW.FileTypeRepository.Update(FileType);

                FileType = await UOW.FileTypeRepository.Get(FileType.Id);
                await Logging.CreateAuditLog(FileType, oldData, nameof(FileTypeService));
                return FileType;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(FileTypeService));
            }
            return null;
        }

        public async Task<FileType> Delete(FileType FileType)
        {
            if (!await FileTypeValidator.Delete(FileType))
                return FileType;

            try
            {
                await UOW.FileTypeRepository.Delete(FileType);
                await Logging.CreateAuditLog(new { }, FileType, nameof(FileTypeService));
                return FileType;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(FileTypeService));
            }
            return null;
        }

        public async Task<List<FileType>> BulkDelete(List<FileType> FileTypes)
        {
            if (!await FileTypeValidator.BulkDelete(FileTypes))
                return FileTypes;

            try
            {
                await UOW.FileTypeRepository.BulkDelete(FileTypes);
                await Logging.CreateAuditLog(new { }, FileTypes, nameof(FileTypeService));
                return FileTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(FileTypeService));
            }
            return null;

        }
        
        public async Task<List<FileType>> Import(List<FileType> FileTypes)
        {
            if (!await FileTypeValidator.Import(FileTypes))
                return FileTypes;
            try
            {
                await UOW.FileTypeRepository.BulkMerge(FileTypes);

                await Logging.CreateAuditLog(FileTypes, new { }, nameof(FileTypeService));
                return FileTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(FileTypeService));
            }
            return null;
        }     
        
        public async Task<FileTypeFilter> ToFilter(FileTypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<FileTypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                FileTypeFilter subFilter = new FileTypeFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        subFilter.Code = FilterBuilder.Merge(subFilter.Code, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        subFilter.Name = FilterBuilder.Merge(subFilter.Name, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        {
                        }
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        {
                        }
                    }
                }
            }
            return filter;
        }
    }
}
