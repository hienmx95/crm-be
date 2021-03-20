using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MPhoneType
{
    public interface IPhoneTypeValidator : IServiceScoped
    {
        Task<bool> Create(PhoneType PhoneType);
        Task<bool> Update(PhoneType PhoneType);
        Task<bool> Delete(PhoneType PhoneType);
        Task<bool> BulkDelete(List<PhoneType> PhoneTypes);
        Task<bool> Import(List<PhoneType> PhoneTypes);
    }

    public class PhoneTypeValidator : IPhoneTypeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public PhoneTypeValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(PhoneType PhoneType)
        {
            PhoneTypeFilter PhoneTypeFilter = new PhoneTypeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = PhoneType.Id },
                Selects = PhoneTypeSelect.Id
            };

            int count = await UOW.PhoneTypeRepository.Count(PhoneTypeFilter);
            if (count == 0)
                PhoneType.AddError(nameof(PhoneTypeValidator), nameof(PhoneType.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(PhoneType PhoneType)
        {
            return PhoneType.IsValidated;
        }

        public async Task<bool> Update(PhoneType PhoneType)
        {
            if (await ValidateId(PhoneType))
            {
            }
            return PhoneType.IsValidated;
        }

        public async Task<bool> Delete(PhoneType PhoneType)
        {
            if (await ValidateId(PhoneType))
            {
            }
            return PhoneType.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<PhoneType> PhoneTypes)
        {
            foreach (PhoneType PhoneType in PhoneTypes)
            {
                await Delete(PhoneType);
            }
            return PhoneTypes.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<PhoneType> PhoneTypes)
        {
            return true;
        }
    }
}
