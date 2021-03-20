using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MPotentialResult
{
    public interface IPotentialResultValidator : IServiceScoped
    {
        Task<bool> Create(PotentialResult PotentialResult);
        Task<bool> Update(PotentialResult PotentialResult);
        Task<bool> Delete(PotentialResult PotentialResult);
        Task<bool> BulkDelete(List<PotentialResult> PotentialResults);
        Task<bool> Import(List<PotentialResult> PotentialResults);
    }

    public class PotentialResultValidator : IPotentialResultValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public PotentialResultValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(PotentialResult PotentialResult)
        {
            PotentialResultFilter PotentialResultFilter = new PotentialResultFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = PotentialResult.Id },
                Selects = PotentialResultSelect.Id
            };

            int count = await UOW.PotentialResultRepository.Count(PotentialResultFilter);
            if (count == 0)
                PotentialResult.AddError(nameof(PotentialResultValidator), nameof(PotentialResult.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(PotentialResult PotentialResult)
        {
            return PotentialResult.IsValidated;
        }

        public async Task<bool> Update(PotentialResult PotentialResult)
        {
            if (await ValidateId(PotentialResult))
            {
            }
            return PotentialResult.IsValidated;
        }

        public async Task<bool> Delete(PotentialResult PotentialResult)
        {
            if (await ValidateId(PotentialResult))
            {
            }
            return PotentialResult.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<PotentialResult> PotentialResults)
        {
            foreach (PotentialResult PotentialResult in PotentialResults)
            {
                await Delete(PotentialResult);
            }
            return PotentialResults.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<PotentialResult> PotentialResults)
        {
            return true;
        }
    }
}
