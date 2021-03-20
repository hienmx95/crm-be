using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLATimeUnit
{
    public interface ISLATimeUnitValidator : IServiceScoped
    {
        Task<bool> Create(SLATimeUnit SLATimeUnit);
        Task<bool> Update(SLATimeUnit SLATimeUnit);
        Task<bool> Delete(SLATimeUnit SLATimeUnit);
        Task<bool> BulkDelete(List<SLATimeUnit> SLATimeUnits);
        Task<bool> Import(List<SLATimeUnit> SLATimeUnits);
    }

    public class SLATimeUnitValidator : ISLATimeUnitValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLATimeUnitValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLATimeUnit SLATimeUnit)
        {
            SLATimeUnitFilter SLATimeUnitFilter = new SLATimeUnitFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLATimeUnit.Id },
                Selects = SLATimeUnitSelect.Id
            };

            int count = await UOW.SLATimeUnitRepository.Count(SLATimeUnitFilter);
            if (count == 0)
                SLATimeUnit.AddError(nameof(SLATimeUnitValidator), nameof(SLATimeUnit.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLATimeUnit SLATimeUnit)
        {
            return SLATimeUnit.IsValidated;
        }

        public async Task<bool> Update(SLATimeUnit SLATimeUnit)
        {
            if (await ValidateId(SLATimeUnit))
            {
            }
            return SLATimeUnit.IsValidated;
        }

        public async Task<bool> Delete(SLATimeUnit SLATimeUnit)
        {
            if (await ValidateId(SLATimeUnit))
            {
            }
            return SLATimeUnit.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLATimeUnit> SLATimeUnits)
        {
            foreach (SLATimeUnit SLATimeUnit in SLATimeUnits)
            {
                await Delete(SLATimeUnit);
            }
            return SLATimeUnits.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLATimeUnit> SLATimeUnits)
        {
            return true;
        }
    }
}
