using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MFileType
{
    public interface IFileTypeValidator : IServiceScoped
    {
        Task<bool> Create(FileType FileType);
        Task<bool> Update(FileType FileType);
        Task<bool> Delete(FileType FileType);
        Task<bool> BulkDelete(List<FileType> FileTypes);
        Task<bool> Import(List<FileType> FileTypes);
    }

    public class FileTypeValidator : IFileTypeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public FileTypeValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(FileType FileType)
        {
            FileTypeFilter FileTypeFilter = new FileTypeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = FileType.Id },
                Selects = FileTypeSelect.Id
            };

            int count = await UOW.FileTypeRepository.Count(FileTypeFilter);
            if (count == 0)
                FileType.AddError(nameof(FileTypeValidator), nameof(FileType.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(FileType FileType)
        {
            return FileType.IsValidated;
        }

        public async Task<bool> Update(FileType FileType)
        {
            if (await ValidateId(FileType))
            {
            }
            return FileType.IsValidated;
        }

        public async Task<bool> Delete(FileType FileType)
        {
            if (await ValidateId(FileType))
            {
            }
            return FileType.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<FileType> FileTypes)
        {
            foreach (FileType FileType in FileTypes)
            {
                await Delete(FileType);
            }
            return FileTypes.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<FileType> FileTypes)
        {
            return true;
        }
    }
}
